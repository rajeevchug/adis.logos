using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Linq;

namespace Adis.Log.Reporter.MVC.Helpers
{
	public static class HtmlExtensions
	{
		public static string ActionButton(this HtmlHelper helper, string clickHandler, string buttonText)
		{
			return "<a href=\"#\" onclick=\"" + clickHandler + "\">" + buttonText + "</a>";
		}

		public static string ActionButton(this HtmlHelper helper, string clickHandler, string buttonText, ButtonCategories category)
		{
			return helper.ActionButton(clickHandler, buttonText, category, null);
		}

		public static string ActionButton(this HtmlHelper helper, string clickHandler, string buttonText, ButtonCategories category, object htmlAttributes)
		{
			return helper.ActionButton(clickHandler, buttonText, category, htmlAttributes, null);
		}

		public static string ActionButton(this HtmlHelper helper, string clickHandler, string buttonText, ButtonCategories category, object htmlAttributes, string buttonId)
		{
			var attributes = new System.Web.Routing.RouteValueDictionary(htmlAttributes);
			if (attributes.ContainsKey("class"))
			{
				attributes["class"] = "buttonwrapper " + attributes["class"];
			}
			else
			{
				attributes["class"] = "buttonwrapper";
			}
			var extraAttributes = attributes.Select(c => c.Key)
				.Aggregate("", (accumulator, next) => string.Format("{2} {0}=\"{1}\"", next, attributes[next], accumulator));

			//string hiddenCategory = String.Format("<input class=\"buttonCategory\" type=\"hidden\" name=\"buttonCategory\" id=\"buttonCategory\" value=\"{0}\" />", category.ToString());

			string buttonClass = "squarebutton_" + category.ToString();
			return string.Format("<div {3}><a class=\"squarebutton {2}\" href=\"#\" onclick=\"{0}\" id=\"{4}\"><span>{1}</span></a></div>",
				clickHandler, buttonText, buttonClass, extraAttributes, buttonId); //, hiddenCategory);
		}
	}

	public enum ButtonCategories { None,  Normal, Option, Close, Cancel, Highlight, Disabled };
}

