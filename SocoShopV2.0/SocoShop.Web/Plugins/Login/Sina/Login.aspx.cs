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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SocoShop.Login.Sina
{
    public partial class Login : System.Web.UI.Page
    {
        private string sina_oauth_token_secret_cookiesName = "sina_oauth_token_secret";
        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            oAuthSina oauth = new oAuthSina();
            if (RequestHelper.GetQueryString<string>("oauth_verifier") != string.Empty)
            {
                string error = string.Empty;
                string redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
                oauth.token =RequestHelper.GetQueryString<string>("oauth_token");
                oauth.tokenSecret = StringHelper.Decode(CookiesHelper.ReadCookieValue(sina_oauth_token_secret_cookiesName),ShopConfig.ReadConfigInfo().SecureKey);
                oauth.Verifier = RequestHelper.GetQueryString<string>("oauth_verifier");   
                try
                {
                    //换取访问令牌
                    int sinaUserID = oauth.AccessTokenGet();
                    if (oauth.token != string.Empty && sinaUserID > 0)
                    {
                        string openID = "sina-" + oauth.GetUserName(sinaUserID);
                        //如果没有用户添加用户
                        int userID = UserBLL.ReadUserByOpenID(openID).ID;
                        UserInfo user = new UserInfo();
                        if (userID == 0)
                        {
                            user.UserName = openID;
                            user.UserPassword = StringHelper.Password(Guid.NewGuid().ToString(), (PasswordType)ShopConfig.ReadConfigInfo().PasswordType);
                            user.Email = "";
                            user.RegisterIP = ClientHelper.IP;
                            user.RegisterDate = RequestHelper.DateNow;
                            user.LastLoginIP = ClientHelper.IP;
                            user.LastLoginDate = RequestHelper.DateNow;
                            user.FindDate = RequestHelper.DateNow;
                            user.Status = (int)UserStatus.Normal;
                            user.OpenID = openID;
                            userID = UserBLL.AddUser(user);
                        }
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
                oauth.RequestTokenGet();
                string url = oauth.AuthorizationGet();
                CookiesHelper.AddCookie(sina_oauth_token_secret_cookiesName, StringHelper.Encode(oauth.tokenSecret, ShopConfig.ReadConfigInfo().SecureKey));
                //获取授权令牌
                string redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
                string callBackUrl = "http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Login/Sina/Login.aspx";
                if (redirectUrl != string.Empty)
                {
                    callBackUrl += "?RedirectUrl=" + redirectUrl;
                }
                ResponseHelper.Redirect(url + "&oauth_callback=" + callBackUrl);
            }
        }
    }
}
