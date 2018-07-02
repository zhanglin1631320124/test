namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ProductPhotoDAL : IProductPhoto
    {
        public int AddProductPhoto(ProductPhotoInfo productPhoto)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar) };
            pt[0].Value = productPhoto.ProductID;
            pt[1].Value = productPhoto.Name;
            pt[2].Value = productPhoto.Photo;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddProductPhoto", pt));
        }

        public void DeleteProductPhoto(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductPhoto", pt);
        }

        public void DeleteProductPhotoByProductID(string strProductID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NVarChar) };
            pt[0].Value = strProductID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductPhotoByProductID", pt);
        }

        public void PrepareProductPhotoModel(SqlDataReader dr, List<ProductPhotoInfo> productPhotoList)
        {
            while (dr.Read())
            {
                ProductPhotoInfo item = new ProductPhotoInfo();
                item.ID = dr.GetInt32(0);
                item.ProductID = dr.GetInt32(1);
                item.Name = dr[2].ToString();
                item.Photo = dr[3].ToString();
                productPhotoList.Add(item);
            }
        }

        public ProductPhotoInfo ReadProductPhoto(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ProductPhotoInfo info = new ProductPhotoInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductPhoto", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.ProductID = reader.GetInt32(1);
                    info.Name = reader[2].ToString();
                    info.Photo = reader[3].ToString();
                }
            }
            return info;
        }

        public List<ProductPhotoInfo> ReadProductPhotoByProduct(int productID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int) };
            pt[0].Value = productID;
            List<ProductPhotoInfo> productPhotoList = new List<ProductPhotoInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductPhotoByProduct", pt))
            {
                this.PrepareProductPhotoModel(reader, productPhotoList);
            }
            return productPhotoList;
        }
    }
}

