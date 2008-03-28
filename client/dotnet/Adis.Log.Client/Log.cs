using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
namespace Adis.Log.Client
{
	public class Log : Adis.Log.Client.ILog 
	{
		log4net.ILog log4netLog;

		internal Log(log4net.ILog local_log4netLog)
		{
			log4netLog = local_log4netLog;
		}

		public void Debug(String message, Exception exception)
		{
			log4netLog.Debug(message, exception);
		}

		public void Debug(String message)
		{
			log4netLog.Debug(message);
		}

		public void DebugFormat(string format, params object[] args)
		{
			log4netLog.DebugFormat(format, args);
		}

		public void Error(String message, Exception exception)
		{
			log4netLog.Error(message, exception);
		}

		public void Error(String message)
		{
			log4netLog.Error(message);
		}

		public void Error(String message, Object extraInfo)
		{
			ThreadContext.Properties[ServiceConnection.ExtraInfoPropertyKey] = extraInfo;
			Error(message);
			ThreadContext.Properties.Remove(ServiceConnection.ExtraInfoPropertyKey);
		}

		public void ErrorFormat(string format, params object[] args)
		{
			log4netLog.ErrorFormat(format, args);
		}

		public void Fatal(String message, Exception exception)
		{
			log4netLog.Fatal(message, exception);
		}

		public void Fatal(String message)
		{
			log4netLog.Fatal(message);
		}

		public void Fatal(String message, Object extraInfo)
		{
			ThreadContext.Properties[ServiceConnection.ExtraInfoPropertyKey] = extraInfo;
			Fatal(message);
			ThreadContext.Properties.Remove(ServiceConnection.ExtraInfoPropertyKey);
		}

		public void FatalFormat(string format, params object[] args)
		{
			log4netLog.FatalFormat(format, args);
		}

		public void Info(String message, Exception exception)
		{
			log4netLog.Info(message, exception);
		}

		public void Info(String message)
		{
			log4netLog.Info(message);
		}

		public void Info(String message, Object extraInfo)
		{
			ThreadContext.Properties[ServiceConnection.ExtraInfoPropertyKey] = extraInfo;
			Info(message);
			ThreadContext.Properties.Remove(ServiceConnection.ExtraInfoPropertyKey);
		}

		public void InfoFormat(string format, params object[] args)
		{
			log4netLog.InfoFormat(format, args);
		}

		public void Warn(String message, Exception exception)
		{
			log4netLog.Warn(message, exception);
		}

		public void Warn(String message)
		{
			log4netLog.Warn(message);
		}

		public void Warn(String message, Object extraInfo)
		{
			ThreadContext.Properties[ServiceConnection.ExtraInfoPropertyKey] = extraInfo;
			Warn(message);
			ThreadContext.Properties.Remove(ServiceConnection.ExtraInfoPropertyKey);
		}

		public void WarnFormat(string format, params object[] args)
		{
			log4netLog.WarnFormat(format, args);
		}

		public bool IsDebugEnabled
		{
			get { return log4netLog.IsDebugEnabled; }
			set { ((log4net.Repository.Hierarchy.Logger)(log4netLog.Logger)).Level = log4net.Core.Level.Debug; }
		}

		public bool IsErrorEnabled
		{
			get { return log4netLog.IsErrorEnabled; }
		}

		public bool IsFatalEnabled
		{
			get { return log4netLog.IsFatalEnabled; }
		}

		public bool IsInfoEnabled
		{
			get { return log4netLog.IsInfoEnabled; }
		}

		public bool IsWarnEnabled
		{
			get { return log4netLog.IsWarnEnabled; }
		}

	}
}
