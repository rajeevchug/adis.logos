using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adis.Log.Contract;

namespace Adis.Log.Reporter.MVC.ViewData
{
	public class FilterBarViewData
	{
		public RequestFilter RequestFilter;
		public string SelectedCategory;
		public IEnumerable<string> Categories;
		public IEnumerable<string> Applications;
		public IEnumerable<string> LogServers;
		public string LogServer;
	}

}