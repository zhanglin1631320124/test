namespace SocoShop.Web.Admin
{
    using Newtonsoft.Json;
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.Security;
    using System.Xml;

    public partial class TaobaoProductAdd : AdminBasePage
    {
        private string GetProductID(string access_token, string appKey, string appSecret, int pageSize, int currentPage, ref int totalCount)
        {
            string str = "http://gw.api.taobao.com/router/rest?";
            string[] strArray2 = StringHelper.BubbleSortASC(new string[] { "method=taobao.items.onsale.get", "session=" + access_token, "timestamp=" + RequestHelper.DateNow.ToString("yyyy-MM-dd HH:mm:ss"), "app_key=" + appKey, "v=2.0", "sign_method=md5", "fields=num_iid", "page_size=" + pageSize, "page_no=" + currentPage });
            string str2 = string.Empty;
            string str3 = string.Empty;
            for (int i = 0; i < strArray2.Length; i++)
            {
                str3 = str3 + strArray2[i].Replace("=", string.Empty);
                str2 = str2 + "&" + strArray2[i];
            }
            string xml = HttpHelper.WebRequestGet(str + "sign=" + FormsAuthentication.HashPasswordForStoringInConfigFile(appSecret + str3 + appSecret, "MD5") + str2);
            string str5 = string.Empty;
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            XmlNodeList childNodes = document.SelectSingleNode("items_onsale_get_response/items").ChildNodes;
            foreach (XmlNode node in childNodes)
            {
                str5 = str5 + "," + node["num_iid"].InnerText;
            }
            if (currentPage == 1) totalCount = Convert.ToInt32(document.SelectSingleNode("items_onsale_get_response/total_results").InnerText);
            return str5.Substring(1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string appKey = ShopConfig.ReadConfigInfo().AppKey;
            string appSecret = ShopConfig.ReadConfigInfo().AppSecret;
            if (RequestHelper.GetQueryString<string>("code") != string.Empty || RequestHelper.GetQueryString<string>("error") != string.Empty)
            {
                string queryString = RequestHelper.GetQueryString<string>("code");
                string content = string.Empty;
                try
                {
                    string url = "https://oauth.taobao.com/token";
                    string postData = "grant_type=authorization_code&code=" + queryString + "&redirect_uri=http://" + base.Request.ServerVariables["Http_Host"] + "&client_id=" + appKey + "&client_secret=" + appSecret;
                    AccessData data = (AccessData) JavaScriptConvert.DeserializeObject(HttpHelper.WebRequestPost(url, postData), typeof(AccessData));
                    string str8 = data.access_token;
                    string str9 = string.Empty;
                    int totalCount = 0;
                    int pageSize = 200;
                    str9 = this.GetProductID(str8, appKey, appSecret, pageSize, 1, ref totalCount);
                    int num3 = (int) Math.Ceiling((decimal) (totalCount / pageSize));
                    int currentPage = 2;
                    while (currentPage <= num3)
                    {
                        str9 = str9 + "," + this.GetProductID(str8, appKey, appSecret, pageSize, currentPage, ref totalCount);
                        currentPage++;
                    }
                    decimal discount = UserGradeBLL.ReadUserGradeByMoney(0M).Discount;
                    foreach (string str10 in str9.Split(new char[] { ',' }))
                    {
                        string str11 = "http://gw.api.taobao.com/router/rest?";
                        string[] strArray2 = StringHelper.BubbleSortASC(new string[] { "method=taobao.item.get", "timestamp=" + RequestHelper.DateNow.ToString("yyyy-MM-dd HH:mm:ss"), "app_key=" + appKey, "v=2.0", "sign_method=md5", "fields=title,desc,created,seller_cids,pic_url,num,price", "num_iid=" + str10 });
                        string str12 = string.Empty;
                        string str13 = string.Empty;
                        for (currentPage = 0; currentPage < strArray2.Length; currentPage++)
                        {
                            str13 = str13 + strArray2[currentPage].Replace("=", string.Empty);
                            str12 = str12 + "&" + strArray2[currentPage];
                        }
                        string xml = HttpHelper.WebRequestGet(str11 + "sign=" + FormsAuthentication.HashPasswordForStoringInConfigFile(appSecret + str13 + appSecret, "MD5") + str12);
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(xml);
                        ProductInfo product = new ProductInfo();
                        product.Name = document.SelectSingleNode("item_get_response/item/title").InnerText;
                        product.Spelling = ChineseCharacterHelper.GetFirstLetter(product.Name);
                        product.Introduction = document.SelectSingleNode("item_get_response/item/desc").InnerText;
                        product.Summary = StringHelper.Substring(StringHelper.KillHTML(product.Introduction), 200);
                        product.AddDate = Convert.ToDateTime(document.SelectSingleNode("item_get_response/item/created").InnerText);
                        product.ClassID = this.ReadSystemClassID(document.SelectSingleNode("item_get_response/item/seller_cids").InnerText);
                        product.Photo = document.SelectSingleNode("item_get_response/item/pic_url").InnerText;
                        product.TotalStorageCount = Convert.ToInt32(document.SelectSingleNode("item_get_response/item/num").InnerText);
                        product.MarketPrice = 100M * Convert.ToDecimal(document.SelectSingleNode("item_get_response/item/price").InnerText) / discount;
                        product.TaobaoID = Convert.ToInt64(str10);
                        ProductBLL.TaobaoProduct(product);
                    }
                    string str15 = "<script language='javascript'>window.close();</script>";
                    ResponseHelper.Write(str15);
                    ResponseHelper.End();
                }
                catch (Exception exception)
                {
                    content = exception.Message.ToString();
                }
                if (content == string.Empty) content = RequestHelper.GetQueryString<string>("error");
                if (content != string.Empty) ResponseHelper.Write(content);
            }
            else
                ResponseHelper.Redirect("https://oauth.taobao.com/authorize?response_type=code&client_id=" + appKey + "&redirect_uri=" + ("http://" + base.Request.ServerVariables["Http_Host"] + "/Admin/TaobaoProductAdd.aspx"));
        }

        private string ReadSystemClassID(string taobaoClassIDList)
        {
            string str = string.Empty;
            if (taobaoClassIDList != string.Empty)
            {
                foreach (string str2 in taobaoClassIDList.Split(new char[] { ',' }))
                {
                    if (str2 != string.Empty)
                    {
                        ProductClassInfo info = ProductClassBLL.ReadProductClassCacheByTaobaoID(Convert.ToInt64(str2));
                        if (info.FatherID == 0)
                            str = str + "|" + info.ID.ToString() + "|";
                        else
                        {
                            string str4 = str;
                            str = str4 + "|" + info.FatherID.ToString() + "|" + info.ID.ToString() + "|";
                        }
                    }
                }
            }
            return str;
        }
    }
}

