namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserCouponAjax : UserAjaxBasePage
    {
        protected string action = string.Empty;
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected List<CouponInfo> couponList = new List<CouponInfo>();
        protected List<UserCouponInfo> userCouponList = new List<UserCouponInfo>();

        protected void AddUserCoupon()
        {
            string content = string.Empty;
            string number = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("Number"));
            string password = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("Password"));
            if (number == string.Empty || password == string.Empty)
                content = "请填写卡号和密码";
            else
            {
                UserCouponInfo userCoupon = UserCouponBLL.ReadUserCouponByNumber(number, password);
                if (userCoupon.ID == 0)
                    content = "卡号或者密码错误";
                else if (userCoupon.UserID > 0)
                    content = "该优惠券已经绑定了用户";
                else
                {
                    userCoupon.UserID = base.UserID;
                    userCoupon.UserName = base.UserName;
                    UserCouponBLL.UpdateUserCoupon(userCoupon);
                }
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            this.action = RequestHelper.GetQueryString<string>("Action");
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 20;
            int count = 0;
            string action = this.action;
            if (action != null)
            {
                if (!(action == "NotUse") && !(action == "HasUse"))
                {
                    if (action != "AddCoupon" && action == "AddUserCoupon") this.AddUserCoupon();
                }
                else
                {
                    UserCouponSearchInfo userCoupon = new UserCouponSearchInfo();
                    userCoupon.UserID = base.UserID;
                    if (this.action == "NotUse")
                        userCoupon.IsUse = 0;
                    else
                        userCoupon.IsUse = 1;
                    this.userCouponList = UserCouponBLL.SearchUserCouponList(queryString, pageSize, userCoupon, ref count);
                    this.ajaxPagerClass.CurrentPage = queryString;
                    this.ajaxPagerClass.PageSize = pageSize;
                    this.ajaxPagerClass.Count = count;
                    string str = string.Empty;
                    foreach (UserCouponInfo info2 in this.userCouponList)
                    {
                        if (str == string.Empty)
                            str = info2.CouponID.ToString();
                        else
                            str = str + "," + info2.CouponID.ToString();
                    }
                    if (str != string.Empty)
                    {
                        CouponSearchInfo coupon = new CouponSearchInfo();
                        coupon.InCouponID = str;
                        this.couponList = CouponBLL.SearchCouponList(coupon);
                    }
                }
            }
        }
    }
}

