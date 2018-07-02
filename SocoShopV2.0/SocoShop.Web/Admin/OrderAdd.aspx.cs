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

    public partial class OrderAdd : AdminBasePage
    {
        protected OrderInfo order = new OrderInfo();
        protected string title = string.Empty;

        protected decimal CountFavorableMoney(OrderInfo order)
        {
            decimal favorableMoney = order.FavorableMoney;
            int favorableActivityID = order.FavorableActivityID;
            if (favorableActivityID > 0)
            {
                favorableMoney = 0M;
                FavorableActivityInfo info = FavorableActivityBLL.ReadFavorableActivity(favorableActivityID);
                if (info.ShippingWay == 1 && ShippingRegionBLL.IsRegionIn(order.RegionID, info.RegionID)) favorableMoney += order.ShippingMoney;
                switch (info.ReduceWay)
                {
                    case 1:
                        return (favorableMoney + info.ReduceMoney);

                    case 2:
                        return (favorableMoney + order.ProductMoney * (10M - info.ReduceDiscount) / 10M);
                }
            }
            return favorableMoney;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadOrder", PowerCheckType.Single);
                    this.order = OrderBLL.ReadOrder(queryString, 0);
                    int isCod = PayPlugins.ReadPayPlugins(this.order.PayKey).IsCod;
                    if (this.order.OrderStatus != 1 && this.order.OrderStatus != 2 || isCod != 1 || this.order.IsActivity != 0)
                    {
                        string content = "<script language='javascript'>alert(\"订单已经审核，不能修改\");parent.cancel();</script>";
                        ResponseHelper.Write(content);
                        ResponseHelper.End();
                    }
                    else
                    {
                        this.RegionID.DataSource = RegionBLL.ReadRegionUnlimitClass();
                        this.OrderNote.Text = this.order.OrderNote;
                        this.ProductMoney.Text = this.order.ProductMoney.ToString();
                        this.FavorableMoney.Text = this.order.FavorableMoney.ToString();
                        this.Consignee.Text = this.order.Consignee;
                        this.RegionID.ClassID = this.order.RegionID;
                        this.Address.Text = this.order.Address;
                        this.ZipCode.Text = this.order.ZipCode;
                        this.Tel.Text = this.order.Tel;
                        this.Email.Text = this.order.Email;
                        this.Mobile.Text = this.order.Mobile;
                        this.ShippingDate.Text = this.order.ShippingDate.ToString("yyyy-MM-dd");
                        this.ShippingNumber.Text = this.order.ShippingNumber;
                        this.ShippingMoney.Text = this.order.ShippingMoney.ToString();
                        this.OtherMoney.Text = this.order.OtherMoney.ToString();
                        this.Balance.Text = this.order.Balance.ToString();
                        this.InvoiceTitle.Text = this.order.InvoiceTitle;
                        this.InvoiceContent.Text = this.order.InvoiceContent;
                        this.UserMessage.Text = this.order.UserMessage;
                        string str3 = RequestHelper.GetQueryString<string>("Action");
                        if (str3 != null)
                        {
                            if (!(str3 == "Shipping"))
                            {
                                if (str3 == "Other")
                                {
                                    this.title = "其他信息";
                                    this.Other.Visible = true;
                                }
                                else if (str3 == "Money")
                                {
                                    this.title = "费用信息";
                                    this.Money.Visible = true;
                                    List<UserCouponInfo> list = UserCouponBLL.ReadUserCouponCanUse(this.order.UserID);
                                    UserCouponInfo info = UserCouponBLL.ReadUserCouponByOrder(this.order.ID);
                                    if (info.ID > 0) list.Add(info);
                                    ListItem item = null;
                                    foreach (UserCouponInfo info2 in list)
                                    {
                                        item = new ListItem();
                                        item.Text = string.Concat(new object[] { "编号：", info2.Number, " （", info2.Coupon.Money, "元 ）" });
                                        if (info2.Coupon.UseMinAmount <= this.order.ProductMoney)
                                            item.Value = string.Concat(new object[] { info2.ID, "|", info2.Coupon.Money, "|1" });
                                        else
                                        {
                                            item.Value = string.Concat(new object[] { info2.ID, "|", info2.Coupon.Money, "|0" });
                                            item.Attributes.Add("style", "background-color: #D50000");
                                        }
                                        this.UserCoupon.Items.Add(item);
                                    }
                                    this.UserCoupon.Items.Insert(0, new ListItem("请选择优惠券", "0|0|1"));
                                    if (info.ID > 0) this.UserCoupon.Text = item.Value;
                                }
                                else if (str3 == "Gift")
                                {
                                    this.title = "礼品信息";
                                    this.Gift.Visible = true;
                                }
                            }
                            else
                            {
                                this.title = "邮寄信息";
                                this.Shipping.Visible = true;
                                if (this.order.OrderStatus >= 5) this.ShippingInfo.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            OrderInfo order = OrderBLL.ReadOrder(RequestHelper.GetQueryString<int>("ID"), 0);
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != null)
            {
                if (!(queryString == "Shipping"))
                {
                    if (queryString == "Other")
                    {
                        order.OrderNote = this.OrderNote.Text;
                        order.InvoiceTitle = this.InvoiceTitle.Text;
                        order.InvoiceContent = this.InvoiceContent.Text;
                    }
                    else if (queryString == "Money")
                    {
                        order.OtherMoney = Convert.ToDecimal(this.OtherMoney.Text);
                        decimal num3 = Convert.ToDecimal(this.Balance.Text);
                        UserInfo info2 = UserBLL.ReadUserMore(order.UserID);
                        if (num3 > info2.MoneyLeft + order.Balance) ScriptHelper.Alert("您的账户余额不足");
                        string text = this.UserCoupon.Text;
                        int id = Convert.ToInt32(text.Split(new char[] { '|' })[0]);
                        decimal num5 = Convert.ToDecimal(text.Split(new char[] { '|' })[1]);
                        if (Convert.ToInt32(text.Split(new char[] { '|' })[2]) == 0) ScriptHelper.Alert("该订单的产品金额未能达到该优惠券规定的最低金额，不能使用");
                        if (order.Balance != num3)
                        {
                            decimal money = order.Balance - num3;
                            string note = string.Empty;
                            if (money > 0M)
                                note = "减少订单：" + order.OrderNumber + "的余额支付";
                            else
                                note = "增加订单：" + order.OrderNumber + "的余额支付";
                            UserAccountRecordBLL.AddUserAccountRecord(money, 0, note, order.UserID, order.UserName);
                            order.Balance = num3;
                        }
                        UserCouponInfo userCoupon = UserCouponBLL.ReadUserCouponByOrder(order.ID);
                        if (id != userCoupon.ID)
                        {
                            if (userCoupon.ID > 0)
                            {
                                userCoupon.IsUse = 0;
                                userCoupon.OrderID = 0;
                                UserCouponBLL.UpdateUserCoupon(userCoupon);
                            }
                            if (id > 0)
                            {
                                userCoupon = UserCouponBLL.ReadUserCoupon(id, order.UserID);
                                userCoupon.IsUse = 1;
                                userCoupon.OrderID = order.ID;
                                UserCouponBLL.UpdateUserCoupon(userCoupon);
                            }
                            order.CouponMoney = num5;
                        }
                    }
                    else if (queryString == "Gift") order.GiftID = RequestHelper.GetForm<int>("GiftID");
                }
                else
                {
                    string classID = this.RegionID.ClassID;
                    int form = RequestHelper.GetForm<int>("ShippingID");
                    if (classID == string.Empty || form <= 0) ScriptHelper.Alert("收货地区和配送方式不能为空");
                    if (order.RegionID != classID || order.ShippingID != form)
                    {
                        order.ShippingID = form;
                        order.RegionID = classID;
                        order.ShippingMoney = OrderBLL.ReadOrderShippingMoney(order);
                        order.FavorableMoney = this.CountFavorableMoney(order);
                    }
                    order.OrderNote = this.OrderNote.Text;
                    order.Consignee = this.Consignee.Text;
                    order.Address = this.Address.Text;
                    order.ZipCode = this.ZipCode.Text;
                    order.Tel = this.Tel.Text;
                    order.Email = this.Email.Text;
                    order.Mobile = this.Mobile.Text;
                    order.ShippingDate = Convert.ToDateTime(this.ShippingDate.Text);
                    order.ShippingNumber = this.ShippingNumber.Text;
                }
            }
            base.CheckAdminPower("UpdateOrder", PowerCheckType.Single);
            OrderBLL.UpdateOrder(order);
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Order"), order.ID);
            AdminBasePage.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
        }
    }
}

