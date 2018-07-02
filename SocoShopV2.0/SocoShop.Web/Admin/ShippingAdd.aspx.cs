namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ShippingAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadShipping", PowerCheckType.Single);
                    ShippingInfo info = ShippingBLL.ReadShippingCache(queryString);
                    this.Name.Text = info.Name;
                    this.Description.Text = info.Description;
                    this.IsEnabled.Text = info.IsEnabled.ToString();
                    this.ShippingType.Text = info.ShippingType.ToString();
                    if (info.ShippingType == 2)
                    {
                        this.FirstWeight.Text = info.FirstWeight.ToString();
                        this.AgainWeight.Text = info.AgainWeight.ToString();
                    }
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ShippingInfo shipping = new ShippingInfo();
            shipping.ID = RequestHelper.GetQueryString<int>("ID");
            shipping.Name = this.Name.Text;
            shipping.Description = this.Description.Text;
            shipping.IsEnabled = Convert.ToInt32(this.IsEnabled.Text);
            shipping.ShippingType = Convert.ToInt32(this.ShippingType.Text);
            shipping.FirstWeight = Convert.ToInt32(this.FirstWeight.Text);
            shipping.AgainWeight = Convert.ToInt32(this.AgainWeight.Text);
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (shipping.ID == -2147483648)
            {
                base.CheckAdminPower("AddShipping", PowerCheckType.Single);
                int id = ShippingBLL.AddShipping(shipping);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Shipping"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateShipping", PowerCheckType.Single);
                ShippingBLL.UpdateShipping(shipping);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Shipping"), shipping.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

