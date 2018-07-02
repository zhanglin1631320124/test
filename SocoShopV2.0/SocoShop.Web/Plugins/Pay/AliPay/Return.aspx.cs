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
    public partial class Return : System.Web.UI.Page
    {       
        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>
        public static string GetMD5(string s, string _input_charset)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
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
        public static string[] BubbleSort(string[] r)
        {
            int i, j;
            string temp;
            bool exchange;
            for (i = 0; i < r.Length; i++)
            {
                exchange = false;
                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;
                        exchange = true;
                    }
                }
                if (!exchange)
                {
                    break;
                }
            }
            return r;
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

        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PayConfig payConfig = new PayConfig();
            string alipayNotifyURL = "https://www.alipay.com/cooperate/gateway.do?";
            string key = payConfig.SecurityKey; //partner 的对应交易安全校验码（必须填写）
            string _input_charset = "utf-8";
            string partner = payConfig.Partner; 		//partner合作伙伴id（必须填写）
            alipayNotifyURL = alipayNotifyURL + "service=notify_verify" + "&partner=" + partner + "&notify_id=" + Request.QueryString["notify_id"];
            //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的
            string responseTxt = Get_Http(alipayNotifyURL, 120000);
            int i;
            NameValueCollection coll = Request.QueryString;
            String[] requestarr = coll.AllKeys;
            //进行排序；
            string[] Sortedstr = BubbleSort(requestarr);
            //构造待md5摘要字符串 ；
            StringBuilder prestr = new StringBuilder();
            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type")
                {
                    if (i == Sortedstr.Length - 1)
                    {
                        prestr.Append(Sortedstr[i] + "=" + Request.QueryString[Sortedstr[i]]);
                    }
                    else
                    {
                        prestr.Append(Sortedstr[i] + "=" + Request.QueryString[Sortedstr[i]] + "&");

                    }
                }
            }
            prestr.Append(key);
            //生成Md5摘要；
            string mysign = GetMD5(prestr.ToString(), _input_charset);
            string sign = Request.QueryString["sign"];
            if (mysign == sign && responseTxt == "true")   //验证支付发过来的消息，签名是否正确
            {
                //更新数据库的订单语句
                message = "成功付款";
                string orderNumber = RequestHelper.GetQueryString<string>("out_trade_no");
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
                        string note = "支付宝在线冲值：" + userRecharge.Number;
                        UserAccountRecordBLL.AddUserAccountRecord(userRecharge.Money, 0, note, userRecharge.UserID, userRecharge.UserName);

                        message = "您的冲值已经成功完成";
                    }
                }
            }
            else
            {
                message = "支付订单出现问题";
            }

        }
    }
}
