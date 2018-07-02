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

    public partial class ThemeActivityAdd : AdminBasePage
    {
        protected string[] idArray = new string[0];
        protected string[] linkArray = new string[0];
        protected string[] photoArray = new string[0];
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected string strThemeActivityID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadThemeActivity", PowerCheckType.Single);
                    this.strThemeActivityID = queryString.ToString();
                    ThemeActivityInfo info = ThemeActivityBLL.ReadThemeActivity(queryString);
                    this.Name.Text = info.Name;
                    this.Photo.Text = info.Photo;
                    this.Description.Text = info.Description;
                    this.Css.Text = info.Css;
                    if (info.ProductGroup != string.Empty)
                    {
                        string str = string.Empty;
                        int length = info.ProductGroup.Split(new char[] { '#' }).Length;
                        this.photoArray = new string[length];
                        this.linkArray = new string[length];
                        this.idArray = new string[length];
                        for (int i = 0; i < length; i++)
                        {
                            string[] strArray = info.ProductGroup.Split(new char[] { '#' })[i].Split(new char[] { '|' });
                            this.photoArray[i] = strArray[0];
                            this.linkArray[i] = strArray[1];
                            this.idArray[i] = strArray[2];
                            if (strArray[2] != string.Empty) str = str + strArray[2] + ",";
                        }
                        if (str != string.Empty) str = str.Substring(0, str.Length - 1);
                        ProductSearchInfo productSearch = new ProductSearchInfo();
                        productSearch.InProductID = str;
                        this.productList = ProductBLL.SearchProductList(productSearch);
                    }
                    this.TopImage.Text = info.Style.Split(new char[] { '|' })[0];
                    this.BackgroundImage.Text = info.Style.Split(new char[] { '|' })[1];
                    this.BottomImage.Text = info.Style.Split(new char[] { '|' })[2];
                    this.ProductColor.Text = info.Style.Split(new char[] { '|' })[3];
                    this.ProductColor.Attributes.Add("style", "color:" + info.Style.Split(new char[] { '|' })[3] + ";");
                    this.ProductSize.Text = info.Style.Split(new char[] { '|' })[4];
                    this.PriceColor.Text = info.Style.Split(new char[] { '|' })[5];
                    this.PriceColor.Attributes.Add("style", "color:" + info.Style.Split(new char[] { '|' })[5] + ";");
                    this.PriceSize.Text = info.Style.Split(new char[] { '|' })[6];
                    this.OtherColor.Text = info.Style.Split(new char[] { '|' })[7];
                    this.OtherColor.Attributes.Add("style", "color:" + info.Style.Split(new char[] { '|' })[7] + ";");
                    this.OtherSize.Text = info.Style.Split(new char[] { '|' })[8];
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
            ThemeActivityInfo themeActivity = new ThemeActivityInfo();
            themeActivity.ID = RequestHelper.GetQueryString<int>("ID");
            themeActivity.Name = this.Name.Text;
            themeActivity.Photo = this.Photo.Text;
            themeActivity.Description = this.Description.Text;
            themeActivity.Css = this.Css.Text;
            int form = RequestHelper.GetForm<int>("ProductGroupCount");
            string str = string.Empty;
            for (int i = 0; i < form; i++)
            {
                if (RequestHelper.GetForm<string>("ProductGroupValue" + i) != string.Empty) str = str + RequestHelper.GetForm<string>("ProductGroupValue" + i) + "#";
            }
            if (str.EndsWith("#")) str = str.Substring(0, str.Length - 1);
            themeActivity.ProductGroup = str;
            themeActivity.Style = string.Concat(new object[] { 
                this.TopImage.Text, '|', this.BackgroundImage.Text, '|', this.BottomImage.Text, '|', this.ProductColor.Text, '|', this.ProductSize.Text, '|', this.PriceColor.Text, '|', this.PriceSize.Text, '|', this.OtherColor.Text, '|', 
                this.OtherSize.Text
             });
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (themeActivity.ID == -2147483648)
            {
                base.CheckAdminPower("AddThemeActivity", PowerCheckType.Single);
                int id = ThemeActivityBLL.AddThemeActivity(themeActivity);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("ThemeActivity"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateThemeActivity", PowerCheckType.Single);
                ThemeActivityBLL.UpdateThemeActivity(themeActivity);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("ThemeActivity"), themeActivity.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

