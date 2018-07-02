namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ActivityPlugins
    {
        private static string activityCacheKey = CacheKey.ReadCacheKey("Activity");
        private static string path = ServerHelper.MapPath("/Plugins/Activity");

        public static List<ActivityPluginsInfo> ReadActivityPluginsList()
        {
            if (CacheHelper.Read(activityCacheKey) == null) RefreshActivityPluginsCache();
            return (List<ActivityPluginsInfo>) CacheHelper.Read(activityCacheKey);
        }

        public static List<ActivityPluginsInfo> ReadIsEnabledActivityPluginsList()
        {
            List<ActivityPluginsInfo> list = new List<ActivityPluginsInfo>();
            foreach (ActivityPluginsInfo info in ReadActivityPluginsList())
            {
                if (info.IsEnabled == 1) list.Add(info);
            }
            return list;
        }

        public static void RefreshActivityPluginsCache()
        {
            List<ActivityPluginsInfo> cacheValue = new List<ActivityPluginsInfo>();
            List<FileInfo> list2 = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list2)
            {
                if (info.FullName.ToLower().IndexOf(@"\common.config") > -1)
                {
                    using (XmlHelper helper = new XmlHelper(info.FullName))
                    {
                        ActivityPluginsInfo item = new ActivityPluginsInfo();
                        item.Name = helper.ReadAttribute("Activity/Name", "Value");
                        item.Key = helper.ReadAttribute("Activity/Key", "Value");
                        item.Description = helper.ReadAttribute("Activity/Description", "Value");
                        item.AdminUrl = helper.ReadAttribute("Activity/AdminUrl", "Value");
                        item.ShowUrl = helper.ReadAttribute("Activity/ShowUrl", "Value");
                        item.Photo = helper.ReadAttribute("Activity/Photo", "Value");
                        item.IsEnabled = Convert.ToInt32(helper.ReadAttribute("Activity/IsEnabled", "Value"));
                        item.ApplyVersion = helper.ReadAttribute("Activity/ApplyVersion", "Value");
                        item.CopyRight = helper.ReadAttribute("Activity/CopyRight", "Value");
                        cacheValue.Add(item);
                    }
                }
            }
            CacheHelper.Write(activityCacheKey, cacheValue);
        }

        public static void UpdateActivityPlugins(string key, Dictionary<string, string> configDic)
        {
            List<FileInfo> list = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list)
            {
                if (info.FullName.ToLower().IndexOf(@"\common.config") > -1)
                {
                    using (XmlHelper helper = new XmlHelper(info.FullName))
                    {
                        if (helper.ReadAttribute("Activity/Key", "Value") == key)
                        {
                            foreach (KeyValuePair<string, string> pair in configDic)
                            {
                                helper.UpdateAttribute("Activity/" + pair.Key, "Value", pair.Value);
                            }
                            helper.Save();
                        }
                    }
                }
            }
            RefreshActivityPluginsCache();
        }
    }
}

