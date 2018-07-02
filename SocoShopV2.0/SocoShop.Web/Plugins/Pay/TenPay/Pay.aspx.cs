using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocoShop.Common;
using SocoShop.Business;
using SocoShop.Entity;
using SkyCES.EntLib;

namespace SocoShop.Pay.TenPay
{
    public partial class Pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayConfig payConfig = new PayConfig();
            int userID = Cookies.User.GetUserID(true);
            Md5Pay md5pay = new Md5Pay();
            string action = RequestHelper.GetQueryString<string>("Action");
            switch (action)
            {
                case "Apply":
                    int applyID = RequestHelper.GetQueryString<int>("ApplyID");
                    UserRechargeInfo userRecharge = UserRechargeBLL.ReadUserRecharge(applyID, userID);
                    md5pay.Sp_billno = userRecharge.Number;
                    md5pay.Desc = "网上冲值：" + userRecharge.Number;
                    md5pay.Total_fee = Convert.ToInt64(userRecharge.Money * 100);
                    break;
                case "PayOrder":
                    int orderID = RequestHelper.GetQueryString<int>("OrderID");
                    OrderInfo order = OrderBLL.ReadOrder(orderID, userID);
                    md5pay.Sp_billno = order.OrderNumber;
                    md5pay.Desc = "在线支付：" + order.OrderNumber;
                    md5pay.Total_fee = Convert.ToInt64((order.ProductMoney - order.FavorableMoney + order.ShippingMoney + order.OtherMoney - order.Balance - order.CouponMoney) * 100);
                    break;
                default:
                    break;
            }
            md5pay.Bargainor_id = payConfig.BargainorID;  //卖家商户号           
            md5pay.Key = payConfig.BusinessKey; //卖家商户key   
            md5pay.Date = DateTime.Now.ToString("yyyyMMdd");//交易日期            
            md5pay.Attach = "tenpay"; //交易标识           
            md5pay.Purchaser_id = "";  //买家帐号            
            md5pay.Return_url = "http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Pay/TenPay/Return.aspx"; //商户回调url
            md5pay.Transaction_id = md5pay.Bargainor_id + md5pay.Date + md5pay.UnixStamp();  //財付通交易号,需保证此订单号每天唯一,切不能重复！
          //  md5pay.Spbill_create_ip = Page.Request.UserHostAddress;
            string url = "";
            if (!md5pay.GetPayUrl(out url))
            {
                ResponseHelper.Write("创建地址失败");
            }
            else
            {
                Response.Redirect(url);
            }
        }
    }
}
