using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adis.Log.Contract;

namespace Adis.Log.server.Tests
{
	class ListenerCallbackContractMock : IListenerCallbackContract
	{
		public bool NotificationComplete = false;
		#region IListenerCallbackContract Members

		public void Notify(LogTransportObject logObject)
		{
			NotificationComplete = true;
		}

		#endregion
	}
}
