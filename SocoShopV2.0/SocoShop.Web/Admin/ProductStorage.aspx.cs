namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class ProductStorage : AdminBasePage
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
                this.ClassID.Text = RequestHelper.GetQueryString<string>("ClassID");
                this.BrandID.Text = RequestHelper.GetQueryString<string>("BrandID");
                this.Name.Text = RequestHelper.GetQueryString<string>("Name");
                this.StorageAnalyse.Text = RequestHelper.GetQueryString<string>("StorageAnalyse");
                ProductSearchInfo product = new ProductSearchInfo();
                product.IsSale = 1;
                product.Name = RequestHelper.GetQueryString<string>("Name");
                product.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                product.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                product.StorageAnalyse = RequestHelper.GetQueryString<int>("StorageAnalyse");
                List<ProductInfo> dataSource = ProductBLL.SearchProductList(base.CurrentPage, base.PageSize, product, ref this.Count);
                base.BindControl(dataSource, this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect(((("ProductStorage.aspx?Action=search&" + "Name=" + this.Name.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text + "&") + "StorageAnalyse=" + this.StorageAnalyse.Text);
        }

        protected string ShowColor(int lowerCount, int storageCount, int importActualStorageCount, int upperCount)
        {
            int num = storageCount;
            if (ShopConfig.ReadConfigInfo().ProductStorageType == 2) num = importActualStorageCount;
            if (num < lowerCount) return "#0000FF";
            if (num <= upperCount) return "#349802";
            return "#FF0000";
        }

        protected string ShowStorageCount(int storageCount, int importActualStorageCount)
        {
            int num = storageCount;
            if (ShopConfig.ReadConfigInfo().ProductStorageType == 2) num = importActualStorageCount;
            return num.ToString();
        }
    }
}

