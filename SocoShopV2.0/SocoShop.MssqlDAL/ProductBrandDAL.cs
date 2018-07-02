namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ProductBrandDAL : IProductBrand
    {
        public int AddProductBrand(ProductBrandInfo productBrand)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@logo", SqlDbType.NVarChar), new SqlParameter("@url", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@isTop", SqlDbType.Int), new SqlParameter("@productCount", SqlDbType.Int) };
            pt[0].Value = productBrand.Name;
            pt[1].Value = productBrand.Logo;
            pt[2].Value = productBrand.Url;
            pt[3].Value = productBrand.Description;
            pt[4].Value = productBrand.OrderID;
            pt[5].Value = productBrand.IsTop;
            pt[6].Value = productBrand.ProductCount;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddProductBrand", pt));
        }

        public void ChangeProductBrandCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductBrandCount", pt);
        }

        public void ChangeProductBrandCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductBrandCountByGeneral", pt);
        }

        public void ChangeProductBrandOrder(ChangeAction action, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@action", SqlDbType.NVarChar), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = action.ToString();
            pt[1].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeProductBrandOrder", pt);
        }

        public void DeleteProductBrand(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductBrand", pt);
        }

        public void PrepareProductBrandModel(SqlDataReader dr, List<ProductBrandInfo> productBrandList)
        {
            while (dr.Read())
            {
                ProductBrandInfo item = new ProductBrandInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Logo = dr[2].ToString();
                item.Url = dr[3].ToString();
                item.Description = dr[4].ToString();
                item.OrderID = dr.GetInt32(5);
                item.IsTop = dr.GetInt32(6);
                item.ProductCount = dr.GetInt32(7);
                productBrandList.Add(item);
            }
        }

        public List<ProductBrandInfo> ReadProductBrandAllList()
        {
            List<ProductBrandInfo> productBrandList = new List<ProductBrandInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductBrandAllList"))
            {
                this.PrepareProductBrandModel(reader, productBrandList);
            }
            return productBrandList;
        }

        public void UpdateProductBrand(ProductBrandInfo productBrand)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@logo", SqlDbType.NVarChar), new SqlParameter("@url", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@isTop", SqlDbType.Int) };
            pt[0].Value = productBrand.ID;
            pt[1].Value = productBrand.Name;
            pt[2].Value = productBrand.Logo;
            pt[3].Value = productBrand.Url;
            pt[4].Value = productBrand.Description;
            pt[5].Value = productBrand.IsTop;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductBrand", pt);
        }
    }
}

