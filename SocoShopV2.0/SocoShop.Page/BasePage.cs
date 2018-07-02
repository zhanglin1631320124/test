namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using System;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;

    public class BasePage : IHttpHandler, IRequiresSessionState
    {
        private HttpContext context;
        protected int GradeID = 0;
        protected decimal MoneyUsed = 0M;
        private bool needUserCookies = true;
        protected double processTime = 0.0;
        private HttpRequest request;
        private HttpResponse response;
        private HttpServerUtility server;
        private HttpSessionState session;
        protected int UserID = 0;
        protected string UserName = string.Empty;

        protected void CheckUserLogin()
        {
            if (this.UserID == 0)
            {
                ResponseHelper.Write("<script language='javascript'>window.location.href='/User/Login.aspx';</script>");
                ResponseHelper.End();
            }
        }

        private void PageInit(HttpContext context)
        {
            this.request = context.Request;
            this.server = context.Server;
            this.response = context.Response;
            this.session = context.Session;
            this.context = context;
            if (this.needUserCookies) this.ReadUserCookies();
        }

        protected virtual void PageLoad()
        {
        }

        protected virtual void PostBack()
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            this.PageInit(context);
            if (RequestHelper.GetForm<string>("Action") == "PostBack")
                this.PostBack();
            else
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                this.PageLoad();
                stopwatch.Stop();
                this.processTime = stopwatch.Elapsed.TotalSeconds;
                this.ShowPage();
            }
        }

        private void ReadUserCookies()
        {
            string userCookies = ShopConfig.ReadConfigInfo().UserCookies;
            string str2 = CookiesHelper.ReadCookieValue(userCookies);
            if (str2 != string.Empty)
            {
                try
                {
                    string[] strArray = str2.Split(new char[] { '|' });
                    string str3 = strArray[0];
                    string str4 = strArray[1];
                    string s = strArray[2];
                    string str6 = strArray[3];
                    string str7 = strArray[4];
                    if (FormsAuthentication.HashPasswordForStoringInConfigFile(str4 + s + str6.ToString() + str7.ToString() + ShopConfig.ReadConfigInfo().SecureKey + ClientHelper.Agent, "MD5").ToLower() == str3.ToLower())
                    {
                        this.UserID = Convert.ToInt32(str4);
                        this.UserName = HttpContext.Current.Server.UrlDecode(s);
                        this.MoneyUsed = Convert.ToDecimal(str6);
                        this.GradeID = Convert.ToInt32(str7);
                    }
                    else
                        CookiesHelper.DeleteCookie(userCookies);
                }
                catch
                {
                    CookiesHelper.DeleteCookie(userCookies);
                }
            }
            if (this.GradeID == 0) this.GradeID = UserGradeBLL.ReadUserGradeByMoney(0M).ID;
        }

        protected virtual void ShowPage()
        {
        }

        public HttpContext Context
        {
            get
            {
                return this.context;
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public bool NeedUserCookies
        {
            set
            {
                this.needUserCookies = value;
            }
        }

        public HttpRequest Request
        {
            get
            {
                return this.request;
            }
        }

        public HttpResponse Response
        {
            get
            {
                return this.response;
            }
        }

        public HttpServerUtility Server
        {
            get
            {
                return this.server;
            }
        }

        public HttpSessionState Session
        {
            get
            {
                return this.session;
            }
        }
    }
}

