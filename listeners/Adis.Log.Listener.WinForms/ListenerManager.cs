using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using Adis.Log.Contract;

namespace Adis.Log.Listener.WinForms
{
	class StartServiceEventArgs : EventArgs
	{
		public bool Succeeded { get; set; }
		public Exception Exception { get; set; }
	}

	class ListenerManager
	{
		private static readonly String _ListenerEndpointName = "ListenerEndpoint";

		private static Object _LockObject = new object();
		private static bool _ExpectedClose = false;


		private static ChannelFactory<IListenerContract> channel;
		private static IListenerContract listener;
		
		private System.ComponentModel.BackgroundWorker bgWorker;
		private RequestFilter requestFilter;

		public bool StartService()
		{
			InstanceContext iContext;

			Disconnect();

			bool success = false;
			iContext = new InstanceContext(new Adis.Log.Listener.WinForms.ListenerCallback());
			lock (typeof(ListenerManager))
			{
				channel = new DuplexChannelFactory<IListenerContract>(iContext, _ListenerEndpointName);
				listener = channel.CreateChannel();
				if (channel.State == CommunicationState.Opened)
				{
					channel.Closed += new EventHandler(channel_Closed);
				}
			}
			if (requestFilter == null)
			{
				success = listener.InitialiseLink(new RequestFilter());
			}
			else
			{
				success = listener.InitialiseLink(requestFilter);
				if (success)
				{
					GetInitialRecords();
				}
			}
			if (success)
			{
				KeepConnectionAlive.StartKeepAliveThread(listener, _ListenerEndpointName);
			}
			
			return success;
		}

		/// <summary>
		/// Get any existing records that are logged after the filter's start time.
		/// </summary>
		/// <param name="success"></param>
		/// <returns></returns>
		private void GetInitialRecords()
		{
			int skip = 0;
			int take = 10;
			List<LogTransportObject> logsReturned = null;
			if (requestFilter.StartTime != null)
			{
				do
				{
					logsReturned = listener.GetRecords(requestFilter, skip, take);
					//we got "take" many records so skip that many next time
					skip += take;

					Program.mainForm.ListOfLogObjects.AddRange(logsReturned);

				} while (logsReturned.Count == take);
			}
		}

		private static void Disconnect()
		{
			if (channel != null && channel.State == CommunicationState.Opened)
			{
				lock (_LockObject)
				{
					_ExpectedClose = true;
					channel.Close();
					_ExpectedClose = false;
				}
			}
		}

		void channel_Closed(object sender, EventArgs e)
		{
			lock (_LockObject)
			{
				if (_ExpectedClose)
				{
					return;
				}
			}
			requestFilter.StartTime = DateTime.Now;			
			if (System.Threading.Thread.CurrentThread.ManagedThreadId == Program.UiThreadId)
			{
				StartServiceAsync();
			}
			else
			{
				StartService();
			}
		}

		public void SetRequestFilter(
			String categoryFilter, bool categoryExactMatch,
			String applicationFilter, bool applicationExactMatch,
			String instanceFilter, bool instanceExactMatch,
			String machineFilter, bool machineExactMatch,
			String userFilter, bool userExactMatch,
			String SeverityFilter,
			DateTime? startTimeFilter,
			String messageFilter, bool messageExactMatch
			)
		{
			requestFilter = new Adis.Log.Contract.RequestFilter()
			{
				Category = categoryFilter,
				CategoryExactMatch = categoryExactMatch,
				Application = applicationFilter,
				ApplicationExactMatch = applicationExactMatch,
				Instance = instanceFilter,
				InstanceExactMatch = instanceExactMatch,
				Machine = machineFilter,
				MachineExactMatch = machineExactMatch,
				User = userFilter,
				UserExactMatch = userExactMatch,
				Message = messageFilter,
				StartTime = startTimeFilter,
				Severity = SeverityFilter
			};
		}

		public void StartServiceAsync()
		{
			bgWorker = new System.ComponentModel.BackgroundWorker();
			bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
			bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(bgWorker_DoWork);
			bgWorker.RunWorkerAsync();
		}

		void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			e.Result = StartService();
		}

		void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			StartServiceEventArgs eventArgs;
			if (e.Error != null)
			{
				eventArgs = new StartServiceEventArgs() { Succeeded = false, Exception = e.Error };
			}
			else
			{
				eventArgs = new StartServiceEventArgs() { Succeeded = (e.Result as bool?) ?? false, Exception = null };
			}
			OnStartServiceCompleted(eventArgs);
			bgWorker.Dispose();
			bgWorker = null;
		}

		protected virtual void OnStartServiceCompleted(StartServiceEventArgs e)
		{
			if (StartServiceCompleted != null)
			{
				StartServiceCompleted(this, e);
			}
		}

		public event EventHandler<StartServiceEventArgs> StartServiceCompleted;
	}
}
