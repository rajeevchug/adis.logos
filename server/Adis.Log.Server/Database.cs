using System;
using Adis.Log.Contract;

namespace Adis.Log.Server
{

	partial class LogEvent
	{
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
				Severity = this.Message,
				Time = this.EventTime,
				User = this.User
			};
			return logObj;
		}

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

			int MaxApplicationLength = 256;
			int MaxCategoryLength = 64;
			int MaxInstanceLength = 256;
			int MaxMachineLength = 256;
			int MaxMessageLength = 2048;
			int MaxSeverityLength = 5;
			int MaxUserLength = 256;
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