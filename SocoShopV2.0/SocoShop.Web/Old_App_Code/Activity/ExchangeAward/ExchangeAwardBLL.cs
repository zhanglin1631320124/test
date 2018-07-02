using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Reflection;
using System.Web.Caching;
using SocoShop.Business;
using SocoShop.Common;
using SkyCES.EntLib;

namespace SocoShop.Web
{
    public sealed class ExchangeAwardBLL
    {
        private static string fileName = ServerHelper.MapPath("~/Plugins/Activity/ExchangeAward/ExchangeAward.config");
        private static string cacheKey = "ExchangeAward";
        /// <summary>
        /// 从缓存读取设置值
        /// </summary>
        /// <returns></returns>
        public static ExchangeAwardInfo ReadConfigInfo()
        {
            if (CacheHelper.Read(cacheKey) == null)
            {
                RefreshConfigCache();
            }
            return (ExchangeAwardInfo)CacheHelper.Read(cacheKey);
        }
        /// <summary>
        /// 刷新抽奖活动
        /// </summary>
        public static void RefreshConfigCache()
        {
            ExchangeAwardInfo exchangeAward = new ExchangeAwardInfo();
            PropertyInfo[] pi = typeof(ExchangeAwardInfo).GetProperties();
            using (XmlHelper xh = new XmlHelper(fileName))
            {
                foreach (PropertyInfo p in pi)
                {
                    object innerText = xh.ReadAttribute("ExchangeAward/" + p.Name, "Value");
                    if (p.PropertyType == typeof(System.Int32))
                    {
                        p.SetValue(exchangeAward, Convert.ToInt32(innerText), null);
                    }
                    else if (p.PropertyType == typeof(System.DateTime))
                    {
                        p.SetValue(exchangeAward, Convert.ToDateTime(innerText), null);
                    }
                    else if (p.PropertyType == typeof(System.Decimal))
                    {
                        p.SetValue(exchangeAward, Convert.ToDecimal(innerText), null);
                    }
                    else
                    {
                        p.SetValue(exchangeAward, innerText, null);
                    }
                }
            }
            CacheDependency cd = new CacheDependency(fileName);
            CacheHelper.Write(cacheKey, exchangeAward, cd);
        }
        /// <summary>
        /// 更新抽奖活动
        /// </summary>
        /// <param name="config"></param>
        public static void UpdateConfigInfo(ExchangeAwardInfo exchangeAward)
        {
            PropertyInfo[] pi = typeof(ExchangeAwardInfo).GetProperties();
            using (XmlHelper xh = new XmlHelper(fileName))
            {
                foreach (PropertyInfo p in pi)
                {
                    object oj = p.GetValue(exchangeAward, null);
                    if (oj == null)
                    {
                        continue;
                    }
                    xh.UpdateAttribute("ExchangeAward/" + p.Name, "Value", oj.ToString());
                }
                xh.Save();
            }
        }
    }
}
