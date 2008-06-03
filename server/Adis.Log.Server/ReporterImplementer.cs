﻿using System.Linq;
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

			logEvents = logEvents.WithId(filter.Id)
				.WithApplication(filter.Application, filter.ApplicationExactMatch)
				.WithCategory(filter.Category, filter.CategoryExactMatch)
				.WithInstance(filter.Instance, filter.InstanceExactMatch)
				.WithMachine(filter.Machine, filter.MachineExactMatch)
				.WithUser(filter.User, filter.UserExactMatch)
				.WithMaxSeverity(filter.Severity)
				.WithMessage(filter.Message, filter.MessageExactMatch)
				;

			if (useTimeLoggedNotEventTime)
			{
				logEvents = logEvents.WithTimeLoggedInRange(filter.StartTime, filter.EndTime);
			}
			else
			{
				logEvents = logEvents.WithEventTimeInRange(filter.StartTime, filter.EndTime);
			}

			logEvents = logEvents.Skip(skipFirst);
			if (maxRecords > 0)
			{
				logEvents = logEvents.Take(maxRecords);
			}

			List<LogTransportObject> returnVal = new List<LogTransportObject>();
			foreach (LogEvent log in logEvents)
			{
				returnVal.Add(log.ToLogTransportObject());
			}

			internalLog.DebugFormat("supplying {0} records to reporter client (remote address {1})", returnVal.Count,
				remoteAddress);
			return returnVal;
		}

	}
}
