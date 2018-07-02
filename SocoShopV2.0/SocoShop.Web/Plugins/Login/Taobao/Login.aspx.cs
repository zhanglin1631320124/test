using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;
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

namespace SocoShop.Login.Taobao
{
    public partial class Login : System.Web.UI.Page
    {
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
            if (RequestHelper.GetQueryString<string>("code") != string.Empty || RequestHelper.GetQueryString<string>("error") != string.Empty)
            {
                string code = RequestHelper.GetQueryString<string>("code");
                string redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
                string error = string.Empty;
                try
                {
                    //获取访问令牌
                    string url = "https://oauth.taobao.com/token";
                    string postData = "grant_type=authorization_code&code=" + code + "&redirect_uri=http://" + Request.ServerVariables["Http_Host"] + "&client_id=" + appKey + "&client_secret=" + appSecret;
                    string returnData = HttpHelper.WebRequestPost(url, postData);
                    AccessData accessData = (AccessData)JavaScriptConvert.DeserializeObject(returnData, typeof(AccessData));
                    string access_token = accessData.access_token;
                    //获取用户数据
                    string userURL = "http://gw.api.taobao.com/router/rest";
                    string[] parameters ={
                        "method=taobao.user.get",
                        "session="+access_token,
                        "timestamp="+RequestHelper.DateNow.ToString("yyyy-MM-dd HH:mm:ss"),
                        "app_key="+appKey,
                        "v=2.0",
                        "sign_method=md5",
                        "fields=nick,email"
                     };
                    string[] sortParameters = StringHelper.BubbleSortASC(parameters);
                    string strParameters = string.Empty;
                    string sign = string.Empty;
                    for (int i = 0; i < sortParameters.Length; i++)
                    {
                        sign += sortParameters[i].Replace("=", string.Empty);
                        strParameters += "&" + sortParameters[i];
                    }
                    sign = FormsAuthentication.HashPasswordForStoringInConfigFile(appSecret + sign + appSecret, "MD5");
                    string postUserData = "sign=" + sign + strParameters;
                    string returnUserData = HttpHelper.WebRequestPost(userURL, postUserData);
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(returnUserData);
                    string taobaoUserName = xd.SelectSingleNode("user_get_response/user/nick").InnerText;
                    string taobaoUserEmail = xd.SelectSingleNode("user_get_response/user/email").InnerText;
                    //如果没有用户添加用户
                    string openID = "taobao-" + taobaoUserName;//淘宝的关键字，参照Login.Config的Key的value值                    
                    int userID = UserBLL.ReadUserByOpenID(openID).ID;
                    UserInfo user = new UserInfo();
                    if (userID == 0)
                    {
                        user.UserName = openID;
                        user.UserPassword = StringHelper.Password(Guid.NewGuid().ToString(), (PasswordType)ShopConfig.ReadConfigInfo().PasswordType);
                        user.Email = taobaoUserEmail;
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
                    if (!redirectUrl.StartsWith("http") && !redirectUrl.StartsWith("wwww"))
                    {
                        redirectUrl = "http://" + Request.ServerVariables["Http_Host"] + "/" + redirectUrl;
                    }
                    //淘宝用户退出
                    ResponseHelper.Redirect("https://oauth.taobao.com/logoff?client_id=" + appKey + "&redirect_uri=" + redirectUrl);

                }
                catch (Exception ex)
                {
                    error = ex.Message.ToString();
                }
                if (error == string.Empty)
                {
                    error = RequestHelper.GetQueryString<string>("error");
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
                string redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
                string callBackUrl = "http://" + Request.ServerVariables["Http_Host"] + "/Plugins/Login/Taobao/Login.aspx";
                if (redirectUrl != string.Empty)
                {
                    callBackUrl += "?RedirectUrl=" + redirectUrl;
                }
                string url = "https://oauth.taobao.com/authorize?response_type=code&client_id=" + appKey + "&redirect_uri=" + callBackUrl;
                ResponseHelper.Redirect(url);
            }
        }
    }
}
