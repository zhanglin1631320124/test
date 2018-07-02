namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserRechargeDAL : IUserRecharge
    {
        public int AddUserRecharge(UserRechargeInfo userRecharge)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@number", SqlDbType.NVarChar), new SqlParameter("@money", SqlDbType.Decimal), new SqlParameter("@payKey", SqlDbType.NVarChar), new SqlParameter("@payName", SqlDbType.NVarChar), new SqlParameter("@rechargeDate", SqlDbType.DateTime), new SqlParameter("@rechargeIP", SqlDbType.NVarChar), new SqlParameter("@isFinish", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userRecharge.Number;
            pt[1].Value = userRecharge.Money;
            pt[2].Value = userRecharge.PayKey;
            pt[3].Value = userRecharge.PayName;
            pt[4].Value = userRecharge.RechargeDate;
            pt[5].Value = userRecharge.RechargeIP;
            pt[6].Value = userRecharge.IsFinish;
            pt[7].Value = userRecharge.UserID;
            pt[8].Value = userRecharge.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserRecharge", pt));
        }

        public void DeleteUserRecharge(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserRecharge", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, UserRechargeSearchInfo userRechargeSearch)
        {
            mssqlCondition.Add("[Number]", userRechargeSearch.Number, ConditionType.Like);
            mssqlCondition.Add("[RechargeDate]", userRechargeSearch.StartRechargeDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[RechargeDate]", userRechargeSearch.EndRechargeDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[IsFinish]", userRechargeSearch.IsFinish, ConditionType.Equal);
            mssqlCondition.Add("[UserID]", userRechargeSearch.UserID, ConditionType.Equal);
            mssqlCondition.Add("[UserName]", userRechargeSearch.UserName, ConditionType.Like);
        }

        public void PrepareUserRechargeModel(SqlDataReader dr, List<UserRechargeInfo> userRechargeList)
        {
            while (dr.Read())
            {
                UserRechargeInfo item = new UserRechargeInfo();
                item.ID = dr.GetInt32(0);
                item.Number = dr[1].ToString();
                item.Money = dr.GetDecimal(2);
                item.PayKey = dr[3].ToString();
                item.PayName = dr[4].ToString();
                item.RechargeDate = dr.GetDateTime(5);
                item.RechargeIP = dr[6].ToString();
                item.IsFinish = dr.GetInt32(7);
                item.UserID = dr.GetInt32(8);
                item.UserName = dr[9].ToString();
                userRechargeList.Add(item);
            }
        }

        public UserRechargeInfo ReadUserRecharge(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            UserRechargeInfo info = new UserRechargeInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserRecharge", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Number = reader[1].ToString();
                    info.Money = reader.GetDecimal(2);
                    info.PayKey = reader[3].ToString();
                    info.PayName = reader[4].ToString();
                    info.RechargeDate = reader.GetDateTime(5);
                    info.RechargeIP = reader[6].ToString();
                    info.IsFinish = reader.GetInt32(7);
                    info.UserID = reader.GetInt32(8);
                    info.UserName = reader[9].ToString();
                }
            }
            return info;
        }

        public UserRechargeInfo ReadUserRechargeByNumber(string number, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@number", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = number;
            pt[1].Value = userID;
            UserRechargeInfo info = new UserRechargeInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserRechargeByNumber", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Number = reader[1].ToString();
                    info.Money = reader.GetDecimal(2);
                    info.PayKey = reader[3].ToString();
                    info.PayName = reader[4].ToString();
                    info.RechargeDate = reader.GetDateTime(5);
                    info.RechargeIP = reader[6].ToString();
                    info.IsFinish = reader.GetInt32(7);
                    info.UserID = reader.GetInt32(8);
                    info.UserName = reader[9].ToString();
                }
            }
            return info;
        }

        public string ReadUserRechargeIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserRechargeIDList", pt))
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

        public List<UserRechargeInfo> SearchUserRechargeList(UserRechargeSearchInfo userRechargeSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userRechargeSearch);
            List<UserRechargeInfo> userRechargeList = new List<UserRechargeInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchUserRechargeList", pt))
            {
                this.PrepareUserRechargeModel(reader, userRechargeList);
            }
            return userRechargeList;
        }

        public List<UserRechargeInfo> SearchUserRechargeList(int currentPage, int pageSize, UserRechargeSearchInfo userRechargeSearch, ref int count)
        {
            List<UserRechargeInfo> userRechargeList = new List<UserRechargeInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "UserRecharge";
            class2.Fields = "[ID],[Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userRechargeSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareUserRechargeModel(reader, userRechargeList);
            }
            return userRechargeList;
        }

        public void UpdateUserRecharge(UserRechargeInfo userRecharge)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@isFinish", SqlDbType.Int) };
            pt[0].Value = userRecharge.ID;
            pt[1].Value = userRecharge.IsFinish;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserRecharge", pt);
        }
    }
}

