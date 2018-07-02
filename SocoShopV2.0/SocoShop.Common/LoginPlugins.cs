namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public sealed class LoginPlugins
    {
        private static string loginPluginsCacheKey = CacheKey.ReadCacheKey("LoginPlugins");
        private static string path = ServerHelper.MapPath("/Plugins/Login");

        public static void ReadCanChangeLoginPlugins(string key, ref Dictionary<string, string> nameDic, ref Dictionary<string, string> valueDic, ref Dictionary<string, string> selectValueDic)
        {
            List<FileInfo> list = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    if (helper.ReadAttribute("Login/Key", "Value") == key)
                    {
                        XmlNodeList childNodes = helper.ReadNode("Login").ChildNodes;
                        foreach (XmlNode node in childNodes)
                        {
                            if (Convert.ToInt32(node.Attributes["IsSystem"].Value) == 0)
                            {
                                nameDic.Add(node.Name, node.Attributes["Name"].Value);
                                valueDic.Add(node.Name, node.Attributes["Value"].Value);
                                if (node.Attributes["SelectValue"] == null)
                                    selectValueDic.Add(node.Name, string.Empty);
                                else
                                    selectValueDic.Add(node.Name, node.Attributes["SelectValue"].Value);
                            }
                        }
                    }
                }
            }
        }

        public static List<LoginPluginsInfo> ReadEnabledLoginPluginsList()
        {
            List<LoginPluginsInfo> list = new List<LoginPluginsInfo>();
            foreach (LoginPluginsInfo info in ReadLoginPluginsList())
            {
                if (info.IsEnabled == 1) list.Add(info);
            }
            return list;
        }

        public static LoginPluginsInfo ReadLoginPlugins(string key)
        {
            LoginPluginsInfo info = new LoginPluginsInfo();
            foreach (LoginPluginsInfo info2 in ReadLoginPluginsList())
            {
                if (info2.Key == key) return info2;
            }
            return info;
        }

        public static List<LoginPluginsInfo> ReadLoginPluginsList()
        {
            if (CacheHelper.Read(loginPluginsCacheKey) == null) RefreshLoginPluginsCache();
            return (List<LoginPluginsInfo>) CacheHelper.Read(loginPluginsCacheKey);
        }

        public static void RefreshLoginPluginsCache()
        {
            List<LoginPluginsInfo> cacheValue = new List<LoginPluginsInfo>();
            List<FileInfo> list2 = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list2)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    LoginPluginsInfo item = new LoginPluginsInfo();
                    item.Key = helper.ReadAttribute("Login/Key", "Value");
                    item.Name = helper.ReadAttribute("Login/Name", "Value");
                    item.Photo = helper.ReadAttribute("Login/Photo", "Value");
                    item.Description = helper.ReadAttribute("Login/Description", "Value");
                    item.IsEnabled = Convert.ToInt32(helper.ReadAttribute("Login/IsEnabled", "Value"));
                    cacheValue.Add(item);
                }
            }
            CacheHelper.Write(loginPluginsCacheKey, cacheValue);
        }

        public static void UpdateLoginPlugins(string key, Dictionary<string, string> configDic)
        {
            List<FileInfo> list = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    if (helper.ReadAttribute("Login/Key", "Value") == key)
                    {
                        foreach (KeyValuePair<string, string> pair in configDic)
                        {
                            helper.UpdateAttribute("Login/" + pair.Key, "Value", pair.Value);
                        }
                        helper.Save();
                    }
                }
            }
            RefreshLoginPluginsCache();
        }
    }
}

