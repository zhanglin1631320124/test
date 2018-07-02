namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ProductClass : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAdminPower("ReadProductClass", PowerCheckType.Single);
            string queryString = RequestHelper.GetQueryString<string>("Action");
            int id = RequestHelper.GetQueryString<int>("ID");
            if (queryString != string.Empty && id != -2147483648)
            {
                string str2 = queryString;
                if (str2 != null)
                {
                    if (!(str2 == "Up"))
                    {
                        if (str2 == "Down")
                        {
                            base.CheckAdminPower("UpdateProductClass", PowerCheckType.Single);
                            ProductClassBLL.MoveDownProductClass(id);
                            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("ProductClass"), id);
                        }
                        else if (str2 == "Delete")
                        {
                            base.CheckAdminPower("DeleteProductClass", PowerCheckType.Single);
                            ProductClassBLL.DeleteProductClass(id);
                            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("ProductClass"), id);
                        }
                    }
                    else
                    {
                        base.CheckAdminPower("UpdateProductClass", PowerCheckType.Single);
                        ProductClassBLL.MoveUpProductClass(id);
                        AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("ProductClass"), id);
                    }
                }
            }
            base.BindControl(ProductClassBLL.ReadProductClassNamedList(), this.RecordList);
        }
    }
}

