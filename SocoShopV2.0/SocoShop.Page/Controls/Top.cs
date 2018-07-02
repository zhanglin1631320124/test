namespace SocoShop.Page.Controls
{
    using SocoShop.Business;
    using SocoShop.Common;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using SocoShop.Entity;

    public class Top : UserControl
    {
        protected List<ProductClassInfo> allProductClassList = new List<ProductClassInfo>();
        protected string hotKeyword = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.allProductClassList = ProductClassBLL.ReadProductClassNamedList();
            this.hotKeyword = ShopConfig.ReadConfigInfo().HotKeyword;
        }
    }
}

