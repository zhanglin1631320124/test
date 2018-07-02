namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class CouponAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadCoupon", PowerCheckType.Single);
                    CouponInfo info = CouponBLL.ReadCoupon(queryString);
                    this.Name.Text = info.Name;
                    this.Money.Text = info.Money.ToString();
                    this.UseMinAmount.Text = info.UseMinAmount.ToString();
                    this.UseStartDate.Text = info.UseStartDate.ToString("yyyy-MM-dd");
                    this.UseEndDate.Text = info.UseEndDate.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            CouponInfo coupon = new CouponInfo();
            coupon.ID = RequestHelper.GetQueryString<int>("ID");
            coupon.Name = this.Name.Text;
            coupon.Money = Convert.ToDecimal(this.Money.Text);
            coupon.UseMinAmount = Convert.ToDecimal(this.UseMinAmount.Text);
            coupon.UseStartDate = Convert.ToDateTime(this.UseStartDate.Text);
            coupon.UseEndDate = Convert.ToDateTime(this.UseEndDate.Text).AddDays(1.0).AddSeconds(-1.0);
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (coupon.ID == -2147483648)
            {
                base.CheckAdminPower("AddCoupon", PowerCheckType.Single);
                coupon.ID = CouponBLL.AddCoupon(coupon);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Coupon"), coupon.ID);
            }
            else
            {
                base.CheckAdminPower("UpdateCoupon", PowerCheckType.Single);
                CouponBLL.UpdateCoupon(coupon);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Coupon"), coupon.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

