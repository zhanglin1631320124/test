namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ShopConfigAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadConfig", PowerCheckType.Single);
                this.txtTitle.Text = ShopConfig.ReadConfigInfo().Title;
                this.Copyright.Text = ShopConfig.ReadConfigInfo().Copyright;
                this.Author.Text = ShopConfig.ReadConfigInfo().Author;
                this.Keywords.Text = ShopConfig.ReadConfigInfo().Keywords;
                this.Description.Text = ShopConfig.ReadConfigInfo().Description;
                this.CodeType.Text = ShopConfig.ReadConfigInfo().CodeType.ToString();
                this.CodeLength.Text = ShopConfig.ReadConfigInfo().CodeLength.ToString();
                this.CodeDot.Text = ShopConfig.ReadConfigInfo().CodeDot.ToString();
                this.StartYear.Text = ShopConfig.ReadConfigInfo().StartYear.ToString();
                this.EndYear.Text = ShopConfig.ReadConfigInfo().EndYear.ToString();
                this.Tel.Text = ShopConfig.ReadConfigInfo().Tel;
                this.RecordCode.Text = ShopConfig.ReadConfigInfo().RecordCode;
                this.StaticCode.Text = ShopConfig.ReadConfigInfo().StaticCode;
                this.VoteID.Text = ShopConfig.ReadConfigInfo().VoteID.ToString();
                this.AllowAnonymousVote.Text = ShopConfig.ReadConfigInfo().AllowAnonymousVote.ToString();
                this.VoteRestrictTime.Text = ShopConfig.ReadConfigInfo().VoteRestrictTime.ToString();
                this.SaveAutoClosePop.Text = ShopConfig.ReadConfigInfo().SaveAutoClosePop.ToString();
                this.PopCloseRefresh.Text = ShopConfig.ReadConfigInfo().PopCloseRefresh.ToString();
                this.UploadFile.Text = ShopConfig.ReadConfigInfo().UploadFile;
                this.HotKeyword.Text = ShopConfig.ReadConfigInfo().HotKeyword;
                this.ProductStorageType.Text = ShopConfig.ReadConfigInfo().ProductStorageType.ToString();
                this.AllowAnonymousComment.Text = ShopConfig.ReadConfigInfo().AllowAnonymousComment.ToString();
                this.CommentDefaultStatus.Text = ShopConfig.ReadConfigInfo().CommentDefaultStatus.ToString();
                this.CommentRestrictTime.Text = ShopConfig.ReadConfigInfo().CommentRestrictTime.ToString();
                this.AllowAnonymousCommentOperate.Text = ShopConfig.ReadConfigInfo().AllowAnonymousCommentOperate.ToString();
                this.CommentOperateRestrictTime.Text = ShopConfig.ReadConfigInfo().CommentOperateRestrictTime.ToString();
                this.AllowAnonymousReply.Text = ShopConfig.ReadConfigInfo().AllowAnonymousReply.ToString();
                this.ReplyRestrictTime.Text = ShopConfig.ReadConfigInfo().ReplyRestrictTime.ToString();
                this.AllowAnonymousAddTags.Text = ShopConfig.ReadConfigInfo().AllowAnonymousAddTags.ToString();
                this.AddTagsRestrictTime.Text = ShopConfig.ReadConfigInfo().AddTagsRestrictTime.ToString();
                this.EmailUserName.Text = ShopConfig.ReadConfigInfo().EmailUserName;
                this.EmailPassword.Text = ShopConfig.ReadConfigInfo().EmailPassword;
                this.EmailServer.Text = ShopConfig.ReadConfigInfo().EmailServer;
                this.EmailServerPort.Text = ShopConfig.ReadConfigInfo().EmailServerPort.ToString();
                this.ForbiddenName.Text = ShopConfig.ReadConfigInfo().ForbiddenName;
                this.PasswordMinLength.Text = ShopConfig.ReadConfigInfo().PasswordMinLength.ToString();
                this.PasswordMaxLength.Text = ShopConfig.ReadConfigInfo().PasswordMaxLength.ToString();
                this.UserNameMinLength.Text = ShopConfig.ReadConfigInfo().UserNameMinLength.ToString();
                this.UserNameMaxLength.Text = ShopConfig.ReadConfigInfo().UserNameMaxLength.ToString();
                this.RegisterCheck.Text = ShopConfig.ReadConfigInfo().RegisterCheck.ToString();
                this.Agreement.Text = ShopConfig.ReadConfigInfo().Agreement;
                this.FindPasswordTimeRestrict.Text = ShopConfig.ReadConfigInfo().FindPasswordTimeRestrict.ToString();
                this.AllowAnonymousAddCart.Text = ShopConfig.ReadConfigInfo().AllowAnonymousAddCart.ToString();
                this.PayOrder.Text = ShopConfig.ReadConfigInfo().PayOrder.ToString();
                this.CancleOrder.Text = ShopConfig.ReadConfigInfo().CancleOrder.ToString();
                this.CheckOrder.Text = ShopConfig.ReadConfigInfo().CheckOrder.ToString();
                this.SendOrder.Text = ShopConfig.ReadConfigInfo().SendOrder.ToString();
                this.ReceivedOrder.Text = ShopConfig.ReadConfigInfo().ReceivedOrder.ToString();
                this.ChangeOrder.Text = ShopConfig.ReadConfigInfo().ChangeOrder.ToString();
                this.ReturnOrder.Text = ShopConfig.ReadConfigInfo().ReturnOrder.ToString();
                this.BackOrder.Text = ShopConfig.ReadConfigInfo().BackOrder.ToString();
                this.RefundOrder.Text = ShopConfig.ReadConfigInfo().RefundOrder.ToString();
                this.WaterType.Text = ShopConfig.ReadConfigInfo().WaterType.ToString();
                this.WaterPossition.Text = ShopConfig.ReadConfigInfo().WaterPossition.ToString();
                this.Text.Text = ShopConfig.ReadConfigInfo().Text;
                this.TextFont.Text = ShopConfig.ReadConfigInfo().TextFont;
                this.TextSize.Text = ShopConfig.ReadConfigInfo().TextSize.ToString();
                this.TextColor.Text = ShopConfig.ReadConfigInfo().TextColor;
                this.WaterPhoto.Text = ShopConfig.ReadConfigInfo().WaterPhoto;
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("UpdateConfig", PowerCheckType.Single);
            ShopConfigInfo config = ShopConfig.ReadConfigInfo();
            config.Title = this.txtTitle.Text;
            config.Copyright = this.Copyright.Text;
            config.Author = this.Author.Text;
            config.Keywords = this.Keywords.Text;
            config.Description = this.Description.Text;
            config.CodeType = Convert.ToInt32(this.CodeType.Text);
            config.CodeLength = Convert.ToInt32(this.CodeLength.Text);
            config.CodeDot = Convert.ToInt32(this.CodeDot.Text);
            config.StartYear = Convert.ToInt32(this.StartYear.Text);
            config.EndYear = Convert.ToInt32(this.EndYear.Text);
            config.Tel = this.Tel.Text;
            config.RecordCode = this.RecordCode.Text;
            config.StaticCode = this.StaticCode.Text;
            config.VoteID = Convert.ToInt32(this.VoteID.Text);
            config.AllowAnonymousVote = Convert.ToInt32(this.AllowAnonymousVote.Text);
            config.VoteRestrictTime = Convert.ToInt32(this.VoteRestrictTime.Text);
            config.SaveAutoClosePop = Convert.ToInt32(this.SaveAutoClosePop.Text);
            config.PopCloseRefresh = Convert.ToInt32(this.PopCloseRefresh.Text);
            config.UploadFile = this.UploadFile.Text;
            config.HotKeyword = this.HotKeyword.Text;
            config.ProductStorageType = Convert.ToInt32(this.ProductStorageType.Text);
            config.AllowAnonymousComment = Convert.ToInt32(this.AllowAnonymousComment.Text);
            config.CommentDefaultStatus = Convert.ToInt32(this.CommentDefaultStatus.Text);
            config.CommentRestrictTime = Convert.ToInt32(this.CommentRestrictTime.Text);
            config.AllowAnonymousCommentOperate = Convert.ToInt32(this.AllowAnonymousCommentOperate.Text);
            config.CommentOperateRestrictTime = Convert.ToInt32(this.CommentOperateRestrictTime.Text);
            config.AllowAnonymousReply = Convert.ToInt32(this.AllowAnonymousReply.Text);
            config.ReplyRestrictTime = Convert.ToInt32(this.ReplyRestrictTime.Text);
            config.AllowAnonymousAddTags = Convert.ToInt32(this.AllowAnonymousAddTags.Text);
            config.AddTagsRestrictTime = Convert.ToInt32(this.AddTagsRestrictTime.Text);
            config.EmailUserName = this.EmailUserName.Text;
            config.EmailPassword = this.EmailPassword.Text;
            config.EmailServer = this.EmailServer.Text;
            config.EmailServerPort = Convert.ToInt32(this.EmailServerPort.Text);
            config.ForbiddenName = this.ForbiddenName.Text;
            config.PasswordMinLength = Convert.ToInt32(this.PasswordMinLength.Text);
            config.PasswordMaxLength = Convert.ToInt32(this.PasswordMaxLength.Text);
            config.UserNameMinLength = Convert.ToInt32(this.UserNameMinLength.Text);
            config.UserNameMaxLength = Convert.ToInt32(this.UserNameMaxLength.Text);
            config.RegisterCheck = Convert.ToInt32(this.RegisterCheck.Text);
            config.Agreement = this.Agreement.Text;
            config.FindPasswordTimeRestrict = Convert.ToDouble(this.FindPasswordTimeRestrict.Text);
            config.AllowAnonymousAddCart = Convert.ToInt32(this.AllowAnonymousAddCart.Text);
            config.PayOrder = Convert.ToInt32(this.PayOrder.Text);
            config.CancleOrder = Convert.ToInt32(this.CancleOrder.Text);
            config.CheckOrder = Convert.ToInt32(this.CheckOrder.Text);
            config.SendOrder = Convert.ToInt32(this.SendOrder.Text);
            config.ReceivedOrder = Convert.ToInt32(this.ReceivedOrder.Text);
            config.ChangeOrder = Convert.ToInt32(this.ChangeOrder.Text);
            config.ReturnOrder = Convert.ToInt32(this.ReturnOrder.Text);
            config.BackOrder = Convert.ToInt32(this.BackOrder.Text);
            config.RefundOrder = Convert.ToInt32(this.RefundOrder.Text);
            config.WaterType = Convert.ToInt32(this.WaterType.Text);
            config.WaterPossition = Convert.ToInt32(this.WaterPossition.Text);
            config.Text = this.Text.Text;
            config.TextFont = this.TextFont.Text;
            config.TextSize = Convert.ToInt32(this.TextSize.Text);
            config.TextColor = this.TextColor.Text;
            config.WaterPhoto = this.WaterPhoto.Text;
            ShopConfig.UpdateConfigInfo(config);
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateConfig"));
            ScriptHelper.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
        }
    }
}

