namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Link : AdminBasePage
    {
        protected int classID = 0;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteLink", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                LinkBLL.DeleteLink(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Link"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadLink", PowerCheckType.Single);
                string queryString = RequestHelper.GetQueryString<string>("Action");
                int id = RequestHelper.GetQueryString<int>("ID");
                if (queryString != string.Empty && id != -2147483648)
                {
                    base.CheckAdminPower("UpdateLink", PowerCheckType.Single);
                    string str2 = queryString;
                    if (str2 != null)
                    {
                        if (!(str2 == "Up"))
                        {
                            if (str2 == "Down") LinkBLL.ChangeLinkOrder(ChangeAction.Down, id);
                        }
                        else
                            LinkBLL.ChangeLinkOrder(ChangeAction.Up, id);
                    }
                    AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("Link"), id);
                }
                this.classID = RequestHelper.GetQueryString<int>("ClassID");
                if (this.classID == -2147483648) this.classID = 1;
                base.BindControl(LinkBLL.ReadLinkCacheListByClass(this.classID), this.RecordList);
            }
        }
    }
}

