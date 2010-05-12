using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Adis.Log.Client
{
	class Appender : log4net.Appender.AppenderSkeleton
	{
		protected override void Append(log4net.Core.LoggingEvent loggingEvent)
		{
			ServiceConnection.PostLog(loggingEvent);
		}
	}
}
