namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class VoteRecord : AdminBasePage
    {
        protected List<VoteItemInfo> voteItemList = new List<VoteItemInfo>();

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteVoteRecord", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                VoteRecordBLL.DeleteVoteRecord(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("VoteRecord"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadVoteRecord", PowerCheckType.Single);
                int queryString = RequestHelper.GetQueryString<int>("VoteID");
                this.voteItemList = VoteItemBLL.ReadVoteItemByVote(queryString);
                base.BindControl(VoteRecordBLL.ReadVoteRecordList(queryString, base.CurrentPage, base.PageSize, ref this.Count), this.RecordList, this.MyPager);
            }
        }
    }
}

