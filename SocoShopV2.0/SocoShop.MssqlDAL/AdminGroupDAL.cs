namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class AdminGroupDAL : IAdminGroup
    {
        public int AddAdminGroup(AdminGroupInfo adminGroup)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@power", SqlDbType.NText), new SqlParameter("@adminCount", SqlDbType.Int), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@iP", SqlDbType.NVarChar), new SqlParameter("@note", SqlDbType.NText) };
            pt[0].Value = adminGroup.Name;
            pt[1].Value = adminGroup.Power;
            pt[2].Value = adminGroup.AdminCount;
            pt[3].Value = adminGroup.AddDate;
            pt[4].Value = adminGroup.IP;
            pt[5].Value = adminGroup.Note;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddAdminGroup", pt));
        }

        public void ChangeAdminGroupCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAdminGroupCount", pt);
        }

        public void ChangeAdminGroupCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAdminGroupCountByGeneral", pt);
        }

        public void DeleteAdminGroup(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdminGroup", pt);
        }

        public void PrepareAdminGroupModel(SqlDataReader dr, List<AdminGroupInfo> adminGroupList)
        {
            while (dr.Read())
            {
                AdminGroupInfo item = new AdminGroupInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Power = dr[2].ToString();
                item.AdminCount = dr.GetInt32(3);
                item.AddDate = dr.GetDateTime(4);
                item.IP = dr[5].ToString();
                item.Note = dr[6].ToString();
                adminGroupList.Add(item);
            }
        }

        public List<AdminGroupInfo> ReadAdminGroupAllList()
        {
            List<AdminGroupInfo> adminGroupList = new List<AdminGroupInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadAdminGroupAllList"))
            {
                this.PrepareAdminGroupModel(reader, adminGroupList);
            }
            return adminGroupList;
        }

        public void UpdateAdminGroup(AdminGroupInfo adminGroup)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@power", SqlDbType.NText), new SqlParameter("@note", SqlDbType.NText) };
            pt[0].Value = adminGroup.ID;
            pt[1].Value = adminGroup.Name;
            pt[2].Value = adminGroup.Power;
            pt[3].Value = adminGroup.Note;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAdminGroup", pt);
        }
    }
}

