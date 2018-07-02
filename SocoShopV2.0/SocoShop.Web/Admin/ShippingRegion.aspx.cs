namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ShippingRegion : AdminBasePage
    {
        protected ShippingInfo shipping = new ShippingInfo();
        protected int shippingID = 0;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteShippingRegion", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ShippingRegionBLL.DeleteShippingRegion(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("ShippingRegion"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadShippingRegion", PowerCheckType.Single);
                this.RegionID.DataSource = RegionBLL.ReadRegionUnlimitClass();
                this.shippingID = RequestHelper.GetQueryString<int>("ShippingID");
                this.shipping = ShippingBLL.ReadShippingCache(this.shippingID);
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    ShippingRegionInfo info = new ShippingRegionInfo();
                    info = ShippingRegionBLL.ReadShippingRegion(queryString);
                    this.Name.Text = info.Name;
                    this.RegionID.ClassIDList = info.RegionID;
                    this.FixedMoeny.Text = info.FixedMoeny.ToString();
                    this.FirstMoney.Text = info.FirstMoney.ToString();
                    this.AgainMoney.Text = info.AgainMoney.ToString();
                    this.OneMoeny.Text = info.OneMoeny.ToString();
                    this.AnotherMoeny.Text = info.AnotherMoeny.ToString();
                }
                base.BindControl(ShippingRegionBLL.ReadShippingRegionByShipping(this.shippingID.ToString()), this.RecordList);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ShippingRegionInfo shippingRegion = new ShippingRegionInfo();
            shippingRegion.ID = RequestHelper.GetQueryString<int>("ID");
            shippingRegion.Name = this.Name.Text;
            shippingRegion.ShippingID = RequestHelper.GetQueryString<int>("ShippingID");
            shippingRegion.RegionID = this.RegionID.ClassIDList;
            try
            {
                shippingRegion.FixedMoeny = Convert.ToDecimal(this.FixedMoeny.Text);
                shippingRegion.FirstMoney = Convert.ToDecimal(this.FirstMoney.Text);
                shippingRegion.AgainMoney = Convert.ToDecimal(this.AgainMoney.Text);
                shippingRegion.OneMoeny = Convert.ToDecimal(this.OneMoeny.Text);
                shippingRegion.AnotherMoeny = Convert.ToDecimal(this.AnotherMoeny.Text);
            }
            catch
            {
            }
            string message = ShopLanguage.ReadLanguage("AddOK");
            if (shippingRegion.ID == -2147483648)
            {
                base.CheckAdminPower("AddShippingRegion", PowerCheckType.Single);
                int id = ShippingRegionBLL.AddShippingRegion(shippingRegion);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("ShippingRegion"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateShippingRegion", PowerCheckType.Single);
                ShippingRegionBLL.UpdateShippingRegion(shippingRegion);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("ShippingRegion"), shippingRegion.ID);
                message = ShopLanguage.ReadLanguage("UpdateOK");
            }
            ScriptHelper.Alert(message, RequestHelper.RawUrl);
        }
    }
}

