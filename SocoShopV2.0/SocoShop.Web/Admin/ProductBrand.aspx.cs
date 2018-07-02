namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ProductBrand : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteProductBrand", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ProductBrandBLL.DeleteProductBrand(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("ProductBrand"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadProductBrand", PowerCheckType.Single);
                string queryString = RequestHelper.GetQueryString<string>("Action");
                int id = RequestHelper.GetQueryString<int>("ID");
                if (id != 0 && queryString != string.Empty)
                {
                    base.CheckAdminPower("UpdateProductBrand", PowerCheckType.Single);
                    ChangeAction up = ChangeAction.Up;
                    if (queryString == "down") up = ChangeAction.Down;
                    ProductBrandBLL.ChangeProductBrandOrder(up, id);
                    AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("ProductBrand"), id);
                }
                base.BindControl(ProductBrandBLL.ReadProductBrandCacheList(), this.RecordList);
            }
        }
    }
}

