using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Adis.Log.Contract;

namespace Adis.Log.Listener.WinForms
{
	class KeepConnectionAlive
	{
		#region fields
		private static KeepConnectionAlive _Instance;
		#endregion

		#region Properties
		#region static Properties
		/// <summary>
		/// The singleton instance of this class
		/// </summary>
		private static KeepConnectionAlive Instance
		{
			get
			{
				if (null == _Instance)
				{
					_Instance = new KeepConnectionAlive();
				}
				return _Instance;
			}
		}

		public static EventHandler<ExceptionEventArgs> DetectedProblemWithConnection
		{
			get { return Instance.DetectedProblemWithConnectionInternal; }
			set { Instance.DetectedProblemWithConnectionInternal += value; }
		}

		#endregion

		#region private properties
		private TimeSpan IntervalBetweenKeepAliveCalls { get; set; }

		private Thread KeepAliveThread { get; set; }

		#endregion
		#endregion

		/// <summary>
		/// We really don't want anyone else creating one of these classes
		/// </summary>
		private KeepConnectionAlive()
		{
		}

		#region static methods
		public static void StartKeepAliveThread(IListenerContract listener, String endpointConfigurationName)
		{
			StopKeepAliveThread();

			//set the interval between keep alive calls to be the binding's recieve timeout time span - 1 minute
			Instance.IntervalBetweenKeepAliveCalls = GetConfiguredReceiveTimeout(endpointConfigurationName).Subtract(new TimeSpan(0, 1, 0));

			ParameterizedThreadStart doWork = new ParameterizedThreadStart(Instance.ThreadWorker);
			Instance.KeepAliveThread = new Thread(doWork);
			Instance.KeepAliveThread.Name = "Keep channel alive";
			Instance.KeepAliveThread.Start(listener);
		}

		public static void StopKeepAliveThread()
		{
			if (Instance.KeepAliveThread != null && Instance.KeepAliveThread.IsAlive)
			{
				Instance.KeepAliveThread.Abort();
			}
		}

		/// <summary>
		/// This method gets the receiveTimeout attribute of the endpoint's binding config. 
		/// </summary>
		/// <remarks>
		/// the receiveTimout is important because that is the timeout that will arbitrarily close the channel. 
		/// our keepalive call needs to be less than that
		/// </remarks>
		/// <param name="endpointName"></param>
		/// <returns></returns>
		private static TimeSpan GetConfiguredReceiveTimeout(String endpointName)
		{
			System.ServiceModel.Configuration.ClientSection clientSection = System.Configuration.ConfigurationManager.GetSection("system.serviceModel/client")
				as System.ServiceModel.Configuration.ClientSection;

			String bindingName = null;
			String bindingConfigurationName = null;
			foreach (System.ServiceModel.Configuration.ChannelEndpointElement endpoint in clientSection.Endpoints)
			{
				if (endpoint.Name == endpointName)
				{
					bindingName = endpoint.Binding;
					bindingConfigurationName = endpoint.BindingConfiguration;
				}
			}
			System.ServiceModel.Configuration.BindingsSection bindingSection = System.Configuration.ConfigurationManager.GetSection("system.serviceModel/bindings")
				as System.ServiceModel.Configuration.BindingsSection;
			var binding = bindingSection.BindingCollections.Where(b => b.BindingName == bindingName).First();
			var bindingConfiguration = binding.ConfiguredBindings.Where(bc => bc.Name == bindingConfigurationName).First();

			return bindingConfiguration.ReceiveTimeout;
		}
		#endregion
	
		
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>this is the only method that should actually run on the other thread</remarks>
		/// <param name="listener"></param>
		private void ThreadWorker(Object listener)
		{
			IListenerContract theListener = (IListenerContract)listener;
			while (true)
			{
				try
				{
					theListener.KeepAlive();
				}
				catch (CommunicationException e)
				{
					//for some reason we couldn't keep the channel alive. It died from something other than a timeout.
					//we can't do anything else here so just exit the thread
					OnDetectedProblemWithConnection(new ExceptionEventArgs(e));
					break;
				}
				catch(ObjectDisposedException e)
				{
					//this means that the program has detected that the channel was closed but this thread hasn't been informed.
					//it's a minor bug.
					System.Diagnostics.Debug.WriteLine(e.ToString());
					break;
				}
				Thread.Sleep(IntervalBetweenKeepAliveCalls);
			}
		}

		public event EventHandler<ExceptionEventArgs> DetectedProblemWithConnectionInternal;

		protected void OnDetectedProblemWithConnection(ExceptionEventArgs e)
		{
			if (DetectedProblemWithConnectionInternal != null)
			{
				DetectedProblemWithConnectionInternal(this, e);
			}
		}

	}
}
