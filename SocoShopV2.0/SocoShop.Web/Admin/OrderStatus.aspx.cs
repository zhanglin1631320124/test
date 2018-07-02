namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class OrderStatus : AdminBasePage
    {
        protected string result = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("StatisticsOrder", PowerCheckType.Single);
                OrderSearchInfo orderSearch = new OrderSearchInfo();
                orderSearch.StartAddDate = RequestHelper.GetQueryString<DateTime>("StartAddDate");
                orderSearch.EndAddDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndAddDate"));
                this.StartAddDate.Text = RequestHelper.GetQueryString<string>("StartAddDate");
                this.EndAddDate.Text = RequestHelper.GetQueryString<string>("EndAddDate");
                DataTable table = OrderBLL.StatisticsOrderStatus(orderSearch);
                string[] strArray = new string[] { "33FF66", "FF6600", "FFCC33", "CC3399", "CC7036", "349802", "066C93" };
                int index = 0;
                bool flag = false;
                foreach (EnumInfo info2 in EnumHelper.ReadEnumList<OrderStatus>())
                {
                    flag = false;
                    foreach (DataRow row in table.Rows)
                    {
                        if (Convert.ToInt16(row["OrderStatus"]) == info2.Value)
                        {
                            object result = this.result;
                            this.result = string.Concat(new object[] { result, " <set value='", row["Count"], "' name='", info2.ChineseName, "' color='", strArray[index], "' />" });
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        string str = this.result;
                        this.result = str + " <set value='0' name='" + info2.ChineseName + "' color='" + strArray[index] + "' />";
                    }
                    index++;
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect(("OrderStatus.aspx?Action=search&" + "StartAddDate=" + this.StartAddDate.Text + "&") + "EndAddDate=" + this.EndAddDate.Text);
        }
    }
}

