namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class EmailContent : AdminBasePage
    {
        protected int isSystem = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadEmailContent", PowerCheckType.Single);
                this.isSystem = RequestHelper.GetQueryString<int>("IsSystem");
                if (this.isSystem == -2147483648 || this.isSystem == 0)
                {
                    this.isSystem = 0;
                    base.BindControl(EmailContentHelper.ReadCommonEmailContentList(), this.RecordList);
                }
                else
                    base.BindControl(EmailContentHelper.ReadSystemEmailContentList(), this.RecordList);
            }
        }
    }
}

