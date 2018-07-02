namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class CouponSend : AdminBasePage
    {
        protected CouponInfo coupon = new CouponInfo();

        private void CreateOfflineCoupon(int couponID, int sendCount, ref int startNumber)
        {
            if (sendCount > 0)
            {
                for (int i = 0; i < sendCount; i++)
                {
                    startNumber++;
                    UserCouponInfo userCoupon = new UserCouponInfo();
                    userCoupon.CouponID = couponID;
                    userCoupon.GetType = 2;
                    userCoupon.Number = ShopCommon.CreateCouponNo(couponID, startNumber);
                    userCoupon.Password = ShopCommon.CreateCouponPassword(startNumber);
                    userCoupon.IsUse = 0;
                    userCoupon.OrderID = 0;
                    userCoupon.UserID = 0;
                    userCoupon.UserName = string.Empty;
                    UserCouponBLL.AddUserCoupon(userCoupon);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadCoupon", PowerCheckType.Single);
                int queryString = RequestHelper.GetQueryString<int>("CouponID");
                this.coupon = CouponBLL.ReadCoupon(queryString);
            }
        }

        private void SeandUserCoupon(int couponID, string sendUser, ref int startNumber)
        {
            if (sendUser != string.Empty)
            {
                foreach (string str in sendUser.Split(new char[] { ',' }))
                {
                    startNumber++;
                    int num = Convert.ToInt32(str.Split(new char[] { '|' })[0]);
                    string str2 = str.Split(new char[] { '|' })[1];
                    UserCouponInfo userCoupon = new UserCouponInfo();
                    userCoupon.CouponID = couponID;
                    userCoupon.GetType = 1;
                    userCoupon.Number = ShopCommon.CreateCouponNo(couponID, startNumber);
                    userCoupon.Password = ShopCommon.CreateCouponPassword(startNumber);
                    userCoupon.IsUse = 0;
                    userCoupon.OrderID = 0;
                    userCoupon.UserID = num;
                    userCoupon.UserName = str2;
                    UserCouponBLL.AddUserCoupon(userCoupon);
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("SendCoupon", PowerCheckType.Single);
            int sendCount = Convert.ToInt32(this.SendCount.Text);
            int queryString = RequestHelper.GetQueryString<int>("CouponID");
            string form = RequestHelper.GetForm<string>("RelationUser");
            UserCouponInfo info = UserCouponBLL.ReadTopUserCoupon(queryString);
            int startNumber = 0;
            if (info.ID > 0)
            {
                string str2 = info.Number.Substring(3, 5);
                while (str2.Substring(0, 1) == "0")
                {
                    str2 = str2.Substring(1);
                }
                startNumber = Convert.ToInt32(str2);
            }
            this.CreateOfflineCoupon(queryString, sendCount, ref startNumber);
            this.SeandUserCoupon(queryString, form, ref startNumber);
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("SendCoupon"), this.coupon.ID);
            AdminBasePage.Alert(ShopLanguage.ReadLanguage("SendOK"), RequestHelper.RawUrl);
        }
    }
}

