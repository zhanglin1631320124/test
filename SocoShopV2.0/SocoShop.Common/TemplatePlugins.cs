namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TemplatePlugins
    {
        private static string path = ServerHelper.MapPath("/Plugins/Template");
        private static string templateCacheKey = CacheKey.ReadCacheKey("Template");

        public static TemplatePluginsInfo ReadTemplatePlugins(string path)
        {
            List<TemplatePluginsInfo> list = ReadTemplatePluginsList();
            TemplatePluginsInfo info = new TemplatePluginsInfo();
            foreach (TemplatePluginsInfo info2 in list)
            {
                if (info2.Path == path) return info2;
            }
            return info;
        }

        public static List<TemplatePluginsInfo> ReadTemplatePluginsList()
        {
            if (CacheHelper.Read(templateCacheKey) == null) RefreshTemplateCache();
            return (List<TemplatePluginsInfo>) CacheHelper.Read(templateCacheKey);
        }

        public static void RefreshTemplateCache()
        {
            List<TemplatePluginsInfo> cacheValue = new List<TemplatePluginsInfo>();
            List<FileInfo> list2 = FileHelper.ListDirectory(path, "|.xml|");
            foreach (FileInfo info in list2)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    TemplatePluginsInfo item = new TemplatePluginsInfo();
                    item.Path = helper.ReadAttribute("Template/Path", "Value");
                    item.Name = helper.ReadAttribute("Template/Name", "Value");
                    item.Photo = helper.ReadAttribute("Template/Photo", "Value");
                    item.DisCreateFile = helper.ReadAttribute("Template/DisCreateFile", "Value");
                    item.CopyRight = helper.ReadAttribute("Template/CopyRight", "Value");
                    item.PublishDate = helper.ReadAttribute("Template/PublishDate", "Value");
                    cacheValue.Add(item);
                }
            }
            CacheHelper.Write(templateCacheKey, cacheValue);
        }
    }
}

