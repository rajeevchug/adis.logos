using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adis.Log.Listener.WinForms
{
	class ListenerCallback : Adis.Log.Contract.IListenerCallbackContract
	{
		#region IListenerCallbackContract Members

		public void Notify(Adis.Log.Contract.LogTransportObject logObject)
		{
			Program.mainForm.ListOfLogObjects.Add(logObject);
		}

		#endregion
	}
}
