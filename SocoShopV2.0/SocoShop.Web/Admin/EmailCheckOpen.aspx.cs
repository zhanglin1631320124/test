namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Page;
    using System;

    public partial class EmailCheckOpen : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string queryString = RequestHelper.GetQueryString<string>("Email");
            int id = RequestHelper.GetQueryString<int>("ID");
            EmailSendRecordBLL.RecordOpenedEmailRecord(queryString, id);
        }
    }
}

