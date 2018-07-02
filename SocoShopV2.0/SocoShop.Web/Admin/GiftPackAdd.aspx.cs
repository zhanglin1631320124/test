namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class GiftPackAdd : AdminBasePage
    {
        protected string[] countArray = new string[0];
        protected string[] nameArray = new string[0];
        protected string[] productArray = new string[0];
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected string strGiftPackID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadGiftPack", PowerCheckType.Single);
                    this.strGiftPackID = queryString.ToString();
                    GiftPackInfo info = GiftPackBLL.ReadGiftPack(queryString);
                    this.Name.Text = info.Name;
                    this.Photo.Text = info.Photo;
                    this.StartDate.Text = info.StartDate.ToString("yyyy-MM-dd");
                    this.EndDate.Text = info.EndDate.ToString("yyyy-MM-dd");
                    this.Price.Text = info.Price.ToString();
                    if (info.GiftGroup != string.Empty)
                    {
                        string str = string.Empty;
                        int length = info.GiftGroup.Split(new char[] { '#' }).Length;
                        this.nameArray = new string[length];
                        this.countArray = new string[length];
                        this.productArray = new string[length];
                        for (int i = 0; i < length; i++)
                        {
                            string[] strArray = info.GiftGroup.Split(new char[] { '#' })[i].Split(new char[] { '|' });
                            this.nameArray[i] = strArray[0];
                            this.countArray[i] = strArray[1];
                            this.productArray[i] = strArray[2];
                            if (strArray[2] != string.Empty) str = str + strArray[2] + ",";
                        }
                        if (str != string.Empty)
                        {
                            str = str.Substring(0, str.Length - 1);
                            ProductSearchInfo productSearch = new ProductSearchInfo();
                            productSearch.InProductID = str;
                            this.productList = ProductBLL.SearchProductList(productSearch);
                        }
                    }
                }
            }
        }

        protected ProductInfo ReadProduct(List<ProductInfo> productList, int id)
        {
            ProductInfo info = new ProductInfo();
            foreach (ProductInfo info2 in productList)
            {
                if (info2.ID == id) info = info2;
            }
            return info;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            GiftPackInfo giftPack = new GiftPackInfo();
            giftPack.ID = RequestHelper.GetQueryString<int>("ID");
            giftPack.Name = this.Name.Text;
            giftPack.Photo = this.Photo.Text;
            giftPack.StartDate = Convert.ToDateTime(this.StartDate.Text);
            giftPack.EndDate = Convert.ToDateTime(this.EndDate.Text).AddDays(1.0).AddSeconds(-1.0);
            giftPack.Price = Convert.ToDecimal(this.Price.Text);
            int form = RequestHelper.GetForm<int>("GiftGroupCount");
            string str = string.Empty;
            for (int i = 0; i < form; i++)
            {
                if (RequestHelper.GetForm<string>("GiftGroupValue" + i) != string.Empty) str = str + RequestHelper.GetForm<string>("GiftGroupValue" + i) + "#";
            }
            if (str.EndsWith("#")) str = str.Substring(0, str.Length - 1);
            giftPack.GiftGroup = str;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (giftPack.ID == -2147483648)
            {
                base.CheckAdminPower("AddGiftPack", PowerCheckType.Single);
                int id = GiftPackBLL.AddGiftPack(giftPack);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("GiftPack"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateGiftPack", PowerCheckType.Single);
                GiftPackBLL.UpdateGiftPack(giftPack);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("GiftPack"), giftPack.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

