using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Adis.Log.Contract;

namespace Adis.Log.Client
{
	class ServiceConnection
	{
		private static ChannelFactory<ILoggingContract> channel;
		private static ILoggingContract logger;

		private static String _ApplicationPropertyKey = "application";
		private static String _CategoryPropertyKey = "category";
		private static String _UserPropertyKey = "user";
		private static String _InstancePropertyKey = "instance";
		private static String _ExtraInfoPropertyKey = "ExtraInfo";
		private static String _HostNamePropertyKey = "log4net:HostName";

		public static String ApplicationPropertyKey
		{
			get { return _ApplicationPropertyKey; }
		}

		public static String CategoryPropertyKey
		{
			get { return _CategoryPropertyKey; }
		}

		public static String UserPropertyKey
		{
			get { return _UserPropertyKey; }
		}

		public static String InstancePropertyKey
		{
			get { return _InstancePropertyKey; }
		}

		public static String ExtraInfoPropertyKey
		{
			get { return _ExtraInfoPropertyKey; }
		}

		public static String HostNamePropertyKey
		{
			get { return _HostNamePropertyKey; }
		}

		protected static ILoggingContract Logger
		{
			get
			{
				if (null == logger)
				{
					CreateConnectionToLoggingServer();
				}
				return logger;
			}
		}

		private static bool CreateConnectionToLoggingServer()
		{
			try
			{
				channel = new ChannelFactory<ILoggingContract>("Logger");
				channel.Open();
				logger = channel.CreateChannel();

				return channel.State == CommunicationState.Opened;
			}
			catch (System.ServiceModel.EndpointNotFoundException)
			{
				//should go to offline mode here
				throw;
			}
		}

		public static LogTransportObject GenerateLogTransportObject(log4net.Core.LoggingEvent loggingEvent)
		{
			LogTransportObject logObject = new LogTransportObject()
			{
				Category = (loggingEvent.GetProperties()[CategoryPropertyKey] ?? "").ToString(),
				Application = (loggingEvent.GetProperties()[ApplicationPropertyKey] ?? "").ToString(),
				//Use CurrentThread.Name if the Instance property hasn't been set.
				Instance = (loggingEvent.GetProperties()[InstancePropertyKey] ?? System.Threading.Thread.CurrentThread.Name).ToString(),
				Machine = (loggingEvent.GetProperties()[HostNamePropertyKey] ?? "").ToString(),
				Message = (loggingEvent.MessageObject.ToString() ?? ""),
				Time = loggingEvent.TimeStamp,
				User = (loggingEvent.GetProperties()[UserPropertyKey] ?? "").ToString(),
				Severity = (loggingEvent.Level.Name ?? "").ToString()
			};
			if (loggingEvent.GetProperties()[ExtraInfoPropertyKey] != null)
			{
				logObject.ExtraInfo = loggingEvent.GetProperties()[ExtraInfoPropertyKey].ToString();
			}
			else if (loggingEvent.ExceptionObject != null)
			{
				logObject.ExtraInfo = loggingEvent.ExceptionObject.ToString();
			}


			return logObject;
		}

		public static void PostLog(log4net.Core.LoggingEvent loggingEvent)
		{
			try
			{
				Logger.PostLog(GenerateLogTransportObject(loggingEvent));
			}
			catch (EndpointNotFoundException e)
			{
				Console.WriteLine(e.ToString());
				System.Diagnostics.Debug.WriteLine(e.ToString());
				//should go offline here
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				System.Diagnostics.Debug.WriteLine(e.ToString());
			}
		}
	}
}
