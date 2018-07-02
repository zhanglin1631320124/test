namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Menu : AdminBasePage
    {
        protected int fatherID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAdminPower("ReadMenu", PowerCheckType.Single);
            string queryString = RequestHelper.GetQueryString<string>("Action");
            int id = RequestHelper.GetQueryString<int>("ID");
            this.fatherID = RequestHelper.GetQueryString<int>("FatherID");
            if (this.fatherID == -2147483648) this.fatherID = 1;
            if (queryString != string.Empty && id != -2147483648)
            {
                string str2 = queryString;
                if (str2 != null)
                {
                    if (!(str2 == "Up"))
                    {
                        if (str2 == "Down")
                        {
                            base.CheckAdminPower("UpdateMenu", PowerCheckType.Single);
                            MenuBLL.MoveDownMenu(id);
                            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("Menu"), id);
                        }
                        else if (str2 == "Delete")
                        {
                            base.CheckAdminPower("DeleteMenu", PowerCheckType.Single);
                            MenuBLL.DeleteMenu(id);
                            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Menu"), id);
                        }
                    }
                    else
                    {
                        base.CheckAdminPower("UpdateMenu", PowerCheckType.Single);
                        MenuBLL.MoveUpMenu(id);
                        AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("Menu"), id);
                    }
                }
            }
            base.BindControl(MenuBLL.ReadMenuAllNamedChildList(this.fatherID), this.RecordList);
        }
    }
}

