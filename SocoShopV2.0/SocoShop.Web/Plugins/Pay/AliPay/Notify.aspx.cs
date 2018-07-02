using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using SkyCES.EntLib;
using SocoShop.Business;
using SocoShop.Entity;

namespace SocoShop.Pay.AliPay
{
    public partial class Notify : System.Web.UI.Page
    {
        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>
        public static string GetMD5(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 冒泡排序法
        /// </summary>
        public static string[] BubbleSort(string[] R)
        {
            int i, j; //交换标志 
            string temp;
            bool exchange;
            for (i = 0; i < R.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假
                for (j = R.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(R[j + 1], R[j]) < 0)　//交换条件
                    {
                        temp = R[j + 1];
                        R[j + 1] = R[j];
                        R[j] = temp;
                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }
                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }
            return R;
        }
        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="a_strUrl"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public String Get_Http(String a_strUrl, int timeout)
        {
            string strResult;
            try
            {

                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {

                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PayConfig payConfig = new PayConfig();
            string alipayNotifyURL = "https://www.alipay.com/cooperate/gateway.do?";
            string key = payConfig.SecurityKey; //partner 的对应交易安全校验码（必须填写）
            string partner = payConfig.Partner; 		//partner合作伙伴id（必须填写）
            alipayNotifyURL = alipayNotifyURL + "service=notify_verify" + "&partner=" + partner + "&notify_id=" + Request.Form["notify_id"];
            //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的
            string responseTxt = Get_Http(alipayNotifyURL, 120000000);
            int i;
            NameValueCollection coll = Request.Form;
            String[] requestarr = coll.AllKeys;
            //进行排序；
            string[] Sortedstr = BubbleSort(requestarr);
            //构造待md5摘要字符串 ；
            string prestr = "";
            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type")
                {
                    if (i == Sortedstr.Length - 1)
                    {
                        prestr = prestr + Sortedstr[i] + "=" + Request.Form[Sortedstr[i]];
                    }
                    else
                    {
                        prestr = prestr + Sortedstr[i] + "=" + Request.Form[Sortedstr[i]] + "&";
                    }
                }

            }
            prestr = prestr + key;
            //生成Md5摘要；
            string mysign = GetMD5(prestr);
            string sign = Request.Form["sign"];

            //StreamWriter sw = File.AppendText(Server.MapPath("log.txt"));
            //sw.WriteLine(DateTime.Now.ToString() + "返回页面：" + RequestHelper.GetStringForm("out_trade_no") + "：" + Request.Form["trade_status"]);
            //sw.WriteLine("mysign：" + mysign + "；sign：" + sign + "；responseTxt：" + responseTxt);
            //sw.Flush();
            //sw.Close();

            if (mysign == sign && responseTxt == "true")   //验证支付发过来的消息，签名是否正确
            {
                //sw = File.AppendText(Server.MapPath("log.txt"));
                //sw.WriteLine(DateTime.Now.ToString() + "验证成功：" + RequestHelper.GetStringForm("out_trade_no") +"："+ Request.Form["trade_status"]);
                //sw.Flush();
                //sw.Close();

                if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "WAIT_SELLER_SEND_GOODS")//   判断支付状态TRADE_FINISHED（文档中有枚举表可以参考）            
                {
                    //更新数据库的订单语句
                    string orderNumber = RequestHelper.GetForm<string>("out_trade_no");
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
                            orderAction.Note = "客户支付宝在线支付";
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
                            string note = "支付宝在线冲值：" + userRecharge.Number;
                            UserAccountRecordBLL.AddUserAccountRecord(userRecharge.Money, 0, note, userRecharge.UserID, userRecharge.UserName);
                        }
                    }
                    Response.Write("success");
                }
                else
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("fail");
            }
        }
    }
}
