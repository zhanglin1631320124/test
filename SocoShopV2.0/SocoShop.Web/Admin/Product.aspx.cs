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

    public partial class Product : AdminBasePage
    {

        protected void OffSaleButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("OffSaleProduct", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ProductBLL.OffSaleProduct(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("OffSaleRecord"), ShopLanguage.ReadLanguage("Product"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("OffSaleOK"), RequestHelper.RawUrl);
            }
        }

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
                this.StartAddDate.Text = RequestHelper.GetQueryString<string>("StartAddDate");
                this.EndAddDate.Text = RequestHelper.GetQueryString<string>("EndAddDate");
                this.IsSpecial.Text = RequestHelper.GetQueryString<string>("IsSpecial");
                this.IsNew.Text = RequestHelper.GetQueryString<string>("IsNew");
                this.IsHot.Text = RequestHelper.GetQueryString<string>("IsHot");
                this.IsTop.Text = RequestHelper.GetQueryString<string>("IsTop");
                List<ProductInfo> dataSource = new List<ProductInfo>();
                ProductSearchInfo product = new ProductSearchInfo();
                product.Key = RequestHelper.GetQueryString<string>("Key");
                product.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                product.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                product.IsSpecial = RequestHelper.GetQueryString<int>("IsSpecial");
                product.IsNew = RequestHelper.GetQueryString<int>("IsNew");
                product.IsHot = RequestHelper.GetQueryString<int>("IsHot");
                product.IsSale = 1;
                product.IsTop = RequestHelper.GetQueryString<int>("IsTop");
                product.StartAddDate = RequestHelper.GetQueryString<DateTime>("StartAddDate");
                product.EndAddDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndAddDate"));
                base.PageSize = 10;
                dataSource = ProductBLL.SearchProductList(base.CurrentPage, base.PageSize, product, ref this.Count);
                base.BindControl(dataSource, this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((((((("Product.aspx?Action=search&" + "Key=" + this.Key.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text + "&") + "StartAddDate=" + this.StartAddDate.Text + "&") + "EndAddDate=" + this.EndAddDate.Text + "&") + "IsSpecial=" + this.IsSpecial.Text + "&") + "IsNew=" + this.IsNew.Text + "&") + "IsHot=" + this.IsHot.Text + "&") + "IsTop=" + this.IsTop.Text);
        }
    }
}

