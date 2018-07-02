namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class StandardAdd : AdminBasePage
    {
        protected StandardInfo standard = new StandardInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.DisplayTye.DataSource = EnumHelper.ReadEnumList<SocoShop.Entity.DisplayTye>();
                this.DisplayTye.DataTextField = "ChineseName";
                this.DisplayTye.DataValueField = "Value";
                this.DisplayTye.DataBind();
                this.DisplayTye.SelectedIndex = 0;
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadStandard", PowerCheckType.Single);
                    this.standard = StandardBLL.ReadStandardCache(queryString);
                    this.Name.Text = this.standard.Name;
                    this.DisplayTye.Text = this.standard.DisplayTye.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            StandardInfo standard = new StandardInfo();
            standard.ID = RequestHelper.GetQueryString<int>("ID");
            standard.Name = this.Name.Text;
            standard.DisplayTye = Convert.ToInt32(this.DisplayTye.Text);
            standard.ValueList = RequestHelper.GetForm<string>("ValueList");
            standard.PhotoList = RequestHelper.GetForm<string>("PhotoList");
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (standard.ID == -2147483648)
            {
                base.CheckAdminPower("AddStandard", PowerCheckType.Single);
                int id = StandardBLL.AddStandard(standard);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Standard"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateStandard", PowerCheckType.Single);
                StandardBLL.UpdateStandard(standard);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Standard"), standard.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

