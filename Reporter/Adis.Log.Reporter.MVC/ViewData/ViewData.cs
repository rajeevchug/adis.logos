using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adis.Log.Contract;

namespace Adis.Log.Reporter.MVC.ViewData
{
	public enum Severity
	{
		All = 0,
		Debug,
		Info,
		Warning,
		Error,
		Fatal
	};
}
