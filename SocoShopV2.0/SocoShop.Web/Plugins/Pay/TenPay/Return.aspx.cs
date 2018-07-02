using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocoShop.Common;
using SocoShop.Business;
using SocoShop.Entity;
using SkyCES.EntLib;

namespace SocoShop.Pay.TenPay
{
    public partial class Return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string suchtml = "<meta content=\"China TENCENT\" name=\"TENCENT_ONLINE_PAYMENT\">\n"
				+ "<script language=\"javascript\">\n"
                + "window.location.href='http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Pay/TenPay/Show.aspx';\n"
				+ "</script>";
            string errmsg = "";

            PayConfig payConfig = new PayConfig();
			Md5Pay md5pay = new Md5Pay();
            md5pay.Key = payConfig.BusinessKey; //卖家商户key 
            md5pay.Bargainor_id = payConfig.BargainorID;
			//判断签名
            if (md5pay.GetPayValueFromUrl(Request.QueryString, out  errmsg))
			{
				//认证签名成功
				//支付判断
				if(md5pay.Pay_Result==Md5Pay.PAYOK) 
				{				
					//支付成功，同定单号md5pay.Transaction_id可能会多次通知，请务必注意判断订单是否重复的逻辑
					//跳转到成功页面，财付通收到<meta content=\"China TENCENT\" name=\"TENCENT_ONLINE_PAYMENT\">，认为通知成功   
                    string orderNumber = md5pay.Sp_billno;
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
                            orderAction.Note = "客户财付通在线支付";
                            orderAction.IP = ClientHelper.IP;
                            orderAction.Date = RequestHelper.DateNow;
                            orderAction.AdminID = 0;
                            orderAction.AdminName = string.Empty;
                            OrderActionBLL.AddOrderAction(orderAction);
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
                            string note= "财付通在线冲值：" + userRecharge.Number;
                            UserAccountRecordBLL.AddUserAccountRecord(userRecharge.Money,0,note,userRecharge.UserID,userRecharge.UserName);
                        }
                    }
                    //StreamWriter sw = File.AppendText(Server.MapPath("log.txt"));
                    //sw.WriteLine(DateTime.Now.ToString() + orderNumber);
                    //sw.Flush();
                    //sw.Close();
                    Response.Write(suchtml);		
				} 
				else 
				{
                    //支付失败，请不要按成功处理
                    Response.Write("支付失败"+errmsg );
				}				
			} 
			else
            {
                //认证签名失败
                Response.Write("认证签名失败");			
			}

		}
    }
}
