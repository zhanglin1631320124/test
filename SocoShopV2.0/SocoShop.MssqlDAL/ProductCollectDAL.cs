namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ProductCollectDAL : IProductCollect
    {
        public int AddProductCollect(ProductCollectInfo productCollect)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = productCollect.ProductID;
            pt[1].Value = productCollect.Date;
            pt[2].Value = productCollect.UserID;
            pt[3].Value = productCollect.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddProductCollect", pt));
        }

        public void DeleteProductCollect(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductCollect", pt);
        }

        public void PrepareProductCollectModel(SqlDataReader dr, List<ProductCollectInfo> productCollectList)
        {
            while (dr.Read())
            {
                ProductCollectInfo item = new ProductCollectInfo();
                item.ID = dr.GetInt32(0);
                item.ProductID = dr.GetInt32(1);
                item.Date = dr.GetDateTime(2);
                item.UserID = dr.GetInt32(3);
                item.UserName = dr[4].ToString();
                productCollectList.Add(item);
            }
        }

        public ProductCollectInfo ReadProductCollect(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            ProductCollectInfo info = new ProductCollectInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductCollect", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.ProductID = reader.GetInt32(1);
                    info.Date = reader.GetDateTime(2);
                    info.UserID = reader.GetInt32(3);
                    info.UserName = reader[4].ToString();
                }
            }
            return info;
        }

        public ProductCollectInfo ReadProductCollectByProductID(int productID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = productID;
            pt[1].Value = userID;
            ProductCollectInfo info = new ProductCollectInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductCollectByProductID", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.ProductID = reader.GetInt32(1);
                    info.Date = reader.GetDateTime(2);
                    info.UserID = reader.GetInt32(3);
                    info.UserName = reader[4].ToString();
                }
            }
            return info;
        }

        public string ReadProductCollectIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductCollectIDList", pt))
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

        public List<ProductCollectInfo> ReadProductCollectList(int currentPage, int pageSize, ref int count, int userID)
        {
            List<ProductCollectInfo> productCollectList = new List<ProductCollectInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "ProductCollect";
            class2.Fields = "[ID],[ProductID],[Date],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[UserID]", userID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareProductCollectModel(reader, productCollectList);
            }
            return productCollectList;
        }

        public void UpdateProductCollect(ProductCollectInfo productCollect)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@productID", SqlDbType.Int) };
            pt[0].Value = productCollect.ID;
            pt[1].Value = productCollect.ProductID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductCollect", pt);
        }
    }
}

