namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using System;
    using System.Collections.Generic;
    using System.Web.Caching;
    using System.Xml;

    public sealed class ShopLanguage
    {
        private static string languageCacheKey = SocoShop.Common.CacheKey.ReadCacheKey("Language");

        public static string ReadLanguage(string key)
        {
            if (CacheHelper.Read(languageCacheKey) == null) RefreshLanguageCache();
            return ((Dictionary<string, string>) CacheHelper.Read(languageCacheKey))[key];
        }

        public static void RefreshLanguageCache()
        {
            string xmlFile = ServerHelper.MapPath("~/Config/ShopLanguage.config");
            Dictionary<string, string> cacheValue = new Dictionary<string, string>();
            using (XmlHelper helper = new XmlHelper(xmlFile))
            {
                foreach (XmlNode node in helper.ReadNode("Language").ChildNodes)
                {
                    cacheValue.Add(node.Attributes["key"].Value, node.InnerText);
                }
            }
            CacheDependency cd = new CacheDependency(xmlFile);
            CacheHelper.Write(languageCacheKey, cacheValue, cd);
        }
    }
}

