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
		private IDictionary<string, string> _Addresses;
		private static readonly string CACHE_CATEGORIES = "categories";
    private ICacheProvider _cache;

		public MainController()
		{
			_cache = new WebCacheProvider();
			var client = (ClientSection)WebConfigurationManager.GetSection("system.serviceModel/client");
			_Addresses = client.Endpoints.Cast<ChannelEndpointElement>().ToDictionary(c => c.Address.AbsoluteUri, c => c.Name);
		}

		public ActionResult Test()
		{

			DateTime startTime = DateTime.MinValue;
			DateTime.TryParse(Request.Cookies["startTime"] == null ? "" : Request.Cookies["startTime"].Value, out startTime);
			DateTime endTime = DateTime.MinValue;
			DateTime.TryParse(Request.Cookies["endTime"] == null ? "" : Request.Cookies["endTime"].Value, out endTime);
			bool instanceExact = false;
			if (Request.Cookies["instanceExact"] != null) bool.TryParse(Request.Cookies["instanceExact"].Value, out instanceExact);
			bool userExact = false;
			if (Request.Cookies["userExact"] != null) bool.TryParse(Request.Cookies["userExact"].Value, out userExact);
			bool machineExact = false;
			if (Request.Cookies["machineExact"] != null) bool.TryParse(Request.Cookies["machineExact"].Value, out machineExact);
			Severity severity = Severity.All;
			if (!CookieIsBlank(Request.Cookies["severity"])) severity = (Severity)Enum.Parse(typeof(Severity), Request.Cookies["severity"].Value);


			return ViewList(
				CookieIsBlank(Request.Cookies["category"]) ? null : Request.Cookies["category"].Value,
				CookieIsBlank(Request.Cookies["application"]) ? null : Request.Cookies["application"].Value,
				CookieIsBlank(Request.Cookies["instance"]) ? null : Request.Cookies["instance"].Value,
				CookieIsBlank(Request.Cookies["user"]) ? null : Request.Cookies["user"].Value,
				CookieIsBlank(Request.Cookies["machine"]) ? null : Request.Cookies["machine"].Value,
				CookieIsBlank(Request.Cookies["message"]) ? null : Request.Cookies["message"].Value,
				severity,
				startTime == DateTime.MinValue ? null : (DateTime?)startTime,
				endTime == DateTime.MinValue ? null : (DateTime?)endTime,
				instanceExact,
				userExact,
				machineExact,
				1,
				CookieIsBlank(Request.Cookies["LogServer"]) ? null : Request.Cookies["LogServer"].Value
			 );

		}

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
			var filter = new RequestFilter()
			{
				Category = category == "" ? null : category,
				Application = application == "" ? null : application,
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
				Severity = severity == Severity.All ? Severity.Debug.ToString() : severity.ToString(),
			};

			ReporterContractClient reporterContractClient = null;
			if (!string.IsNullOrEmpty(logServer))
			{
				reporterContractClient = new ReporterContractClient(_Addresses[logServer]);
			}

			var totalRecordCount = reporterContractClient == null ? 0 : reporterContractClient.GetCount(filter);
			var recordsToSkip = totalRecordCount < recordsPerPage ? 0 : totalRecordCount - ((pageNumber + 1) * recordsPerPage);
			var maxPage = totalRecordCount / recordsPerPage;

			//get the last partial page
			if (recordsToSkip < 0)
			{
				recordsToSkip = 0;
			}

			IEnumerable<LogTransportObject> logsList = new List<LogTransportObject>();
			IEnumerable<string> categories = new List<string>();
			IEnumerable<string> applications = new List<string>();
			var error = "";

			try
			{
				logsList = reporterContractClient == null ? new List<LogTransportObject>() : reporterContractClient.GetRecords(filter, recordsToSkip, recordsPerPage).Reverse();
				categories = GetCategories(reporterContractClient);
				applications = GetApplications(reporterContractClient, category);
			}
			catch (Exception e)
			{
				error = "An error occured while attempting to get the log information : " + e.Message;
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

			Response.Cookies.Set(new System.Web.HttpCookie("Category", filter.Category) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("CategoryExactMatch", filter.CategoryExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("Application", filter.Application) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("ApplicationExactMatch", filter.ApplicationExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("Instance", filter.Instance) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("InstanceExactMatch", filter.InstanceExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("User", filter.User) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("UserExactMatch", filter.UserExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("Machine", filter.Machine) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("MachineExactMatch", filter.MachineExactMatch.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("StartTime", filter.StartTime.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("EndTime", filter.EndTime.ToString()) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("Message", filter.Message) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("Severity", filter.Severity) { Expires = DateTime.Now.AddDays(365) });
			Response.Cookies.Set(new System.Web.HttpCookie("LogServer", logServer) { Expires = DateTime.Now.AddDays(365) });

			return View("ListView", viewdata);
		}

		public ActionResult Categories()
		{
			var logServer = CookieIsBlank(Request.Cookies["LogServer"]) ? null : Request.Cookies["LogServer"].Value;
			ReporterContractClient reporterContractClient = null;
			if (!string.IsNullOrEmpty(logServer))
			{
				reporterContractClient = new ReporterContractClient(_Addresses[logServer]);
			}
			return Json(GetCategories(reporterContractClient));
		}

		public ActionResult Applications(string category)
		{
			var logServer = CookieIsBlank(Request.Cookies["LogServer"]) ? null : Request.Cookies["LogServer"].Value;
			ReporterContractClient reporterContractClient = null;
			if (!string.IsNullOrEmpty(logServer))
			{
				reporterContractClient = new ReporterContractClient(_Addresses[logServer]);
			}
			return Json(GetApplications(reporterContractClient, category));
		}


		private IEnumerable<string> GetCategories(ReporterContractClient server)
		{
			if (server == null)
			{
				throw new InvalidOperationException("server is null");
			}
			if (_cache != null)
			{
				var list = _cache[CACHE_CATEGORIES] as Dictionary<string, IEnumerable<string>>;
				if (list == null)
				{
					list = server.GetApplicationList();
					_cache.Add(CACHE_CATEGORIES, list, TimeSpan.FromHours(1));
				}

				return list.Keys;
			}
			return server.GetApplicationList().Keys;

		}

		private IEnumerable<string> GetApplications(ReporterContractClient server, string category)
		{
			if (server == null)
			{
				throw new InvalidOperationException("server is null");
			}
			if (_cache != null)
			{
				var list = _cache[CACHE_CATEGORIES] as Dictionary<string, IEnumerable<string>>;
				if (list == null)
				{
					list = server.GetApplicationList();
					_cache.Add(CACHE_CATEGORIES, list, TimeSpan.FromHours(1));
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
