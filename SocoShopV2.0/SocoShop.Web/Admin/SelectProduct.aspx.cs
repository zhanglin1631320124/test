namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class SelectProduct : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadProduct", PowerCheckType.Single);
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
                this.Key.Text = RequestHelper.GetQueryString<string>("Key");
                ProductSearchInfo product = new ProductSearchInfo();
                product.IsSale = 1;
                product.StandardType = 0;
                product.Key = RequestHelper.GetQueryString<string>("Key");
                product.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                product.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                base.BindControl(ProductBLL.SearchProductList(base.CurrentPage, base.PageSize, product, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect(((("SelectProduct.aspx?Action=search&" + "Key=" + this.Key.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text + "&") + "Tag=" + RequestHelper.GetQueryString<string>("Tag"));
        }
    }
}

