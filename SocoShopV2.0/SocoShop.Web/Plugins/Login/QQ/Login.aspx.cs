using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SocoShop.Common;
using SocoShop.Business;
using SocoShop.Entity;
using SkyCES.EntLib;

namespace SocoShop.Login.QQ
{
    public partial class Login : System.Web.UI.Page
    {
        private string qq_oauth_token_secret_cookiesName = "qq_oauth_token_secret";
        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginConfig loginConfig = new LoginConfig();
            string appKey = loginConfig.AppKey;
            string appSecret = loginConfig.AppSecret;
            if (RequestHelper.GetQueryString<string>("oauth_token") != string.Empty)
            {
                //修正ie8兼容视图取user-agent出错问题
                if (RequestHelper.GetQueryString<string>("time") == string.Empty)
                {
                    ResponseHelper.Write("<script>window.location.href='" + RequestHelper.RawUrl + "&time=1';</script>");
                    ResponseHelper.End();
                }
                string oauth_token = RequestHelper.GetQueryString<string>("oauth_token");
                string oauth_vericode = RequestHelper.GetQueryString<string>("oauth_vericode");
                string error = string.Empty;
                string redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
                try
                {
                    //换取访问令牌
                    string openID = GetAccessToken(oauth_token, oauth_vericode);
                    if (openID != string.Empty)
                    {
                        //如果没有用户添加用户
                        int userID = UserBLL.ReadUserByOpenID(openID).ID;
                        UserInfo user = new UserInfo();
                        if (userID == 0)
                        {
                            if (redirectUrl != string.Empty)
                            {
                                redirectUrl = "&RedirectUrl=" + redirectUrl;
                            }
                            ResponseHelper.Write("<script type=\"text/javascript\">window.location.href='AddUser.aspx?OpenID=" + openID + redirectUrl + "';</script>");
                            ResponseHelper.End();
                        }
                        else
                        {
                            //当前用户登录
                            user = UserBLL.ReadUserMore(userID);
                            UserBLL.UserLoginInit(user);
                            UserBLL.UpdateUserLogin(user.ID, RequestHelper.DateNow, ClientHelper.IP);
                            //页面跳转
                            if (redirectUrl == string.Empty)
                            {
                                redirectUrl = "/User/Index.aspx";
                            }
                            ResponseHelper.Redirect(redirectUrl);
                        }

                    }
                    else
                    {
                        error = "不存在该用户";
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message.ToString();
                }
                //错误处理，返回登录页，并且提示错误
                if (error != string.Empty)
                {
                    if (redirectUrl != string.Empty)
                    {
                        redirectUrl = "&RedirectUrl=" + redirectUrl;
                    }
                    ResponseHelper.Redirect("/User/Login.aspx?Message=" + error + redirectUrl);
                }
            }
            else
            {
                //获取临时令牌
                string oauth_token = GetRequestToken();
                //获取授权令牌
                if (oauth_token != string.Empty)
                {
                    string redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
                    string callBackUrl = "http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Login/QQ/Login.aspx";
                    if (redirectUrl != string.Empty)
                    {
                        callBackUrl += "?RedirectUrl=" + redirectUrl;
                    }
                    ResponseHelper.Redirect("http://openapi.qzone.qq.com/oauth/qzoneoauth_authorize?oauth_consumer_key=" + appKey + "&oauth_token=" + oauth_token + "&oauth_callback=" + Server.UrlEncode(callBackUrl));
                }
            }
        }
        /// <summary>
        /// 获取未授权的临时令牌
        /// </summary>
        private string GetRequestToken()
        {
            LoginConfig loginConfig = new LoginConfig();
            string appKey = loginConfig.AppKey;
            string appSecret = loginConfig.AppSecret;

            string url = "http://openapi.qzone.qq.com/oauth/qzoneoauth_request_token";
            string[] parameters ={
                "oauth_consumer_key="+appKey,
                "oauth_nonce="+this.GenerateNonce(),
                "oauth_timestamp="+this.GenerateTimeStamp(),
                "oauth_version=1.0",
                "oauth_signature_method=HMAC-SHA1",            
                "oauth_client_ip=1"
            };
            //生成签名
            string sign = GenerateSign(url, parameters, "GET", appSecret + "&");
            string tempParameters = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                tempParameters += parameters[i] + "&";
            }
            url = url + "?" + tempParameters + "oauth_signature=" + UrlEncode(sign);
            //请求临时令牌
            string response = HttpHelper.WebRequestGet(url);
            string oauth_token = string.Empty;
            if (response.Length > 0)
            {
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    oauth_token = qs["oauth_token"];
                    CookiesHelper.AddCookie(qq_oauth_token_secret_cookiesName, StringHelper.Encode(qs["oauth_token_secret"], ShopConfig.ReadConfigInfo().SecureKey));
                }
            }
            return oauth_token;
        }
        /// <summary>
        /// 换取访问令牌
        /// </summary>
        private string GetAccessToken(string oauth_token, string oauth_vericode)
        {
            LoginConfig loginConfig = new LoginConfig();
            string appKey = loginConfig.AppKey;
            string appSecret = loginConfig.AppSecret;

            string url = "http://openapi.qzone.qq.com/oauth/qzoneoauth_access_token";
            string[] parameters ={
                "oauth_consumer_key="+appKey,
                "oauth_token="+oauth_token,
                "oauth_nonce="+this.GenerateNonce(),
                "oauth_timestamp="+this.GenerateTimeStamp(),
                "oauth_version=1.0",
                "oauth_signature_method=HMAC-SHA1",   
                "oauth_vericode="+oauth_vericode,   
                "oauth_client_ip=1"
            };
            //生成url
            string oauth_token_secret = StringHelper.Decode(CookiesHelper.ReadCookieValue(qq_oauth_token_secret_cookiesName), ShopConfig.ReadConfigInfo().SecureKey);
            CookiesHelper.DeleteCookie(qq_oauth_token_secret_cookiesName);
            string sign = GenerateSign(url, parameters, "GET", appSecret + "&" + oauth_token_secret);
            string tempParameters = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                tempParameters += parameters[i] + "&";
            }
            url = url + "?" + tempParameters + "oauth_signature=" + UrlEncode(sign);
            //换取访问令牌
            string response = HttpHelper.WebRequestGet(url);
            string openID = string.Empty;
            if (response.Length > 0)
            {
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["openid"] != null)
                {
                    openID = qs["openid"];
                }
            }
            return openID;
        }
        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        private string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        private string GenerateNonce()
        {
            Random random = new Random();
            return random.Next(123400, 9999999).ToString();
        }
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="method"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        private string GenerateSign(string url, string[] parameters, string method, string secret)
        {
            string[] sortParameters = StringHelper.BubbleSortASC(parameters);
            string tempParameters = string.Empty;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (tempParameters == string.Empty)
                {
                    tempParameters = parameters[i];
                }
                else
                {
                    tempParameters += "&" + parameters[i];
                }
            }
            string source = method + "&" + UrlEncode(url) + "&" + UrlEncode(tempParameters);
            return Convert.ToBase64String(StringHelper.HMACSHA1(source, secret));
        }
        /// <summary>
        /// url编码
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        private string UrlEncode(string value)
        {
            string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            StringBuilder result = new StringBuilder();
            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
                }
            }
            return result.ToString();
        }
    }
}
