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
using SocoShop.Page;

namespace SocoShop.Login.QQ
{
    public partial class AddUser : PluginsBasePage
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        protected string errorMessage = string.Empty;
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            errorMessage = RequestHelper.GetQueryString<string>("ErrorMessage");
        }
        /// <summary>
        /// 提交按钮点击方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string userName = StringHelper.SearchSafe(StringHelper.AddSafe(RequestHelper.GetForm<string>("UserName")));
            string redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
            string openID = RequestHelper.GetQueryString<string>("OpenID");
            //检查用户名
            if (userName == string.Empty)
            {
                errorMessage = "用户名不能为空";
            }
            if (errorMessage == string.Empty)
            {
                string forbiddinName = ShopConfig.ReadConfigInfo().ForbiddenName;
                if (forbiddinName != string.Empty)
                {
                    foreach (string TempName in forbiddinName.Split('|'))
                    {
                        if (userName.IndexOf(TempName.Trim()) != -1)
                        {
                            errorMessage = "用户名含有非法字符";
                            break;
                        }
                    }
                }
            }
            if (errorMessage == string.Empty)
            {
                if (UserBLL.CheckUserName(userName) > 0)
                {
                    errorMessage = "用户名已经被占用";
                }
            }
            //注册用户
            if (errorMessage == string.Empty)
            {
                UserInfo user = new UserInfo();
                user.UserName = userName;
                user.UserPassword = StringHelper.Password(Guid.NewGuid().ToString(), (PasswordType)ShopConfig.ReadConfigInfo().PasswordType);
                user.Email = "";
                user.RegisterIP = ClientHelper.IP;
                user.RegisterDate = RequestHelper.DateNow;
                user.LastLoginIP = ClientHelper.IP;
                user.LastLoginDate = RequestHelper.DateNow;
                user.FindDate = RequestHelper.DateNow;
                user.Status = (int)UserStatus.Normal;
                user.OpenID = openID;
                int userID = UserBLL.AddUser(user);
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
                ResponseHelper.Redirect("AddUser.aspx?ErrorMessage=" + Server.UrlEncode(errorMessage) + "&RedirectUrl=" + redirectUrl + "&OpenID=" + openID);
            }
        }
    }
}
