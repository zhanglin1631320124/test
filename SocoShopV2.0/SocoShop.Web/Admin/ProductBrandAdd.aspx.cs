namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ProductBrandAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadProductBrand", PowerCheckType.Single);
                    ProductBrandInfo info = ProductBrandBLL.ReadProductBrandCache(queryString);
                    this.Name.Text = info.Name;
                    this.Logo.Text = info.Logo;
                    this.Url.Text = info.Url;
                    this.Description.Text = info.Description;
                    this.IsTop.Text = info.IsTop.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ProductBrandInfo productBrand = new ProductBrandInfo();
            productBrand.ID = RequestHelper.GetQueryString<int>("ID");
            productBrand.Name = this.Name.Text;
            productBrand.Logo = this.Logo.Text;
            productBrand.Url = this.Url.Text;
            productBrand.Description = this.Description.Text;
            productBrand.IsTop = Convert.ToInt32(this.IsTop.Text);
            productBrand.ProductCount = 0;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (productBrand.ID == -2147483648)
            {
                base.CheckAdminPower("AddProductBrand", PowerCheckType.Single);
                int id = ProductBrandBLL.AddProductBrand(productBrand);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("ProductBrand"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateProductBrand", PowerCheckType.Single);
                ProductBrandBLL.UpdateProductBrand(productBrand);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("ProductBrand"), productBrand.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

