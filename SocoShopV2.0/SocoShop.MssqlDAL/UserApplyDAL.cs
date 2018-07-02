namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserApplyDAL : IUserApply
    {
        public int AddUserApply(UserApplyInfo userApply)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@number", SqlDbType.NVarChar), new SqlParameter("@money", SqlDbType.Decimal), new SqlParameter("@userNote", SqlDbType.NVarChar), new SqlParameter("@status", SqlDbType.Int), new SqlParameter("@applyDate", SqlDbType.DateTime), new SqlParameter("@applyIP", SqlDbType.NVarChar), new SqlParameter("@adminNote", SqlDbType.NVarChar), new SqlParameter("@updateDate", SqlDbType.DateTime), new SqlParameter("@updateAdminID", SqlDbType.Int), new SqlParameter("@updateAdminName", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userApply.Number;
            pt[1].Value = userApply.Money;
            pt[2].Value = userApply.UserNote;
            pt[3].Value = userApply.Status;
            pt[4].Value = userApply.ApplyDate;
            pt[5].Value = userApply.ApplyIP;
            pt[6].Value = userApply.AdminNote;
            pt[7].Value = userApply.UpdateDate;
            pt[8].Value = userApply.UpdateAdminID;
            pt[9].Value = userApply.UpdateAdminName;
            pt[10].Value = userApply.UserID;
            pt[11].Value = userApply.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserApply", pt));
        }

        public void DeleteUserApply(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserApply", pt);
        }

        public void DeleteUserApplyByUserID(string strUserID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strUserID", SqlDbType.NVarChar) };
            pt[0].Value = strUserID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserApplyByUserID", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, UserApplySearchInfo userApplySearch)
        {
            mssqlCondition.Add("[Number]", userApplySearch.Number, ConditionType.Like);
            mssqlCondition.Add("[Status]", userApplySearch.Status, ConditionType.Equal);
            mssqlCondition.Add("[ApplyDate]", userApplySearch.StartApplyDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[ApplyDate]", userApplySearch.EndApplyDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[UserID]", userApplySearch.UserID, ConditionType.Equal);
            mssqlCondition.Add("[UserName]", userApplySearch.UserName, ConditionType.Like);
        }

        public void PrepareUserApplyModel(SqlDataReader dr, List<UserApplyInfo> userApplyList)
        {
            while (dr.Read())
            {
                UserApplyInfo item = new UserApplyInfo();
                item.ID = dr.GetInt32(0);
                item.Number = dr[1].ToString();
                item.Money = dr.GetDecimal(2);
                item.UserNote = dr[3].ToString();
                item.Status = dr.GetInt32(4);
                item.ApplyDate = dr.GetDateTime(5);
                item.ApplyIP = dr[6].ToString();
                item.AdminNote = dr[7].ToString();
                item.UpdateDate = dr.GetDateTime(8);
                item.UpdateAdminID = dr.GetInt32(9);
                item.UpdateAdminName = dr[10].ToString();
                item.UserID = dr.GetInt32(11);
                item.UserName = dr[12].ToString();
                userApplyList.Add(item);
            }
        }

        public UserApplyInfo ReadUserApply(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            UserApplyInfo info = new UserApplyInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserApply", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Number = reader[1].ToString();
                    info.Money = reader.GetDecimal(2);
                    info.UserNote = reader[3].ToString();
                    info.Status = reader.GetInt32(4);
                    info.ApplyDate = reader.GetDateTime(5);
                    info.ApplyIP = reader[6].ToString();
                    info.AdminNote = reader[7].ToString();
                    info.UpdateDate = reader.GetDateTime(8);
                    info.UpdateAdminID = reader.GetInt32(9);
                    info.UpdateAdminName = reader[10].ToString();
                    info.UserID = reader.GetInt32(11);
                    info.UserName = reader[12].ToString();
                }
            }
            return info;
        }

        public string ReadUserApplyIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserApplyIDList", pt))
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

        public List<UserApplyInfo> SearchUserApplyList(UserApplySearchInfo userApplySearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userApplySearch);
            List<UserApplyInfo> userApplyList = new List<UserApplyInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchUserApplyList", pt))
            {
                this.PrepareUserApplyModel(reader, userApplyList);
            }
            return userApplyList;
        }

        public List<UserApplyInfo> SearchUserApplyList(int currentPage, int pageSize, UserApplySearchInfo userApplySearch, ref int count)
        {
            List<UserApplyInfo> userApplyList = new List<UserApplyInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "UserApply";
            class2.Fields = "[ID],[Number],[Money],[UserNote],[Status],[ApplyDate],[ApplyIP],[AdminNote],[UpdateDate],[UpdateAdminID],[UpdateAdminName],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userApplySearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareUserApplyModel(reader, userApplyList);
            }
            return userApplyList;
        }

        public void UpdateUserApply(UserApplyInfo userApply)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@status", SqlDbType.Int), new SqlParameter("@adminNote", SqlDbType.NVarChar), new SqlParameter("@updateDate", SqlDbType.DateTime), new SqlParameter("@updateAdminID", SqlDbType.Int), new SqlParameter("@updateAdminName", SqlDbType.NVarChar) };
            pt[0].Value = userApply.ID;
            pt[1].Value = userApply.Status;
            pt[2].Value = userApply.AdminNote;
            pt[3].Value = userApply.UpdateDate;
            pt[4].Value = userApply.UpdateAdminID;
            pt[5].Value = userApply.UpdateAdminName;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserApply", pt);
        }
    }
}

