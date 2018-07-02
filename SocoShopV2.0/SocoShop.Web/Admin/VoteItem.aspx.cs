namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class VoteItem : AdminBasePage
    {
        protected int voteID = 0;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteVoteItem", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                VoteItemBLL.DeleteVoteItem(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("VoteItem"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadVoteItem", PowerCheckType.Single);
                string queryString = RequestHelper.GetQueryString<string>("Action");
                this.voteID = RequestHelper.GetQueryString<int>("VoteID");
                int id = RequestHelper.GetQueryString<int>("ItemID");
                if (queryString != string.Empty && id != -2147483648)
                {
                    string str2 = queryString;
                    if (str2 != null)
                    {
                        if (!(str2 == "Up"))
                        {
                            if (str2 == "Down") VoteItemBLL.ChangeVoteItemOrder(ChangeAction.Down, id);
                        }
                        else
                            VoteItemBLL.ChangeVoteItemOrder(ChangeAction.Up, id);
                    }
                }
                int num2 = RequestHelper.GetQueryString<int>("ID");
                if (num2 != -2147483648)
                {
                    VoteItemInfo info = VoteItemBLL.ReadVoteItem(num2);
                    this.ItemName.Text = info.ItemName;
                }
                base.BindControl(VoteItemBLL.ReadVoteItemByVote(this.voteID), this.RecordList);
            }
        }

        protected string ShowCheckBox(int id, int voteCount)
        {
            string str = string.Empty;
            if (voteCount == 0) str = "<input type=\"checkbox\" name=\"SelectID\" value=" + id.ToString() + " />";
            return str;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            VoteItemInfo voteItem = new VoteItemInfo();
            voteItem.ID = RequestHelper.GetQueryString<int>("ID");
            voteItem.VoteID = RequestHelper.GetQueryString<int>("VoteID");
            voteItem.ItemName = this.ItemName.Text;
            string message = ShopLanguage.ReadLanguage("AddOK");
            if (voteItem.ID == -2147483648)
            {
                base.CheckAdminPower("AddVoteItem", PowerCheckType.Single);
                int id = VoteItemBLL.AddVoteItem(voteItem);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("VoteItem"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateVoteItem", PowerCheckType.Single);
                VoteItemBLL.UpdateVoteItem(voteItem);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("VoteItem"), voteItem.ID);
                message = ShopLanguage.ReadLanguage("UpdateOK");
            }
            ScriptHelper.Alert(message, RequestHelper.RawUrl);
        }
    }
}

