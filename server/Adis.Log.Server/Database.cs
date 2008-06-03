using System;
using Adis.Log.Contract;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;

namespace Adis.Log.Server
{
	partial class LogEvent
	{
		//These are the max lengths of the fields in the database
		private static readonly int MaxApplicationLength = 256;
		private static readonly int MaxCategoryLength = 64;
		private static readonly int MaxInstanceLength = 256;
		private static readonly int MaxMachineLength = 256;
		private static readonly int MaxMessageLength = 2048;
		private static readonly int MaxSeverityLength = 5;
		private static readonly int MaxUserLength = 256;

		partial void OnCreated()
		{
				Application = "";
				Category = "";
				ExtraInfo = "";
				Instance = "";
				Machine = "";
				Message = "";
				Severity = "";
				EventTime = DateTime.MinValue;
				TimeLogged = DateTime.MinValue;
				User = "";
		}

		/// <summary>
		/// This method will convert the current Logevent Object to a LogTransportObject
		/// </summary>
		/// <returns>A LogTransportObject</returns>
		public LogTransportObject ToLogTransportObject()
		{
			LogTransportObject logObj = new LogTransportObject()
			{
				Application = this.Application,
				Category = this.Category,
				ExtraInfo = this.ExtraInfo,
				Id = this.EventID,
				Instance = this.Instance,
				Machine = this.Machine,
				Message = this.Message,
				Severity = this.Severity,
				Time = this.EventTime,
				User = this.User
			};
			return logObj;
		}

		/// <summary>
		/// Conversion operator to convert a LogTransportObject to a LogEvent
		/// </summary>
		/// <param name="transportObj">The object to convert</param>
		/// <returns></returns>
		public static explicit operator LogEvent(LogTransportObject transportObj)
		{
			LogEvent logEvent = new LogEvent()
			{
				Application = transportObj.Application,
				Category = transportObj.Category,
				EventTime = transportObj.Time,
				ExtraInfo = transportObj.ExtraInfo,
				Instance = transportObj.Instance,
				Machine = transportObj.Machine,
				Message = transportObj.Message,
				Severity = transportObj.Severity,
				TimeLogged = DateTime.Now,
				User = transportObj.User
			};

			//restrict the lengths of string fields to their maximum DB length
			if (!String.IsNullOrEmpty(logEvent.Application) && logEvent.Application.Length > MaxApplicationLength)
			{
				logEvent.Application = logEvent.Application.Substring(0, MaxApplicationLength);
			}
			if (!String.IsNullOrEmpty(logEvent.Category) && logEvent.Category.Length > MaxCategoryLength)
			{
				logEvent.Category = logEvent.Category.Substring(0, MaxCategoryLength);
			}
			if (!String.IsNullOrEmpty(logEvent.Instance) && logEvent.Instance.Length > MaxInstanceLength)
			{
				logEvent.Instance = logEvent.Instance.Substring(0, MaxInstanceLength);
			}
			if (!String.IsNullOrEmpty(logEvent.Machine) && logEvent.Machine.Length > MaxMachineLength)
			{
				logEvent.Machine = logEvent.Machine.Substring(0, MaxMachineLength);
			}
			if (!String.IsNullOrEmpty(logEvent.Message) && logEvent.Message.Length > MaxMessageLength)
			{
				logEvent.Message = logEvent.Message.Substring(0, MaxMessageLength);
			}
			if (!String.IsNullOrEmpty(logEvent.Severity) && logEvent.Severity.Length > MaxSeverityLength)
			{
				logEvent.Severity = logEvent.Severity.Substring(0, MaxSeverityLength);
			}
			if (!String.IsNullOrEmpty(logEvent.User) && logEvent.User.Length > MaxUserLength)
			{
				logEvent.User = logEvent.User.Substring(0, MaxUserLength);
			}
			return logEvent;
		}
	}
}