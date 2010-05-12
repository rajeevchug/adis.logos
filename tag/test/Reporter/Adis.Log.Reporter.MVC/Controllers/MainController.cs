using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Adis.Log.Contract;
using Adis.Log.Reporter.MVC.ViewData;
using System.Web;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Web.Configuration;
using Adis.Log.Reporter.MVC.Providers;


namespace Adis.Log.Reporter.MVC.Controllers
{

	public class MainController : Controller
	{
		private struct Cookies
		{
			public static string Category = "LogReporterCategory";
			public static string Application = "LogReporterApplication";
			public static string LogServer = "LogReporterLogServer";
			public static string Instance = "LogReporterInstance";
			public static string User = "LogReporterUser";
			public static string Machine = "LogReporterMachine";
			public static string Message = "LogReporterMessage";
			public static string Severity = "LogReporterSeverity";
			public static string MachineExact = "machineExact";
			public static string UserExact = "userExact";
			public static string InstanceExact = "instanceExact";
			public static string EndTime = "LogReporterEndTime";
			public static string StartTime = "LogReporterStartTime";
			public static string MachineExactMatch = "LogReporterMachineExactMatch";
			public static string UserExactMatch = "LogReporterUserExactMatch";
			public static string InstanceExactMatch = "LogReporterInstanceExactMatch";
		}

		private IDictionary<string, string> _Addresses;
		private static readonly string CACHE_CATEGORIES = "categories";
    private ICacheProvider _cache;

		public MainController()
		{
			_cache = new WebCacheProvider();
			var client = (ClientSection)WebConfigurationManager.GetSection("system.serviceModel/client");
			_Addresses = client.Endpoints.Cast<ChannelEndpointElement>().ToDictionary(c => c.Name, c => c.Name);
		}

		public ActionResult Default()
		{
			return ViewList(null);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult ViewList(int? pageNumber)
		{
			DateTime startTime = DateTime.MinValue;
			DateTime.TryParse(Request.Cookies[Cookies.StartTime] == null ? "" : Request.Cookies[Cookies.StartTime].Value, out startTime);
			DateTime endTime = DateTime.MinValue;
			DateTime.TryParse(Request.Cookies[Cookies.EndTime] == null ? "" : Request.Cookies[Cookies.EndTime].Value, out endTime);
			bool instanceExact = false;
			if (Request.Cookies[Cookies.InstanceExact] != null) bool.TryParse(Request.Cookies[Cookies.InstanceExact].Value, out instanceExact);
			bool userExact = false;
			if (Request.Cookies[Cookies.UserExact] != null) bool.TryParse(Request.Cookies[Cookies.UserExact].Value, out userExact);
			bool machineExact = false;
			if (Request.Cookies[Cookies.MachineExact] != null) bool.TryParse(Request.Cookies[Cookies.MachineExact].Value, out machineExact);
			Severity severity = Severity.All;
			if (!CookieIsBlank(Request.Cookies[Cookies.Severity])) severity = (Severity)Enum.Parse(typeof(Severity), Request.Cookies[Cookies.Severity].Value);
			
      return ViewList(
				CookieIsBlank(Request.Cookies[Cookies.Category]) ? null : Request.Cookies[Cookies.Category].Value,
				CookieIsBlank(Request.Cookies[Cookies.Application]) ? null : Request.Cookies[Cookies.Application].Value,
				CookieIsBlank(Request.Cookies[Cookies.Instance]) ? null : Request.Cookies[Cookies.Instance].Value,
				CookieIsBlank(Request.Cookies[Cookies.User]) ? null : Request.Cookies[Cookies.User].Value,
				CookieIsBlank(Request.Cookies[Cookies.Machine]) ? null : Request.Cookies[Cookies.Machine].Value,
				CookieIsBlank(Request.Cookies[Cookies.Message]) ? null : Request.Cookies[Cookies.Message].Value,
				severity,
				startTime == DateTime.MinValue ? null : (DateTime?)startTime,
				endTime == DateTime.MinValue ? null : (DateTime?)endTime,
				instanceExact,
				userExact,
				machineExact,
				pageNumber == null ? 1 : pageNumber.Value,
				CookieIsBlank(Request.Cookies[Cookies.LogServer]) ? null : Request.Cookies[Cookies.LogServer].Value
			 );
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult ViewList(
			string category,
			string application,
			string instance,
			string user,
			string machine,
			string message,
			Severity severity,
			DateTime? startTime,
			DateTime? endTime,
			bool instanceExact,
			bool userExact,
			bool machineExact,
			int pageNumber,
			string logServer
			)
		{
			pageNumber--; //0 base page
			var recordsPerPage = 25;

			if (endTime != null)
			{
				endTime = endTime.Value.Add(new TimeSpan(0,
					23 - endTime.Value.Hour, 59 - endTime.Value.Minute, 59 - endTime.Value.Second, 999 - endTime.Value.Millisecond)); 
			}

			var filter = new RequestFilter()
			{
				Category = category == "" ? null : category,
				CategoryExactMatch = true,
				Application = application == "" ? null : application,
				ApplicationExactMatch = true,
				Instance = instance == "" ? null : instance,
				InstanceExactMatch = instanceExact,
				User = user == "" ? null : user,
				UserExactMatch = userExact,
				Machine = machine == "" ? null : machine,
				MachineExactMatch = machineExact,
				StartTime = startTime,
				EndTime = endTime,
				Message = message == "" ? null : message,
				MessageExactMatch = false,
				Severity = severity == Severity.All ? (string)null : severity.ToString(),
			};

			var error = "";


			IEnumerable<LogTransportObject> logsList = new List<LogTransportObject>();
			IEnumerable<string> categories = new List<string>();
			IEnumerable<string> applications = new List<string>();
			var maxPage = 0;
			var totalRecordCount = 0;
			if (!string.IsNullOrEmpty(logServer))
			{
				ReporterContractClient reporterContractClient = null;

				reporterContractClient = new ReporterContractClient(_Addresses[logServer]);

				try
				{
					totalRecordCount = reporterContractClient == null ? 0 : reporterContractClient.GetCount(filter);
				}
				catch (System.ServiceModel.EndpointNotFoundException)
				{
					error = string.Format("The log server {0} cannot be found. It may be down.", _Addresses[logServer]);
				}
				var recordsToSkip = totalRecordCount < recordsPerPage ? 0 : totalRecordCount - ((pageNumber + 1) * recordsPerPage);
				maxPage = totalRecordCount / recordsPerPage;

				//get the last partial page
				if (recordsToSkip < 0)
				{
					recordsToSkip = 0;
				}

				if (reporterContractClient.State == System.ServiceModel.CommunicationState.Opened)
				{
					try
					{
						logsList = reporterContractClient == null ? new List<LogTransportObject>() : reporterContractClient.GetRecords(filter, recordsToSkip, recordsPerPage).Reverse();
						categories = GetCategories(_Addresses[logServer], reporterContractClient);
						applications = GetApplications(_Addresses[logServer], reporterContractClient, category);
					}
					catch (Exception e)
					{
						error = "An error occured while attempting to get the log information : " + e.Message;
					}
				}
			}
			var viewdata = new ListViewData()
			{
				Logs = logsList,
				RequestFilter = filter,
				TotalRecords = totalRecordCount,
				Page = pageNumber + 1,
				SelectedCategory = category,
				Categories = categories,
				Applications = applications,
				MaxPage = maxPage,
				LogServers = this._Addresses.Keys,
				ErrorMessage = error,
				LogServer = logServer
			};

			viewdata.MaxPage = (int)System.Math.Ceiling((double)viewdata.TotalRecords / (double)recordsPerPage);

			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.Category, filter.Category) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.Application, filter.Application) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.Instance, filter.Instance) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.InstanceExactMatch, filter.InstanceExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.User, filter.User) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.UserExactMatch, filter.UserExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.Machine, filter.Machine) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.MachineExactMatch, filter.MachineExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.StartTime, filter.StartTime.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.EndTime, filter.EndTime.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.Message, filter.Message) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.Severity, filter.Severity) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie(Cookies.LogServer, logServer) { Expires = DateTime.Now.AddDays(365) });
			return View("ListView", viewdata);
		}

		public ActionResult Categories(string server)
		{
			var logServer = server == null ? CookieIsBlank(Request.Cookies["LogServer"]) ? null : Request.Cookies["LogServer"].Value : server;
			ReporterContractClient reporterContractClient = null;
			if (!string.IsNullOrEmpty(logServer))
			{
				reporterContractClient = new ReporterContractClient(_Addresses[logServer]);
			}
			return Json(GetCategories(logServer, reporterContractClient));
		}

		public ActionResult Applications(string server, string category)
		{
			var logServer = server == null ? CookieIsBlank(Request.Cookies["LogServer"]) ? null : Request.Cookies["LogServer"].Value : server;
			ReporterContractClient reporterContractClient = null;
			if (!string.IsNullOrEmpty(logServer))
			{
				reporterContractClient = new ReporterContractClient(_Addresses[logServer]);
			}
			return Json(GetApplications(logServer, reporterContractClient, category));
		}


		private IEnumerable<string> GetCategories(string serverName, ReporterContractClient server)
		{
			var cacheName = CACHE_CATEGORIES+serverName;
			if (server == null)
			{
				throw new InvalidOperationException("server is null");
			}
			if (_cache != null)
			{
				var list = _cache[cacheName] as Dictionary<string, IEnumerable<string>>;
				if (list == null)
				{
					list = server.GetApplicationList().OrderBy(c => c.Key).ToDictionary(c => c.Key, c => c.Value.OrderBy(d => d).AsEnumerable());
					_cache.Add(cacheName, list, TimeSpan.FromHours(1));
				}

				return list.Keys;
			}
			return server.GetApplicationList().Keys;

		}

		private IEnumerable<string> GetApplications(string serverName, ReporterContractClient server, string category)
		{
			var cacheName = CACHE_CATEGORIES + serverName;
			if (server == null)
			{
				throw new InvalidOperationException("server is null");
			}
			if (_cache != null)
			{
				var list = _cache[cacheName] as Dictionary<string, IEnumerable<string>>;
				if (list == null)
				{
					list = server.GetApplicationList().OrderBy(c => c.Key).ToDictionary(c => c.Key, c => c.Value.OrderBy(d => d).AsEnumerable());
					_cache.Add(cacheName, list, TimeSpan.FromHours(1));
				}

				return list[category];
			}
			return server.GetApplicationList()[category];
		}

		private static bool CookieIsBlank(HttpCookie c)
		{
			return c == null || string.IsNullOrEmpty(c.Value);
		}
	}

}
