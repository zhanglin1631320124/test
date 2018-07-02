namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserAccountRecordDAL : IUserAccountRecord
    {
        public int AddUserAccountRecord(UserAccountRecordInfo userAccountRecord)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@money", SqlDbType.Decimal), new SqlParameter("@point", SqlDbType.Int), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@iP", SqlDbType.NVarChar), new SqlParameter("@note", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userAccountRecord.Money;
            pt[1].Value = userAccountRecord.Point;
            pt[2].Value = userAccountRecord.Date;
            pt[3].Value = userAccountRecord.IP;
            pt[4].Value = userAccountRecord.Note;
            pt[5].Value = userAccountRecord.UserID;
            pt[6].Value = userAccountRecord.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserAccountRecord", pt));
        }

        public void DeleteUserAccountRecord(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserAccountRecord", pt);
        }

        public void DeleteUserAccountRecordByUserID(string strUserID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strUserID", SqlDbType.NVarChar) };
            pt[0].Value = strUserID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserAccountRecordByUserID", pt);
        }

        public void PrepareUserAccountRecordModel(SqlDataReader dr, List<UserAccountRecordInfo> userAccountRecordList)
        {
            while (dr.Read())
            {
                UserAccountRecordInfo item = new UserAccountRecordInfo();
                item.ID = dr.GetInt32(0);
                item.Money = dr.GetDecimal(1);
                item.Point = dr.GetInt32(2);
                item.Date = dr.GetDateTime(3);
                item.IP = dr[4].ToString();
                item.Note = dr[5].ToString();
                item.UserID = dr.GetInt32(6);
                item.UserName = dr[7].ToString();
                userAccountRecordList.Add(item);
            }
        }

        public decimal ReadMoneyLeftBeforID(int id, int userID)
        {
            decimal num = 0M;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            object obj2 = ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "ReadMoneyLeftBeforID", pt);
            if (obj2 != DBNull.Value) num = Convert.ToDecimal(obj2);
            return num;
        }

        public int ReadPointLeftBeforID(int id, int userID)
        {
            int num = 0;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            object obj2 = ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "ReadPointLeftBeforID", pt);
            if (obj2 != DBNull.Value) num = Convert.ToInt32(obj2);
            return num;
        }

        public UserAccountRecordInfo ReadUserAccountRecord(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            UserAccountRecordInfo info = new UserAccountRecordInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserAccountRecord", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Money = reader.GetDecimal(1);
                    info.Point = reader.GetInt32(2);
                    info.Date = reader.GetDateTime(3);
                    info.IP = reader[4].ToString();
                    info.Note = reader[5].ToString();
                    info.UserID = reader.GetInt32(6);
                    info.UserName = reader[7].ToString();
                }
            }
            return info;
        }

        public List<UserAccountRecordInfo> ReadUserAccountRecordList(int userID)
        {
            List<UserAccountRecordInfo> userAccountRecordList = new List<UserAccountRecordInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserAccountRecordListByUserID", pt))
            {
                this.PrepareUserAccountRecordModel(reader, userAccountRecordList);
            }
            return userAccountRecordList;
        }

        public List<UserAccountRecordInfo> ReadUserAccountRecordList(int currentPage, int pageSize, ref int count, int userID, int accountType)
        {
            List<UserAccountRecordInfo> userAccountRecordList = new List<UserAccountRecordInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "UserAccountRecord";
            class2.Fields = "[ID],[Money],[Point],[Date],[IP],[Note],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Asc;
            class2.MssqlCondition.Add("[UserID]", userID, ConditionType.Equal);
            if (accountType == 1)
                class2.MssqlCondition.Add("[Money]!=0");
            else
                class2.MssqlCondition.Add("[Point]!=0");
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareUserAccountRecordModel(reader, userAccountRecordList);
            }
            return userAccountRecordList;
        }
    }
}

