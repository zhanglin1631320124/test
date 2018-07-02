namespace SocoShop.Page
{
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Index : UserBasePage
    {
        protected DataTable dt = new DataTable();
        protected List<OrderInfo> orderList = new List<OrderInfo>();
        protected UserInfo user = new UserInfo();
        protected string userGradeName = string.Empty;

        protected override void PageLoad()
        {
            base.PageLoad();
            this.user = UserBLL.ReadUserMore(base.UserID);
            this.userGradeName = UserGradeBLL.ReadUserGradeCache(base.GradeID).Name;
            this.dt = UserBLL.UserIndexStatistics(base.UserID);
            OrderSearchInfo order = new OrderSearchInfo();
            order.UserID = base.UserID;
            int count = -2147483648;
            this.orderList = OrderBLL.SearchOrderList(1, 10, order, ref count);
        }
    }
}

