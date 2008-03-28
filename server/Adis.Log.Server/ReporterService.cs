using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using Adis.Log.Contract;
using log4net;

namespace Adis.Log.Server
{
	/// <summary>
	/// 
	/// </summary>
	class ReporterService : IReporterContract
	{
		#region IReporterContract Members

		/// <summary>
		/// Returns Log events from the database according to the filtering required
		/// </summary>
		/// <param name="filter">allows you to filter the results returned to just those you are interested in</param>
		/// <param name="skipFirst">skip the first X records</param>
		/// <param name="maxRecords">Limits the number of recors returned to X</param>
		/// <returns>a List&lt;LogTransportObject&gt;. Each LogTransportObject contains the entire record from the database </returns>
		public List<LogTransportObject> GetRecords(RequestFilter filter, int skipFirst, int maxRecords)
		{
			ILog internalLog = LogManager.GetLogger(this.GetType());
			LoggerDataContext dbContext = new LoggerDataContext();


			//will match *all* the records in eventLog
			var logEvents = from le in dbContext.LogEvents
											select le;

			//These methods will add where clauses to the linq query
			logEvents = AddApplicationFilter(filter, logEvents);
			logEvents = AddCategoryFilter(filter, logEvents);
			logEvents = AddInstanceFilter(filter, logEvents);
			logEvents = AddMachineFilter(filter, logEvents);
			logEvents = AddUserFilter(filter, logEvents);
			logEvents = AddTimeFilter(filter, logEvents);

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
				System.ServiceModel.OperationContext.Current.Channel.RemoteAddress);
			return returnVal;
		}

		///Add where clause to filter out records whose eventTime isn't between the filter object's
		///start and end times
		private IQueryable<LogEvent> AddTimeFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
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

		///Add where clause to filter out records based on the application field
		private IQueryable<LogEvent> AddApplicationFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Application != null)
			{
				if (filter.ApplicationExactMatch)
				{
					logEvents = logEvents.Where(le => le.Application == filter.Application);
				}
				else
				{
					logEvents = logEvents.Where(le => SqlMethods.Like(le.Application, filter.Application + "%"));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the category field
		private IQueryable<LogEvent> AddCategoryFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Category != null)
			{
				if (filter.CategoryExactMatch)
				{
					logEvents = logEvents.Where(le => le.Category == filter.Category);
				}
				else
				{
					logEvents = logEvents.Where(le => SqlMethods.Like(le.Category, filter.Category + "%"));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the instance field
		private IQueryable<LogEvent> AddInstanceFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Instance != null)
			{
				if (filter.InstanceExactMatch)
				{
					logEvents = logEvents.Where(le => le.Instance == filter.Instance);
				}
				else
				{
					logEvents = logEvents.Where(le => SqlMethods.Like(le.Instance, filter.Instance + "%"));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the machine field
		private IQueryable<LogEvent> AddMachineFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.Machine != null)
			{
				if (filter.MachineExactMatch)
				{
					logEvents = logEvents.Where(le => le.Machine == filter.Machine);
				}
				else
				{
					logEvents = logEvents.Where(le => SqlMethods.Like(le.Machine, filter.Machine + "%"));
				}
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the user field
		private IQueryable<LogEvent> AddUserFilter(RequestFilter filter, IQueryable<LogEvent> logEvents)
		{
			if (filter.User != null)
			{
				if (filter.UserExactMatch)
				{
					logEvents = logEvents.Where(le => le.User == filter.User);
				}
				else
				{
					logEvents = logEvents.Where(le => SqlMethods.Like(le.User, filter.User + "%"));
				}
			}
			return logEvents;
		}

		#endregion
	}
}
