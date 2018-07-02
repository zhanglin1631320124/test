namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class VoteResult : AjaxBasePage
    {
        protected string action = string.Empty;
        protected VoteInfo vote = new VoteInfo();
        protected List<VoteItemInfo> voteItemList = new List<VoteItemInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int voteID = ShopConfig.ReadConfigInfo().VoteID;
            this.action = RequestHelper.GetQueryString<string>("Action");
            string action = this.action;
            if (action != null)
            {
                if (!(action == "Vote"))
                {
                    if (action == "View" || action == "Prepare")
                    {
                    }
                }
                else
                    this.Vote(voteID);
            }
            this.vote = VoteBLL.ReadVote(voteID);
            this.voteItemList = VoteItemBLL.ReadVoteItemByVote(voteID);
        }

        protected void Vote(int voteID)
        {
            string content = "ok";
            if (ShopConfig.ReadConfigInfo().AllowAnonymousVote == 0 && base.UserID == 0)
                content = "还未登录";
            else
            {
                string str2 = CookiesHelper.ReadCookieValue("VoteCookies" + voteID.ToString());
                if (ShopConfig.ReadConfigInfo().VoteRestrictTime > 0 && str2 != string.Empty)
                    content = "请不要频繁提交";
                else
                {
                    VoteRecordInfo voteRecord = new VoteRecordInfo();
                    voteRecord.VoteID = voteID;
                    voteRecord.ItemID = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("ItemID"));
                    voteRecord.AddDate = RequestHelper.DateNow;
                    voteRecord.UserIP = ClientHelper.IP;
                    voteRecord.UserID = base.UserID;
                    voteRecord.UserName = base.UserName;
                    VoteRecordBLL.AddVoteRecord(voteRecord);
                    if (ShopConfig.ReadConfigInfo().VoteRestrictTime > 0) CookiesHelper.AddCookie("VoteCookies" + voteID.ToString(), "VoteCookies" + voteID.ToString(), ShopConfig.ReadConfigInfo().VoteRestrictTime, TimeType.Second);
                }
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }
    }
}

