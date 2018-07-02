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
    public partial class Pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayConfig payConfig = new PayConfig();
            merchant_id.Value = payConfig.MerchantID;		///商户编号
            string merchant_key = payConfig.MerchantKey;		///商户密钥
            string action = RequestHelper.GetQueryString<string>("Action");
            int userID = Cookies.User.GetUserID(true);
            switch (action)
            {
                case "Apply":
                    int applyID = RequestHelper.GetQueryString<int>("ApplyID");
                    UserRechargeInfo userRecharge = UserRechargeBLL.ReadUserRecharge(applyID, userID);
                    orderid.Value = userRecharge.Number;	///订单编号
                    amount.Value = userRecharge.Money.ToString();		///订单金额
                    commodity_info.Value = "网上冲值：" + userRecharge.Number;		///商品信息,如果含中文请通过System.Web.HttpUtility.UrlEncode()编码
                    break;
                case "PayOrder":
                    int orderID = RequestHelper.GetQueryString<int>("OrderID");
                    OrderInfo order = OrderBLL.ReadOrder(orderID, userID);
                    orderid.Value = order.OrderNumber; 	///订单编号
                    amount.Value = (order.ProductMoney - order.FavorableMoney + order.ShippingMoney + order.OtherMoney - order.Balance - order.CouponMoney).ToString(); ///订单金额
                    commodity_info.Value = "在线支付：" + order.OrderNumber;		///商品信息,如果含中文请通过System.Web.HttpUtility.UrlEncode()编码
                    break;
                default:
                    break;
            }
            currency.Value = "1";		///货币类型,1为人民币
            isSupportDES.Value = "2";		///是否安全校验,2为必校验,推荐
            merchant_url.Value = "http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Pay/BillPay/Return.aspx";		///支付结果返回地址
            pname.Value = "";		///支付人姓名,如果含中文请通过System.Web.HttpUtility.UrlEncode()编码            
            merchant_param.Value = "";		///商户私有参数
            pemail.Value = "";		///传递email到快钱网关页面
            pid.Value = "";		///代理/合作伙伴商户编号

            ///生成加密串,注意顺序
            String ScrtStr = "merchant_id=" + merchant_id.Value + "&orderid=" + orderid.Value + "&amount=" + amount.Value + "&merchant_url=" + merchant_url.Value + "&merchant_key=" + merchant_key;
            mac.Value = FormsAuthentication.HashPasswordForStoringInConfigFile(ScrtStr, "MD5");
        }
    }
}
