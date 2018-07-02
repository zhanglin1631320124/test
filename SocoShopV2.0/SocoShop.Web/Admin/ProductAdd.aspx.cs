namespace SocoShop.Web.Admin
{
    using FredCK.FCKeditorV2;
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class ProductAdd : AdminBasePage
    {
        protected string color = string.Empty;
        protected int productID = 0;
        protected List<ProductPhotoInfo> productPhotoList = new List<ProductPhotoInfo>();
        protected string promotDetail = string.Empty;
        protected int sendCount = 0;
        protected List<UserGradeInfo> userGradeList = new List<UserGradeInfo>();

        protected void AddProductPhoto(int productID)
        {
            string form = RequestHelper.GetForm<string>("ProductPhoto");
            if (form != string.Empty)
            {
                foreach (string str2 in form.Split(new char[] { ',' }))
                {
                    ProductPhotoInfo productPhoto = new ProductPhotoInfo();
                    productPhoto.ProductID = productID;
                    productPhoto.Name = str2.Split(new char[] { '|' })[0];
                    productPhoto.Photo = str2.Split(new char[] { '|' })[1];
                    ProductPhotoBLL.AddProductPhoto(productPhoto);
                }
            }
        }

        protected void BindClassBrandAttributeClassStandardType()
        {
            List<ProductBrandInfo> list = ProductBrandBLL.ReadProductBrandCacheList();
            this.BrandID.DataSource = list;
            this.BrandID.DataTextField = "Name";
            this.BrandID.DataValueField = "ID";
            this.BrandID.DataBind();
            this.BrandID.Items.Insert(0, new ListItem("选择品牌", "0"));
            this.RelationBrandID.DataSource = list;
            this.RelationBrandID.DataTextField = "Name";
            this.RelationBrandID.DataValueField = "ID";
            this.RelationBrandID.DataBind();
            this.RelationBrandID.Items.Insert(0, new ListItem("所有品牌", string.Empty));
            this.AccessoryBrandID.DataSource = list;
            this.AccessoryBrandID.DataTextField = "Name";
            this.AccessoryBrandID.DataValueField = "ID";
            this.AccessoryBrandID.DataBind();
            this.AccessoryBrandID.Items.Insert(0, new ListItem("所有品牌", string.Empty));
            List<ProductClassInfo> list2 = ProductClassBLL.ReadProductClassNamedList();
            foreach (ProductClassInfo info in list2)
            {
                this.RelationClassID.Items.Add(new ListItem(info.ClassName, "|" + info.ID + "|"));
            }
            this.RelationClassID.Items.Insert(0, new ListItem("所有分类", string.Empty));
            foreach (ProductClassInfo info in list2)
            {
                this.AccessoryClassID.Items.Add(new ListItem(info.ClassName, "|" + info.ID + "|"));
            }
            this.AccessoryClassID.Items.Insert(0, new ListItem("所有分类", string.Empty));
            this.AttributeClassID.DataSource = AttributeClassBLL.ReadAttributeClassCacheList();
            this.AttributeClassID.DataTextField = "Name";
            this.AttributeClassID.DataValueField = "ID";
            this.AttributeClassID.DataBind();
            this.AttributeClassID.Items.Insert(0, new ListItem("请选择", "0"));
            foreach (ArticleClassInfo info2 in ArticleClassBLL.ReadArticleClassChildList(3))
            {
                this.ArticleClassID.Items.Add(new ListItem(info2.ClassName, "|" + info2.ID + "|"));
            }
            this.ArticleClassID.Items.Insert(0, new ListItem(ArticleClassBLL.ReadArticleClassCache(3).ClassName, "|" + 3 + "|"));
            this.StandardType.DataSource = EnumHelper.ReadEnumList<ProductStandardType>();
            this.StandardType.DataTextField = "ChineseName";
            this.StandardType.DataValueField = "Value";
            this.StandardType.DataBind();
        }

        protected void BindRelation(ProductInfo product)
        {
            ProductSearchInfo info2;
            if (product.RelationArticle != string.Empty)
            {
                ArticleSearchInfo articleSearch = new ArticleSearchInfo();
                articleSearch.InArticleID = product.RelationArticle;
                this.Article.DataSource = ArticleBLL.SearchArticleList(articleSearch);
                this.Article.DataTextField = "Title";
                this.Article.DataValueField = "ID";
                this.Article.DataBind();
            }
            if (product.RelationProduct != string.Empty)
            {
                info2 = new ProductSearchInfo();
                info2.InProductID = product.RelationProduct;
                this.Product.DataSource = ProductBLL.SearchProductList(info2);
                this.Product.DataTextField = "Name";
                this.Product.DataValueField = "ID";
                this.Product.DataBind();
            }
            if (product.Accessory != string.Empty)
            {
                info2 = new ProductSearchInfo();
                info2.InProductID = product.Accessory;
                this.Accessory.DataSource = ProductBLL.SearchProductList(info2);
                this.Accessory.DataTextField = "Name";
                this.Accessory.DataValueField = "ID";
                this.Accessory.DataBind();
            }
        }

        protected void HanderAttribute(ProductInfo product)
        {
            if (product.ID > 0) AttributeRecordBLL.DeleteAttributeRecordByProductID(product.ID.ToString());
            List<AttributeInfo> list = AttributeBLL.ReadAttributeListByClassID(product.AttributeClassID);
            foreach (AttributeInfo info in list)
            {
                AttributeRecordInfo attributeRecord = new AttributeRecordInfo();
                attributeRecord.AttributeID = info.ID;
                attributeRecord.ProductID = product.ID;
                attributeRecord.Value = RequestHelper.GetForm<string>(info.ID.ToString() + "Value");
                AttributeRecordBLL.AddAttributeRecord(attributeRecord);
            }
        }

        protected void HanderMemberPrice(int productID)
        {
            if (productID > 0) MemberPriceBLL.DeleteMemberPriceByProductID(productID.ToString());
            List<UserGradeInfo> list = UserGradeBLL.ReadUserGradeCacheList();
            decimal form = -1M;
            foreach (UserGradeInfo info in list)
            {
                form = RequestHelper.GetForm<decimal>("MemberPrice" + info.ID);
                if (form != -1M)
                {
                    MemberPriceInfo memberPrice = new MemberPriceInfo();
                    memberPrice.ProductID = productID;
                    memberPrice.GradeID = info.ID;
                    memberPrice.Price = form;
                    MemberPriceBLL.AddMemberPrice(memberPrice);
                }
            }
        }

        protected void HanderProductStandard(ProductInfo product)
        {
            string strID = string.Empty;
            if (product.StandardType == 2)
            {
                strID = ("," + RequestHelper.GetForm<string>("Product") + ",").Replace(",0,", "," + product.ID.ToString() + ",");
                strID = strID.Substring(1, strID.Length - 2);
            }
            ProductBLL.UpdateProductStandardType(strID, product.StandardType, product.ID);
            if (product.ID > 0) StandardRecordBLL.DeleteStandardRecordByProductID(product.ID.ToString());
            if (product.StandardType != 0)
            {
                int form = RequestHelper.GetForm<int>("recordCount");
                string str2 = RequestHelper.GetForm<string>("StandardIDList");
                if (str2 != string.Empty)
                {
                    string[] strArray = strID.Split(new char[] { ',' });
                    int index = 0;
                    for (int i = 0; i < form; i++)
                    {
                        if (base.Request.Form["Standard" + i] != null)
                        {
                            StandardRecordInfo standardRecord = new StandardRecordInfo();
                            standardRecord.StandardIDList = str2;
                            standardRecord.ValueList = RequestHelper.GetForm<string>("Standard" + i);
                            if (product.StandardType == 2)
                            {
                                standardRecord.GroupTag = strID;
                                standardRecord.ProductID = Convert.ToInt32(strArray[index]);
                            }
                            else
                                standardRecord.ProductID = product.ID;
                            StandardRecordBLL.AddStandardRecord(standardRecord);
                            index++;
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindClassBrandAttributeClassStandardType();
                this.ProductClass.DataSource = ProductClassBLL.ReadProductClassUnlimitClass();
                this.productID = RequestHelper.GetQueryString<int>("ID");
                if (this.productID != -2147483648)
                {
                    base.CheckAdminPower("ReadProduct", PowerCheckType.Single);
                    ProductInfo product = ProductBLL.ReadProduct(this.productID);
                    this.Name.Text = product.Name;
                    this.Name.Attributes.Add("style", "color:" + product.Color);
                    this.color = product.Color;
                    this.FontStyle.Text = product.FontStyle;
                    this.ProductNumber.Text = product.ProductNumber;
                    this.ProductClass.ClassIDList = product.ClassID;
                    this.Keywords.Text = product.Keywords;
                    this.BrandID.Text = product.BrandID.ToString();
                    this.MarketPrice.Text = product.MarketPrice.ToString();
                    this.SendPoint.Text = product.SendPoint.ToString();
                    this.Photo.Text = product.Photo;
                    this.Summary.Text = product.Summary;
                    this.Introduction.Value = product.Introduction;
                    this.Weight.Text = product.Weight.ToString();
                    this.IsSpecial.Text = product.IsSpecial.ToString();
                    this.IsNew.Text = product.IsNew.ToString();
                    this.IsHot.Text = product.IsHot.ToString();
                    this.IsSale.Text = product.IsSale.ToString();
                    this.IsTop.Text = product.IsTop.ToString();
                    this.Remark.Text = product.Remark;
                    this.AllowComment.Text = product.AllowComment.ToString();
                    this.TotalStorageCount.Text = product.TotalStorageCount.ToString();
                    this.LowerCount.Text = product.LowerCount.ToString();
                    this.UpperCount.Text = product.UpperCount.ToString();
                    this.AttributeClassID.Text = product.AttributeClassID.ToString();
                    this.StandardType.Text = product.StandardType.ToString();
                    this.sendCount = product.SendCount;
                    this.BindRelation(product);
                    this.productPhotoList = ProductPhotoBLL.ReadProductPhotoByProduct(this.productID);
                }
                this.userGradeList = UserGradeBLL.JoinUserGrade(this.productID);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ProductInfo product = new ProductInfo();
            product.ID = RequestHelper.GetQueryString<int>("ID");
            product.Name = this.Name.Text;
            product.Spelling = ChineseCharacterHelper.GetFirstLetter(this.Name.Text);
            product.Color = RequestHelper.GetForm<string>("ProductColor");
            product.FontStyle = this.FontStyle.Text;
            product.ProductNumber = this.ProductNumber.Text;
            product.ClassID = this.ProductClass.ClassIDList;
            product.Keywords = this.Keywords.Text;
            product.BrandID = Convert.ToInt32(this.BrandID.Text);
            product.MarketPrice = Convert.ToDecimal(this.MarketPrice.Text);
            product.SendPoint = Convert.ToInt32(this.SendPoint.Text);
            product.Photo = this.Photo.Text;
            product.Summary = this.Summary.Text;
            product.Introduction = this.Introduction.Value;
            product.Weight = Convert.ToInt32(this.Weight.Text);
            product.IsSpecial = Convert.ToInt32(this.IsSpecial.Text);
            product.IsNew = Convert.ToInt32(this.IsNew.Text);
            product.IsHot = Convert.ToInt32(this.IsHot.Text);
            product.IsSale = Convert.ToInt32(this.IsSale.Text);
            product.IsTop = Convert.ToInt32(this.IsTop.Text);
            product.Remark = this.Remark.Text;
            product.Accessory = RequestHelper.GetForm<string>("RelationAccessoryID");
            product.RelationProduct = RequestHelper.GetForm<string>("RelationProductID");
            product.RelationArticle = RequestHelper.GetForm<string>("RelationArticleID");
            product.AllowComment = Convert.ToInt32(this.AllowComment.Text);
            product.TotalStorageCount = Convert.ToInt32(this.TotalStorageCount.Text);
            product.LowerCount = Convert.ToInt32(this.LowerCount.Text);
            product.UpperCount = Convert.ToInt32(this.UpperCount.Text);
            product.AttributeClassID = Convert.ToInt32(this.AttributeClassID.Text);
            product.StandardType = Convert.ToInt32(this.StandardType.Text);
            product.AddDate = RequestHelper.DateNow;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (product.ID == -2147483648)
            {
                base.CheckAdminPower("AddProduct", PowerCheckType.Single);
                int productID = ProductBLL.AddProduct(product);
                this.AddProductPhoto(productID);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Product"), productID);
            }
            else
            {
                base.CheckAdminPower("UpdateProduct", PowerCheckType.Single);
                ProductBLL.UpdateProduct(product);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Product"), product.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            this.HanderAttribute(product);
            this.HanderMemberPrice(product.ID);
            this.HanderProductStandard(product);
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

