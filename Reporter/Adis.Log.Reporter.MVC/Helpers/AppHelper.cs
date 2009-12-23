using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Adis.Log.Reporter.MVC.Helpers
{
	public static class AppHelper
	{

		public static string SiteRoot
		{
			get
			{
				
				return VirtualPathUtility.ToAbsolute("~/");
			}
		}

		/// <summary>
		/// Returns an absolute reference to the Content directory
		/// </summary>
		public static string ContentRoot
		{
			get { return string.Format("{0}{1}", SiteRoot, "Content"); }
		}

		/// <summary>
		/// Returns an absolute reference to the Images directory
		/// </summary>
		public static string ImageRoot
		{
			get { return string.Format("{0}/{1}", ContentRoot, "Images"); }
		}

		/// <summary>
		/// Returns an absolute reference to the Script directory
		/// </summary>
		public static string ScriptRoot
		{
			get { return string.Format("{0}/{1}", ContentRoot, "Script"); }
		}

		/// <summary>
		/// Returns an absolute reference to the CSS directory
		/// </summary>
		public static string CssRoot
		{
			get { return string.Format("{0}/{1}", ContentRoot, "CSS"); }
		}

		public static string ViewsRoot
		{
			get
			{
				string viewsVirtualRoot = "~/Views";
				return VirtualPathUtility.ToAbsolute(viewsVirtualRoot);
			}
		}

		/// <summary>
		/// Builds an Image URL
		/// </summary>
		/// <param name="imageFile">The file name of the image</param>
		public static string ImageUrl(string imageFile)
		{
			string result = string.Format("{0}/{1}", ImageRoot, imageFile);
			return result;
		}

		/// <summary>
		/// Builds a CSS URL
		/// </summary>
		/// <param name="cssFile">The name of the CSS file</param>
		public static string CssUrl(string cssFile)
		{
			string result = string.Format("{0}/{1}", CssRoot, cssFile);
			return result;
		}

		/// <summary>
		/// Builds a script URL
		/// </summary>
		/// <param name="scriptFile">The name of the script file</param>
		public static string ScriptUrl(string scriptFile)
		{
			string result = string.Format("{0}/{1}", ScriptRoot, scriptFile);
			return result;
		}

	}
}

