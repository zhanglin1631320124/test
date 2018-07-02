namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.Security;
    using System.Xml;

    public partial class Ajax : AdminBasePage
    {
        protected void AddProductPhoto()
        {
            ProductPhotoInfo productPhoto = new ProductPhotoInfo();
            productPhoto.ProductID = RequestHelper.GetQueryString<int>("ProductID");
            productPhoto.Name = RequestHelper.GetQueryString<string>("Name");
            productPhoto.Photo = RequestHelper.GetQueryString<string>("Photo");
            int num = ProductPhotoBLL.AddProductPhoto(productPhoto);
            ResponseHelper.Write(num.ToString() + "|" + productPhoto.Name + "|" + productPhoto.Photo);
            ResponseHelper.End();
        }

        private void CheckEmail()
        {
            string content = "ok";
            if (UserBLL.CheckEmail(RequestHelper.GetQueryString<string>("Email"))) content = "error";
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        private void CheckUserName()
        {
            string content = "ok";
            if (UserBLL.CheckUserName(RequestHelper.GetQueryString<string>("UserName")) > 0) content = "error";
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        private void CreateSitemap()
        {
            using (XmlTextWriter writer = new XmlTextWriter(base.Server.MapPath("~/Sitemap.xml"), null))
            {
                XmlDocument domDoc = new XmlDocument();
                XmlDeclaration newChild = domDoc.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, null);
                domDoc.AppendChild(newChild);
                XmlElement element = domDoc.CreateElement("urlset");
                element.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                domDoc.AppendChild(element);
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.IsSale = 1;
                List<ProductInfo> list = ProductBLL.SearchProductList(productSearch);
                foreach (ProductInfo info2 in list)
                {
                    this.CreateSitemapUrl(domDoc, element, "http://" + ShopConfig.ReadConfigInfo().Domain + "/Product-I" + info2.ID.ToString() + ".aspx");
                }
                TagsSearchInfo tags = new TagsSearchInfo();
                List<TagsInfo> list2 = TagsBLL.SearchTagsList(tags);
                foreach (TagsInfo info4 in list2)
                {
                    this.CreateSitemapUrl(domDoc, element, "http://" + ShopConfig.ReadConfigInfo().Domain + "/Product/Tags/" + base.Server.UrlEncode(info4.Word) + ".aspx");
                }
                ProductCommentSearchInfo productComment = new ProductCommentSearchInfo();
                productComment.Status = 2;
                List<ProductCommentInfo> list3 = ProductCommentBLL.SearchProductCommentList(productComment);
                foreach (ProductCommentInfo info6 in list3)
                {
                    this.CreateSitemapUrl(domDoc, element, "http://" + ShopConfig.ReadConfigInfo().Domain + "/ProductReply-C" + info6.ID.ToString() + ".aspx");
                }
                domDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
            }
            ResponseHelper.Write("ok");
            ResponseHelper.End();
        }

        private void CreateSitemapUrl(XmlDocument domDoc, XmlElement root, string url)
        {
            XmlElement newChild = domDoc.CreateElement("url");
            root.AppendChild(newChild);
            XmlElement element2 = domDoc.CreateElement("loc");
            element2.InnerText = url;
            newChild.AppendChild(element2);
            XmlElement element3 = domDoc.CreateElement("lastmod");
            element3.InnerText = DateTime.Now.ToString("yyy-MM-dd");
            newChild.AppendChild(element3);
            XmlElement element4 = domDoc.CreateElement("changefreq");
            element4.InnerText = ShopConfig.ReadConfigInfo().Frequency;
            newChild.AppendChild(element4);
            XmlElement element5 = domDoc.CreateElement("priority");
            element5.InnerText = ShopConfig.ReadConfigInfo().Priority;
            newChild.AppendChild(element5);
        }

        protected void DeleteEmailContent()
        {
            string queryString = RequestHelper.GetQueryString<string>("Key");
            try
            {
                EmailContentHelper.DeleteEmailContent(queryString);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("EmailContent"), queryString);
            }
            catch
            {
                queryString = "error";
            }
            ResponseHelper.Write(queryString);
            ResponseHelper.End();
        }

        private void DeleteProduct()
        {
            string content = "ok";
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            if (OrderDetailBLL.ReadOrderDetailByProductID(queryString).Count > 0)
                content = "error";
            else
                ProductBLL.DeleteProduct(queryString.ToString());
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void DeleteProductPhoto()
        {
            ProductPhotoBLL.DeleteProductPhoto(RequestHelper.GetQueryString<string>("ProductPhotoID"));
            ResponseHelper.End();
        }

        private void DeleteUser()
        {
            string content = "ok";
            int queryString = RequestHelper.GetQueryString<int>("UserID");
            if (UserAccountRecordBLL.ReadUserAccountRecordList(queryString).Count > 0)
                content = "error";
            else
                UserBLL.DeleteUser(queryString.ToString());
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        private void DownTaobaoPhoto()
        {
            ProductSearchInfo productSearch = new ProductSearchInfo();
            productSearch.IsTaobao = 1;
            List<ProductInfo> list = ProductBLL.SearchProductList(productSearch);
            foreach (ProductInfo info2 in list)
            {
                if (!info2.Photo.ToLower().StartsWith("/upload/productcoverphoto/"))
                {
                    string str = FileHelper.CreateFileName(FileNameType.Guid, string.Empty, FileHelper.GetFileExtension(info2.Photo));
                    string filePath = "/Upload/ProductCoverPhoto/Original/" + RequestHelper.DateNow.ToString("yyyyMM") + "/";
                    DirectoryInfo info3 = new DirectoryInfo(ServerHelper.MapPath(filePath));
                    if (!info3.Exists) info3.Create();
                    string str3 = filePath + str;
                    WebClient client = new WebClient();
                    try
                    {
                        client.DownloadFile(info2.Photo, ServerHelper.MapPath(str3));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    ProductBLL.UpdateProductCoverPhoto(info2.ID, str3);
                    Dictionary<int, int> dictionary = new Dictionary<int, int>();
                    dictionary.Add(60, 60);
                    dictionary.Add(120, 120);
                    dictionary.Add(340, 340);
                    string str4 = string.Empty;
                    string str5 = string.Empty;
                    foreach (KeyValuePair<int, int> pair in dictionary)
                    {
                        str4 = str3.Replace("Original", pair.Key.ToString() + "-" + pair.Value.ToString());
                        str5 = str5 + str4 + "|";
                        ImageHelper.MakeThumbnailImage(ServerHelper.MapPath(str3), ServerHelper.MapPath(str4), pair.Key, pair.Value, ThumbnailType.InBox);
                    }
                    str5 = str5.Substring(0, str5.Length - 1);
                    UploadInfo upload = new UploadInfo();
                    upload.TableID = ProductBLL.TableID;
                    upload.ClassID = 0;
                    upload.RecordID = info2.ID;
                    upload.UploadName = str3;
                    upload.OtherFile = str5;
                    upload.Size = Convert.ToInt32(new FileInfo(ServerHelper.MapPath(str3)).Length);
                    upload.FileType = FileHelper.GetFileExtension(info2.Photo);
                    upload.RandomNumber = Cookies.Admin.GetRandomNumber(false);
                    upload.Date = RequestHelper.DateNow;
                    upload.IP = ClientHelper.IP;
                    UploadBLL.AddUpload(upload);
                }
            }
            ResponseHelper.Write("ok");
            ResponseHelper.End();
        }

        private void ImportProductClass()
        {
            string appKey = ShopConfig.ReadConfigInfo().AppKey;
            string appSecret = ShopConfig.ReadConfigInfo().AppSecret;
            string nickName = ShopConfig.ReadConfigInfo().NickName;
            if (ShopConfig.ReadConfigInfo().DeleteProductClass == 1) ProductClassBLL.DeleteTaobaoProductClass();
            string str4 = "http://gw.api.taobao.com/router/rest?";
            string[] strArray2 = StringHelper.BubbleSortASC(new string[] { "method=taobao.sellercats.list.get", "timestamp=" + RequestHelper.DateNow.ToString("yyyy-MM-dd HH:mm:ss"), "app_key=" + appKey, "v=2.0", "sign_method=md5", "nick=" + nickName, "fields=cid,name,parent_cid,sort_order" });
            string str5 = string.Empty;
            string str6 = string.Empty;
            for (int i = 0; i < strArray2.Length; i++)
            {
                str6 = str6 + strArray2[i].Replace("=", string.Empty);
                str5 = str5 + "&" + strArray2[i];
            }
            string xml = HttpHelper.WebRequestGet(str4 + "sign=" + FormsAuthentication.HashPasswordForStoringInConfigFile(appSecret + str6 + appSecret, "MD5") + str5);
            Dictionary<long, int> fatherIDDic = new Dictionary<long, int>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            XmlNodeList childNodes = document.SelectSingleNode("sellercats_list_get_response/seller_cats").ChildNodes;
            foreach (XmlNode node in childNodes)
            {
                ProductClassInfo productClass = new ProductClassInfo();
                productClass.ClassName = node["name"].InnerText;
                productClass.FatherID = Convert.ToInt32(node["parent_cid"].InnerText);
                productClass.OrderID = Convert.ToInt32(node["sort_order"].InnerText);
                productClass.TaobaoID = Convert.ToInt32(node["cid"].InnerText);
                ProductClassBLL.AddProductClass(productClass);
                fatherIDDic.Add(Convert.ToInt64(node["cid"].InnerText), productClass.ID);
            }
            ProductClassBLL.UpdateProductFatherID(fatherIDDic);
            ResponseHelper.Write("ok");
            ResponseHelper.End();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ClearCache();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            switch (queryString)
            {
                case "SearchProduct":
                    this.SearchProduct();
                    break;

                case "SearchGift":
                    this.SearchGift();
                    break;

                case "ReadChildRegion":
                    ResponseHelper.Write(RegionBLL.SearchRegionList(RequestHelper.GetQueryString<int>("RegionID")));
                    ResponseHelper.End();
                    break;

                case "ReadChildProductClass":
                    ResponseHelper.Write(ProductClassBLL.SearchProductClassList(RequestHelper.GetQueryString<int>("ProductClassID")));
                    ResponseHelper.End();
                    break;

                case "AddProductPhoto":
                    this.AddProductPhoto();
                    break;

                case "DeleteProductPhoto":
                    this.DeleteProductPhoto();
                    break;

                case "TestSendEmail":
                    this.TestSendEmail();
                    break;

                case "DeleteEmailContent":
                    this.DeleteEmailContent();
                    break;

                case "SendEmail":
                    this.SendEmail();
                    break;

                case "ReadUserEmail":
                    this.ReadUserEmail();
                    break;

                case "IsEnabled":
                    this.UpdateActivityPluginsIsEnabled();
                    break;

                case "CheckUserName":
                    this.CheckUserName();
                    break;

                case "CheckEmail":
                    this.CheckEmail();
                    break;

                case "IsSpecial":
                case "IsNew":
                case "IsHot":
                case "IsTop":
                case "AllowComment":
                    this.UpdateProductStatus(queryString);
                    break;

                case "TagsIsTop":
                    this.UpdateTagsIsTop(queryString);
                    break;

                case "DeleteProduct":
                    this.DeleteProduct();
                    break;

                case "DeleteUser":
                    this.DeleteUser();
                    break;

                case "ImportProductClass":
                    this.ImportProductClass();
                    break;

                case "DownTaobaoPhoto":
                    this.DownTaobaoPhoto();
                    break;

                case "CreateSitemap":
                    this.CreateSitemap();
                    break;
            }
        }

        protected void ReadUserEmail()
        {
            ResponseHelper.Write(UserBLL.ReadUserByUserName(RequestHelper.GetQueryString<string>("UserName")).Email);
            ResponseHelper.End();
        }

        protected void SearchGift()
        {
            string content = string.Empty;
            GiftSearchInfo gift = new GiftSearchInfo();
            gift.Name = RequestHelper.GetQueryString<string>("Name");
            gift.InGiftID = RequestHelper.GetQueryString<string>("GiftID");
            List<GiftInfo> list = GiftBLL.SearchGiftList(gift);
            foreach (GiftInfo info2 in list)
            {
                if (content == string.Empty)
                    content = info2.ID + "|" + info2.Name.Replace("|", "").Replace("#", "");
                else
                {
                    object obj2 = content;
                    content = string.Concat(new object[] { obj2, "#", info2.ID, "|", info2.Name.Replace("|", "").Replace("#", "") });
                }
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void SearchProduct()
        {
            string content = string.Empty;
            ProductSearchInfo productSearch = new ProductSearchInfo();
            productSearch.Name = RequestHelper.GetQueryString<string>("ProductName");
            productSearch.ClassID = RequestHelper.GetQueryString<string>("ClassID");
            List<ProductInfo> list = ProductBLL.SearchProductList(productSearch);
            foreach (ProductInfo info2 in list)
            {
                if (content == string.Empty)
                    content = string.Concat(new object[] { info2.ID, "|", info2.Name, "|", info2.Photo });
                else
                {
                    object obj2 = content;
                    content = string.Concat(new object[] { obj2, "#", info2.ID, "|", info2.Name, "|", info2.Photo });
                }
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void SendEmail()
        {
            ResponseHelper.Write(EmailSendRecordBLL.SendEmail(EmailSendRecordBLL.ReadEmailSendRecord(RequestHelper.GetQueryString<int>("ID"))).SendDate.ToString());
            ResponseHelper.End();
        }

        protected void TestSendEmail()
        {
            string content = "ok";
            EmailContentInfo info = EmailContentHelper.ReadSystemEmailContent("TestEmail");
            MailInfo mail = new MailInfo();
            mail.ToEmail = RequestHelper.GetQueryString<string>("ToEmail");
            mail.Title = info.EmailTitle;
            mail.Content = info.EmailContent;
            mail.UserName = RequestHelper.GetQueryString<string>("EmailUserName");
            mail.Password = RequestHelper.GetQueryString<string>("EmailPassword");
            mail.Server = RequestHelper.GetQueryString<string>("EmailServer");
            mail.ServerPort = RequestHelper.GetQueryString<int>("EmailServerPort");
            try
            {
                MailClass.SendEmail(mail);
            }
            catch
            {
                content = "error";
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        private void UpdateActivityPluginsIsEnabled()
        {
            string queryString = RequestHelper.GetQueryString<string>("Action");
            string key = RequestHelper.GetQueryString<string>("ID");
            Dictionary<string, string> configDic = new Dictionary<string, string>();
            configDic.Add("IsEnabled", RequestHelper.GetQueryString<string>("Status"));
            SocoShop.Common.ActivityPlugins.UpdateActivityPlugins(key, configDic);
            ResponseHelper.Write(queryString + "|" + key);
            ResponseHelper.End();
        }

        private void UpdateProductStatus(string action)
        {
            int queryString = RequestHelper.GetQueryString<int>("ID");
            int status = RequestHelper.GetQueryString<int>("Status");
            string str = action;
            if (str != null)
            {
                if (!(str == "IsSpecial"))
                {
                    if (str == "IsNew")
                        ProductBLL.ChangProductIsNew(queryString, status);
                    else if (str == "IsHot")
                        ProductBLL.ChangProductIsHot(queryString, status);
                    else if (str == "IsTop")
                        ProductBLL.ChangProductIsTop(queryString, status);
                    else if (str == "AllowComment") ProductBLL.ChangProductAllowComment(queryString, status);
                }
                else
                    ProductBLL.ChangProductIsSpecial(queryString, status);
            }
            ResponseHelper.Write(action + "|" + queryString.ToString());
            ResponseHelper.End();
        }

        private void UpdateTagsIsTop(string action)
        {
            int queryString = RequestHelper.GetQueryString<int>("ID");
            int status = RequestHelper.GetQueryString<int>("Status");
            TagsBLL.UpdateTagsIsTop(queryString, status);
            ResponseHelper.Write(action + "|" + queryString.ToString());
            ResponseHelper.End();
        }
    }
}

