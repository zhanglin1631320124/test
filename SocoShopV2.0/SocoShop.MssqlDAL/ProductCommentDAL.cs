namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ProductCommentDAL : IProductComment
    {
        public int AddProductComment(ProductCommentInfo productComment)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@userIP", SqlDbType.NVarChar), new SqlParameter("@postDate", SqlDbType.DateTime), new SqlParameter("@support", SqlDbType.Int), new SqlParameter("@against", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int), new SqlParameter("@rank", SqlDbType.Int), new SqlParameter("@replyCount", SqlDbType.Int), new SqlParameter("@adminReplyContent", SqlDbType.NVarChar), new SqlParameter("@adminReplyDate", SqlDbType.DateTime), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = productComment.ProductID;
            pt[1].Value = productComment.Title;
            pt[2].Value = productComment.Content;
            pt[3].Value = productComment.UserIP;
            pt[4].Value = productComment.PostDate;
            pt[5].Value = productComment.Support;
            pt[6].Value = productComment.Against;
            pt[7].Value = productComment.Status;
            pt[8].Value = productComment.Rank;
            pt[9].Value = productComment.ReplyCount;
            pt[10].Value = productComment.AdminReplyContent;
            pt[11].Value = productComment.AdminReplyDate;
            pt[12].Value = productComment.UserID;
            pt[13].Value = productComment.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddProductComment", pt));
        }

        public void ChangeProductCommentAgainstCount(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCommentAgainstCount", pt);
        }

        public void ChangeProductCommentCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCommentCount", pt);
        }

        public void ChangeProductCommentCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCommentCountByGeneral", pt);
        }

        public void ChangeProductCommentStatus(string strID, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@status", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCommentStatus", pt);
        }

        public void ChangeProductCommentSupportCount(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductCommentSupportCount", pt);
        }

        public void DeleteProductComment(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductComment", pt);
        }

        public void DeleteProductCommentByProductID(string strProductID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NVarChar) };
            pt[0].Value = strProductID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductCommentByProductID", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, ProductCommentSearchInfo productCommentSearch)
        {
            mssqlCondition.Add("[Name]", productCommentSearch.ProductName, ConditionType.Like);
            mssqlCondition.Add("[Title]", productCommentSearch.Title, ConditionType.Like);
            mssqlCondition.Add("[Content]", productCommentSearch.Content, ConditionType.Like);
            mssqlCondition.Add("[UserIP]", productCommentSearch.UserIP, ConditionType.Like);
            mssqlCondition.Add("[PostDate]", productCommentSearch.StartPostDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[PostDate]", productCommentSearch.EndPostDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[UserID]", productCommentSearch.UserID, ConditionType.Equal);
            mssqlCondition.Add("[Status]", productCommentSearch.Status, ConditionType.Equal);
            mssqlCondition.Add("[ProductID]", productCommentSearch.ProductID, ConditionType.Equal);
        }

        public void PrepareProductCommentModel(SqlDataReader dr, List<ProductCommentInfo> productCommentList)
        {
            while (dr.Read())
            {
                ProductCommentInfo item = new ProductCommentInfo();
                item.ID = dr.GetInt32(0);
                item.ProductID = dr.GetInt32(1);
                item.Title = dr[2].ToString();
                item.Content = dr[3].ToString();
                item.UserIP = dr[4].ToString();
                item.PostDate = dr.GetDateTime(5);
                item.Support = dr.GetInt32(6);
                item.Against = dr.GetInt32(7);
                item.Status = dr.GetInt32(8);
                item.Rank = dr.GetInt32(9);
                item.ReplyCount = dr.GetInt32(10);
                item.AdminReplyContent = dr[11].ToString();
                item.AdminReplyDate = dr.GetDateTime(12);
                item.UserID = dr.GetInt32(13);
                item.UserName = dr[14].ToString();
                productCommentList.Add(item);
            }
        }

        public ProductCommentInfo ReadProductComment(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            ProductCommentInfo info = new ProductCommentInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductComment", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.ProductID = reader.GetInt32(1);
                    info.Title = reader[2].ToString();
                    info.Content = reader[3].ToString();
                    info.UserIP = reader[4].ToString();
                    info.PostDate = reader.GetDateTime(5);
                    info.Support = reader.GetInt32(6);
                    info.Against = reader.GetInt32(7);
                    info.Status = reader.GetInt32(8);
                    info.Rank = reader.GetInt32(9);
                    info.ReplyCount = reader.GetInt32(10);
                    info.AdminReplyContent = reader[11].ToString();
                    info.AdminReplyDate = reader.GetDateTime(12);
                    info.UserID = reader.GetInt32(13);
                    info.UserName = reader[14].ToString();
                }
            }
            return info;
        }

        public string ReadProductCommentIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductCommentIDList", pt))
            {
                while (reader.Read())
                {
                    if (str == string.Empty)
                        str = reader.GetInt32(0).ToString();
                    else
                        str = str + "," + reader.GetInt32(0).ToString();
                }
            }
            return str;
        }

        public List<ProductCommentInfo> SearchProductCommentInnerList(int currentPage, int pageSize, ProductCommentSearchInfo productCommentSearch, ref int count)
        {
            List<ProductCommentInfo> list = new List<ProductCommentInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = string.Concat(new object[] { ' ', ShopMssqlHelper.TablePrefix, "ProductComment INNER JOIN ", ShopMssqlHelper.TablePrefix, "Product ON ", ShopMssqlHelper.TablePrefix, "ProductComment.[ProductID]=", ShopMssqlHelper.TablePrefix, "Product.[ID] " });
            class2.Fields = ShopMssqlHelper.TablePrefix + "ProductComment.[ID],[ProductID],[Title],[UserIP],[PostDate],[Support],[Against],[Status],[Rank],[ReplyCount],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName],[Name]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = ShopMssqlHelper.TablePrefix + "ProductComment.[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, productCommentSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProductCommentInfo item = new ProductCommentInfo();
                    item.ID = reader.GetInt32(0);
                    item.ProductID = reader.GetInt32(1);
                    item.Title = reader[2].ToString();
                    item.UserIP = reader[3].ToString();
                    item.PostDate = reader.GetDateTime(4);
                    item.Support = reader.GetInt32(5);
                    item.Against = reader.GetInt32(6);
                    item.Status = reader.GetInt32(7);
                    item.Rank = reader.GetInt32(8);
                    item.ReplyCount = reader.GetInt32(9);
                    item.AdminReplyContent = reader[10].ToString();
                    item.AdminReplyDate = reader.GetDateTime(11);
                    item.UserID = reader.GetInt32(12);
                    item.UserName = reader[13].ToString();
                    item.Product.Name = reader[14].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<ProductCommentInfo> SearchProductCommentList(ProductCommentSearchInfo productCommentSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, productCommentSearch);
            List<ProductCommentInfo> productCommentList = new List<ProductCommentInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchProductCommentList", pt))
            {
                this.PrepareProductCommentModel(reader, productCommentList);
            }
            return productCommentList;
        }

        public List<ProductCommentInfo> SearchProductCommentList(int currentPage, int pageSize, ProductCommentSearchInfo productCommentSearch, ref int count)
        {
            List<ProductCommentInfo> productCommentList = new List<ProductCommentInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "ProductComment";
            class2.Fields = "[ID],[ProductID],[Title],[Content],[UserIP],[PostDate],[Support],[Against],[Status],[Rank],[ReplyCount],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, productCommentSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareProductCommentModel(reader, productCommentList);
            }
            return productCommentList;
        }

        public void UpdateProductComment(ProductCommentInfo productComment)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int), new SqlParameter("@adminReplyContent", SqlDbType.NVarChar), new SqlParameter("@adminReplyDate", SqlDbType.DateTime) };
            pt[0].Value = productComment.ID;
            pt[1].Value = productComment.Status;
            pt[2].Value = productComment.AdminReplyContent;
            pt[3].Value = productComment.AdminReplyDate;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductComment", pt);
        }
    }
}

