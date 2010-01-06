using System.Web;
using System.Web.Caching;
using System;

namespace Adis.Log.Reporter.MVC.Providers
{
	public sealed class WebCacheProvider : ICacheProvider
	{
		private static Cache Cache
		{
			get { return HttpContext.Current.Cache; }
		}

		public static bool Contains(string name)
		{
			return Cache[name] != null;
		}

		public void Add(string key, object value, TimeSpan expiry)
		{
			Cache.Add(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, expiry, CacheItemPriority.Normal, null);
		}

		object this[string name]
		{
			get { return Cache[name]; }
			set
			{
				if (value == null) Cache.Remove(name);
				else Cache[name] = value;
			}
		}


		object ICacheProvider.this[string name]
		{
			get { return Cache[name]; }
			set
			{
				if (value == null) Cache.Remove(name);
				else Cache[name] = value;
			}
		}
	}
}
