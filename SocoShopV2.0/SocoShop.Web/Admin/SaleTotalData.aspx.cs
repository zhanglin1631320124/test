namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public partial class SaleTotalData : AdminBasePage
    {
        protected int days = 0;
        protected int month = 0;
        protected Dictionary<int, int> orderCountDic = new Dictionary<int, int>();
        protected Dictionary<int, decimal> orderMoneyDic = new Dictionary<int, decimal>();
        protected int year = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string queryString = RequestHelper.GetQueryString<string>("Date");
            this.year = Convert.ToInt32(queryString.Split(new char[] { '|' })[0]);
            this.month = Convert.ToInt32(queryString.Split(new char[] { '|' })[1]);
            OrderSearchInfo orderSearch = new OrderSearchInfo();
            DateType day = DateType.Day;
            if (this.month == -2147483648)
            {
                day = DateType.Month;
                orderSearch.StartAddDate = Convert.ToDateTime(this.year + "-01-01");
                orderSearch.EndAddDate = Convert.ToDateTime(this.year + "-01-01").AddYears(1);
            }
            else
            {
                this.days = ShopCommon.CountMonthDays(this.year, this.month);
                orderSearch.StartAddDate = Convert.ToDateTime(string.Concat(new object[] { this.year, "-", this.month, "-01" }));
                orderSearch.EndAddDate = Convert.ToDateTime(string.Concat(new object[] { this.year, "-", this.month, "-01" })).AddMonths(1);
            }
            DataTable table = OrderBLL.StatisticsSaleTotal(orderSearch, day);
            foreach (DataRow row in table.Rows)
            {
                this.orderCountDic.Add(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]));
                this.orderMoneyDic.Add(Convert.ToInt32(row[0]), Convert.ToDecimal(row[2]));
            }
        }
    }
}

