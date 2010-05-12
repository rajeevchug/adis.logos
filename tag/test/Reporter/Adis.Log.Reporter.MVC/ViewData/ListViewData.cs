using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adis.Log.Contract;

namespace Adis.Log.Reporter.MVC.ViewData
{
	public class ListViewData
	{
    public RequestFilter RequestFilter;
		public IEnumerable<Contract.LogTransportObject> Logs;
		public int TotalRecords;
		public int Page;
		public int MaxPage;
		public string SelectedCategory;
		public IEnumerable<string> Categories;
		public IEnumerable<string> Applications;
		public IEnumerable<string> LogServers;
		public string ErrorMessage;
		public string LogServer;
	}
}
