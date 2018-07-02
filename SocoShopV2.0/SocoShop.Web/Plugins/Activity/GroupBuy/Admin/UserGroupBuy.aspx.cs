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

namespace SocoShop.Web
{
    public partial class UserGroupBuy : SocoShop.Page.AdminBasePage
	{
        protected GroupBuyInfo groupBuy = new GroupBuyInfo();
        protected bool isCreateOrder = false;
		/// <summary>
		/// 页面加载方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				CheckAdminPower("ReadUserGroupBuy", PowerCheckType.Single);
                int groupBuyID = RequestHelper.GetQueryString<int>("GroupBuyID");
                groupBuy = GroupBuyBLL.ReadGroupBuy(groupBuyID);
                BindControl(UserGroupBuyBLL.ReadUserGroupBuyList(groupBuyID,CurrentPage,PageSize, ref Count), RecordList, MyPager);          
                if (groupBuy.MinCount <= UserGroupBuyBLL.ReadUserGroupBuyCount(groupBuyID))
                {
                    isCreateOrder = true;
                }
			}
		}
	
		/// <summary>
		/// 删除按钮点击方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DeleteButton_Click(object sender, EventArgs e)
		{
			CheckAdminPower( "DeleteUserGroupBuy", PowerCheckType.Single);
			string deleteID = RequestHelper.GetIntsForm("SelectID");
			if(deleteID != string.Empty)
			{
				UserGroupBuyBLL.DeleteUserGroupBuy(deleteID);
				AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("UserGroupBuy"), deleteID);
				ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
			}
		}
        /// <summary>
        /// 读取状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        protected string ReadStatus(int orderID)
        {
            string result = string.Empty;
            if (orderID == 0)
            {
                result = "未处理";
            }
            else if (orderID ==-1)
            {
                result = "已退还金额";
            }
            else
            {
                result = "<a href=\"/Admin/OrderDetail.aspx?ID=" + orderID + "\" target=\"_blank\">查看订单</a>";
            }
            return result;
        }
        
		/// <summary>
        /// 全部生成订单按钮点击方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void CreateOrderButton_Click(object sender, EventArgs e)
        {
            int groupBuyID = RequestHelper.GetQueryString<int>("GroupBuyID");
            groupBuy = GroupBuyBLL.ReadGroupBuy(groupBuyID);
            ProductInfo product = ProductBLL.ReadProduct(groupBuy.ProductID);
            List<UserGroupBuyInfo> userGroupBuyList = UserGroupBuyBLL.ReadUserGroupBuyList(groupBuyID);
            foreach (UserGroupBuyInfo userGroupBuy in userGroupBuyList)
            {
                if (userGroupBuy.OrderID == 0)
                {
                    decimal totalPrice = groupBuy.Price * userGroupBuy.BuyCount;
                    //添加订单
                    OrderInfo order = new OrderInfo();
                    order.OrderNumber = ShopCommon.CreateOrderNumber();
                    order.IsActivity = (int)BoolType.True;
                    order.OrderStatus = (int)OrderStatus.WaitCheck;
                    order.OrderNote = "团购活动：" + groupBuy.Name;
                    order.ProductMoney = totalPrice;
                    order.Balance = totalPrice;
                    order.FavorableMoney = 0;
                    order.OtherMoney = 0;
                    order.CouponMoney = 0;
                    order.Consignee = userGroupBuy.Consignee;
                    order.RegionID = userGroupBuy.RegionID;
                    order.Address = userGroupBuy.Address;
                    order.ZipCode = userGroupBuy.ZipCode;
                    order.Tel = userGroupBuy.Tel;
                    order.Email = userGroupBuy.Email;
                    order.Mobile = userGroupBuy.Mobile;
                    order.ShippingID = 0;
                    order.ShippingDate = RequestHelper.DateNow;
                    order.ShippingNumber = string.Empty;
                    order.ShippingMoney = 0;
                    order.PayKey = string.Empty;
                    order.PayName = string.Empty;
                    order.PayDate = userGroupBuy.Date; ;
                    order.IsRefund = (int)BoolType.False;
                    order.FavorableActivityID = 0;
                    order.GiftID = 0;
                    order.InvoiceTitle = string.Empty;
                    order.InvoiceContent = string.Empty;
                    order.UserMessage = string.Empty;
                    order.AddDate = RequestHelper.DateNow;
                    order.IP = userGroupBuy.IP;
                    order.UserID = userGroupBuy.UserID;
                    order.UserName = userGroupBuy.UserName;
                    int orderID = OrderBLL.AddOrder(order);
                    //添加订单详细
                    OrderDetailInfo orderDetail = new OrderDetailInfo();
                    orderDetail.OrderID = orderID;
                    orderDetail.ProductID = product.ID;
                    orderDetail.ProductName = product.Name;
                    orderDetail.ProductWeight = product.Weight;
                    orderDetail.SendPoint = 0;
                    orderDetail.ProductPrice = groupBuy.Price;
                    orderDetail.BuyCount = userGroupBuy.BuyCount;
                    orderDetail.FatherID = 0;
                    orderDetail.RandNumber = string.Empty;
                    orderDetail.GiftPackID = 0;
                    OrderDetailBLL.AddOrderDetail(orderDetail);
                    //更新订单 ID
                    UserGroupBuyBLL.UpdateUserGroupBuy(userGroupBuy.ID, orderID);
                    //更改产品库存订单数量
                    ProductBLL.ChangeProductOrderCountByOrder(orderID, ChangeAction.Plus);
                }
            }
            ScriptHelper.Alert("处理成功", RequestHelper.RawUrl);
        }
        
		/// <summary>
        /// 取消活动，退还用户余额
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void CancleButton_Click(object sender, EventArgs e)
        {
            int groupBuyID = RequestHelper.GetQueryString<int>("GroupBuyID");
            groupBuy = GroupBuyBLL.ReadGroupBuy(groupBuyID);
            List<UserGroupBuyInfo> userGroupBuyList  = UserGroupBuyBLL.ReadUserGroupBuyList(groupBuyID);
            foreach (UserGroupBuyInfo userGroupBuy in userGroupBuyList)
            {
                if (userGroupBuy.OrderID == 0)
                {
                    UserGroupBuyBLL.UpdateUserGroupBuy(userGroupBuy.ID, -1);
                    UserAccountRecordBLL.AddUserAccountRecord(groupBuy.Price * userGroupBuy.BuyCount, 0, "退还团购活动支付余额：" + groupBuy.Name, userGroupBuy.UserID, userGroupBuy.UserName);
                }
            }
            ScriptHelper.Alert("处理成功", RequestHelper.RawUrl);
        }
	}
}