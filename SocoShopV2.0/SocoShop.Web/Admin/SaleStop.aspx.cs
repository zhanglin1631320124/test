namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class SaleStop : AdminBasePage
    {
        protected DataTable dt = new DataTable();
        protected List<ProductInfo> productList = new List<ProductInfo>();

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
                ProductSearchInfo product = new ProductSearchInfo();
                product.IsSale = 1;
                product.Name = RequestHelper.GetQueryString<string>("Name");
                product.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                product.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                this.productList = ProductBLL.SearchProductList(base.CurrentPage, base.PageSize, product, ref this.Count);
                this.dt = this.StatisticsSaleStop(this.productList);
                base.BindControl(this.MyPager);
            }
        }

        protected DataRow ReadSaleStop(int productID, DataTable dt)
        {
            foreach (DataRow row2 in dt.Rows)
            {
                if (row2["ProductID"].ToString() == productID.ToString()) return row2;
            }
            return null;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((("SaleStop.aspx?Action=search&" + "Name=" + this.Name.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text);
        }

        protected DataTable StatisticsSaleStop(List<ProductInfo> productList)
        {
            string productIDList = string.Empty;
            foreach (ProductInfo info in productList)
            {
                if (productIDList == string.Empty)
                    productIDList = info.ID.ToString();
                else
                    productIDList = productIDList + "," + info.ID.ToString();
            }
            return OrderBLL.StatisticsSaleStop(productIDList);
        }
    }
}

