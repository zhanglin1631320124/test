using System;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SocoShop.Common;
using SocoShop.Business;
using SocoShop.Entity;
using SkyCES.EntLib;
using SocoShop.Page;

namespace SocoShop.Web
{
    public partial class GroupBuyDetail : PluginsBasePage
    {
        protected GroupBuyInfo groupBuy = new GroupBuyInfo();
        protected List<UserGroupBuyInfo> userGroupBuyList= new List<UserGroupBuyInfo>();
        protected ProductInfo product = new ProductInfo();
        protected long leftTime = 0;
        protected int buyCount = 0;
        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestHelper.GetQueryString<int>("ID");
            groupBuy = GroupBuyBLL.ReadGroupBuy(id);
            TimeSpan timeSpan = groupBuy.EndDate - RequestHelper.DateNow;
            leftTime = timeSpan.Days * 24 * 3600 + timeSpan.Hours * 3600 + timeSpan.Minutes * 60 + timeSpan.Seconds;
            userGroupBuyList = UserGroupBuyBLL.ReadUserGroupBuyList(id);
            foreach (UserGroupBuyInfo userGroupBuy in userGroupBuyList)
            {
                buyCount += userGroupBuy.BuyCount;
            }
            product = ProductBLL.ReadProduct(groupBuy.ProductID);
            Head.Title = product.Name + " - 商品团购";
        }
    }
}
