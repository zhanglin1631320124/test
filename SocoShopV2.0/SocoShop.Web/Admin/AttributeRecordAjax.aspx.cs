namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using SocoShop.Entity;

    public partial class AttributeRecordAjax : AdminBasePage
    {
        protected List<AttributeInfo> attributeList = new List<AttributeInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ClearCache();
            int queryString = RequestHelper.GetQueryString<int>("AttributeClassID");
            int productID = RequestHelper.GetQueryString<int>("ProductID");
            this.attributeList = AttributeBLL.JoinAttribute(queryString, productID);
        }
    }
}

