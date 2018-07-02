namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using System;
    using System.Collections.Generic;
    using System.Web.Caching;
    using System.Xml;

    public sealed class CacheKey
    {
        private static string cacheKey = "SocoShop_CacheKey";

        public static string ReadCacheKey(string key)
        {
            if (CacheHelper.Read(cacheKey) == null) RefreshCacheKey();
            Dictionary<string, string> dictionary = (Dictionary<string, string>) CacheHelper.Read(cacheKey);
            if (dictionary.ContainsKey(key)) return dictionary[key];
            return Guid.NewGuid().ToString();
        }

        public static void RefreshCacheKey()
        {
            string xmlFile = ServerHelper.MapPath("~/Config/CacheKey.config");
            Dictionary<string, string> cacheValue = new Dictionary<string, string>();
            using (XmlHelper helper = new XmlHelper(xmlFile))
            {
                foreach (XmlNode node in helper.ReadNode("CacheKeys").ChildNodes)
                {
                    cacheValue.Add(node.Attributes["Key"].Value, node.Attributes["Value"].Value);
                }
            }
            CacheDependency cd = new CacheDependency(xmlFile);
            CacheHelper.Write(cacheKey, cacheValue, cd);
        }
    }
}

