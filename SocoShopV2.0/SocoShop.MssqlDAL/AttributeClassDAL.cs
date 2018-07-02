namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class AttributeClassDAL : IAttributeClass
    {
        public int AddAttributeClass(AttributeClassInfo attributeClass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@attributeCount", SqlDbType.Int) };
            pt[0].Value = attributeClass.Name;
            pt[1].Value = attributeClass.AttributeCount;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddAttributeClass", pt));
        }

        public void ChangeAttributeClassCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAttributeClassCount", pt);
        }

        public void ChangeAttributeClassCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAttributeClassCountByGeneral", pt);
        }

        public void DeleteAttributeClass(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAttributeClass", pt);
        }

        public void PrepareAttributeClassModel(SqlDataReader dr, List<AttributeClassInfo> attributeClassList)
        {
            while (dr.Read())
            {
                AttributeClassInfo item = new AttributeClassInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.AttributeCount = dr.GetInt32(2);
                attributeClassList.Add(item);
            }
        }

        public List<AttributeClassInfo> ReadAttributeClassAllList()
        {
            List<AttributeClassInfo> attributeClassList = new List<AttributeClassInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadAttributeClassAllList"))
            {
                this.PrepareAttributeClassModel(reader, attributeClassList);
            }
            return attributeClassList;
        }

        public void UpdateAttributeClass(AttributeClassInfo attributeClass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@attributeCount", SqlDbType.Int) };
            pt[0].Value = attributeClass.ID;
            pt[1].Value = attributeClass.Name;
            pt[2].Value = attributeClass.AttributeCount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAttributeClass", pt);
        }
    }
}

