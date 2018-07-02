namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;

    public partial class EmailSendRecordDetail : AdminBasePage
    {
        protected EmailSendRecordInfo emailSendRecord = new EmailSendRecordInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadEmailSendRecord", PowerCheckType.Single);
                    this.emailSendRecord = EmailSendRecordBLL.ReadEmailSendRecord(queryString);
                }
            }
        }
    }
}

