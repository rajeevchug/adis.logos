using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adis.Log.Contract;

namespace Adis.Log.Listener.WinForms
{
	public class LogObjectList 
	{
		private List<LogTransportObject> theList;

		public LogObjectList()
		{
			theList = new List<LogTransportObject>();
		}

		delegate void voidVoidDelegate();

		public void Add(LogTransportObject logObject)
		{
			theList.Add(logObject);
			ResetVirtualSize();
		}

		private static void ResetVirtualSize()
		{
			if (Program.mainForm.InvokeRequired)
			{
				voidVoidDelegate d = new voidVoidDelegate(Program.mainForm.ResetVirtualListSize);
				Program.mainForm.Invoke(d);
			}
			else
			{
				Program.mainForm.ResetVirtualListSize();
			}
		}

		public void AddRange(IEnumerable<LogTransportObject> collection)
		{
			theList.AddRange(collection);
			ResetVirtualSize();
		}

		public void Clear()
		{
			theList.Clear();
		}

		public LogTransportObject this[int i] { get { return theList[i]; } }

		public int Count { get { return theList.Count; } }


	}
}
