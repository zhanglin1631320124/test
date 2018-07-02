namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class AdminDAL : IAdmin
    {
        public int AddAdmin(AdminInfo admin)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@email", SqlDbType.NVarChar), new SqlParameter("@groupID", SqlDbType.Int), new SqlParameter("@password", SqlDbType.NVarChar), new SqlParameter("@lastLoginIP", SqlDbType.NVarChar), new SqlParameter("@lastLoginDate", SqlDbType.DateTime), new SqlParameter("@loginTimes", SqlDbType.Int), new SqlParameter("@noteBook", SqlDbType.NText), new SqlParameter("@isCreate", SqlDbType.Int) };
            pt[0].Value = admin.Name;
            pt[1].Value = admin.Email;
            pt[2].Value = admin.GroupID;
            pt[3].Value = admin.Password;
            pt[4].Value = admin.LastLoginIP;
            pt[5].Value = admin.LastLoginDate;
            pt[6].Value = admin.LoginTimes;
            pt[7].Value = admin.NoteBook;
            pt[8].Value = admin.IsCreate;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddAdmin", pt));
        }

        public void ChangeAdminCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAdminCount", pt);
        }

        public void ChangeAdminCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAdminCountByGeneral", pt);
        }

        public void ChangePassword(int id, string newPassword)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@password", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = newPassword;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAdminPassword", pt);
        }

        public void ChangePassword(int id, string oldPassword, string newPassword)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@oldPassword", SqlDbType.NVarChar), new SqlParameter("@newPassword", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = oldPassword;
            pt[2].Value = newPassword;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAdminPassword", pt);
        }

        public AdminInfo CheckAdminLogin(string loginName, string loginPass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@loginName", SqlDbType.NVarChar), new SqlParameter("@loginPass", SqlDbType.NVarChar) };
            pt[0].Value = loginName;
            pt[1].Value = loginPass;
            AdminInfo info = new AdminInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "CheckAdminLogin", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.GroupID = reader.GetInt32(2);
                }
            }
            return info;
        }

        public void DeleteAdmin(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdmin", pt);
        }

        public void DeleteAdminByGroupID(string strGroupID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strGroupID", SqlDbType.NVarChar) };
            pt[0].Value = strGroupID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdminByGroupID", pt);
        }

        public void PrepareAdminModel(SqlDataReader dr, List<AdminInfo> adminList)
        {
            while (dr.Read())
            {
                AdminInfo item = new AdminInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Email = dr[2].ToString();
                item.GroupID = dr.GetInt32(3);
                item.Password = dr[4].ToString();
                item.LastLoginIP = dr[5].ToString();
                item.LastLoginDate = dr.GetDateTime(6);
                item.LoginTimes = dr.GetInt32(7);
                item.NoteBook = dr[8].ToString();
                item.IsCreate = dr.GetInt32(9);
                adminList.Add(item);
            }
        }

        public AdminInfo ReadAdmin(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar) };
            pt[0].Value = id;
            AdminInfo info = new AdminInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadAdmin", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Email = reader[2].ToString();
                    info.GroupID = reader.GetInt32(3);
                    info.Password = reader[4].ToString();
                    info.LastLoginIP = reader[5].ToString();
                    info.LastLoginDate = reader.GetDateTime(6);
                    info.LoginTimes = reader.GetInt32(7);
                    info.NoteBook = reader[8].ToString();
                    info.IsCreate = reader.GetInt32(9);
                }
            }
            return info;
        }

        public List<AdminInfo> ReadAdminList(int currentPage, int pageSize, ref int count)
        {
            List<AdminInfo> adminList = new List<AdminInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Admin";
            class2.Fields = "[ID],[Name],[Email],[GroupID],[Password],[LastLoginIP],[LastLoginDate],[LoginTimes],[NoteBook],[IsCreate]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareAdminModel(reader, adminList);
            }
            return adminList;
        }

        public List<AdminInfo> ReadAdminList(int groupID, int currentPage, int pageSize, ref int count)
        {
            List<AdminInfo> adminList = new List<AdminInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Admin";
            class2.Fields = "[ID],[Name],[Email],[GroupID],[Password],[LastLoginIP],[LastLoginDate],[LoginTimes],[NoteBook],[IsCreate]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[GroupID]", groupID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareAdminModel(reader, adminList);
            }
            return adminList;
        }

        public void UpdateAdmin(AdminInfo admin)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@email", SqlDbType.NVarChar), new SqlParameter("@groupID", SqlDbType.Int), new SqlParameter("@noteBook", SqlDbType.NText) };
            pt[0].Value = admin.ID;
            pt[1].Value = admin.Name;
            pt[2].Value = admin.Email;
            pt[3].Value = admin.GroupID;
            pt[4].Value = admin.NoteBook;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAdmin", pt);
        }

        public void UpdateAdminLogin(int id, DateTime lastLoginDate, string lastLoginIP)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@lastLoginDate", SqlDbType.DateTime), new SqlParameter("@lastLoginIP", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = lastLoginDate;
            pt[2].Value = lastLoginIP;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAdminLogin", pt);
        }
    }
}

