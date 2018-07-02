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

    public partial class ProductUnionEdit : AdminBasePage
    {
        protected string userGradeIDList = string.Empty;
        protected List<UserGradeInfo> userGradeList = new List<UserGradeInfo>();
        protected string userGradeNameList = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ProductBatchEdit", PowerCheckType.Single);
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
                string queryString = RequestHelper.GetQueryString<string>("Action");
                if (queryString != null)
                {
                    if (!(queryString == "UnionEdit"))
                    {
                        if (queryString == "search")
                        {
                            ProductSearchInfo productSearch = new ProductSearchInfo();
                            productSearch.Name = RequestHelper.GetQueryString<string>("Name");
                            productSearch.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                            productSearch.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                            productSearch.StartAddDate = RequestHelper.GetQueryString<DateTime>("StartAddDate");
                            productSearch.EndAddDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndAddDate"));
                            this.ClassID.Text = RequestHelper.GetQueryString<string>("ClassID");
                            this.BrandID.Text = RequestHelper.GetQueryString<string>("BrandID");
                            this.Name.Text = RequestHelper.GetQueryString<string>("Name");
                            this.StartAddDate.Text = RequestHelper.GetQueryString<string>("StartAddDate");
                            this.EndAddDate.Text = RequestHelper.GetQueryString<string>("EndAddDate");
                            base.BindControl(ProductBLL.SearchProductList(productSearch), this.RecordList);
                        }
                    }
                    else
                        this.UnionEdit();
                }
                this.userGradeList = UserGradeBLL.ReadUserGradeCacheList();
                foreach (UserGradeInfo info3 in this.userGradeList)
                {
                    if (this.userGradeIDList == string.Empty)
                    {
                        this.userGradeIDList = info3.ID.ToString();
                        this.userGradeNameList = info3.Name;
                    }
                    else
                    {
                        this.userGradeIDList = this.userGradeIDList + "," + info3.ID.ToString();
                        this.userGradeNameList = this.userGradeNameList + "," + info3.Name;
                    }
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((("ProductUnionEdit.aspx?Action=search&" + "Name=" + this.Name.Text + "&") + "ClassID=" + this.ClassID.Text + "&") + "BrandID=" + this.BrandID.Text + "&") + "StartAddDate=" + this.StartAddDate.Text + "&") + "EndAddDate=" + this.EndAddDate.Text);
        }

        protected void UnionEdit()
        {
            string queryString = RequestHelper.GetQueryString<string>("ProductIDList");
            ProductInfo product = new ProductInfo();
            if (RequestHelper.GetQueryString<string>("Weight") != string.Empty)
                product.Weight = RequestHelper.GetQueryString<int>("Weight");
            else
                product.Weight = -2;
            if (RequestHelper.GetQueryString<string>("MarketPrice") != string.Empty)
                product.MarketPrice = RequestHelper.GetQueryString<decimal>("MarketPrice");
            else
                product.MarketPrice = -2M;
            if (RequestHelper.GetQueryString<string>("SendPoint") != string.Empty)
                product.SendPoint = RequestHelper.GetQueryString<int>("SendPoint");
            else
                product.SendPoint = -2;
            if (RequestHelper.GetQueryString<string>("TotalStorageCount") != string.Empty)
                product.TotalStorageCount = RequestHelper.GetQueryString<int>("TotalStorageCount");
            else
                product.TotalStorageCount = -2;
            if (RequestHelper.GetQueryString<string>("LowerCount") != string.Empty)
                product.LowerCount = RequestHelper.GetQueryString<int>("LowerCount");
            else
                product.LowerCount = -2;
            if (RequestHelper.GetQueryString<string>("UpperCount") != string.Empty)
                product.UpperCount = RequestHelper.GetQueryString<int>("UpperCount");
            else
                product.UpperCount = -2;
            ProductBLL.UnionUpdateProduct(queryString, product);
            string str2 = RequestHelper.GetQueryString<string>("MemberPrice");
            if (str2 != string.Empty)
            {
                string[] strArray = str2.Split(new char[] { ',' });
                List<UserGradeInfo> list = UserGradeBLL.ReadUserGradeCacheList();
                decimal num = -1M;
                int index = 0;
                int num3 = 0;
                foreach (UserGradeInfo info2 in list)
                {
                    if (strArray[index] != string.Empty)
                    {
                        foreach (string str3 in queryString.Split(new char[] { ',' }))
                        {
                            num3 = Convert.ToInt32(str3);
                            MemberPriceBLL.DeleteMemberPriceByProductID(str3);
                            num = Convert.ToDecimal(strArray[index]);
                            if (num != -1M)
                            {
                                MemberPriceInfo memberPrice = new MemberPriceInfo();
                                memberPrice.ProductID = num3;
                                memberPrice.GradeID = info2.ID;
                                memberPrice.Price = num;
                                MemberPriceBLL.AddMemberPrice(memberPrice);
                            }
                        }
                    }
                    index++;
                }
            }
            ResponseHelper.Write(ShopLanguage.ReadLanguage("UpdateOK"));
            ResponseHelper.End();
        }
    }
}

