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

    public partial class OrderArea : AdminBasePage
    {
        protected string result = string.Empty;

        protected string GetProvinceName(string regionID)
        {
            string regionName = string.Empty;
            if (regionID != string.Empty)
            {
                string[] strArray = regionID.Split(new char[] { '|' });
                if (strArray.Length < 4) return regionName;
                try
                {
                    regionName = RegionBLL.ReadRegionCache(Convert.ToInt32(strArray[2])).RegionName;
                }
                catch (Exception)
                {
                }
            }
            return regionName;
        }

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
                DataTable table = OrderBLL.StatisticsOrderArea(orderSearch);
                string[] strArray = new string[] { "33FF66", "FF6600", "FFCC33", "CC3399", "CC7036", "349802", "066C93" };
                int index = 0;
                foreach (DataRow row in table.Rows)
                {
                    object result = this.result;
                    this.result = string.Concat(new object[] { result, " <set value='", row["Count"], "' name='", this.GetProvinceName(row["RegionID"].ToString()), "' color='", strArray[index], "' />" });
                    index++;
                    if (index == 6) index = 0;
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect(("OrderArea.aspx?Action=search&" + "StartAddDate=" + this.StartAddDate.Text + "&") + "EndAddDate=" + this.EndAddDate.Text);
        }
    }
}

