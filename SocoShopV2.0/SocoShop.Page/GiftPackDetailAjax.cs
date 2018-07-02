namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using System;

    public class GiftPackDetailAjax : AjaxBasePage
    {
        protected string giftPackID = string.Empty;
        protected string[] selectProuct = new string[0];

        protected override void PageLoad()
        {
            base.PageLoad();
            this.giftPackID = RequestHelper.GetQueryString<string>("GiftPackID");
            string content = CookiesHelper.ReadCookieValue("GiftPack" + this.giftPackID);
            if (content != string.Empty) this.selectProuct = StringHelper.Decode(content, ShopConfig.ReadConfigInfo().SecureKey).Split(new char[] { '|' });
        }
    }
}

