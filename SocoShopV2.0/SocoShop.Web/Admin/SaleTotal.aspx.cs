namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class SaleTotal : AdminBasePage
    {
        protected string queryString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("StatisticsSale", PowerCheckType.Single);
                ShopCommon.BindYearMonth(this.Year, this.Month);
                int queryString = RequestHelper.GetQueryString<int>("Year");
                queryString = (queryString == -2147483648) ? DateTime.Now.Year : queryString;
                int num2 = RequestHelper.GetQueryString<int>("Month");
                this.Year.Text = queryString.ToString();
                this.Month.Text = num2.ToString();
                this.queryString = "?Date=" + queryString.ToString() + "|" + num2.ToString();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect(("SaleTotal.aspx?Action=search&" + "Year=" + this.Year.Text + "&") + "Month=" + this.Month.Text);
        }
    }
}

