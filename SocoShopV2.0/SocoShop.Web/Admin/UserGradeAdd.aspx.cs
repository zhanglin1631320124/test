namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserGradeAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadUserGrade", PowerCheckType.Single);
                    UserGradeInfo info = UserGradeBLL.ReadUserGradeCache(queryString);
                    this.Name.Text = info.Name;
                    this.MinMoney.Text = info.MinMoney.ToString();
                    this.MaxMoney.Text = info.MaxMoney.ToString();
                    this.Discount.Text = info.Discount.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            UserGradeInfo userGrade = new UserGradeInfo();
            userGrade.ID = RequestHelper.GetQueryString<int>("ID");
            userGrade.Name = this.Name.Text;
            userGrade.MinMoney = Convert.ToDecimal(this.MinMoney.Text);
            userGrade.MaxMoney = Convert.ToDecimal(this.MaxMoney.Text);
            userGrade.Discount = Convert.ToDecimal(this.Discount.Text);
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (userGrade.ID == -2147483648)
            {
                base.CheckAdminPower("AddUserGrade", PowerCheckType.Single);
                int id = UserGradeBLL.AddUserGrade(userGrade);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("UserGrade"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateUserGrade", PowerCheckType.Single);
                UserGradeBLL.UpdateUserGrade(userGrade);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("UserGrade"), userGrade.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

