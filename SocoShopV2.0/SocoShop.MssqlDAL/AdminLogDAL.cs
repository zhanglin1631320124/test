namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class AdminLogDAL : IAdminLog
    {
        public void AddAdminLog(AdminLogInfo adminLog)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@groupID", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar), new SqlParameter("@iP", SqlDbType.NVarChar), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@adminID", SqlDbType.Int), new SqlParameter("@adminName", SqlDbType.NVarChar) };
            pt[0].Value = adminLog.GroupID;
            pt[1].Value = adminLog.Action;
            pt[2].Value = adminLog.IP;
            pt[3].Value = adminLog.AddDate;
            pt[4].Value = adminLog.AdminID;
            pt[5].Value = adminLog.AdminName;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "AddAdminLog", pt);
        }

        public void DeleteAdminLog(string strID, int adminID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@adminID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = adminID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdminLog", pt);
        }

        public void DeleteAdminLogByAdminID(string strAdminID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strAdminID", SqlDbType.NVarChar) };
            pt[0].Value = strAdminID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdminLogByAdminID", pt);
        }

        public void DeleteAdminLogByGroupID(string strGroupID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strGroupID", SqlDbType.NVarChar) };
            pt[0].Value = strGroupID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdminLogByGroupID", pt);
        }

        public void PrepareAdminLogModel(SqlDataReader dr, List<AdminLogInfo> adminLogList)
        {
            while (dr.Read())
            {
                AdminLogInfo item = new AdminLogInfo();
                item.ID = dr.GetInt32(0);
                item.GroupID = dr.GetInt32(1);
                item.Action = dr[2].ToString();
                item.IP = dr[3].ToString();
                item.AddDate = dr.GetDateTime(4);
                item.AdminID = dr.GetInt32(5);
                item.AdminName = dr[6].ToString();
                adminLogList.Add(item);
            }
        }

        public AdminLogInfo ReadAdminLog(int id, int adminID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar), new SqlParameter("@adminID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = adminID;
            AdminLogInfo info = new AdminLogInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadAdminLog", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.GroupID = reader.GetInt32(1);
                    info.Action = reader[2].ToString();
                    info.IP = reader[3].ToString();
                    info.AddDate = reader.GetDateTime(4);
                    info.AdminID = reader.GetInt32(5);
                    info.AdminName = reader[6].ToString();
                }
            }
            return info;
        }

        public List<AdminLogInfo> ReadAdminLogList(int currentPage, int pageSize, ref int count, int adminID)
        {
            List<AdminLogInfo> adminLogList = new List<AdminLogInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "AdminLog";
            class2.Fields = "[ID],[GroupID],[Action],[IP],[AddDate],[AdminID],[AdminName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[AdminID]", adminID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareAdminLogModel(reader, adminLogList);
            }
            return adminLogList;
        }

        public void UpdateAdminLog(AdminLogInfo adminLog)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = adminLog.ID;
            pt[1].Value = adminLog.Action;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAdminLog", pt);
        }
    }
}

