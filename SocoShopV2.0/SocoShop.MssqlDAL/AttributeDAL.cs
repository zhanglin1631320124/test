namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class AttributeDAL : IAttribute
    {
        public int AddAttribute(AttributeInfo attribute)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@attributeClassID", SqlDbType.Int), new SqlParameter("@inputType", SqlDbType.Int), new SqlParameter("@inputValue", SqlDbType.NVarChar), new SqlParameter("@orderID", SqlDbType.Int) };
            pt[0].Value = attribute.Name;
            pt[1].Value = attribute.AttributeClassID;
            pt[2].Value = attribute.InputType;
            pt[3].Value = attribute.InputValue;
            pt[4].Value = attribute.OrderID;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddAttribute", pt));
        }

        public void ChangeAttributeOrder(ChangeAction action, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@action", SqlDbType.NVarChar), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = action.ToString();
            pt[1].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAttributeOrder", pt);
        }

        public void DeleteAttribute(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAttribute", pt);
        }

        public void DeleteAttributeByAttributeClassID(string strAttributeClassID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strAttributeClassID", SqlDbType.NVarChar) };
            pt[0].Value = strAttributeClassID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAttributeByAttributeClassID", pt);
        }

        public void PrepareAttributeModel(SqlDataReader dr, List<AttributeInfo> attributeList)
        {
            while (dr.Read())
            {
                AttributeInfo item = new AttributeInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.AttributeClassID = dr.GetInt32(2);
                item.InputType = dr.GetInt32(3);
                item.InputValue = dr[4].ToString();
                item.OrderID = dr.GetInt32(5);
                attributeList.Add(item);
            }
        }

        public List<AttributeInfo> ReadAttributeAllList()
        {
            List<AttributeInfo> attributeList = new List<AttributeInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadAttributeAllList"))
            {
                this.PrepareAttributeModel(reader, attributeList);
            }
            return attributeList;
        }

        public void UpdateAttribute(AttributeInfo attribute)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@attributeClassID", SqlDbType.Int), new SqlParameter("@inputType", SqlDbType.Int), new SqlParameter("@inputValue", SqlDbType.NVarChar) };
            pt[0].Value = attribute.ID;
            pt[1].Value = attribute.Name;
            pt[2].Value = attribute.AttributeClassID;
            pt[3].Value = attribute.InputType;
            pt[4].Value = attribute.InputValue;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAttribute", pt);
        }
    }
}

