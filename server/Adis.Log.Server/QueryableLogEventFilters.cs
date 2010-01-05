using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Adis.Log.Contract;

namespace Adis.Log.Server
{
	public static class QueryableLogEventFilters
	{
		public static IQueryable<LogEvent> WithMaxSeverity(this IQueryable<LogEvent> logEvents, String severity)
		{
			if (severity != null)
			{
				logEvents = logEvents.Where(le => RequestFilter.GetValidSeverities(severity).Contains(le.Severity));
			}
			return logEvents;
		}

		public static IQueryable<LogEvent> WithSeverity(this IQueryable<LogEvent> logEvents, String severity)
		{
			if (severity != null)
			{
				logEvents = logEvents.Where(le => le.Severity.ToLower() == severity.ToLower());
			}
			return logEvents;
		}

		///Add where clause to filter out records whose eventTime isn't between the filter object's
		///start and end times
		public static IQueryable<LogEvent> WithEventTimeInRange(this IQueryable<LogEvent> logEvents, DateTime? startTime, DateTime? endTime)
		{
			if (startTime != null)
			{
				logEvents = logEvents.Where(le => le.EventTime >= startTime);
			}
			if (endTime != null)
			{
				logEvents = logEvents.Where(le => le.EventTime <= endTime);
			}
			return logEvents;
		}

		///Add where clause to filter out records whose eventTime isn't between the filter object's
		///start and end times
		public static IQueryable<LogEvent> WithTimeLoggedInRange(this IQueryable<LogEvent> logEvents, DateTime? startTime, DateTime? endTime)
		{
			if (startTime != null)
			{
				logEvents = logEvents.Where(le => le.TimeLogged >= startTime);
			}
			if (endTime != null)
			{
				logEvents = logEvents.Where(le => le.TimeLogged <= endTime);
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the application field
		public static IQueryable<LogEvent> WithId(this IQueryable<LogEvent> logEvents, int? id)
		{
			if (id != null)
			{
				logEvents = logEvents.Where(le => le.EventID == id);
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the application field
		public static IQueryable<LogEvent> WithApplication(this IQueryable<LogEvent> logEvents, String application, bool exact)
		{
			if (application != null)
			{
				System.Linq.Expressions.Expression<Func<LogEvent, bool>> whereFunc;
				if (exact)
				{
					whereFunc = le => le.Application == application;
				}
				else
				{
					whereFunc = le => le.Application.StartsWith(application);
				}

				logEvents = logEvents.Where(whereFunc);

			}
			return logEvents;
		}

		///Add where clause to filter out records based on the category field
		public static IQueryable<LogEvent> WithCategory(this IQueryable<LogEvent> logEvents, String category, bool exact)
		{
			if (category != null)
			{
				System.Linq.Expressions.Expression<Func<LogEvent, bool>> whereFunc;
				if (exact)
				{
					whereFunc = le => le.Category == category;
				}
				else
				{
					whereFunc = le => le.Category.StartsWith(category);
				}
				logEvents = logEvents.Where(whereFunc);

			}
			return logEvents;
		}

		///Add where clause to filter out records based on the instance field
		public static IQueryable<LogEvent> WithInstance(this IQueryable<LogEvent> logEvents, String instance, bool exact)
		{
			if (instance != null)
			{
				System.Linq.Expressions.Expression<Func<LogEvent, bool>> whereFunc;
				if (exact)
				{
					whereFunc = le => le.Instance == instance;
				}
				else
				{
					whereFunc = le => le.Instance.StartsWith(instance);
				}
				logEvents = logEvents.Where(whereFunc);
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the machine field
		public static IQueryable<LogEvent> WithMachine(this IQueryable<LogEvent> logEvents, String machine, bool exact)
		{
			if (machine != null)
			{
				System.Linq.Expressions.Expression<Func<LogEvent, bool>> whereFunc;
				if (exact)
				{
					whereFunc = le => le.Machine == machine;
				}
				else
				{
					whereFunc = le => le.Machine.StartsWith(machine);
				}
				logEvents = logEvents.Where(whereFunc);
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the user field
		public static IQueryable<LogEvent> WithUser(this IQueryable<LogEvent> logEvents, String user, bool exact)
		{
			if (user != null)
			{
				System.Linq.Expressions.Expression<Func<LogEvent, bool>> whereFunc;
				if (exact)
				{
					whereFunc = le => le.User == user;
				}
				else
				{
					whereFunc = le => le.User.StartsWith(user);
				}
				logEvents = logEvents.Where(whereFunc);
			}
			return logEvents;
		}

		///Add where clause to filter out records based on the message field
		public static IQueryable<LogEvent> WithMessage(this IQueryable<LogEvent> logEvents, String message, bool exact)
		{
			if (message != null)
			{
				System.Linq.Expressions.Expression<Func<LogEvent, bool>> whereFunc;
				if (exact)
				{
					whereFunc = le => le.Message == message;
				}
				else
				{
					whereFunc = le => le.Message.Contains(message);
				}
				logEvents = logEvents.Where(whereFunc);
			}
			return logEvents;
		}
	}
}
