using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Adis.CA.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



			routes.MapRoute(
				"ViewList_Get",
				"Main.mvc/ViewList/page/{pageNumber}",
				new { controller = "Main", action = "ViewList" }
			);

			routes.MapRoute(
				"default ViewList_Get",
				"Main.mvc/ViewList",
				new { controller = "Main", action = "ViewList", pageNumber = 1 }
			);

			routes.MapRoute(
				"Default",
				"{controller}.mvc/{action}/{id}",
				new { controller = "Main", action = "Default", id = "" }
			);

		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
			

		}


	}
}
