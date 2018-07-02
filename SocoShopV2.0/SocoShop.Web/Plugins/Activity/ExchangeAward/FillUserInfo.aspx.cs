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
using System.IO;
using SocoShop.Page;

namespace SocoShop.Web
{
    public partial class FillUserInfo : PluginsBasePage
    {
        protected ProductInfo product = new ProductInfo();
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
                if (userID == 0)
                {
                    ScriptHelper.Alert("请先登录", "/User/Login.aspx?RedirectUrl=/Plugins/Activity/ExchangeAward/FillUserInfo.aspx?ID="+id.ToString());
                }
                UserInfo user = UserBLL.ReadUser(userID);
                Consignee.Text = user.UserName;
                Tel.Text = user.Tel;
                Mobile.Text = user.Mobile;
                Address.Text = user.Address;
                product = ProductBLL.ReadProduct(id);
                RegionID.DataSource = RegionBLL.ReadRegionUnlimitClass();
                RegionID.ClassID = user.RegionID;
                Head.Title = product.Name + " - 奖品兑换";
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
            int pointLeft = UserBLL.ReadUserMore(userID).PointLeft;
            string userName = Cookies.User.GetUserName(false);
            string userEmail = CookiesHelper.ReadCookieValue("UserEmail");
            //检测积分
            int id = RequestHelper.GetQueryString<int>("ID");
            product = ProductBLL.ReadProduct(id);
            ExchangeAwardInfo exchangeAward = ExchangeAwardBLL.ReadConfigInfo();
            int productPoint = 0;
            if (exchangeAward.PorudctIDList != string.Empty)
            {
                string[] productArray = exchangeAward.PorudctIDList.Split(',');
                string[] pointArray = exchangeAward.PointList.Split(',');
                for (int i = 0; i < productArray.Length; i++)
                {
                    if (productArray[i] == id.ToString())
                    {
                        productPoint = Convert.ToInt32(pointArray[i]);
                    }
                }
            }
            if (productPoint > pointLeft)
            {
                ScriptHelper.Alert("您当前的积分不足以兑取该奖品", RequestHelper.RawUrl);
            }
            //添加订单
            OrderInfo order = new OrderInfo();
            order.OrderNumber = ShopCommon.CreateOrderNumber();
            order.IsActivity = (int)BoolType.True;
            order.OrderStatus = (int)OrderStatus.WaitCheck;
            order.OrderNote = "积分兑换奖品";
            order.ProductMoney = 0;
            order.Balance = 0;
            order.FavorableMoney = 0;
            order.OtherMoney = 0;
            order.CouponMoney = 0;
            order.Consignee = StringHelper.AddSafe(Consignee.Text);
            SingleUnlimitClass singleUnlimitClass = new SingleUnlimitClass();
            order.RegionID = singleUnlimitClass.ClassID;
            order.Address = StringHelper.AddSafe( Address.Text);
            order.ZipCode =  StringHelper.AddSafe(ZipCode.Text);
            order.Tel = StringHelper.AddSafe(Tel.Text);
            order.Email = userEmail;
            order.Mobile =  StringHelper.AddSafe(Mobile.Text);
            order.ShippingID = 0;
            order.ShippingDate = RequestHelper.DateNow;
            order.ShippingNumber = string.Empty;
            order.ShippingMoney = 0;
            order.PayKey = string.Empty;
            order.PayName = string.Empty;
            order.PayDate = RequestHelper.DateNow; ;
            order.IsRefund = (int)BoolType.False;
            order.FavorableActivityID = 0;
            order.GiftID = 0;
            order.InvoiceTitle = string.Empty;
            order.InvoiceContent = string.Empty;
            order.UserMessage = string.Empty;
            order.AddDate = RequestHelper.DateNow;
            order.IP = ClientHelper.IP;
            order.UserID = userID;
            order.UserName = userName;
            int orderID = OrderBLL.AddOrder(order);
            //添加订单详细
            OrderDetailInfo orderDetail = new OrderDetailInfo();
            orderDetail.OrderID = orderID;
            orderDetail.ProductID = product.ID;
            orderDetail.ProductName = product.Name;
            orderDetail.ProductWeight = product.Weight;
            orderDetail.SendPoint = 0;
            orderDetail.ProductPrice = 0;
            orderDetail.BuyCount = 1;
            orderDetail.FatherID = 0;
            orderDetail.RandNumber = string.Empty;
            orderDetail.GiftPackID = 0;
            OrderDetailBLL.AddOrderDetail(orderDetail);
            //积分操作
            UserAccountRecordBLL.AddUserAccountRecord(0, -productPoint, "兑换奖品-" + product.Name, userID, userName);
            //更改产品库存订单数量
            ProductBLL.ChangeProductOrderCountByOrder(orderID, ChangeAction.Plus);
            //保存记录
            string fileName = StringHelper.Encode(ShopConfig.ReadConfigInfo().SecureKey, ShopConfig.ReadConfigInfo().SecureKey) + ".txt";
            fileName = Server.MapPath("Admin/" + fileName);
            File.AppendAllText(fileName, userName + "，订单号：" + order.OrderNumber + "，商品：" + product.Name + "\r\n", System.Text.Encoding.Default);   
            ScriptHelper.Alert("成功兑换", "/User/OrderDetail.aspx?ID=" + orderID.ToString());
        }
    }
}
