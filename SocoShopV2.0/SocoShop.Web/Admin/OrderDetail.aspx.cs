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

    public partial class OrderDetail : AdminBasePage
    {
        protected bool canEdit = false;
        protected OrderInfo order = new OrderInfo();
        protected List<OrderActionInfo> orderActionList = new List<OrderActionInfo>();
        protected int sendPoint = 0;

        protected void BackButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int orderStatus = order.OrderStatus;
            order.OrderStatus = OrderActionBLL.ReadLatestOrderAction(order.ID, order.OrderStatus).StartOrderStatus;
            int point = 0;
            if (orderStatus == 6 && order.IsActivity == 0) point = -OrderBLL.ReadOrderSendPoint(order.ID);
            if (orderStatus == 5 && order.OrderStatus == 6 && order.IsActivity == 0) point = OrderBLL.ReadOrderSendPoint(order.ID);
            if (point != 0) UserAccountRecordBLL.AddUserAccountRecord(0M, point, ShopLanguage.ReadLanguage("OrderBack").Replace("$OrderNumber", order.OrderNumber), order.UserID, order.UserName);
            switch (orderStatus)
            {
                case 1:
                case 2:
                    if (order.OrderStatus == 3) ProductBLL.ChangeProductOrderCountByOrder(order.ID, ChangeAction.Minus);
                    break;

                case 3:
                    ProductBLL.ChangeProductOrderCountByOrder(order.ID, ChangeAction.Plus);
                    break;

                case 4:
                    if (order.OrderStatus == 5) ProductBLL.ChangeProductSendCountByOrder(order.ID, ChangeAction.Plus);
                    break;

                case 5:
                    if (order.OrderStatus == 4) ProductBLL.ChangeProductSendCountByOrder(order.ID, ChangeAction.Minus);
                    if (order.OrderStatus == 7)
                    {
                        ProductBLL.ChangeProductOrderCountByOrder(order.ID, ChangeAction.Minus);
                        ProductBLL.ChangeProductSendCountByOrder(order.ID, ChangeAction.Minus);
                    }
                    break;

                case 7:
                    ProductBLL.ChangeProductOrderCountByOrder(order.ID, ChangeAction.Plus);
                    ProductBLL.ChangeProductSendCountByOrder(order.ID, ChangeAction.Plus);
                    break;
            }
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Back, orderStatus);
        }

        protected void ButtonEnd(OrderInfo order, string note, OrderOperate orderOperate, int startOrderStatus)
        {
            OrderBLL.AdminUpdateOrderAddAction(order, note, (int) orderOperate, startOrderStatus);
            this.OrderOperateSendEmail(order, orderOperate);
            ScriptHelper.Alert(ShopLanguage.ReadLanguage("OperateOK"), RequestHelper.RawUrl);
        }

        protected OrderInfo ButtoStart()
        {
            base.CheckAdminPower("OperateOrder", PowerCheckType.Single);
            return OrderBLL.ReadOrder(RequestHelper.GetQueryString<int>("ID"), 0);
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int orderStatus = order.OrderStatus;
            order.OrderStatus = 3;
            ProductBLL.ChangeProductOrderCountByOrder(order.ID, ChangeAction.Minus);
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Cancle, orderStatus);
        }

        protected void ChangeButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int orderStatus = order.OrderStatus;
            order.OrderStatus = 4;
            ProductBLL.ChangeProductSendCountByOrder(order.ID, ChangeAction.Minus);
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Change, orderStatus);
        }

        protected void CheckButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int orderStatus = order.OrderStatus;
            order.OrderStatus = 4;
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Check, orderStatus);
        }

        protected void OrderOperateSendEmail(OrderInfo order, OrderOperate orderOperate)
        {
            string key = string.Empty;
            int payOrder = 0;
            switch (orderOperate)
            {
                case OrderOperate.Pay:
                    payOrder = ShopConfig.ReadConfigInfo().PayOrder;
                    key = "PayOrder";
                    break;

                case OrderOperate.Check:
                    payOrder = ShopConfig.ReadConfigInfo().CheckOrder;
                    key = "CheckOrder";
                    break;

                case OrderOperate.Cancle:
                    payOrder = ShopConfig.ReadConfigInfo().CancleOrder;
                    key = "CancleOrder";
                    break;

                case OrderOperate.Send:
                    payOrder = ShopConfig.ReadConfigInfo().SendOrder;
                    key = "SendOrder";
                    break;

                case OrderOperate.Received:
                    payOrder = ShopConfig.ReadConfigInfo().ReceivedOrder;
                    key = "ReceivedOrder";
                    break;

                case OrderOperate.Change:
                    payOrder = ShopConfig.ReadConfigInfo().ChangeOrder;
                    key = "ChangeOrder";
                    break;

                case OrderOperate.Return:
                    payOrder = ShopConfig.ReadConfigInfo().ReturnOrder;
                    key = "ReturnOrder";
                    break;

                case OrderOperate.Back:
                    payOrder = ShopConfig.ReadConfigInfo().BackOrder;
                    key = "BackOrder";
                    break;

                case OrderOperate.Refund:
                    payOrder = ShopConfig.ReadConfigInfo().RefundOrder;
                    key = "RefundOrder";
                    break;
            }
            if (payOrder == 1 && key != string.Empty)
            {
                EmailContentInfo info = EmailContentHelper.ReadSystemEmailContent(key);
                EmailSendRecordInfo emailSendRecord = new EmailSendRecordInfo();
                emailSendRecord.Title = info.EmailTitle;
                emailSendRecord.Content = info.EmailContent.Replace("$UserName$", order.UserName).Replace("$OrderNumber$", order.OrderNumber).Replace("$PayName$", order.PayName).Replace("$ShippingName$", ShippingBLL.ReadShippingCache(order.ShippingID).Name).Replace("$ShippingNumber$", order.ShippingNumber).Replace("$ShippingDate$", order.ShippingDate.ToString("yyyy-MM-dd"));
                emailSendRecord.IsSystem = 1;
                emailSendRecord.EmailList = order.Email;
                emailSendRecord.IsStatisticsOpendEmail = 0;
                emailSendRecord.SendStatus = 1;
                emailSendRecord.AddDate = RequestHelper.DateNow;
                emailSendRecord.SendDate = RequestHelper.DateNow;
                emailSendRecord.ID = EmailSendRecordBLL.AddEmailSendRecord(emailSendRecord);
                EmailSendRecordBLL.SendEmail(emailSendRecord);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                this.sendPoint = OrderBLL.ReadOrderSendPoint(queryString);
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadOrder", PowerCheckType.Single);
                    this.order = OrderBLL.ReadOrder(queryString, 0);
                    int isCod = PayPlugins.ReadPayPlugins(this.order.PayKey).IsCod;
                    if ((this.order.OrderStatus == 1 || this.order.OrderStatus == 2 && isCod == 1) && this.order.IsActivity == 0) this.canEdit = true;
                    this.orderActionList = OrderActionBLL.ReadOrderActionByOrder(queryString);
                    this.ShowButton(this.order);
                }
            }
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int orderStatus = order.OrderStatus;
            order.OrderStatus = 2;
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Pay, orderStatus);
        }

        protected string ReadPreNextOrderString(int orderID)
        {
            object obj2;
            string str = string.Empty;
            int[] numArray = new int[2];
            foreach (OrderInfo info in OrderBLL.ReadPreNextOrder(orderID))
            {
                if (info.ID < orderID)
                    numArray[0] = info.ID;
                else
                    numArray[1] = info.ID;
            }
            if (numArray[0] > 0)
            {
                obj2 = str;
                str = string.Concat(new object[] { obj2, "<input type=\"button\" class=\"button\" value=\"上一个\" onclick=\"window.location.href='OrderDetail.aspx?ID=", numArray[0], "'\"/>" });
            }
            if (numArray[1] > 0)
            {
                obj2 = str;
                str = string.Concat(new object[] { obj2, " <input type=\"button\" class=\"button\" value=\"下一个\" onclick=\"window.location.href='OrderDetail.aspx?ID=", numArray[1], "'\"/>" });
            }
            return str;
        }

        protected void ReceivedButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int point = OrderBLL.ReadOrderSendPoint(order.ID);
            if (point > 0 && order.IsActivity == 0) UserAccountRecordBLL.AddUserAccountRecord(0M, point, ShopLanguage.ReadLanguage("OrderReceived").Replace("$OrderNumber", order.OrderNumber), order.UserID, order.UserName);
            int orderStatus = order.OrderStatus;
            order.OrderStatus = 6;
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Received, orderStatus);
        }

        protected void RefundButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            if (order.CouponMoney > 0M)
            {
                UserCouponInfo userCoupon = UserCouponBLL.ReadUserCouponByOrder(order.ID);
                userCoupon.IsUse = 0;
                userCoupon.OrderID = 0;
                UserCouponBLL.UpdateUserCoupon(userCoupon);
            }
            decimal balance = order.Balance;
            int isCod = PayPlugins.ReadPayPlugins(order.PayKey).IsCod;
            if (order.OrderStatus == 7 && isCod == 0)
                balance += OrderBLL.ReadNoPayMoney(order);
            else if (order.OrderStatus == 3 && OrderActionBLL.ReadLatestOrderAction(order.ID, order.OrderStatus).StartOrderStatus == 2 && isCod == 0) balance += OrderBLL.ReadNoPayMoney(order);
            if (balance > 0M)
            {
                string note = "退还订单：" + order.OrderNumber + "的金额";
                UserAccountRecordBLL.AddUserAccountRecord(balance, 0, note, order.UserID, order.UserName);
            }
            int orderStatus = order.OrderStatus;
            order.OrderStatus = order.OrderStatus;
            order.Balance = 0M;
            order.CouponMoney = 0M;
            order.IsRefund = 1;
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Refund, orderStatus);
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int orderStatus = order.OrderStatus;
            order.OrderStatus = 7;
            ProductBLL.ChangeProductOrderCountByOrder(order.ID, ChangeAction.Minus);
            ProductBLL.ChangeProductSendCountByOrder(order.ID, ChangeAction.Minus);
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Return, orderStatus);
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = this.ButtoStart();
            int orderStatus = order.OrderStatus;
            order.OrderStatus = 5;
            order.ShippingNumber = this.ShippingNumber.Text;
            ProductBLL.ChangeProductSendCountByOrder(order.ID, ChangeAction.Plus);
            this.ButtonEnd(order, this.Note.Text, OrderOperate.Send, orderStatus);
        }

        protected void ShowButton(OrderInfo order)
        {
            if (order.OrderStatus == 1) this.PayButton.Visible = true;
            if (order.OrderStatus == 2) this.CheckButton.Visible = true;
            if (order.OrderStatus == 1 || order.OrderStatus == 2) this.CancelButton.Visible = true;
            if (order.OrderStatus == 4) this.SendButton.Visible = true;
            if (order.OrderStatus == 5) this.ReceivedButton.Visible = true;
            if (order.OrderStatus == 5) this.ChangeButton.Visible = true;
            if (order.OrderStatus == 5) this.ReturnButton.Visible = true;
            if (this.orderActionList.Count > 0 && order.IsRefund != 1) this.BackButton.Visible = true;
            if (order.IsRefund == 0)
            {
                object obj2;
                bool flag = false;
                string str = string.Empty;
                int isCod = PayPlugins.ReadPayPlugins(order.PayKey).IsCod;
                if (order.OrderStatus == 3)
                {
                    if (order.Balance > 0M)
                    {
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "退还余额：", order.Balance, "元；" });
                        flag = true;
                    }
                    if (order.CouponMoney > 0M)
                    {
                        str = str + "优惠券：" + UserCouponBLL.ReadUserCouponByOrder(order.ID).Number + "重设为未使用；";
                        flag = true;
                    }
                    if (OrderActionBLL.ReadLatestOrderAction(order.ID, order.OrderStatus).StartOrderStatus == 2 && isCod == 0)
                    {
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "退还已支付金额：", OrderBLL.ReadNoPayMoney(order), "元；" });
                        flag = true;
                    }
                }
                else if (order.OrderStatus == 7)
                {
                    if (order.Balance > 0M)
                    {
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "退还余额：", order.Balance, "元；" });
                        flag = true;
                    }
                    if (order.CouponMoney > 0M)
                    {
                        str = str + "优惠券：" + UserCouponBLL.ReadUserCouponByOrder(order.ID).Number + "重设为未使用；";
                        flag = true;
                    }
                    if (isCod == 0)
                    {
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "退还已支付金额：", OrderBLL.ReadNoPayMoney(order), "元；" });
                        flag = true;
                    }
                }
                this.RefundButton.Text = str;
                this.RefundButton.Visible = flag;
            }
        }
    }
}

