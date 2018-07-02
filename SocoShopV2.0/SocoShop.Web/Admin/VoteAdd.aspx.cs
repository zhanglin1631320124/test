namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class VoteAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadVote", PowerCheckType.Single);
                    VoteInfo info = VoteBLL.ReadVote(queryString);
                    this.txtTitle.Text = info.Title;
                    this.VoteType.Text = info.VoteType.ToString();
                    this.Note.Text = info.Note;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            VoteInfo vote = new VoteInfo();
            vote.ID = RequestHelper.GetQueryString<int>("ID");
            vote.VoteType = Convert.ToInt32(this.VoteType.Text);
            vote.Title = this.txtTitle.Text;
            vote.Note = this.Note.Text;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (vote.ID == -2147483648)
            {
                base.CheckAdminPower("AddVote", PowerCheckType.Single);
                int id = VoteBLL.AddVote(vote);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Vote"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateVote", PowerCheckType.Single);
                VoteBLL.UpdateVote(vote);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Vote"), vote.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

