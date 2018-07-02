namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;

    public class Ad : CommonBasePage
    {
        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("URL");
            AdRecordInfo adRecord = new AdRecordInfo();
            adRecord.AdID = RequestHelper.GetQueryString<int>("AdID");
            adRecord.IP = ClientHelper.IP;
            adRecord.Date = RequestHelper.DateNow;
            adRecord.Page = base.Request.ServerVariables["HTTP_REFERER"];
            adRecord.UserID = base.UserID;
            adRecord.UserName = base.UserName;
            AdRecordBLL.AddAdRecord(adRecord);
            ResponseHelper.Redirect(queryString);
        }
    }
}

