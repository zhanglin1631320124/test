namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;

    public partial class Template : AdminBasePage
    {
        protected List<TemplatePluginsInfo> templatePluginsList = new List<TemplatePluginsInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("Template", PowerCheckType.Single);
                string queryString = RequestHelper.GetQueryString<string>("Action");
                string str2 = RequestHelper.GetQueryString<string>("Path");
                if (queryString == "Active" && str2 != string.Empty)
                {
                    ShopConfigInfo config = ShopConfig.ReadConfigInfo();
                    config.TemplatePath = str2;
                    ShopConfig.UpdateConfigInfo(config);
                    ScriptHelper.Alert("启用成功", "Template.aspx");
                }
                this.templatePluginsList = TemplatePlugins.ReadTemplatePluginsList();
            }
        }
    }
}

