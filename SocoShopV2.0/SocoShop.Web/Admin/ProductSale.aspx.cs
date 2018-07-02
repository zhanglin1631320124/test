namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ProductSale : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("StatisticsProduct", PowerCheckType.Single);
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
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.IsSale = 1;
                productSearch.Name = RequestHelper.GetQueryString<string>("Name");
                productSearch.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                productSearch.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                string queryString = RequestHelper.GetQueryString<string>("ProductOrderType");
                queryString = (queryString == string.Empty) ? "SellCount" : queryString;
                productSearch.ProductOrderType = queryString;
                this.ClassID.Text = RequestHelper.GetQueryString<string>("ClassID");
                this.BrandID.Text = RequestHelper.GetQueryString<string>("BrandID");
                this.Name.Text = RequestHelper.GetQueryString<string>("Name");
                this.StartDate.Text = RequestHelper.GetQueryString<string>("StartDate");
                this.EndDate.Text = RequestHelper.GetQueryString<string>("EndDate");
                this.ProductOrderType.Text = queryString;
                DateTime startDate = RequestHelper.GetQueryString<DateTime>("StartDate");
                DateTime endDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndDate"));
                base.BindControl(ProductBLL.StatisticsProductSale(base.CurrentPage, base.PageSize, productSearch, ref this.Count, startDate, endDate), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect(((((("ProductSale.aspx?Action=search&" + "Name=" + this.Name.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text + "&") + "StartDate=" + this.StartDate.Text + "&") + "EndDate=" + this.EndDate.Text + "&") + "ProductOrderType=" + this.ProductOrderType.Text);
        }
    }
}

