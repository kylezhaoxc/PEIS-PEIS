using System;
using System.Web;
using System.Web.Caching;

namespace PEIS.DALFactory
{
	public class DataCache
	{
		public static object GetCache(string CacheKey)
		{
			Cache cache = HttpRuntime.Cache;
			return cache[CacheKey];
		}

		public static void SetCache(string CacheKey, object objObject)
		{
			Cache cache = HttpRuntime.Cache;
			cache.Insert(CacheKey, objObject);
		}
	}
}
