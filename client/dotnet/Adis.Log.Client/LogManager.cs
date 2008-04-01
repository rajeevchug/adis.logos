using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Adis.Log.Client
{
	public static class LogManager
	{
		private static String DefaultLog4netLoggerConfigName = "Adis.Log.Logger";

		/// <summary>
		/// Configure log4net using the default config file log4net.config
		/// </summary>
		public static void Configure()
		{
			log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
			InitialiseGlobalProperties();
		}

		/// <summary>
		/// Configure log4net using the specified file as the config file
		/// </summary>
		/// <param name="fileInfo">the log4net config file</param>
		public static void Configure(System.IO.FileInfo fileInfo)
		{
			InitialiseGlobalProperties();
			if (!fileInfo.Exists)
			{
				throw new System.IO.FileNotFoundException("couldn't fild the config file", fileInfo.FullName);
			}

			log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
		}

		/// <summary>
		/// Specifies defaults for the global properties. These properties will be defined for logs from *all* threads.
		/// </summary>
		private static void InitialiseGlobalProperties()
		{
			Application = System.Threading.Thread.GetDomain().FriendlyName;
		}

		/// <summary>
		/// Sets or gets the application property
		/// </summary>
		/// <default>the Domain's FriendlyName property</default>
		public static Object Application
		{
			get { return GlobalContext.Properties[ServiceConnection.ApplicationPropertyKey]; }
			set { GlobalContext.Properties[ServiceConnection.ApplicationPropertyKey] = value; }
		}

		public static Object Category
		{
			get { return GlobalContext.Properties[ServiceConnection.CategoryPropertyKey]; }
			set { GlobalContext.Properties[ServiceConnection.CategoryPropertyKey] = value; }
		}

		public static Object User
		{
			get { return ThreadContext.Properties[ServiceConnection.UserPropertyKey]; }
			set { ThreadContext.Properties[ServiceConnection.UserPropertyKey] = value; }
		}

		public static Object Instance
		{
			get { return ThreadContext.Properties[ServiceConnection.InstancePropertyKey]; }
			set { ThreadContext.Properties[ServiceConnection.InstancePropertyKey] = value; }
		}

		public static ILog GetLog()
		{
			return GetLog(DefaultLog4netLoggerConfigName);
		}

		public static ILog GetLog(String logName)
		{
			return new Log(log4net.LogManager.GetLogger(logName));
		}

		public static ILog GetLog(Type classType)
		{
			return new Log(log4net.LogManager.GetLogger(classType));
		}
	}
}
