using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adis.Log.Client
{
	//this class is used by the logManager to populate the Instance field in the default case
	//The app code can set the logManager.Instance to something else which will be used instead
	class DynamicInstance
	{
		public override string ToString()
		{
			return System.Threading.Thread.CurrentThread.Name;
		}
	}
}
