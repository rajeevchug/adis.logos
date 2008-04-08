using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adis.Log.Contract;
using System.ServiceModel;
using log4net;

namespace Adis.Log.Server
{
	public class ListenerImplementer
	{
		#region Nested class declarations
		/// <summary>
		/// Used to store all the info needed to reference a particular listener.
		/// </summary>
		public struct ListenerInfo
		{
			public IListenerCallbackContract callback;
			public RequestFilter filter;
			public Uri remoteAddress;
		}
		#endregion

		#region static member variables
		///used in lock() statements to ensure there are no multithreading issues when accessing callbackObjList
		private static Object listenerListLockObject = new Object();

		///The app-wide list of listeners registered on this server
		private static List<ListenerInfo> callbackObjList = new List<ListenerInfo>();

		private static ILog log = LogManager.GetLogger(typeof(ListenerService));
		#endregion

		#region static properties
		public static List<ListenerInfo> CallbackObjList
		{
			get { return callbackObjList; }
		}

		#endregion

		#region static methods
		/// <summary>
		/// Notify all of the current listeners.
		/// </summary>
		/// <param name="logObject">The log that listeners are being notified of.</param>
		public static void NotifyListeners(LogTransportObject logObject, List<ListenerInfo> listenerList)
		{

			List<ListenerInfo> listenersToRemove = new List<ListenerInfo>();

			log.Debug("Notifying listeners of new log");
			try
			{
				lock (listenerListLockObject)
				{
					foreach (ListenerInfo listenerInfo in listenerList)
					{
						if (RequestFilter.SatisfiesFilter(listenerInfo.filter, logObject))
						{
							try
							{
								listenerInfo.callback.Notify(logObject);
								log.DebugFormat("notifying listener of new log from app:{0}, listner URI:{1}",
									logObject.Application, listenerInfo.remoteAddress);
							}
							catch (System.TimeoutException)
							{
								//we don't want to remove the listener at this stage because the channel *may* still be open.
								//if the channel has errored we will get a System.ServiceModel.CommunicationException and we can 
								//remove the listener at that stage.
								log.DebugFormat("Got a timeout when notifying listener {0}.", listenerInfo.remoteAddress);
							}
							catch (CommunicationObjectAbortedException)
							{
								log.Info("Detected an aborted listener connection. Removing it from listener list");
								listenersToRemove.Add(listenerInfo);
							}
							catch (System.ServiceModel.CommunicationException)
							{
								log.ErrorFormat("A general communication exception has occured for listener with URI {0}. Removing it from listener list",
									listenerInfo.remoteAddress);
								listenersToRemove.Add(listenerInfo);
							}
							catch (ObjectDisposedException e)
							{

							}
						}
					}
				}
				RemoveBadListeners(listenerList, listenersToRemove);
			}
			catch (Exception e)
			{
				log.Error(String.Format("Failed to notify (all) listeners of a new log. App: {0}",
					logObject != null ? logObject.Application : "{null}"),
					e);
			}
		}

		/// <summary>
		/// Removes ListenerInfo objects from listenerList.
		/// </summary>
		/// <remarks>
		/// The most common reason to call this method is that the channel in a ListenerInfo object has raised an error or closed
		/// </remarks>
		/// <param name="listenersToRemove">A List of ListenerInfo objects to remove.</param>
		private static void RemoveBadListeners(List<ListenerInfo> listenerList, List<ListenerInfo> listenersToRemove)
		{
			if (listenersToRemove.Count > 0)
			{
				lock (listenerListLockObject)
				{
					foreach (ListenerInfo listenerInfo in listenersToRemove)
					{
						listenerList.Remove(listenerInfo);
					}
				}
			}
		}
		#endregion

		#region Non static methods
		/// <summary>
		/// responds to a listener trying to initialise a link to this server. Adds the listeners details into the listenerList
		/// </summary>
		/// <param name="requestFilter"></param>
		/// <returns></returns>
		public bool InitialiseLink(
			RequestFilter requestFilter, 
			IListenerCallbackContract callbackContract, 
			Uri remoteAddressUri, 
			IContextChannel channel,
			List<ListenerInfo> listenerList)
		{
			bool success = true;
			log.Info("Initialising new Listener");
			try
			{
				lock (listenerListLockObject)
				{
					ListenerInfo newListener = new ListenerInfo()
					{
						callback = callbackContract,
						filter = requestFilter,
						remoteAddress = remoteAddressUri
					};
					listenerList.Add(newListener);

				}
			}
			catch (Exception e)
			{
				success = false;
				log.Error("Failed to add new listener into listener list", e);
			}

			if (success)
			{
				log.DebugFormat("Added new listener with URI {0}", remoteAddressUri);
				channel.Closed += new EventHandler(Channel_Closed);
			}

			return success;
		}

		void Channel_Closed(object sender, EventArgs e)
		{
			//we must perform this check in case there was a problem in the InitialiseLink() call at the client end.
			//In that case the channel is open as far as the server is concerned but there is no current OperationContext
			if (OperationContext.Current != null)
			{
				Uri remoteAddressBeingClosed = OperationContext.Current.Channel.RemoteAddress.Uri;
				ChannelClosedInternal(remoteAddressBeingClosed, ListenerImplementer.CallbackObjList);
			}
			else
			{
				log.Warn("We got a channel closed event but can't identify which channel it is so it can't be removed from the listeners list. This *may* cause ObjectDisposedExceptions ");
			}
		}

		/// <summary>
		/// Removes the listener from the listeners list when it closes
		/// </summary>
		/// <remarks>
		/// This functionality is encalsulated away from the actual closed event handler to make it unit testable 
		/// </remarks>
		/// <param name="remoteAddressBeingClosed"></param>
		private void ChannelClosedInternal(Uri remoteAddressBeingClosed, List<ListenerInfo> listenerList)
		{
			lock (listenerListLockObject)
			{
				IEnumerable<ListenerInfo> listenerInfoList = listenerList.Where(
					listenerInfo => listenerInfo.remoteAddress == remoteAddressBeingClosed);

				if (listenerInfoList.Count() == 1)
				{
					listenerList.Remove(listenerInfoList.First());
				}
				else
				{
					if (listenerInfoList.Count() < 1)
					{
						log.DebugFormat("Closing a listener channel that doesn't exist in the listenerList. {0}",
							remoteAddressBeingClosed);
					}
					else //must be > 1
					{
						log.DebugFormat("Closing a listener channel that exists multiple times in the listenerList. {0}",
							remoteAddressBeingClosed);
					}
				}
			}
		}
		#endregion


	}
}
