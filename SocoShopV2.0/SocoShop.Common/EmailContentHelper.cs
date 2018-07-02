namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class EmailContentHelper
    {
        private static string emailContentCacheKey = CacheKey.ReadCacheKey("EmailContent");
        private static string path = ServerHelper.MapPath("/EmailContent");

        public static void AddEmailContent(EmailContentInfo emailContent)
        {
            XmlDocument document = new XmlDocument();
            document.Load(ServerHelper.MapPath("/EmailContent/Template.config"));
            document.SelectSingleNode("EmailConfig/EmailTitle").InnerText = emailContent.EmailTitle;
            document.SelectSingleNode("EmailConfig/IsSystem").InnerText = 0.ToString();
            document.SelectSingleNode("EmailConfig/Key").InnerText = emailContent.Key;
            document.SelectSingleNode("EmailConfig/EmailContent").InnerText = emailContent.EmailContent;
            document.Save(ServerHelper.MapPath("/EmailContent/" + emailContent.Key + ".config"));
        }

        public static void DeleteEmailContent(string key)
        {
            File.Delete(ServerHelper.MapPath("/EmailContent/" + key + ".config"));
        }

        public static EmailContentInfo ReadCommonEmailContent(string key)
        {
            EmailContentInfo info = new EmailContentInfo();
            List<FileInfo> list = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info2 in list)
            {
                using (XmlHelper helper = new XmlHelper(info2.FullName))
                {
                    if (Convert.ToInt32(helper.ReadInnerText("EmailConfig/IsSystem")) == 0 && helper.ReadInnerText("EmailConfig/Key") == key)
                    {
                        info.EmailTitle = helper.ReadInnerText("EmailConfig/EmailTitle");
                        info.IsSystem = Convert.ToInt32(helper.ReadInnerText("EmailConfig/IsSystem"));
                        info.Key = helper.ReadInnerText("EmailConfig/Key");
                        info.EmailContent = helper.ReadInnerText("EmailConfig/EmailContent");
                        info.Note = helper.ReadInnerText("EmailConfig/Note");
                        return info;
                    }
                }
            }
            return info;
        }

        public static List<EmailContentInfo> ReadCommonEmailContentList()
        {
            List<EmailContentInfo> list = new List<EmailContentInfo>();
            List<FileInfo> list2 = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list2)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    if (Convert.ToInt32(helper.ReadInnerText("EmailConfig/IsSystem")) == 0)
                    {
                        EmailContentInfo item = new EmailContentInfo();
                        item.EmailTitle = helper.ReadInnerText("EmailConfig/EmailTitle");
                        item.IsSystem = Convert.ToInt32(helper.ReadInnerText("EmailConfig/IsSystem"));
                        item.Key = helper.ReadInnerText("EmailConfig/Key");
                        item.EmailContent = helper.ReadInnerText("EmailConfig/EmailContent");
                        item.Note = helper.ReadInnerText("EmailConfig/Note");
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static EmailContentInfo ReadSystemEmailContent(string key)
        {
            EmailContentInfo info = new EmailContentInfo();
            foreach (EmailContentInfo info2 in ReadSystemEmailContentList())
            {
                if (info2.Key == key) return info2;
            }
            return info;
        }

        public static List<EmailContentInfo> ReadSystemEmailContentList()
        {
            if (CacheHelper.Read(emailContentCacheKey) == null) RefreshEmailContentCache();
            return (List<EmailContentInfo>) CacheHelper.Read(emailContentCacheKey);
        }

        public static void RefreshEmailContentCache()
        {
            List<EmailContentInfo> cacheValue = new List<EmailContentInfo>();
            List<FileInfo> list2 = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list2)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    if (Convert.ToInt32(helper.ReadInnerText("EmailConfig/IsSystem")) == 1)
                    {
                        EmailContentInfo item = new EmailContentInfo();
                        item.EmailTitle = helper.ReadInnerText("EmailConfig/EmailTitle");
                        item.IsSystem = Convert.ToInt32(helper.ReadInnerText("EmailConfig/IsSystem"));
                        item.Key = helper.ReadInnerText("EmailConfig/Key");
                        item.EmailContent = helper.ReadInnerText("EmailConfig/EmailContent");
                        item.Note = helper.ReadInnerText("EmailConfig/Note");
                        cacheValue.Add(item);
                    }
                }
            }
            CacheHelper.Write(emailContentCacheKey, cacheValue);
        }

        public static void UpdateEmailContent(EmailContentInfo emailContent)
        {
            List<FileInfo> list = FileHelper.ListDirectory(path, "|.config|");
            foreach (FileInfo info in list)
            {
                using (XmlHelper helper = new XmlHelper(info.FullName))
                {
                    if (helper.ReadInnerText("EmailConfig/Key") == emailContent.Key)
                    {
                        helper.UpdateInnerText("EmailConfig/EmailTitle", emailContent.EmailTitle);
                        helper.UpdateInnerText("EmailConfig/EmailContent", emailContent.EmailContent);
                        helper.Save();
                        break;
                    }
                }
            }
            if (emailContent.IsSystem == 1) RefreshEmailContentCache();
        }
    }
}

