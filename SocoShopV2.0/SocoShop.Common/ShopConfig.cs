namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.Web.Caching;

    public sealed class ShopConfig
    {
        private static string configCacheKey = SocoShop.Common.CacheKey.ReadCacheKey("Config");

        public static ShopConfigInfo ReadConfigInfo()
        {
            if (CacheHelper.Read(configCacheKey) == null) RefreshConfigCache();
            return (ShopConfigInfo) CacheHelper.Read(configCacheKey);
        }

        public static void RefreshApp()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(".aspx", ".ashx");
            URLRewriterModule.ReplaceFileTypeDic = dictionary;
            URLRewriterModule.Path = "/Ashx/" + ReadConfigInfo().TemplatePath;
            URLRewriterModule.ForbidFolder = "|/Admin/|/Plugins/|/Install/|";
            CheckCode.CodeDot = ReadConfigInfo().CodeDot;
            CheckCode.CodeLength = ReadConfigInfo().CodeLength;
            CheckCode.CodeType = (CodeType) ReadConfigInfo().CodeType;
            CheckCode.Key = ReadConfigInfo().SecureKey;
        }

        public static void RefreshConfigCache()
        {
            string fileName = ServerHelper.MapPath("~/Config/ShopConfig.config");
            ShopConfigInfo cacheValue = ConfigHelper.ReadPropertyFromXml<ShopConfigInfo>(fileName);
            CacheDependency cd = new CacheDependency(fileName);
            CacheHelper.Write(configCacheKey, cacheValue, cd);
            RefreshApp();
        }

        public static void UpdateConfigInfo(ShopConfigInfo config)
        {
            ConfigHelper.UpdatePropertyToXml<ShopConfigInfo>(ServerHelper.MapPath("~/Config/ShopConfig.config"), config);
        }
    }
}

