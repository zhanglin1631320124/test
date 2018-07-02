namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ProductClassAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.FatherID.DataSource = ProductClassBLL.ReadProductClassNamedList();
                this.FatherID.DataTextField = "ClassName";
                this.FatherID.DataValueField = "ID";
                this.FatherID.DataBind();
                this.FatherID.Items.Insert(0, new ListItem("作为最大类", "0"));
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadProductClass", PowerCheckType.Single);
                    ProductClassInfo info = ProductClassBLL.ReadProductClassCache(queryString);
                    this.FatherID.Text = info.FatherID.ToString();
                    this.OrderID.Text = info.OrderID.ToString();
                    this.ClassName.Text = info.ClassName;
                    this.Keywords.Text = info.Keywords;
                    this.Description.Text = info.Description;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ProductClassInfo productClass = new ProductClassInfo();
            productClass.ID = RequestHelper.GetQueryString<int>("ID");
            productClass.FatherID = Convert.ToInt32(this.FatherID.Text);
            productClass.OrderID = Convert.ToInt32(this.OrderID.Text);
            productClass.ClassName = this.ClassName.Text;
            productClass.Keywords = this.Keywords.Text;
            productClass.Description = this.Description.Text;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (productClass.ID == -2147483648)
            {
                base.CheckAdminPower("AddProductClass", PowerCheckType.Single);
                int id = ProductClassBLL.AddProductClass(productClass);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("ProductClass"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateProductClass", PowerCheckType.Single);
                ProductClassBLL.UpdateProductClass(productClass);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("ProductClass"), productClass.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

