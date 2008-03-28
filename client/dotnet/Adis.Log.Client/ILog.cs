using System;
namespace Adis.Log.Client
{
	public interface ILog
	{
		void Debug(string message);
		void Debug(string message, Exception exception);
		void DebugFormat(string format, params object[] args);
		void Error(string message, object extraInfo);
		void Error(string message);
		void Error(string message, Exception exception);
		void ErrorFormat(string format, params object[] args);
		void Fatal(string message, object extraInfo);
		void Fatal(string message);
		void Fatal(string message, Exception exception);
		void FatalFormat(string format, params object[] args);
		void Info(string message, object extraInfo);
		void Info(string message);
		void Info(string message, Exception exception);
		void InfoFormat(string format, params object[] args);
		bool IsDebugEnabled { get; set; }
		bool IsErrorEnabled { get; }
		bool IsFatalEnabled { get; }
		bool IsInfoEnabled { get; }
		bool IsWarnEnabled { get; }
		void Warn(string message, Exception exception);
		void Warn(string message);
		void Warn(string message, object extraInfo);
		void WarnFormat(string format, params object[] args);
	}
}
