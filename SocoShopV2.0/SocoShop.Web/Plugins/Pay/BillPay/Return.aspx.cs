using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SocoShop.Common;
using SocoShop.Business;
using SocoShop.Entity;
using SkyCES.EntLib;

namespace SocoShop.Pay.BillPay
{
    public partial class Return : System.Web.UI.Page
    {
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PayConfig payConfig = new PayConfig();
            string merchant_key = payConfig.MerchantKey;///商户密钥
            string merchant_id = Request["merchant_id"].ToString();		///获取商户编号
            string orderid = Request["orderid"].ToString();		///获取订单编号
            string amount = Request["amount"].ToString();		///获取订单金额
            string dealdate = Request["date"].ToString();		///获取交易日期
            string succeed = Request["succeed"].ToString();		///获取交易结果,Y成功,N失败
            string mac = Request["mac"].ToString();		///获取安全加密串
            string merchant_param = Request["merchant_param"].ToString();		///获取商户私有参数

            string couponid = Request["couponid"].ToString();		///获取优惠券编码
            string couponvalue = Request["couponvalue"].ToString();		///获取优惠券面额

            ///生成加密串,注意顺序
            string SrctStr = "merchant_id=" + merchant_id + "&orderid=" + orderid + "&amount=" + amount + "&date=" + dealdate + "&succeed=" + succeed + "&merchant_key=" + merchant_key;
            string mymac = FormsAuthentication.HashPasswordForStoringInConfigFile(SrctStr, "MD5");


            if (mac == mymac)
            {
                if (succeed == "Y")
                {
                    //更新数据库的订单语句
                    message = "成功付款";
                    string orderNumber = orderid;
                    OrderInfo order = OrderBLL.ReadOrderByNumber(orderNumber, 0);
                    if (order.ID > 0)
                    {
                        if (order.OrderStatus == (int)OrderStatus.WaitPay)
                        {
                            order.OrderStatus = (int)OrderStatus.WaitCheck;
                            OrderBLL.UpdateOrder(order);
                            //增加操作记录
                            OrderActionInfo orderAction = new OrderActionInfo();
                            orderAction.OrderID = order.ID;
                            orderAction.OrderOperate = (int)OrderOperate.Pay;
                            orderAction.StartOrderStatus = (int)OrderStatus.WaitPay;
                            orderAction.EndOrderStatus = (int)OrderStatus.WaitCheck;
                            orderAction.Note = "客户快钱在线支付";
                            orderAction.IP = ClientHelper.IP;
                            orderAction.Date = RequestHelper.DateNow;
                            orderAction.AdminID = 0;
                            orderAction.AdminName = string.Empty;
                            OrderActionBLL.AddOrderAction(orderAction);

                            message = "您已经成功支付订单：" + orderNumber;
                        }
                    }
                    else
                    {
                        UserRechargeInfo userRecharge = UserRechargeBLL.ReadUserRechargeByNumber(orderNumber, 0);
                        if (userRecharge.ID > 0 && userRecharge.IsFinish == (int)BoolType.False)
                        {
                            userRecharge.IsFinish = (int)BoolType.True;
                            UserRechargeBLL.UpdateUserRecharge(userRecharge);
                            //账户记录
                            string note = "快钱在线冲值：" + userRecharge.Number;
                            UserAccountRecordBLL.AddUserAccountRecord(userRecharge.Money, 0, note, userRecharge.UserID, userRecharge.UserName);

                            message = "您的冲值已经成功完成";
                        }
                    }
                }
                else
                {
                    message = "支付订单失败";
                }
            }
            else
            {
                message = "签名错误";

            }
        }
    }
}
