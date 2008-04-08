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
		/// <summary>
		/// Inserts a new logObject into the database
		/// </summary>
		/// <param name="logObject"></param>
		/// <returns></returns>
		public bool InsertNewLog(LogTransportObject logObject, IRepository repository)
		{
			ILog internalLog = LogManager.GetLogger(this.GetType());
			internalLog.DebugFormat("Attempting to insert a new log. App={0} Severity={1}", logObject.Application, logObject.Severity);

			LogEvent logEvent = (LogEvent)logObject;
			try
			{
				repository.InsertIntoDatabase(logEvent);
			}
			catch (Exception e)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(e.ToString());
#endif
				internalLog.Error("Failed to insert new log into the database", e);
			}
			return true;
		}

	}
}
