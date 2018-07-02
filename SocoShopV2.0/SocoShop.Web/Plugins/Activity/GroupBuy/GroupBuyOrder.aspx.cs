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
    public partial class GroupBuyOrder : PluginsBasePage
    {
        protected ProductInfo product = new ProductInfo();
        protected GroupBuyInfo groupBuy = new GroupBuyInfo();
        protected int buyCount = 0;
        protected int userID = 0;
        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = RequestHelper.GetQueryString<int>("ID");
                userID = Cookies.User.GetUserID(true);
                decimal moneyLeft = UserBLL.ReadUserMore(userID).MoneyLeft;
                if (userID == 0)
                {
                    ScriptHelper.Alert("请先登录", "/User/Login.aspx?RedirectUrl=/Plugins/Activity/GroupBuy/GroupBuyOrder?ID=" + id.ToString());
                }            
                groupBuy = GroupBuyBLL.ReadGroupBuy(id);
                if (moneyLeft < groupBuy.Price)
                {
                    ScriptHelper.Alert("您账户余额不够该团购活动商品价格，请先充值", "/User/UserRecharge.aspx");
                }
                if (UserGroupBuyBLL.ReadUserGroupBuyByUser(id,userID).ID > 0)
                {
                    ScriptHelper.Alert("您已经参加该活动了，请不要重复参加");
                }
                if (groupBuy.StartDate > DateTime.Now)
                {
                    ScriptHelper.Alert("该团购活动未开始，不能购买");
                }
                if (groupBuy.EndDate < DateTime.Now)
                {
                    ScriptHelper.Alert("该团购活动已经结束，不能购买");
                }
                buyCount = UserGroupBuyBLL.ReadUserGroupBuyCount(id);
                product = ProductBLL.ReadProduct(groupBuy.ProductID);
                RegionID.DataSource = RegionBLL.ReadRegionUnlimitClass();
                UserInfo user = UserBLL.ReadUser(userID);
                Consignee.Text = user.UserName;
                Tel.Text = user.Tel;
                Mobile.Text = user.Mobile;
                Address.Text = user.Address;
                RegionID.ClassID = user.RegionID;
                Head.Title = "确认商品团购";
            }
        }

        /// <summary>
        /// 提交按钮点击方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            //读取用户信息
            userID = Cookies.User.GetUserID(true);
            string userName = Cookies.User.GetUserName(false);
            string userEmail = CookiesHelper.ReadCookieValue("UserEmail");
            //添加团购记录
            int id = RequestHelper.GetQueryString<int>("ID");
            groupBuy = GroupBuyBLL.ReadGroupBuy(id);
            UserGroupBuyInfo userGroupBuy = new UserGroupBuyInfo();
            userGroupBuy.GroupBuyID = id;
            userGroupBuy.Date = RequestHelper.DateNow;
            userGroupBuy.IP = ClientHelper.IP;
            userGroupBuy.BuyCount = RequestHelper.GetForm<int>("BuyCount");
            userGroupBuy.OrderID = 0;
            userGroupBuy.UserID = userID;
            userGroupBuy.UserName = userName;
            userGroupBuy.Consignee =  StringHelper.AddSafe(RequestHelper.GetForm<string>("Consignee"));
            userGroupBuy.RegionID = RegionID.ClassID;
            userGroupBuy.Address =  StringHelper.AddSafe(RequestHelper.GetForm<string>("Address"));
            userGroupBuy.ZipCode =  StringHelper.AddSafe(RequestHelper.GetForm<string>("ZipCode"));
            userGroupBuy.Tel = StringHelper.AddSafe( RequestHelper.GetForm<string>("Tel"));
            userGroupBuy.Email = userEmail;
            userGroupBuy.Mobile =  StringHelper.AddSafe(RequestHelper.GetForm<string>("Mobile"));
            UserGroupBuyBLL.AddUserGroupBuy(userGroupBuy);
            UserAccountRecordBLL.AddUserAccountRecord(-groupBuy.Price * userGroupBuy.BuyCount, 0, "参加团购活动：" + groupBuy.Name, userID, userName);
            ScriptHelper.Alert("购买成功!", "GroupBuyDetail.aspx?ID=" + userGroupBuy.GroupBuyID);
        }
    }
}
