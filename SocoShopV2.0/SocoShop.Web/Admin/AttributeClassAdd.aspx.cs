namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class AttributeClassAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadAttributeClass", PowerCheckType.Single);
                    AttributeClassInfo info = AttributeClassBLL.ReadAttributeClassCache(queryString);
                    this.Name.Text = info.Name;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            AttributeClassInfo attributeClass = new AttributeClassInfo();
            attributeClass.ID = RequestHelper.GetQueryString<int>("ID");
            attributeClass.Name = this.Name.Text;
            attributeClass.AttributeCount = 0;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (attributeClass.ID == -2147483648)
            {
                base.CheckAdminPower("AddAttributeClass", PowerCheckType.Single);
                int id = AttributeClassBLL.AddAttributeClass(attributeClass);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("AttributeClass"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateAttributeClass", PowerCheckType.Single);
                AttributeClassBLL.UpdateAttributeClass(attributeClass);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("AttributeClass"), attributeClass.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

