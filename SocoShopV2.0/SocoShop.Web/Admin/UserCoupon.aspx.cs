namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class UserCoupon : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteUserCoupon", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                UserCouponBLL.DeleteUserCoupon(intsForm, 0);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("UserCoupon"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void ExportButton_Click(object sender, EventArgs e)
        {
            UserCouponSearchInfo userCoupon = new UserCouponSearchInfo();
            userCoupon.CouponID = RequestHelper.GetQueryString<int>("CouponID");
            userCoupon.GetType = RequestHelper.GetQueryString<int>("GetType");
            userCoupon.Number = RequestHelper.GetQueryString<string>("Number");
            userCoupon.IsUse = RequestHelper.GetQueryString<int>("IsUse");
            List<UserCouponInfo> list = UserCouponBLL.SearchUserCouponList(userCoupon);
            StringBuilder builder = new StringBuilder();
            builder.Append("ID\t优惠券编号\t密码\n");
            foreach (UserCouponInfo info2 in list)
            {
                builder.Append(info2.ID.ToString() + "\t#" + info2.Number + "\t" + info2.Password + "\n");
            }
            base.Response.Clear();
            base.Response.Buffer = true;
            base.Response.Charset = "GB2312";
            base.Response.AppendHeader("Content-Disposition", "attachment;filename=userCoupon.xls");
            base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            base.Response.ContentType = "application/ms-excel";
            base.Response.Write(builder.ToString());
            base.Response.End();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadUserCoupon", PowerCheckType.Single);
                UserCouponSearchInfo userCoupon = new UserCouponSearchInfo();
                userCoupon.CouponID = RequestHelper.GetQueryString<int>("CouponID");
                userCoupon.GetType = RequestHelper.GetQueryString<int>("GetType");
                userCoupon.Number = RequestHelper.GetQueryString<string>("Number");
                userCoupon.IsUse = RequestHelper.GetQueryString<int>("IsUse");
                this.ddlGetType.Text = RequestHelper.GetQueryString<string>("GetType");
                this.Number.Text = RequestHelper.GetQueryString<string>("Number");
                this.IsUse.Text = RequestHelper.GetQueryString<string>("IsUse");
                base.PageSize = 12;
                base.BindControl(UserCouponBLL.SearchUserCouponList(base.CurrentPage, base.PageSize, userCoupon, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected string ReadOrderLink(int orderID)
        {
            if (orderID > 0) return ("<a href=\"OrderDetail.aspx?ID=" + orderID + "\" target=\"_blank\" >查看订单</a>");
            return "未消费";
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string str = "UserCoupon.aspx?Action=search&";
            object obj2 = str;
            ResponseHelper.Redirect(((string.Concat(new object[] { obj2, "CouponID=", RequestHelper.GetQueryString<int>("CouponID"), "&" }) + "GetType=" + ddlGetType.Text + "&") + "Number=" + this.Number.Text + "&") + "IsUse=" + this.IsUse.Text);
        }
    }
}

