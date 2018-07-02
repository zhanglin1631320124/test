namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ArticleClassAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.FatherID.DataSource = ArticleClassBLL.ReadArticleClassNamedList();
                this.FatherID.DataTextField = "ClassName";
                this.FatherID.DataValueField = "ID";
                this.FatherID.DataBind();
                this.FatherID.Items.Insert(0, new ListItem("作为最大类", "0"));
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadArticleClass", PowerCheckType.Single);
                    ArticleClassInfo info = ArticleClassBLL.ReadArticleClassCache(queryString);
                    this.FatherID.Text = info.FatherID.ToString();
                    this.OrderID.Text = info.OrderID.ToString();
                    this.ClassName.Text = info.ClassName;
                    this.Description.Text = info.Description;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ArticleClassInfo articleClass = new ArticleClassInfo();
            articleClass.ID = RequestHelper.GetQueryString<int>("ID");
            articleClass.FatherID = Convert.ToInt32(this.FatherID.Text);
            articleClass.OrderID = Convert.ToInt32(this.OrderID.Text);
            articleClass.ClassName = this.ClassName.Text;
            articleClass.IsSystem = 0;
            articleClass.Description = this.Description.Text;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (articleClass.ID == -2147483648)
            {
                base.CheckAdminPower("AddArticleClass", PowerCheckType.Single);
                int id = ArticleClassBLL.AddArticleClass(articleClass);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("ArticleClass"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateArticleClass", PowerCheckType.Single);
                ArticleClassBLL.UpdateArticleClass(articleClass);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("ArticleClass"), articleClass.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

