namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class EmailSendRecord : AdminBasePage
    {
        protected int isSystem = 0;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteEmailSendRecord", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                EmailSendRecordBLL.DeleteEmailSendRecord(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("EmailSendRecord"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadEmailSendRecord", PowerCheckType.Single);
                this.isSystem = RequestHelper.GetQueryString<int>("IsSystem");
                this.isSystem = (this.isSystem == -2147483648) ? 0 : this.isSystem;
                EmailSendRecordSearchInfo emailSendRecord = new EmailSendRecordSearchInfo();
                emailSendRecord.IsSystem = this.isSystem;
                base.BindControl(EmailSendRecordBLL.SearchEmailSendRecordList(base.CurrentPage, base.PageSize, emailSendRecord, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected string ShowSendButton(int sendStatus, int id)
        {
            string str = string.Empty;
            if (sendStatus == 1) str = "<a href=\"javascript:sendEmail(" + id.ToString() + ")\"><img src=\"Style/Images/send.gif\" alt=\"发送\" title=\"发送\" /></a>";
            return str;
        }

        protected string ShowSendDate(int sendStatus, string sendDate)
        {
            if (sendStatus != 3) sendDate = "--";
            return sendDate;
        }
    }
}

