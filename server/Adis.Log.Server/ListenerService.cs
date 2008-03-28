using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adis.Log.Contract;
using System.ServiceModel;
using log4net;

namespace Adis.Log.Server
{
	class ListenerService : IListenerContract
	{
		private static List<IListenerCallbackContract> CallbackObjList = new List<IListenerCallbackContract>();
		private static ILog log = LogManager.GetLogger(typeof(ListenerService));

		#region IListenerContract Members

		public bool InitialiseLink()
		{
			bool success = true;
			log.Info("Initialising new Listener");
			try
			{
				lock (CallbackObjList)
				{
					CallbackObjList.Add(OperationContext.Current.GetCallbackChannel<IListenerCallbackContract>());
				}
			}
			catch (Exception e)
			{
				success = false;
				log.Error("Failed to add new listener into listener list", e);
#if DEBUG
				System.Diagnostics.Debug.WriteLine(e.ToString());
#endif
			}
			return success;
		}

		#endregion

		public static void NotifyListeners(LogTransportObject logObject)
		{
			try
			{
				lock (CallbackObjList)
				{
					foreach (IListenerCallbackContract listener in CallbackObjList)
					{
						log.DebugFormat("notifying listener of new log from app:{0}", logObject.Application);
						listener.Notify(logObject);
					}
				}
			}
			catch (Exception e)
			{
				log.Error(String.Format("Failed to notify (all) listeners of a new log. App: {0}",
					logObject != null ? logObject.Application : "{null}"),
					e);
#if DEBUG
				System.Diagnostics.Debug.WriteLine(e.ToString());
#endif
			}
		}
	}
}
