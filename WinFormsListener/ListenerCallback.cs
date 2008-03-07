using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormsListener
{
	class ListenerCallback : Logos.Contract.IListenerCallbackContract
	{
		#region IListenerCallbackContract Members

		public void Notify(Logos.Contract.LogTransportObject logObject)
		{
			Program.mainForm.ListOfLogObjects.Add(logObject);
		}

		#endregion
	}
}
