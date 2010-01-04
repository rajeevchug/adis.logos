using System;
namespace Adis.Log.Reporter.MVC.Providers
{
	interface ICacheProvider
	{
		void Add(string key, object value, TimeSpan expiry);
		object this[string name] { get; set; }
	}
}
