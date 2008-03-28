using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adis.Log.Contract;
using log4net;

namespace Adis.Log.Server
{
	internal class LoggerImplementer
	{
		private Adis.Log.Server.LoggerDataContext dbContext;

		public LoggerImplementer()
		{
			ILog internalLog = LogManager.GetLogger(this.GetType());
			try
			{
				internalLog.DebugFormat("Attempting to connect to database. ConnectionString is {0}", 
					global::Adis.Log.Properties.Settings.Default.LoggerConnectionString);
				dbContext = new Adis.Log.Server.LoggerDataContext();
			}
			catch (Exception e)
			{
				internalLog.Error("Failed to create LoggerDatabaseContext", e);
				throw;
			}
		}

		~LoggerImplementer()
		{
			dbContext.Dispose();
		}

		public bool InsertNewLog(LogTransportObject logObject)
		{
			ILog internalLog = LogManager.GetLogger(this.GetType());
			LogEvent logEvent = (LogEvent)logObject;
			try
			{
				lock (dbContext)
				{
					dbContext.LogEvents.InsertOnSubmit(logEvent);

					dbContext.SubmitChanges();
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.ToString());
				internalLog.Error("Failed to insert new log into the database", e);
			}
			return true;
		}

		private static LogEvent GenerateLogEvent(LogTransportObject logObject)
		{
			ILog internalLog = LogManager.GetLogger(typeof(LoggerImplementer));
			LogEvent logEvent = new LogEvent()
			{
				Application	= logObject.Application,
				Category		= logObject.Category,
				EventTime		= logObject.Time,
				ExtraInfo		= logObject.ExtraInfo,
				Instance		= logObject.Instance,
				Machine			= logObject.Machine,
				Message			= logObject.Message,
				Severity		= logObject.Severity,
				TimeLogged	= DateTime.Now,
				User				= logObject.User
			};

			int MaxApplicationLength	= 256;
			int MaxCategoryLength			= 64;
			int MaxInstanceLength			= 256;
			int MaxMachineLength			= 256;
			int MaxMessageLength			= 2048;
			int MaxSeverityLength			= 5;
			int MaxUserLength					= 256;
			if (logEvent.Application.Length > MaxApplicationLength)
			{
				logEvent.Application = logEvent.Application.Substring(0, MaxApplicationLength);
			}
			if (logEvent.Category.Length > MaxCategoryLength)
			{
				logEvent.Category = logEvent.Category.Substring(0, MaxCategoryLength);
			}
			if (logEvent.Instance.Length > MaxInstanceLength)
			{
				logEvent.Instance = logEvent.Instance.Substring(0, MaxInstanceLength);
			}
			if (logEvent.Machine.Length > MaxMachineLength)
			{
				logEvent.Machine = logEvent.Machine.Substring(0, MaxMachineLength);
			}
			if (logEvent.Message.Length > MaxMessageLength)
			{
				logEvent.Message = logEvent.Message.Substring(0, MaxMessageLength);
			}
			if (logEvent.Severity.Length > MaxSeverityLength)
			{
				logEvent.Severity = logEvent.Severity.Substring(0, MaxSeverityLength);
			}
			if (logEvent.User.Length > MaxUserLength)
			{
				logEvent.User = logEvent.User.Substring(0, MaxUserLength);
			}
			return logEvent;
		}
	}
}
