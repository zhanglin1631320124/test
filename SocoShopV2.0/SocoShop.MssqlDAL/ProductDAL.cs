namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ProductDAL : IProduct
    {
        public void AddKeyCondition(MssqlCondition mssqlCondition, string key)
        {
            string condition = string.Empty;
            if (key != string.Empty)
            {
                condition = (("([Name] LIKE '%" + StringHelper.SearchSafe(key) + "%' OR ") + "[Spelling] LIKE '%" + StringHelper.SearchSafe(key) + "%' OR ") + "[ProductNumber] LIKE '%" + StringHelper.SearchSafe(key) + "%')";
                mssqlCondition.Add(condition);
            }
        }

        public int AddProduct(ProductInfo product)
        {
            SqlParameter[] pt = new SqlParameter[] { 
                new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@spelling", SqlDbType.NVarChar), new SqlParameter("@color", SqlDbType.NVarChar), new SqlParameter("@fontStyle", SqlDbType.NVarChar), new SqlParameter("@productNumber", SqlDbType.NVarChar), new SqlParameter("@classID", SqlDbType.NVarChar), new SqlParameter("@brandID", SqlDbType.Int), new SqlParameter("@marketPrice", SqlDbType.Decimal), new SqlParameter("@weight", SqlDbType.Int), new SqlParameter("@sendPoint", SqlDbType.Int), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@keywords", SqlDbType.NVarChar), new SqlParameter("@summary", SqlDbType.NText), new SqlParameter("@introduction", SqlDbType.NText), new SqlParameter("@remark", SqlDbType.NText), new SqlParameter("@isSpecial", SqlDbType.Int), 
                new SqlParameter("@isNew", SqlDbType.Int), new SqlParameter("@isHot", SqlDbType.Int), new SqlParameter("@isSale", SqlDbType.Int), new SqlParameter("@isTop", SqlDbType.Int), new SqlParameter("@accessory", SqlDbType.NVarChar), new SqlParameter("@relationProduct", SqlDbType.NVarChar), new SqlParameter("@relationArticle", SqlDbType.NVarChar), new SqlParameter("@viewCount", SqlDbType.Int), new SqlParameter("@allowComment", SqlDbType.Int), new SqlParameter("@commentCount", SqlDbType.Int), new SqlParameter("@sumPoint", SqlDbType.Int), new SqlParameter("@perPoint", SqlDbType.Decimal), new SqlParameter("@photoCount", SqlDbType.Int), new SqlParameter("@collectCount", SqlDbType.Int), new SqlParameter("@totalStorageCount", SqlDbType.Int), new SqlParameter("@orderCount", SqlDbType.Int), 
                new SqlParameter("@sendCount", SqlDbType.Int), new SqlParameter("@importActualStorageCount", SqlDbType.Int), new SqlParameter("@importVirtualStorageCount", SqlDbType.Int), new SqlParameter("@lowerCount", SqlDbType.Int), new SqlParameter("@upperCount", SqlDbType.Int), new SqlParameter("@attributeClassID", SqlDbType.Int), new SqlParameter("@standardType", SqlDbType.Int), new SqlParameter("@addDate", SqlDbType.DateTime)
             };
            pt[0].Value = product.Name;
            pt[1].Value = product.Spelling;
            pt[2].Value = product.Color;
            pt[3].Value = product.FontStyle;
            pt[4].Value = product.ProductNumber;
            pt[5].Value = product.ClassID;
            pt[6].Value = product.BrandID;
            pt[7].Value = product.MarketPrice;
            pt[8].Value = product.Weight;
            pt[9].Value = product.SendPoint;
            pt[10].Value = product.Photo;
            pt[11].Value = product.Keywords;
            pt[12].Value = product.Summary;
            pt[13].Value = product.Introduction;
            pt[14].Value = product.Remark;
            pt[15].Value = product.IsSpecial;
            pt[0x10].Value = product.IsNew;
            pt[0x11].Value = product.IsHot;
            pt[0x12].Value = product.IsSale;
            pt[0x13].Value = product.IsTop;
            pt[20].Value = product.Accessory;
            pt[0x15].Value = product.RelationProduct;
            pt[0x16].Value = product.RelationArticle;
            pt[0x17].Value = product.ViewCount;
            pt[0x18].Value = product.AllowComment;
            pt[0x19].Value = product.CommentCount;
            pt[0x1a].Value = product.SumPoint;
            pt[0x1b].Value = product.PerPoint;
            pt[0x1c].Value = product.PhotoCount;
            pt[0x1d].Value = product.CollectCount;
            pt[30].Value = product.TotalStorageCount;
            pt[0x1f].Value = product.OrderCount;
            pt[0x20].Value = product.SendCount;
            pt[0x21].Value = product.ImportActualStorageCount;
            pt[0x22].Value = product.ImportVirtualStorageCount;
            pt[0x23].Value = product.LowerCount;
            pt[0x24].Value = product.UpperCount;
            pt[0x25].Value = product.AttributeClassID;
            pt[0x26].Value = product.StandardType;
            pt[0x27].Value = product.AddDate;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddProduct", pt));
        }

        public void ChangeProductCollectCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCollectCount", pt);
        }

        public void ChangeProductCollectCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCollectCountByGeneral", pt);
        }

        public void ChangeProductCommentCountAndRank(int id, int rank, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@rank", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = rank;
            pt[2].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCommentCountAndRank", pt);
        }

        public void ChangeProductCommentCountAndRankByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCommentCountAndRankByGeneral", pt);
        }

        public void ChangeProductOrderCount(string strProductID, int changeCount)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NVarChar), new SqlParameter("@changeCount", SqlDbType.Int) };
            pt[0].Value = strProductID;
            pt[1].Value = changeCount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductOrderCount", pt);
        }

        public void ChangeProductOrderCountByOrder(int orderID, ChangeAction changeAction)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@changeAction", SqlDbType.NVarChar) };
            pt[0].Value = orderID;
            pt[1].Value = changeAction;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductOrderCountByOrder", pt);
        }

        public void ChangeProductPhotoCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductPhotoCount", pt);
        }

        public void ChangeProductPhotoCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductPhotoCountByGeneral", pt);
        }

        public void ChangeProductSendCountByOrder(int orderID, ChangeAction changeAction)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@changeAction", SqlDbType.NVarChar) };
            pt[0].Value = orderID;
            pt[1].Value = changeAction;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductSendCountByOrder", pt);
        }

        public void ChangeProductViewCount(int productID, int changeCount)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@changeCount", SqlDbType.Int) };
            pt[0].Value = productID;
            pt[1].Value = changeCount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductViewCount", pt);
        }

        public void ChangProductAllowComment(int id, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangProductAllowComment", pt);
        }

        public void ChangProductIsHot(int id, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangProductIsHot", pt);
        }

        public void ChangProductIsNew(int id, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangProductIsNew", pt);
        }

        public void ChangProductIsSpecial(int id, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangProductIsSpecial", pt);
        }

        public void ChangProductIsTop(int id, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangProductIsTop", pt);
        }

        public void DeleteProduct(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProduct", pt);
        }

        public DataTable NoHandlerStatistics()
        {
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "NoHandlerStatistics");
        }

        public void OffSaleProduct(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "OffSaleProduct", pt);
        }

        public void OnSaleProduct(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "OnSaleProduct", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, ProductSearchInfo productSearch)
        {
            this.AddKeyCondition(mssqlCondition, productSearch.Key);
            mssqlCondition.Add("[Name]", productSearch.Name, ConditionType.Like);
            mssqlCondition.Add("[Spelling]", productSearch.Spelling, ConditionType.Like);
            mssqlCondition.Add("[ProductNumber]", productSearch.ProductNumber, ConditionType.Like);
            mssqlCondition.Add("[Keywords]", productSearch.Keywords, ConditionType.Like);
            mssqlCondition.Add("[BrandID]", productSearch.BrandID, ConditionType.Equal);
            mssqlCondition.Add("[ClassID]", productSearch.ClassID, ConditionType.Like);
            mssqlCondition.Add("[IsSpecial]", productSearch.IsSpecial, ConditionType.Equal);
            mssqlCondition.Add("[IsNew]", productSearch.IsNew, ConditionType.Equal);
            mssqlCondition.Add("[IsHot]", productSearch.IsHot, ConditionType.Equal);
            mssqlCondition.Add("[IsSale]", productSearch.IsSale, ConditionType.Equal);
            mssqlCondition.Add("[IsTop]", productSearch.IsTop, ConditionType.Equal);
            mssqlCondition.Add("[AddDate]", productSearch.StartAddDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[AddDate]", productSearch.EndAddDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[ID]", productSearch.InProductID, ConditionType.In);
            mssqlCondition.Add("[ID]", productSearch.NotInProductID, ConditionType.NotIn);
            mssqlCondition.Add("[StandardType]", productSearch.StandardType, ConditionType.Equal);
            if (productSearch.IsTaobao != -2147483648)
            {
                if (productSearch.IsTaobao == 1)
                    mssqlCondition.Add("[TaobaoID] > 0");
                else
                    mssqlCondition.Add("[TaobaoID] = 0");
            }
            if (productSearch.StorageAnalyse != 0)
            {
                string str = "[TotalStorageCount]-[SendCount]";
                if (ShopConfig.ReadConfigInfo().ProductStorageType == 2) str = "[ImportActualStorageCount]";
                switch (productSearch.StorageAnalyse)
                {
                    case 1:
                        mssqlCondition.Add(str + "<[LowerCount]");
                        break;

                    case 2:
                        mssqlCondition.Add("(" + str + ">=[LowerCount] AND " + str + "<=[UpperCount])");
                        break;

                    case 3:
                        mssqlCondition.Add(str + ">[UpperCount]");
                        break;
                }
            }
            if (productSearch.Tags != string.Empty) mssqlCondition.Add("[ID] IN(SELECT [ProductID] FROM " + ShopMssqlHelper.TablePrefix + "Tags WHERE [Word]='" + StringHelper.SearchSafe(productSearch.Tags) + "')");
        }

        public void PrepareProductModel(SqlDataReader dr, List<ProductInfo> productList)
        {
            while (dr.Read())
            {
                ProductInfo item = new ProductInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Spelling = dr[2].ToString();
                item.Color = dr[3].ToString();
                item.FontStyle = dr[4].ToString();
                item.ProductNumber = dr[5].ToString();
                item.ClassID = dr[6].ToString();
                item.BrandID = dr.GetInt32(7);
                item.MarketPrice = dr.GetDecimal(8);
                item.Weight = dr.GetInt32(9);
                item.SendPoint = dr.GetInt32(10);
                item.Photo = dr[11].ToString();
                item.Keywords = dr[12].ToString();
                item.Summary = dr[13].ToString();
                item.IsSpecial = dr.GetInt32(14);
                item.IsNew = dr.GetInt32(15);
                item.IsHot = dr.GetInt32(0x10);
                item.IsSale = dr.GetInt32(0x11);
                item.IsTop = dr.GetInt32(0x12);
                item.Accessory = dr[0x13].ToString();
                item.RelationProduct = dr[20].ToString();
                item.RelationArticle = dr[0x15].ToString();
                item.ViewCount = dr.GetInt32(0x16);
                item.AllowComment = dr.GetInt32(0x17);
                item.CommentCount = dr.GetInt32(0x18);
                item.SumPoint = dr.GetInt32(0x19);
                item.PerPoint = dr.GetDecimal(0x1a);
                item.PhotoCount = dr.GetInt32(0x1b);
                item.CollectCount = dr.GetInt32(0x1c);
                item.TotalStorageCount = dr.GetInt32(0x1d);
                item.OrderCount = dr.GetInt32(30);
                item.SendCount = dr.GetInt32(0x1f);
                item.ImportActualStorageCount = dr.GetInt32(0x20);
                item.ImportVirtualStorageCount = dr.GetInt32(0x21);
                item.LowerCount = dr.GetInt32(0x22);
                item.UpperCount = dr.GetInt32(0x23);
                item.AttributeClassID = dr.GetInt32(0x24);
                item.StandardType = dr.GetInt32(0x25);
                item.AddDate = dr.GetDateTime(0x26);
                productList.Add(item);
            }
        }

        public ProductInfo ReadProduct(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ProductInfo info = new ProductInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProduct", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Spelling = reader[2].ToString();
                    info.Color = reader[3].ToString();
                    info.FontStyle = reader[4].ToString();
                    info.ProductNumber = reader[5].ToString();
                    info.ClassID = reader[6].ToString();
                    info.BrandID = reader.GetInt32(7);
                    info.MarketPrice = reader.GetDecimal(8);
                    info.Weight = reader.GetInt32(9);
                    info.SendPoint = reader.GetInt32(10);
                    info.Photo = reader[11].ToString();
                    info.Keywords = reader[12].ToString();
                    info.Summary = reader[13].ToString();
                    info.Introduction = reader[14].ToString();
                    info.Remark = reader[15].ToString();
                    info.IsSpecial = reader.GetInt32(0x10);
                    info.IsNew = reader.GetInt32(0x11);
                    info.IsHot = reader.GetInt32(0x12);
                    info.IsSale = reader.GetInt32(0x13);
                    info.IsTop = reader.GetInt32(20);
                    info.Accessory = reader[0x15].ToString();
                    info.RelationProduct = reader[0x16].ToString();
                    info.RelationArticle = reader[0x17].ToString();
                    info.ViewCount = reader.GetInt32(0x18);
                    info.AllowComment = reader.GetInt32(0x19);
                    info.CommentCount = reader.GetInt32(0x1a);
                    info.SumPoint = reader.GetInt32(0x1b);
                    info.PerPoint = reader.GetDecimal(0x1c);
                    info.PhotoCount = reader.GetInt32(0x1d);
                    info.CollectCount = reader.GetInt32(30);
                    info.TotalStorageCount = reader.GetInt32(0x1f);
                    info.OrderCount = reader.GetInt32(0x20);
                    info.SendCount = reader.GetInt32(0x21);
                    info.ImportActualStorageCount = reader.GetInt32(0x22);
                    info.ImportVirtualStorageCount = reader.GetInt32(0x23);
                    info.LowerCount = reader.GetInt32(0x24);
                    info.UpperCount = reader.GetInt32(0x25);
                    info.AttributeClassID = reader.GetInt32(0x26);
                    info.StandardType = reader.GetInt32(0x27);
                    info.AddDate = reader.GetDateTime(40);
                }
            }
            return info;
        }

        public List<ProductInfo> SearchProductList(ProductSearchInfo productSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, productSearch);
            List<ProductInfo> productList = new List<ProductInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchProductList", pt))
            {
                this.PrepareProductModel(reader, productList);
            }
            return productList;
        }

        public List<ProductInfo> SearchProductList(int currentPage, int pageSize, ProductSearchInfo productSearch, ref int count)
        {
            List<ProductInfo> productList = new List<ProductInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Product";
            class2.Fields = "[ID],[Name],[Spelling],[Color],[FontStyle],[ProductNumber],[ClassID],[BrandID],[MarketPrice],[Weight],[SendPoint],[Photo],[Keywords],[Summary],[IsSpecial],[IsNew],[IsHot],[IsSale],[IsTop],[Accessory],[RelationProduct],[RelationArticle],[ViewCount],[AllowComment],[CommentCount],[SumPoint],[PerPoint],[PhotoCount],[CollectCount],[TotalStorageCount],[OrderCount],[SendCount],[ImportActualStorageCount],[ImportVirtualStorageCount],[LowerCount],[UpperCount],[AttributeClassID],[StandardType],[AddDate]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            if (productSearch.ProductOrderType != string.Empty)
            {
                string productOrderType = productSearch.ProductOrderType;
                if (productOrderType != null)
                {
                    if (!(productOrderType == "CommentCount"))
                    {
                        if (productOrderType == "PerPoint")
                            class2.OrderField = "[PerPoint],[ID]";
                        else if (productOrderType == "CollectCount")
                            class2.OrderField = "[CollectCount],[ID]";
                        else if (productOrderType == "ViewCount") class2.OrderField = "[ViewCount],[ID]";
                    }
                    else
                        class2.OrderField = "[CommentCount],[ID]";
                }
            }
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, productSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareProductModel(reader, productList);
            }
            return productList;
        }

        public List<ProductInfo> SearchProductList(int currentPage, int pageSize, ProductSearchInfo productSearch, ref int count, int gradeID, decimal disCount)
        {
            string str = "[ID],[Name],[Spelling],[Color],[FontStyle],[ProductNumber],[ClassID],[BrandID],[MarketPrice],[Weight],[SendPoint],[Photo],[Keywords],[Summary],[IsSpecial],[IsNew],[IsHot],[IsSale],[IsTop],[Accessory],[RelationProduct],[RelationArticle],[ViewCount],[AllowComment],[CommentCount],[SumPoint],[PerPoint],[PhotoCount],[CollectCount],[TotalStorageCount],[OrderCount],[SendCount],[ImportActualStorageCount],[ImportVirtualStorageCount],[LowerCount],[UpperCount],[AttributeClassID],[StandardType],[AddDate]";
            List<ProductInfo> list = new List<ProductInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = string.Concat(new object[] { "(SELECT ", str, ",IsNULL(Price,MarketPrice*", disCount, ") As CurrentMemberPrice FROM ", ShopMssqlHelper.TablePrefix, "Product LEFT JOIN ", ShopMssqlHelper.TablePrefix, "MemberPrice ON ", ShopMssqlHelper.TablePrefix, "Product.ID=", ShopMssqlHelper.TablePrefix, "MemberPrice.ProductID AND SocoShop_MemberPrice.GradeID=", gradeID.ToString(), ") As Temp" });
            class2.Fields = str + ",[CurrentMemberPrice]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[CurrentMemberPrice],[ID]";
            class2.OrderType = productSearch.OrderType;
            this.PrepareCondition(class2.MssqlCondition, productSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProductInfo item = new ProductInfo();
                    item.ID = reader.GetInt32(0);
                    item.Name = reader[1].ToString();
                    item.Spelling = reader[2].ToString();
                    item.Color = reader[3].ToString();
                    item.FontStyle = reader[4].ToString();
                    item.ProductNumber = reader[5].ToString();
                    item.ClassID = reader[6].ToString();
                    item.BrandID = reader.GetInt32(7);
                    item.MarketPrice = reader.GetDecimal(8);
                    item.Weight = reader.GetInt32(9);
                    item.SendPoint = reader.GetInt32(10);
                    item.Photo = reader[11].ToString();
                    item.Keywords = reader[12].ToString();
                    item.Summary = reader[13].ToString();
                    item.IsSpecial = reader.GetInt32(14);
                    item.IsNew = reader.GetInt32(15);
                    item.IsHot = reader.GetInt32(0x10);
                    item.IsSale = reader.GetInt32(0x11);
                    item.IsTop = reader.GetInt32(0x12);
                    item.Accessory = reader[0x13].ToString();
                    item.RelationProduct = reader[20].ToString();
                    item.RelationArticle = reader[0x15].ToString();
                    item.ViewCount = reader.GetInt32(0x16);
                    item.AllowComment = reader.GetInt32(0x17);
                    item.CommentCount = reader.GetInt32(0x18);
                    item.SumPoint = reader.GetInt32(0x19);
                    item.PerPoint = reader.GetDecimal(0x1a);
                    item.PhotoCount = reader.GetInt32(0x1b);
                    item.CollectCount = reader.GetInt32(0x1c);
                    item.TotalStorageCount = reader.GetInt32(0x1d);
                    item.OrderCount = reader.GetInt32(30);
                    item.SendCount = reader.GetInt32(0x1f);
                    item.ImportActualStorageCount = reader.GetInt32(0x20);
                    item.ImportVirtualStorageCount = reader.GetInt32(0x21);
                    item.LowerCount = reader.GetInt32(0x22);
                    item.UpperCount = reader.GetInt32(0x23);
                    item.AttributeClassID = reader.GetInt32(0x24);
                    item.StandardType = reader.GetInt32(0x25);
                    item.AddDate = reader.GetDateTime(0x26);
                    item.CurrentMemberPrice = reader.GetDecimal(0x27);
                    list.Add(item);
                }
            }
            return list;
        }

        public DataTable StatisticsProductSale(int currentPage, int pageSize, ProductSearchInfo productSearch, ref int count, DateTime startDate, DateTime endDate)
        {
            List<ProductInfo> list = new List<ProductInfo>();
            string str = string.Empty;
            MssqlCondition condition = new MssqlCondition();
            condition.Add("[AddDate]", startDate, ConditionType.MoreOrEqual);
            condition.Add("[AddDate]", endDate, ConditionType.Less);
            str = condition.ToString();
            if (str != string.Empty) str = " AND" + str;
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = "(SELECT ID,Name,ClassID,BrandID,IsSale, ISNULL(SellCount, 0) AS SellCount, ISNULL(SellMoney,0) AS SellMoney FROM " + ShopMssqlHelper.TablePrefix + "Product ";
            class2.TableName = class2.TableName + "LEFT OUTER JOIN (SELECT ProductID, SUM(BuyCount) AS SellCount, SUM(ProductPrice * BuyCount) AS SellMoney FROM ( ";
            string tableName = class2.TableName;
            class2.TableName = tableName + "SELECT " + ShopMssqlHelper.TablePrefix + "OrderDetail.ProductID," + ShopMssqlHelper.TablePrefix + "OrderDetail.ProductPrice, " + ShopMssqlHelper.TablePrefix + "OrderDetail.BuyCount FROM " + ShopMssqlHelper.TablePrefix + "OrderDetail ";
            tableName = class2.TableName;
            class2.TableName = tableName + " INNER JOIN " + ShopMssqlHelper.TablePrefix + "Order ON " + ShopMssqlHelper.TablePrefix + "OrderDetail.OrderID = " + ShopMssqlHelper.TablePrefix + "Order.ID WHERE (" + ShopMssqlHelper.TablePrefix + "Order.OrderStatus = 6 " + str + ")) AS TEMP1 GROUP BY ProductID) AS TEMP2 ON " + ShopMssqlHelper.TablePrefix + "Product.ID = TEMP2.ProductID) As PageTable";
            class2.Fields = "[ID],[Name],[ClassID],[SellCount],[SellMoney]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            if (productSearch.ProductOrderType != string.Empty)
            {
                string productOrderType = productSearch.ProductOrderType;
                if (productOrderType != null)
                {
                    if (!(productOrderType == "SellCount"))
                    {
                        if (productOrderType == "SellMoney") class2.OrderField = "[SellMoney],[ID]";
                    }
                    else
                        class2.OrderField = "[SellCount],[ID]";
                }
            }
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, productSearch);
            class2.Count = count;
            count = class2.Count;
            return class2.ExecuteDataTable();
        }

        public void TaobaoProduct(ProductInfo product)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@spelling", SqlDbType.NVarChar), new SqlParameter("@classID", SqlDbType.NVarChar), new SqlParameter("@marketPrice", SqlDbType.Decimal), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@introduction", SqlDbType.NText), new SqlParameter("@summary", SqlDbType.NText), new SqlParameter("@totalStorageCount", SqlDbType.Int), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@taobaoID", SqlDbType.BigInt) };
            pt[0].Value = product.Name;
            pt[1].Value = product.Spelling;
            pt[2].Value = product.ClassID;
            pt[3].Value = product.MarketPrice;
            pt[4].Value = product.Photo;
            pt[5].Value = product.Introduction;
            pt[6].Value = product.Summary;
            pt[7].Value = product.TotalStorageCount;
            pt[8].Value = product.AddDate;
            pt[9].Value = product.TaobaoID;
            object obj2 = ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "TaobaoProduct", pt);
        }

        public void UnionUpdateProduct(string productIDList, ProductInfo product)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productIDList", SqlDbType.NVarChar), new SqlParameter("@marketPrice", SqlDbType.Decimal), new SqlParameter("@weight", SqlDbType.Int), new SqlParameter("@sendPoint", SqlDbType.Int), new SqlParameter("@totalStorageCount", SqlDbType.Int), new SqlParameter("@lowerCount", SqlDbType.Int), new SqlParameter("@upperCount", SqlDbType.Int) };
            pt[0].Value = productIDList;
            pt[1].Value = product.MarketPrice;
            pt[2].Value = product.Weight;
            pt[3].Value = product.SendPoint;
            pt[4].Value = product.TotalStorageCount;
            pt[5].Value = product.LowerCount;
            pt[6].Value = product.UpperCount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UnionUpdateProduct", pt);
        }

        public void UpdateProduct(ProductInfo product)
        {
            SqlParameter[] pt = new SqlParameter[] { 
                new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@spelling", SqlDbType.NVarChar), new SqlParameter("@color", SqlDbType.NVarChar), new SqlParameter("@fontStyle", SqlDbType.NVarChar), new SqlParameter("@productNumber", SqlDbType.NVarChar), new SqlParameter("@classID", SqlDbType.NVarChar), new SqlParameter("@brandID", SqlDbType.Int), new SqlParameter("@marketPrice", SqlDbType.Decimal), new SqlParameter("@weight", SqlDbType.Int), new SqlParameter("@sendPoint", SqlDbType.Int), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@keywords", SqlDbType.NVarChar), new SqlParameter("@summary", SqlDbType.NText), new SqlParameter("@introduction", SqlDbType.NText), new SqlParameter("@remark", SqlDbType.NText), 
                new SqlParameter("@isSpecial", SqlDbType.Int), new SqlParameter("@isNew", SqlDbType.Int), new SqlParameter("@isHot", SqlDbType.Int), new SqlParameter("@isSale", SqlDbType.Int), new SqlParameter("@isTop", SqlDbType.Int), new SqlParameter("@accessory", SqlDbType.NVarChar), new SqlParameter("@relationProduct", SqlDbType.NVarChar), new SqlParameter("@relationArticle", SqlDbType.NVarChar), new SqlParameter("@allowComment", SqlDbType.Int), new SqlParameter("@totalStorageCount", SqlDbType.Int), new SqlParameter("@lowerCount", SqlDbType.Int), new SqlParameter("@upperCount", SqlDbType.Int), new SqlParameter("@attributeClassID", SqlDbType.Int), new SqlParameter("@standardType", SqlDbType.Int)
             };
            pt[0].Value = product.ID;
            pt[1].Value = product.Name;
            pt[2].Value = product.Spelling;
            pt[3].Value = product.Color;
            pt[4].Value = product.FontStyle;
            pt[5].Value = product.ProductNumber;
            pt[6].Value = product.ClassID;
            pt[7].Value = product.BrandID;
            pt[8].Value = product.MarketPrice;
            pt[9].Value = product.Weight;
            pt[10].Value = product.SendPoint;
            pt[11].Value = product.Photo;
            pt[12].Value = product.Keywords;
            pt[13].Value = product.Summary;
            pt[14].Value = product.Introduction;
            pt[15].Value = product.Remark;
            pt[0x10].Value = product.IsSpecial;
            pt[0x11].Value = product.IsNew;
            pt[0x12].Value = product.IsHot;
            pt[0x13].Value = product.IsSale;
            pt[20].Value = product.IsTop;
            pt[0x15].Value = product.Accessory;
            pt[0x16].Value = product.RelationProduct;
            pt[0x17].Value = product.RelationArticle;
            pt[0x18].Value = product.AllowComment;
            pt[0x19].Value = product.TotalStorageCount;
            pt[0x1a].Value = product.LowerCount;
            pt[0x1b].Value = product.UpperCount;
            pt[0x1c].Value = product.AttributeClassID;
            pt[0x1d].Value = product.StandardType;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProduct", pt);
        }

        public void UpdateProductCoverPhoto(int id, string photo)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@photo", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = photo;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductCoverPhoto", pt);
        }

        public void UpdateProductStandardType(string strID, int standardType, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@standardType", SqlDbType.Int), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = standardType;
            pt[2].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductStandardType", pt);
        }
    }
}

