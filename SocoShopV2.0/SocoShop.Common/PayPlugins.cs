namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class PayPlugins
    {
        private static string path = ServerHelper.MapPath("/Plugins/Pay");
        private static string payPluginsCacheKey = CacheKey.ReadCacheKey("PayPlugins");

        public static void ReadCanChangePayPlugins(string key, ref Dictionary<string, string> nameDic, ref Dictionary<string, string> valueDic, ref Dictionary<string, string> selectValueDic)
        {
            List<FileInfo> list = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    if (helper.ReadAttribute("Pay/Key", "Value") == key)
                    {
                        XmlNodeList childNodes = helper.ReadNode("Pay").ChildNodes;
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

        public static PayPluginsInfo ReadPayPlugins(string key)
        {
            PayPluginsInfo info = new PayPluginsInfo();
            foreach (PayPluginsInfo info2 in ReadPayPluginsList())
            {
                if (info2.Key == key) return info2;
            }
            return info;
        }

        public static List<PayPluginsInfo> ReadPayPluginsList()
        {
            if (CacheHelper.Read(payPluginsCacheKey) == null) RefreshPayPluginsCache();
            return (List<PayPluginsInfo>) CacheHelper.Read(payPluginsCacheKey);
        }

        public static List<PayPluginsInfo> ReadProductBuyPayPluginsList()
        {
            List<PayPluginsInfo> list = new List<PayPluginsInfo>();
            foreach (PayPluginsInfo info in ReadPayPluginsList())
            {
                if (info.IsEnabled == 1) list.Add(info);
            }
            return list;
        }

        public static List<PayPluginsInfo> ReadRechargePayPluginsList()
        {
            List<PayPluginsInfo> list = new List<PayPluginsInfo>();
            foreach (PayPluginsInfo info in ReadPayPluginsList())
            {
                if (info.IsEnabled == 1 && info.IsOnline == 1 && info.IsCod == 0) list.Add(info);
            }
            return list;
        }

        public static void RefreshPayPluginsCache()
        {
            List<PayPluginsInfo> cacheValue = new List<PayPluginsInfo>();
            List<FileInfo> list2 = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list2)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    PayPluginsInfo item = new PayPluginsInfo();
                    item.Key = helper.ReadAttribute("Pay/Key", "Value");
                    item.Name = helper.ReadAttribute("Pay/Name", "Value");
                    item.Photo = helper.ReadAttribute("Pay/Photo", "Value");
                    item.Description = helper.ReadAttribute("Pay/Description", "Value");
                    item.IsCod = Convert.ToInt32(helper.ReadAttribute("Pay/IsCod", "Value"));
                    item.IsOnline = Convert.ToInt32(helper.ReadAttribute("Pay/IsOnline", "Value"));
                    item.IsEnabled = Convert.ToInt32(helper.ReadAttribute("Pay/IsEnabled", "Value"));
                    cacheValue.Add(item);
                }
            }
            CacheHelper.Write(payPluginsCacheKey, cacheValue);
        }

        public static void UpdatePayPlugins(string key, Dictionary<string, string> configDic)
        {
            List<FileInfo> list = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    if (helper.ReadAttribute("Pay/Key", "Value") == key)
                    {
                        foreach (KeyValuePair<string, string> pair in configDic)
                        {
                            helper.UpdateAttribute("Pay/" + pair.Key, "Value", pair.Value);
                        }
                        helper.Save();
                    }
                }
            }
            RefreshPayPluginsCache();
        }
    }
}

