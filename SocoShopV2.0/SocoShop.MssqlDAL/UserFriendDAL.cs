namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserFriendDAL : IUserFriend
    {
        public int AddUserFriend(UserFriendInfo userFriend)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@friendID", SqlDbType.Int), new SqlParameter("@friendName", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userFriend.FriendID;
            pt[1].Value = userFriend.FriendName;
            pt[2].Value = userFriend.UserID;
            pt[3].Value = userFriend.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserFriend", pt));
        }

        public void DeleteUserFriend(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserFriend", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, UserFriendSearchInfo userFriendSearch)
        {
            mssqlCondition.Add("[FriendName]", userFriendSearch.FriendName, ConditionType.Like);
            mssqlCondition.Add("[UserID]", userFriendSearch.UserID, ConditionType.Equal);
        }

        public void PrepareUserFriendModel(SqlDataReader dr, List<UserFriendInfo> userFriendList)
        {
            while (dr.Read())
            {
                UserFriendInfo item = new UserFriendInfo();
                item.ID = dr.GetInt32(0);
                item.FriendID = dr.GetInt32(1);
                item.FriendName = dr[2].ToString();
                item.UserID = dr.GetInt32(3);
                item.UserName = dr[4].ToString();
                userFriendList.Add(item);
            }
        }

        public UserFriendInfo ReadUserFriend(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            UserFriendInfo info = new UserFriendInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserFriend", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.FriendID = reader.GetInt32(1);
                    info.FriendName = reader[2].ToString();
                    info.UserID = reader.GetInt32(3);
                    info.UserName = reader[4].ToString();
                }
            }
            return info;
        }

        public UserFriendInfo ReadUserFriendByFriendID(int friendID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@friendID", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = friendID;
            pt[1].Value = userID;
            UserFriendInfo info = new UserFriendInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserFriendByFriendID", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.FriendID = reader.GetInt32(1);
                    info.FriendName = reader[2].ToString();
                    info.UserID = reader.GetInt32(3);
                    info.UserName = reader[4].ToString();
                }
            }
            return info;
        }

        public string ReadUserFriendIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserFriendIDList", pt))
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

        public List<UserFriendInfo> SearchUserFriendList(UserFriendSearchInfo userFriendSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userFriendSearch);
            List<UserFriendInfo> userFriendList = new List<UserFriendInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchUserFriendList", pt))
            {
                this.PrepareUserFriendModel(reader, userFriendList);
            }
            return userFriendList;
        }

        public List<UserFriendInfo> SearchUserFriendList(int currentPage, int pageSize, UserFriendSearchInfo userFriendSearch, ref int count)
        {
            List<UserFriendInfo> userFriendList = new List<UserFriendInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "UserFriend";
            class2.Fields = "[ID],[FriendID],[FriendName],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userFriendSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareUserFriendModel(reader, userFriendList);
            }
            return userFriendList;
        }

        public void UpdateUserFriend(UserFriendInfo userFriend)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@friendName", SqlDbType.NVarChar) };
            pt[0].Value = userFriend.ID;
            pt[1].Value = userFriend.FriendName;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserFriend", pt);
        }
    }
}

