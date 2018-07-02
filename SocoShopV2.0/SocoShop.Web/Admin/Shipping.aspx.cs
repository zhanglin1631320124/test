namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Shipping : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteShipping", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ShippingBLL.DeleteShipping(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Shipping"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadShipping", PowerCheckType.Single);
                string queryString = RequestHelper.GetQueryString<string>("Action");
                int id = RequestHelper.GetQueryString<int>("ID");
                if (id != 0 && queryString != string.Empty)
                {
                    base.CheckAdminPower("UpdateShipping", PowerCheckType.Single);
                    ChangeAction up = ChangeAction.Up;
                    if (queryString == "Down") up = ChangeAction.Down;
                    ShippingBLL.ChangeShippingOrder(up, id);
                    AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("Shipping"), id);
                }
                base.BindControl(ShippingBLL.ReadShippingCacheList(), this.RecordList);
            }
        }
    }
}

