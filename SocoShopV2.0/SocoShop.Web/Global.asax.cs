namespace SocoShop.Web
{
    using SocoShop.Common;
    using System;
    using System.Web;
    using System.Web.Hosting;

    public partial class Global : HttpApplication
    {
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            ShopConfig.RefreshApp();
            HostingEnvironment.RegisterVirtualPathProvider(new ShopPathProvider());
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
    }
}

