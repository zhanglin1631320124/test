namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ProductClassDAL : IProductClass
    {
        public int AddProductClass(ProductClassInfo productClass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@className", SqlDbType.NVarChar), new SqlParameter("@keywords", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@taobaoID", SqlDbType.BigInt) };
            pt[0].Value = productClass.FatherID;
            pt[1].Value = productClass.OrderID;
            pt[2].Value = productClass.ClassName;
            pt[3].Value = productClass.Keywords;
            pt[4].Value = productClass.Description;
            pt[5].Value = productClass.TaobaoID;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddProductClass", pt));
        }

        public void DeleteProductClass(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteProductClass", pt);
        }

        public void DeleteTaobaoProductClass()
        {
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteTaobaoProductClass");
        }

        public void MoveDownProductClass(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveDownProductClass", pt);
        }

        public void MoveUpProductClass(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveUpProductClass", pt);
        }

        public void PrepareProductClassModel(SqlDataReader dr, List<ProductClassInfo> productClassList)
        {
            while (dr.Read())
            {
                ProductClassInfo item = new ProductClassInfo();
                item.ID = dr.GetInt32(0);
                item.FatherID = dr.GetInt32(1);
                item.OrderID = dr.GetInt32(2);
                item.ClassName = dr[3].ToString();
                item.Keywords = dr[4].ToString();
                item.Description = dr[5].ToString();
                item.TaobaoID = dr.GetInt64(6);
                productClassList.Add(item);
            }
        }

        public List<ProductClassInfo> ReadProductClassAllList()
        {
            List<ProductClassInfo> productClassList = new List<ProductClassInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadProductClassAllList"))
            {
                this.PrepareProductClassModel(reader, productClassList);
            }
            return productClassList;
        }

        public void UpdateProductClass(ProductClassInfo productClass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@className", SqlDbType.NVarChar), new SqlParameter("@keywords", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText) };
            pt[0].Value = productClass.ID;
            pt[1].Value = productClass.FatherID;
            pt[2].Value = productClass.OrderID;
            pt[3].Value = productClass.ClassName;
            pt[4].Value = productClass.Keywords;
            pt[5].Value = productClass.Description;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductClass", pt);
        }

        public void UpdateProductFatherID(Dictionary<long, int> fatherIDDic)
        {
            foreach (KeyValuePair<long, int> pair in fatherIDDic)
            {
                SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@taobaoID", SqlDbType.BigInt), new SqlParameter("@systemID", SqlDbType.Int) };
                pt[0].Value = pair.Key;
                pt[1].Value = pair.Value;
                ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateProductFatherID", pt);
            }
        }
    }
}

