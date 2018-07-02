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

    public partial class ProductSingleEdit : AdminBasePage
    {
        protected List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected string userGradeIDList = string.Empty;
        protected List<UserGradeInfo> userGradeList = new List<UserGradeInfo>();
        protected string userGradeNameList = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ProductBatchEdit", PowerCheckType.Single);
                if (RequestHelper.GetQueryString<string>("Action") == "SingleEdit") this.SingleEdit();
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
                ProductSearchInfo product = new ProductSearchInfo();
                product.Name = RequestHelper.GetQueryString<string>("Name");
                product.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                product.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                product.StartAddDate = RequestHelper.GetQueryString<DateTime>("StartAddDate");
                product.EndAddDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndAddDate"));
                this.ClassID.Text = RequestHelper.GetQueryString<string>("ClassID");
                this.BrandID.Text = RequestHelper.GetQueryString<string>("BrandID");
                this.Name.Text = RequestHelper.GetQueryString<string>("Name");
                this.StartAddDate.Text = RequestHelper.GetQueryString<string>("StartAddDate");
                this.EndAddDate.Text = RequestHelper.GetQueryString<string>("EndAddDate");
                this.productList = ProductBLL.SearchProductList(base.CurrentPage, base.PageSize, product, ref this.Count);
                base.BindControl(this.MyPager);
                string strProductID = string.Empty;
                foreach (ProductInfo info3 in this.productList)
                {
                    if (strProductID == string.Empty)
                        strProductID = info3.ID.ToString();
                    else
                        strProductID = strProductID + "," + info3.ID.ToString();
                }
                this.userGradeList = UserGradeBLL.ReadUserGradeCacheList();
                this.memberPriceList = MemberPriceBLL.ReadMemberPriceByProduct(strProductID);
                foreach (UserGradeInfo info4 in this.userGradeList)
                {
                    if (this.userGradeIDList == string.Empty)
                    {
                        this.userGradeIDList = info4.ID.ToString();
                        this.userGradeNameList = info4.Name;
                    }
                    else
                    {
                        this.userGradeIDList = this.userGradeIDList + "," + info4.ID.ToString();
                        this.userGradeNameList = this.userGradeNameList + "," + info4.Name;
                    }
                }
            }
        }

        protected decimal ReadMemberPrice(int productID, int userGradeID, List<MemberPriceInfo> memberPriceList)
        {
            decimal price = -1M;
            foreach (MemberPriceInfo info in memberPriceList)
            {
                if (info.GradeID == userGradeID && info.ProductID == productID) price = info.Price;
            }
            return price;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((("ProductSingleEdit.aspx?Action=search&" + "Name=" + this.Name.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text + "&") + "StartAddDate=" + this.StartAddDate.Text + "&") + "EndAddDate=" + this.EndAddDate.Text);
        }

        protected void SingleEdit()
        {
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            ProductInfo product = ProductBLL.ReadProduct(queryString);
            product.ProductNumber = RequestHelper.GetQueryString<string>("ProductNumber");
            product.Weight = RequestHelper.GetQueryString<int>("Weight");
            product.MarketPrice = RequestHelper.GetQueryString<decimal>("MarketPrice");
            product.SendPoint = RequestHelper.GetQueryString<int>("SendPoint");
            product.TotalStorageCount = RequestHelper.GetQueryString<int>("TotalStorageCount");
            product.LowerCount = RequestHelper.GetQueryString<int>("LowerCount");
            product.UpperCount = RequestHelper.GetQueryString<int>("UpperCount");
            ProductBLL.UpdateProduct(product);
            string str = RequestHelper.GetQueryString<string>("MemberPrice");
            if (str != string.Empty)
            {
                string[] strArray = str.Split(new char[] { ',' });
                MemberPriceBLL.DeleteMemberPriceByProductID(queryString.ToString());
                List<UserGradeInfo> list = UserGradeBLL.ReadUserGradeCacheList();
                decimal num2 = -1M;
                int index = 0;
                foreach (UserGradeInfo info2 in list)
                {
                    num2 = Convert.ToDecimal(strArray[index]);
                    if (num2 != -1M)
                    {
                        MemberPriceInfo memberPrice = new MemberPriceInfo();
                        memberPrice.ProductID = queryString;
                        memberPrice.GradeID = info2.ID;
                        memberPrice.Price = num2;
                        MemberPriceBLL.AddMemberPrice(memberPrice);
                    }
                    index++;
                }
            }
            ResponseHelper.Write(ShopLanguage.ReadLanguage("UpdateOK"));
            ResponseHelper.End();
        }
    }
}

