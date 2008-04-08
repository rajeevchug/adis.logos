using System.Linq;
using Adis.Log.Contract;
using System.Collections.Generic;
using log4net;
using System.Data.Linq.SqlClient;
using System.ServiceModel;
using System;
namespace Adis.Log.Server
{
	public class ReporterImplementer
	{
		/// <summary>
		/// </summary>
		/// <returns>a List&lt;LogTransportObject&gt;. Each LogTransportObject contains the entire record from the database </returns>
		
		/// <summary>
		/// Returns Log events from the database according to the filtering required
		/// </summary>
		/// <param name="filter">allows you to filter the results returned to just those you are interested in</param>
		/// <param name="skipFirst">skip the first X records</param>
		/// <param name="maxRecords">Limits the number of recorss returned</param>
		/// <param name="repository"></param>
		/// <param name="remoteAddress"></param>
		/// <param name="useTimeLoggedNotEventTime">if true the time filtering is done on the LogEvent.TimeLogged property. 
		/// If false it uses the LogEvent.EventTime property. </param>
		/// <returns></returns>
		public static List<LogTransportObject> GetRecords(RequestFilter filter, int skipFirst, int maxRecords, 
			IRepository repository, Uri remoteAddress, bool useTimeLoggedNotEventTime)
		{
			ILog internalLog = LogManager.GetLogger(typeof(ReporterImplementer));
			IQueryable<LogEvent> logEvents = repository.GetAllLogLogEvents();
			logEvents = AddApplicationFilter(filter, logEvents);
			logEvents = AddCategoryFilter(filter, logEvents);
			logEvents = AddInstanceFilter(filter, logEvents);
			logEvents = AddMachineFilter(filter, logEvents);
			logEvents = AddUserFilter(filter, logEvents);
			logEvents = AddSeverityFilter(filter, logEvents);

			if (useTimeLoggedNotEventTime)
			{
				logEvents = AddTimeLoggedFilter(filter, logEvents);
			}
			else
			{
				logEvents = AddEventTimeFilter(filter, logEvents);
			}

			logEvents = logEvents.Skip(skipFirst);
			if (maxRecords > 0)
			{
				logEvents = logEvents.Take(maxRecords);
			}

			List<LogTransportObject> returnVal = new List<LogTransportObject>(logEvents.Count());
			foreach (LogEvent log in logEvents)
			{
				returnVal.Add(log.ToLogTransportObject());
			}

			internalLog.DebugFormat("supplying {0} records to reporter client (remote address {1})", returnVal.Count,
				remoteAddress);
			return returnVal;
		}

		#region Private helper methods

		private static IQueryable<LogEvent> AddSeverityFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Severity != null)
			{
				logEvents = logEvents.Where(le => le.Severity.ToLower() == filter.Severity.ToLower());
			}
			return logEvents;
		}

		///Add where clause to filter out records whose eventTime isn't between the filter object's
		///start and end times
		private static IQueryable<LogEvent> AddEventTimeFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.StartTime != null)
			{
				logEvents = logEvents.Where(le => le.EventTime >= filter.StartTime);
			}
			if (filter.EndTime != null)
			{
				logEvents = logEvents.Where(le => le.EventTime <= filter.EndTime);
			}
			return logEvents;
		}

		///Add where clause to filter out records whose eventTime isn't between the filter object's
		///start and end times
		private static IQueryable<LogEvent> AddTimeLoggedFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.StartTime != null)
			{
				logEvents = logEvents.Where(le => le.TimeLogged >= filter.StartTime);
			}
			if (filter.EndTime != null)
			{
				logEvents = logEvents.Where(le => le.TimeLogged <= filter.EndTime);
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the application field
		private static IQueryable<LogEvent> AddApplicationFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Application != null)
			{
				if (filter.ApplicationExactMatch)
				{
					logEvents = logEvents.Where(le => le.Application == filter.Application);
				}
				else
				{
					logEvents = logEvents.Where(le => le.Application.StartsWith(filter.Application));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the category field
		private static IQueryable<LogEvent> AddCategoryFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Category != null)
			{
				if (filter.CategoryExactMatch)
				{
					logEvents = logEvents.Where(le => le.Category == filter.Category);
				}
				else
				{
					logEvents = logEvents.Where(le => le.Category.StartsWith(filter.Category));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the instance field
		private static IQueryable<LogEvent> AddInstanceFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Instance != null)
			{
				if (filter.InstanceExactMatch)
				{
					logEvents = logEvents.Where(le => le.Instance == filter.Instance);
				}
				else
				{
					logEvents = logEvents.Where(le => le.Instance.StartsWith(filter.Instance));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the machine field
		private static IQueryable<LogEvent> AddMachineFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Machine != null)
			{
				if (filter.MachineExactMatch)
				{
					logEvents = logEvents.Where(le => le.Machine == filter.Machine);
				}
				else
				{
					logEvents = logEvents.Where(le => le.Machine.StartsWith(filter.Machine));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the user field
		private static IQueryable<LogEvent> AddUserFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.User != null)
			{
				if (filter.UserExactMatch)
				{
					logEvents = logEvents.Where(le => le.User == filter.User);
				}
				else
				{
					logEvents = logEvents.Where(le => le.User.StartsWith(filter.User));
				}
			}
			return logEvents;
		}
		#endregion
	}
}
