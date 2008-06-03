using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adis.Log.Listener.WinForms
{
	class ListenerCallback : Adis.Log.Contract.IListenerCallbackContract
	{
		private MainForm MainForm { get; set; }

		public ListenerCallback(MainForm mainForm)
		{
			MainForm = mainForm;
		}

		private ListenerCallback() { }

		#region IListenerCallbackContract Members

		public void Notify(Adis.Log.Contract.LogTransportObject logObject)
		{
			MainForm.Add(logObject);
		}

		#endregion
	}
}
