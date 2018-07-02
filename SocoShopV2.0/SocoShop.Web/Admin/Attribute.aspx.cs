namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Attribute : AdminBasePage
    {
        protected int attributeClassIDAspx = 0;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteAttribute", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                AttributeBLL.DeleteAttribute(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Attribute"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadAttribute", PowerCheckType.Single);
                this.AttributeClass.DataSource = AttributeClassBLL.ReadAttributeClassCacheList();
                this.AttributeClass.DataTextField = "Name";
                this.AttributeClass.DataValueField = "ID";
                this.AttributeClass.DataBind();
                this.InputType.DataSource = EnumHelper.ReadEnumList<SocoShop.Entity.InputType>();
                this.InputType.DataTextField = "ChineseName";
                this.InputType.DataValueField = "Value";
                this.InputType.DataBind();
                this.InputType.Items[0].Selected = true;
                string queryString = RequestHelper.GetQueryString<string>("Action");
                int id = RequestHelper.GetQueryString<int>("ID");
                if (queryString != string.Empty && id > 0)
                {
                    if (queryString == "Up")
                        AttributeBLL.ChangeAttributeOrder(ChangeAction.Up, id);
                    else
                        AttributeBLL.ChangeAttributeOrder(ChangeAction.Down, id);
                }
                if (id != -2147483648)
                {
                    AttributeInfo info = AttributeBLL.ReadAttributeCache(id);
                    this.Name.Text = info.Name;
                    this.InputType.Text = info.InputType.ToString();
                    this.InputValue.Text = info.InputValue;
                }
                this.attributeClassIDAspx = RequestHelper.GetQueryString<int>("AttributeClassID");
                this.AttributeClass.SelectedValue = this.attributeClassIDAspx.ToString();
                base.BindControl(AttributeBLL.ReadAttributeListByClassID(this.attributeClassIDAspx), this.RecordList);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            AttributeInfo attribute = new AttributeInfo();
            attribute.ID = RequestHelper.GetQueryString<int>("ID");
            attribute.Name = this.Name.Text;
            attribute.AttributeClassID = RequestHelper.GetQueryString<int>("AttributeClassID");
            attribute.InputType = Convert.ToInt32(this.InputType.Text);
            attribute.InputValue = this.InputValue.Text;
            string message = ShopLanguage.ReadLanguage("AddOK");
            if (attribute.ID == -2147483648)
            {
                base.CheckAdminPower("AddAttribute", PowerCheckType.Single);
                int id = AttributeBLL.AddAttribute(attribute);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Attribute"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateAttribute", PowerCheckType.Single);
                AttributeBLL.UpdateAttribute(attribute);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Attribute"), attribute.ID);
                message = ShopLanguage.ReadLanguage("UpdateOK");
            }
            ScriptHelper.Alert(message, RequestHelper.RawUrl);
        }
    }
}

