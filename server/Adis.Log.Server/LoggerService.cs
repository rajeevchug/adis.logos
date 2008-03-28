using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Adis.Log.Contract;
using log4net;

namespace Adis.Log.Server
{
	/// <summary>
	/// Implements the logger service. 
	/// The ConcurrencyMode and InstanceContextMode are Multiple and PerCall so that each call to the PostLog() method by the client will have it's own thread.
	/// This means that the client call won't wait for previous calls to finish before it returns (this would slow down the client app)
	/// </summary>
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
	public class LoggerService : ILoggingContract
	{
		private LoggerImplementer Implementer;
		public LoggerService()
		{
			ILog internalLog = LogManager.GetLogger(this.GetType());
			try
			{
				Implementer = new LoggerImplementer();
			}
			catch (Exception e)
			{
				internalLog.Error("Failed to initialise LoggerImplementer", e);
				throw;
			}
		}

		#region ILoggingContract Members

		/// <summary>
		/// This is the method that will be called when a client app attempts to post a new log entry
		/// </summary>
		/// <param name="logObject">the LogObject to be posted</param>
		public void PostLog(LogTransportObject logObject)
		{
			ILog internalLog = LogManager.GetLogger(this.GetType());
			internalLog.DebugFormat("Attempting to log a new log. App={0} Severity={1}", logObject.Application, logObject.Severity); 
			try
			{
				Implementer.InsertNewLog(logObject);
				Adis.Log.Server.ListenerService.NotifyListeners(logObject);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.ToString());
				internalLog.Error(String.Format("Failed. App={0} Severity={1}", logObject.Application, logObject.Severity), e); 
				
				//throw;
			}
		}

		#endregion
	}
}
