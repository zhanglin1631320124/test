namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class SaleDetail : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("StatisticsSale", PowerCheckType.Single);
                foreach (ProductClassInfo info in ProductClassBLL.ReadProductClassNamedList())
                {
                    this.ClassID.Items.Add(new ListItem(info.ClassName, "|" + info.ID + "|"));
                }
                this.ClassID.Items.Insert(0, new ListItem("所有分类", string.Empty));
                this.BrandID.DataSource = ProductBrandBLL.ReadProductBrandCacheList();
                this.BrandID.DataTextField = "Name";
                this.BrandID.DataValueField = "ID";
                this.BrandID.DataBind();
                this.BrandID.Items.Insert(0, new ListItem("所有品牌", string.Empty));
                this.ClassID.Text = RequestHelper.GetQueryString<string>("ClassID");
                this.BrandID.Text = RequestHelper.GetQueryString<string>("BrandID");
                this.Name.Text = RequestHelper.GetQueryString<string>("Name");
                this.StartAddDate.Text = RequestHelper.GetQueryString<string>("StartAddDate");
                this.EndAddDate.Text = RequestHelper.GetQueryString<string>("EndAddDate");
                this.UserName.Text = RequestHelper.GetQueryString<string>("UserName");
                this.OrderNumber.Text = RequestHelper.GetQueryString<string>("OrderNumber");
                ProductSearchInfo productSearch = new ProductSearchInfo();
                OrderSearchInfo orderSearch = new OrderSearchInfo();
                productSearch.IsSale = 1;
                productSearch.Name = RequestHelper.GetQueryString<string>("Name");
                productSearch.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                productSearch.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                productSearch.InProductID = RequestHelper.GetQueryString<string>("ProductID");
                orderSearch.StartAddDate = RequestHelper.GetQueryString<DateTime>("StartAddDate");
                orderSearch.EndAddDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndAddDate"));
                orderSearch.UserName = RequestHelper.GetQueryString<string>("UserName");
                orderSearch.OrderNumber = RequestHelper.GetQueryString<string>("OrderNumber");
                base.BindControl(OrderDetailBLL.StatisticsSaleDetail(base.CurrentPage, base.PageSize, orderSearch, productSearch, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((((("SaleDetail.aspx?Action=search&" + "Name=" + this.Name.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text + "&") + "OrderNumber=" + this.OrderNumber.Text + "&") + "UserName=" + this.UserName.Text + "&") + "StartAddDate=" + this.StartAddDate.Text + "&") + "EndAddDate=" + this.EndAddDate.Text);
        }
    }
}

