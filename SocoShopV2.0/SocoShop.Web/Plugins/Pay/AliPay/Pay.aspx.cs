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

namespace SocoShop.Pay.AliPay
{
    public partial class Pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayConfig payConfig = new PayConfig();
            string out_trade_no = string.Empty; //订单号
            string subject = "";	//商品名称
            string body = "";		//商品描述  
            string price = "";  //单价
            string quantity = ""; //数量
            string show_url = "";//展示地址
            string logistics_fee = ""; //邮费
            string action = RequestHelper.GetQueryString<string>("Action");
            int userID = Cookies.User.GetUserID(true);
            string service = payConfig.Service;  //服务参数 trade_create_by_buyer 标准双接口交易 create_direct_pay_by_user 直接付款,create_partner_trade_by_buyer 担保付款 
            switch (action)
            {
                case "Apply":
                    int applyID = RequestHelper.GetQueryString<int>("ApplyID");
                    UserRechargeInfo userRecharge = UserRechargeBLL.ReadUserRecharge(applyID, userID);
                    out_trade_no = userRecharge.Number;
                    subject = "在线冲值：" + userRecharge.Number;
                    body = "在线冲值";
                    price = userRecharge.Money.ToString();
                    quantity="1";
                    show_url = "http://" + Request.ServerVariables["Http_Host"];
                    logistics_fee = "0";
                    break;
                case "PayOrder":
                    int orderID = RequestHelper.GetQueryString<int>("OrderID");
                    OrderInfo order = OrderBLL.ReadOrder(orderID, userID);
                    out_trade_no = order.OrderNumber;
                    subject = "在线支付：" + order.OrderNumber;
                    body = "在线支付";
                    price = (order.ProductMoney - order.FavorableMoney + order.ShippingMoney + order.OtherMoney - order.Balance - order.CouponMoney).ToString();
                    quantity = "1";
                    show_url = "http://" + Request.ServerVariables["Http_Host"];
                    logistics_fee = "0";
                    break;
                default: 
                    break;
            }
            string gateway = "https://www.alipay.com/cooperate/gateway.do?";	//'支付接口
            string partner = payConfig.Partner;		//	合作伙伴ID		
            string sign_type = "MD5"; //加密协议              
            string payment_type = "1";                  //支付类型	            
            string seller_email = payConfig.SellerEmail;             //卖家账号
            string key = payConfig.SecurityKey;              //partner账户的支付宝安全校验码
            string return_url = "http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Pay/AliPay/Return.aspx"; //服务器返回接口
            string notify_url = "http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Pay/AliPay/Notify.aspx"; //服务器通知接口
            string _input_charset ="utf-8";
            string logistics_type = "POST";
            string logistics_payment = "BUYER_PAY";
            AliPay ap = new AliPay();
            string aliay_url = ap.CreatUrl(
                gateway,
                service,
                partner,
                sign_type,
                out_trade_no,
                subject,
                body,
                payment_type,
                price,
                show_url,
                seller_email,
                key,
                return_url,
                _input_charset,
                notify_url,
                logistics_type,
                logistics_fee,
                logistics_payment,
                quantity
                );
            Response.Redirect(aliay_url);
        }

    }
}