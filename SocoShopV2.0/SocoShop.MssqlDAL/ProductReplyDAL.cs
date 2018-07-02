namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ProductReplyDAL : IProductReply
    {
        public int AddProductReply(ProductReplyInfo productReply)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@commentID", SqlDbType.Int), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@userIP", SqlDbType.NVarChar), new SqlParameter("@postDate", SqlDbType.DateTime), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = productReply.ProductID;
            pt[1].Value = productReply.CommentID;
            pt[2].Value = productReply.Content;
            pt[3].Value = productReply.UserIP;
            pt[4].Value = productReply.PostDate;
            pt[5].Value = productReply.UserID;
            pt[6].Value = productReply.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddProductReply", pt));
        }

        public void DeleteProductReply(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductReply", pt);
        }

        public void DeleteProductReplyByCommentID(string strCommentID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strCommentID", SqlDbType.NVarChar) };
            pt[0].Value = strCommentID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductReplyByCommentID", pt);
        }

        public void DeleteProductReplyByProductID(string strProductID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NVarChar) };
            pt[0].Value = strProductID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductReplyByProductID", pt);
        }

        public void PrepareProductReplyModel(SqlDataReader dr, List<ProductReplyInfo> productReplyList)
        {
            while (dr.Read())
            {
                ProductReplyInfo item = new ProductReplyInfo();
                item.ID = dr.GetInt32(0);
                item.ProductID = dr.GetInt32(1);
                item.CommentID = dr.GetInt32(2);
                item.Content = dr[3].ToString();
                item.UserIP = dr[4].ToString();
                item.PostDate = dr.GetDateTime(5);
                item.UserID = dr.GetInt32(6);
                item.UserName = dr[7].ToString();
                productReplyList.Add(item);
            }
        }

        public ProductReplyInfo ReadProductReply(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            ProductReplyInfo info = new ProductReplyInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductReply", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.ProductID = reader.GetInt32(1);
                    info.CommentID = reader.GetInt32(2);
                    info.Content = reader[3].ToString();
                    info.UserIP = reader[4].ToString();
                    info.PostDate = reader.GetDateTime(5);
                    info.UserID = reader.GetInt32(6);
                    info.UserName = reader[7].ToString();
                }
            }
            return info;
        }

        public List<ProductReplyInfo> ReadProductReplyList(int currentPage, int pageSize, ref int count, int userID)
        {
            List<ProductReplyInfo> productReplyList = new List<ProductReplyInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "ProductReply";
            class2.Fields = "[ID],[ProductID],[CommentID],[Content],[UserIP],[PostDate],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[UserID]", userID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareProductReplyModel(reader, productReplyList);
            }
            return productReplyList;
        }

        public List<ProductReplyInfo> ReadProductReplyList(int commentID, int currentPage, int pageSize, ref int count, int userID)
        {
            List<ProductReplyInfo> productReplyList = new List<ProductReplyInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "ProductReply";
            class2.Fields = "[ID],[ProductID],[CommentID],[Content],[UserIP],[PostDate],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[UserID]", userID, ConditionType.Equal);
            class2.MssqlCondition.Add("[CommentID]", commentID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareProductReplyModel(reader, productReplyList);
            }
            return productReplyList;
        }

        public void UpdateProductReply(ProductReplyInfo productReply)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@content", SqlDbType.NText) };
            pt[0].Value = productReply.ID;
            pt[1].Value = productReply.Content;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductReply", pt);
        }
    }
}

