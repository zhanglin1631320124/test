namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    public class AdminBasePage : Page
    {
        protected int AdminID = 0;
        protected int Count = 0;
        protected string IDPrefix = ShopConfig.ReadConfigInfo().IDPrefix;
        protected string NamePrefix = ShopConfig.ReadConfigInfo().NamePrefix;
        protected int PageSize = 20;

        protected static void Alert(string alertMessage, string url)
        {
            if (ShopConfig.ReadConfigInfo().SaveAutoClosePop == 1)
            {
                string str = ("<script language='javascript'>window.alert('" + alertMessage + "');") + "try{ var DG = frameElement.lhgDG;DG.cancel();";
                if (ShopConfig.ReadConfigInfo().PopCloseRefresh == 1) str = str + "DG.curWin.location.reload();";
                ResponseHelper.Write(str + "}catch (e) { }</script>");
                ResponseHelper.End();
            }
            else
                ScriptHelper.Alert(alertMessage, url);
        }

        protected void BindControl(CommonPager commonPager)
        {
            this.BindControl(null, null, commonPager);
        }

        protected void BindControl(object dataSource, Repeater repeater)
        {
            this.BindControl(dataSource, repeater, null);
        }

        protected void BindControl(object dataSource, Repeater repeater, CommonPager commonPager)
        {
            if (repeater != null)
            {
                repeater.DataSource = dataSource;
                repeater.DataBind();
            }
            if (commonPager != null)
            {
                commonPager.CurrentPage = this.CurrentPage;
                commonPager.PageSize = this.PageSize;
                commonPager.Count = this.Count;
            }
        }

        private void CheckAdminLogin()
        {
            if (Cookies.Admin.GetAdminID(true) == 0)
            {
                ResponseHelper.Write("<script language='javascript'>window.parent.location.href='/Admin/Login.aspx';</script>");
                ResponseHelper.End();
            }
        }

        protected void CheckAdminPower(string powerString, PowerCheckType powerCheckType)
        {
            this.CheckAdminPower(ShopConfig.ReadConfigInfo().PowerKey, powerString, PowerCheckType.Single, ref this.AdminID);
        }

        private void CheckAdminPower(string powerKey, string powerString, PowerCheckType powerCheckType, ref int adminID)
        {
            string power = AdminGroupBLL.ReadAdminGroupCache(Cookies.Admin.GetGroupID(false)).Power;
            bool flag = false;
            switch (powerCheckType)
            {
                case PowerCheckType.Single:
                    if (power.IndexOf("|" + powerKey + powerString + "|") > -1) flag = true;
                    break;

                case PowerCheckType.OR:
                    foreach (string str2 in powerString.Split(new char[] { ',' }))
                    {
                        if (power.IndexOf("|" + powerKey + str2 + "|") > -1)
                        {
                            flag = true;
                            break;
                        }
                    }
                    break;

                case PowerCheckType.AND:
                    flag = true;
                    foreach (string str2 in powerString.Split(new char[] { ',' }))
                    {
                        if (power.IndexOf("|" + powerKey + str2 + "|") == -1)
                        {
                            flag = false;
                            break;
                        }
                    }
                    break;
            }
            if (!flag)
                adminID = -1;
            else
            {
                bool flag2 = false;
                Hashtable hashtable = this.ReadAllNeedOther();
                foreach (DictionaryEntry entry in hashtable)
                {
                    if (entry.Key.ToString() == powerString)
                    {
                        flag2 = Convert.ToBoolean(entry.Value);
                        if (!flag2) break;
                    }
                }
                if (flag2)
                {
                    if (power.IndexOf("|" + powerKey + "ManageOther|") > -1)
                        adminID = -2147483648;
                    else
                        adminID = Cookies.Admin.GetAdminID(false);
                }
                else
                    adminID = -2147483648;
            }
            if (adminID == -1) ScriptHelper.Alert(ShopLanguage.ReadLanguage("NoPower"));
        }

        protected void ClearCache()
        {
            base.Response.Cache.SetNoServerCaching();
            base.Response.Cache.SetNoStore();
            base.Response.Expires = 0;
        }

        protected static string GetAddUpdate()
        {
            string str = "添加";
            if (RequestHelper.GetQueryString<int>("ID") > 0) str = "修改";
            return str;
        }

        protected override void OnInit(EventArgs e)
        {
            this.CheckAdminLogin();
            base.OnInit(e);
        }

        private Hashtable ReadAllNeedOther()
        {
            string cacheKey = CacheKey.ReadCacheKey("NeedOther");
            Hashtable cacheValue = new Hashtable();
            if (CacheHelper.Read(cacheKey) == null)
            {
                XmlDocument document = new XmlDocument();
                document.Load(ServerHelper.MapPath("/Config/AdminPower.config"));
                XmlNodeList elementsByTagName = document.GetElementsByTagName("Item");
                foreach (XmlNode node in elementsByTagName)
                {
                    cacheValue.Add(node.Attributes["Value"].Value, node.Attributes["NeedOther"].Value);
                }
                document = null;
                CacheHelper.Write(cacheKey, cacheValue);
                return cacheValue;
            }
            return (Hashtable) CacheHelper.Read(cacheKey);
        }

        protected int CurrentPage
        {
            get
            {
                int queryString = RequestHelper.GetQueryString<int>("Page");
                if (queryString < 1) queryString = 1;
                return queryString;
            }
        }
    }
}

