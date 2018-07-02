namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserDAL : IUser
    {
        public int AddUser(UserInfo user)
        {
            SqlParameter[] pt = new SqlParameter[] { 
                new SqlParameter("@userName", SqlDbType.NVarChar), new SqlParameter("@userPassword", SqlDbType.NVarChar), new SqlParameter("@email", SqlDbType.NVarChar), new SqlParameter("@sex", SqlDbType.Int), new SqlParameter("@introduce", SqlDbType.NText), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@mSN", SqlDbType.NVarChar), new SqlParameter("@qQ", SqlDbType.NVarChar), new SqlParameter("@tel", SqlDbType.NVarChar), new SqlParameter("@mobile", SqlDbType.NVarChar), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@address", SqlDbType.NVarChar), new SqlParameter("@birthday", SqlDbType.NVarChar), new SqlParameter("@registerIP", SqlDbType.NVarChar), new SqlParameter("@registerDate", SqlDbType.DateTime), new SqlParameter("@lastLoginIP", SqlDbType.NVarChar), 
                new SqlParameter("@lastLoginDate", SqlDbType.DateTime), new SqlParameter("@loginTimes", SqlDbType.Int), new SqlParameter("@safeCode", SqlDbType.NVarChar), new SqlParameter("@findDate", SqlDbType.DateTime), new SqlParameter("@status", SqlDbType.Int), new SqlParameter("@openID", SqlDbType.NVarChar)
             };
            pt[0].Value = user.UserName;
            pt[1].Value = user.UserPassword;
            pt[2].Value = user.Email;
            pt[3].Value = user.Sex;
            pt[4].Value = user.Introduce;
            pt[5].Value = user.Photo;
            pt[6].Value = user.MSN;
            pt[7].Value = user.QQ;
            pt[8].Value = user.Tel;
            pt[9].Value = user.Mobile;
            pt[10].Value = user.RegionID;
            pt[11].Value = user.Address;
            pt[12].Value = user.Birthday;
            pt[13].Value = user.RegisterIP;
            pt[14].Value = user.RegisterDate;
            pt[15].Value = user.LastLoginIP;
            pt[0x10].Value = user.LastLoginDate;
            pt[0x11].Value = user.LoginTimes;
            pt[0x12].Value = user.SafeCode;
            pt[0x13].Value = user.FindDate;
            pt[20].Value = user.Status;
            pt[0x15].Value = user.OpenID;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUser", pt));
        }

        public void ChangePassword(int id, string newPassword)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@password", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = newPassword;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserPassword", pt);
        }

        public void ChangePassword(int id, string oldPassword, string newPassword)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@oldPassword", SqlDbType.NVarChar), new SqlParameter("@newPassword", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = oldPassword;
            pt[2].Value = newPassword;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeUserPassword", pt);
        }

        public void ChangeUserSafeCode(int userID, string safeCode, DateTime findDate)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@safeCode", SqlDbType.NVarChar), new SqlParameter("@findDate", SqlDbType.DateTime) };
            pt[0].Value = userID;
            pt[1].Value = safeCode;
            pt[2].Value = findDate;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeUserSafeCode", pt);
        }

        public void ChangeUserStatus(string strID, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@status", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeUserStatus", pt);
        }

        public bool CheckEmail(string email)
        {
            bool flag = false;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@email", SqlDbType.NVarChar) };
            pt[0].Value = email;
            object obj2 = ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "CheckEmail", pt);
            if (obj2 != null && obj2 != DBNull.Value) flag = true;
            return flag;
        }

        public UserInfo CheckUserLogin(string loginName, string loginPass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@loginName", SqlDbType.NVarChar), new SqlParameter("@loginPass", SqlDbType.NVarChar) };
            pt[0].Value = loginName;
            pt[1].Value = loginPass;
            UserInfo info = new UserInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "CheckUserLogin", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Status = reader.GetInt32(1);
                }
            }
            return info;
        }

        public int CheckUserName(string userName)
        {
            int num = 0;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userName;
            object obj2 = ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "CheckUserName", pt);
            if (obj2 != null && obj2 != DBNull.Value) num = Convert.ToInt32(obj2);
            return num;
        }

        public void DeleteUser(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUser", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, UserSearchInfo userSearch)
        {
            mssqlCondition.Add("[UserName]", userSearch.UserName, ConditionType.Like);
            mssqlCondition.Add("[Email]", userSearch.Email, ConditionType.Like);
            mssqlCondition.Add("[Sex]", userSearch.Sex, ConditionType.Equal);
            mssqlCondition.Add("[RegisterDate]", userSearch.StartRegisterDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[RegisterDate]", userSearch.EndRegisterDate, ConditionType.Less);
            mssqlCondition.Add("[Status]", userSearch.Status, ConditionType.Equal);
            mssqlCondition.Add("[ID]", userSearch.InUserID, ConditionType.In);
        }

        public void PrepareUserModel(SqlDataReader dr, List<UserInfo> userList)
        {
            while (dr.Read())
            {
                UserInfo item = new UserInfo();
                item.ID = dr.GetInt32(0);
                item.UserName = dr[1].ToString();
                item.UserPassword = dr[2].ToString();
                item.Email = dr[3].ToString();
                item.Sex = dr.GetInt32(4);
                item.Introduce = dr[5].ToString();
                item.Photo = dr[6].ToString();
                item.MSN = dr[7].ToString();
                item.QQ = dr[8].ToString();
                item.Tel = dr[9].ToString();
                item.Mobile = dr[10].ToString();
                item.RegionID = dr[11].ToString();
                item.Address = dr[12].ToString();
                item.Birthday = dr[13].ToString();
                item.RegisterIP = dr[14].ToString();
                item.RegisterDate = dr.GetDateTime(15);
                item.LastLoginIP = dr[0x10].ToString();
                item.LastLoginDate = dr.GetDateTime(0x11);
                item.LoginTimes = dr.GetInt32(0x12);
                item.SafeCode = dr[0x13].ToString();
                item.FindDate = dr.GetDateTime(20);
                item.Status = dr.GetInt32(0x15);
                item.OpenID = dr[0x16].ToString();
                userList.Add(item);
            }
        }

        public UserInfo ReadUser(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            UserInfo info = new UserInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUser", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.UserName = reader[1].ToString();
                    info.UserPassword = reader[2].ToString();
                    info.Email = reader[3].ToString();
                    info.Sex = reader.GetInt32(4);
                    info.Introduce = reader[5].ToString();
                    info.Photo = reader[6].ToString();
                    info.MSN = reader[7].ToString();
                    info.QQ = reader[8].ToString();
                    info.Tel = reader[9].ToString();
                    info.Mobile = reader[10].ToString();
                    info.RegionID = reader[11].ToString();
                    info.Address = reader[12].ToString();
                    info.Birthday = reader[13].ToString();
                    info.RegisterIP = reader[14].ToString();
                    info.RegisterDate = reader.GetDateTime(15);
                    info.LastLoginIP = reader[0x10].ToString();
                    info.LastLoginDate = reader.GetDateTime(0x11);
                    info.LoginTimes = reader.GetInt32(0x12);
                    info.SafeCode = reader[0x13].ToString();
                    info.FindDate = reader.GetDateTime(20);
                    info.Status = reader.GetInt32(0x15);
                    info.OpenID = reader[0x16].ToString();
                }
            }
            return info;
        }

        public UserInfo ReadUserByOpenID(string openID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@openID", SqlDbType.NVarChar) };
            pt[0].Value = openID;
            UserInfo info = new UserInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserByOpenID", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.UserName = reader[1].ToString();
                    info.UserPassword = reader[2].ToString();
                    info.Email = reader[3].ToString();
                    info.Sex = reader.GetInt32(4);
                    info.Introduce = reader[5].ToString();
                    info.Photo = reader[6].ToString();
                    info.MSN = reader[7].ToString();
                    info.QQ = reader[8].ToString();
                    info.Tel = reader[9].ToString();
                    info.Mobile = reader[10].ToString();
                    info.RegionID = reader[11].ToString();
                    info.Address = reader[12].ToString();
                    info.Birthday = reader[13].ToString();
                    info.RegisterIP = reader[14].ToString();
                    info.RegisterDate = reader.GetDateTime(15);
                    info.LastLoginIP = reader[0x10].ToString();
                    info.LastLoginDate = reader.GetDateTime(0x11);
                    info.LoginTimes = reader.GetInt32(0x12);
                    info.SafeCode = reader[0x13].ToString();
                    info.FindDate = reader.GetDateTime(20);
                    info.Status = reader.GetInt32(0x15);
                    info.OpenID = reader[0x16].ToString();
                }
            }
            return info;
        }

        public UserInfo ReadUserByUserName(string userName)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userName;
            UserInfo info = new UserInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserByUserName", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.UserName = reader[1].ToString();
                    info.UserPassword = reader[2].ToString();
                    info.Email = reader[3].ToString();
                    info.Sex = reader.GetInt32(4);
                    info.Introduce = reader[5].ToString();
                    info.Photo = reader[6].ToString();
                    info.MSN = reader[7].ToString();
                    info.QQ = reader[8].ToString();
                    info.Tel = reader[9].ToString();
                    info.Mobile = reader[10].ToString();
                    info.RegionID = reader[11].ToString();
                    info.Address = reader[12].ToString();
                    info.Birthday = reader[13].ToString();
                    info.RegisterIP = reader[14].ToString();
                    info.RegisterDate = reader.GetDateTime(15);
                    info.LastLoginIP = reader[0x10].ToString();
                    info.LastLoginDate = reader.GetDateTime(0x11);
                    info.LoginTimes = reader.GetInt32(0x12);
                    info.SafeCode = reader[0x13].ToString();
                    info.FindDate = reader.GetDateTime(20);
                    info.Status = reader.GetInt32(0x15);
                    info.OpenID = reader[0x16].ToString();
                }
            }
            return info;
        }

        public List<string> ReadUserEmailByMoneyUsed(Dictionary<decimal, decimal> moneyUsed)
        {
            List<string> list = new List<string>();
            string str = string.Empty;
            foreach (KeyValuePair<decimal, decimal> pair in moneyUsed)
            {
                object obj2;
                if (str == string.Empty)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, "([MoneyUsed]>=", pair.Key, " AND [MoneyUsed]<", pair.Value, ")" });
                }
                else
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, " OR ([MoneyUsed]>=", pair.Key, " AND [MoneyUsed]<", pair.Value, ")" });
                }
            }
            if (str != string.Empty) str = " WHERE " + str;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = str;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserEmailByMoneyUsed", pt))
            {
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
            }
            return list;
        }

        public UserInfo ReadUserMore(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            UserInfo info = new UserInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserMore", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.UserName = reader[1].ToString();
                    info.UserPassword = reader[2].ToString();
                    info.Email = reader[3].ToString();
                    info.Sex = reader.GetInt32(4);
                    info.Introduce = reader[5].ToString();
                    info.Photo = reader[6].ToString();
                    info.MSN = reader[7].ToString();
                    info.QQ = reader[8].ToString();
                    info.Tel = reader[9].ToString();
                    info.Mobile = reader[10].ToString();
                    info.RegionID = reader[11].ToString();
                    info.Address = reader[12].ToString();
                    info.Birthday = reader[13].ToString();
                    info.RegisterIP = reader[14].ToString();
                    info.RegisterDate = reader.GetDateTime(15);
                    info.LastLoginIP = reader[0x10].ToString();
                    info.LastLoginDate = reader.GetDateTime(0x11);
                    info.LoginTimes = reader.GetInt32(0x12);
                    info.SafeCode = reader[0x13].ToString();
                    info.FindDate = reader.GetDateTime(20);
                    info.Status = reader.GetInt32(0x15);
                    info.OpenID = reader[0x16].ToString();
                    info.MoneyLeft = reader.GetDecimal(0x17);
                    info.PointLeft = reader.GetInt32(0x18);
                    info.MoneyUsed = reader.GetDecimal(0x19);
                }
            }
            return info;
        }

        public List<UserInfo> SearchUserList(UserSearchInfo userSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userSearch);
            List<UserInfo> userList = new List<UserInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchUserList", pt))
            {
                this.PrepareUserModel(reader, userList);
            }
            return userList;
        }

        public List<UserInfo> SearchUserList(int currentPage, int pageSize, UserSearchInfo userSearch, ref int count)
        {
            List<UserInfo> userList = new List<UserInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "User";
            class2.Fields = "[ID],[UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareUserModel(reader, userList);
            }
            return userList;
        }

        public DataTable StatisticsUserActive(int currentPage, int pageSize, UserSearchInfo userSearch, ref int count, string orderField)
        {
            List<UserInfo> list = new List<UserInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "View_UserActive";
            class2.Fields = "[ID],[UserName],[Sex],[RegisterDate],[LoginTimes],[CommentCount],[ReplyCount],[MessageCount]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            string str = orderField;
            if (str != null)
            {
                if (!(str == "LoginTimes"))
                {
                    if (str == "CommentCount")
                    {
                        class2.OrderField = "[CommentCount],[ID]";
                        goto Label_00C1;
                    }
                    if (str == "ReplyCount")
                    {
                        class2.OrderField = "[ReplyCount],[ID]";
                        goto Label_00C1;
                    }
                    if (str == "MessageCount")
                    {
                        class2.OrderField = "[MessageCount],[ID]";
                        goto Label_00C1;
                    }
                }
                else
                {
                    class2.OrderField = "[LoginTimes],[ID]";
                    goto Label_00C1;
                }
            }
            class2.OrderField = "[LoginTimes],[ID]";
        Label_00C1:
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userSearch);
            class2.Count = count;
            count = class2.Count;
            return class2.ExecuteDataTable();
        }

        public DataTable StatisticsUserConsume(int currentPage, int pageSize, UserSearchInfo userSearch, ref int count, string orderField, DateTime startDate, DateTime endDate)
        {
            List<UserInfo> list = new List<UserInfo>();
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            MssqlCondition condition = new MssqlCondition();
            condition.Add("[AddDate]", startDate, ConditionType.MoreOrEqual);
            condition.Add("[AddDate]", endDate, ConditionType.Less);
            str = condition.ToString();
            if (str != string.Empty)
            {
                str = " AND" + str;
                str2 = str.Replace("[AddDate]", "[RechargeDate]");
                str3 = str3.Replace("[ApplyDate]", "[RechargeDate]");
            }
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = "(SELECT ID,UserName,Sex,ISNULL(RechargeCount,0) AS RechargeCount,ISNULL(RechargeMoney,0) AS RechargeMoney,ISNULL(ApplyCount,0) AS ApplyCount,ISNULL(ApplyMoney,0) AS ApplyMoney,ISNULL(OrderCount,0) AS OrderCount,ISNULL(OrderMoney,0) AS OrderMoney ";
            class2.TableName = class2.TableName + "FROM " + ShopMssqlHelper.TablePrefix + "User ";
            string tableName = class2.TableName;
            class2.TableName = tableName + "LEFT OUTER JOIN (SELECT UserID, COUNT(*) AS RechargeCount,Sum(Money) AS RechargeMoney FROM " + ShopMssqlHelper.TablePrefix + "UserRecharge WHERE IsFinish=1 " + str2 + " GROUP BY UserID) AS TEMP1 ON " + ShopMssqlHelper.TablePrefix + "User.ID = TEMP1.UserID ";
            tableName = class2.TableName;
            class2.TableName = tableName + "LEFT OUTER JOIN (SELECT UserID, COUNT(*) AS ApplyCount,Sum(Money) AS ApplyMoney FROM " + ShopMssqlHelper.TablePrefix + "UserApply WHERE Status=2 " + str3 + " GROUP BY UserID) AS TEMP2 ON " + ShopMssqlHelper.TablePrefix + "User.ID = TEMP2.UserID ";
            tableName = class2.TableName;
            class2.TableName = tableName + "LEFT OUTER JOIN (SELECT UserID, COUNT(*) AS OrderCount,Sum(ProductMoney-FavorableMoney+ShippingMoney+OtherMoney-CouponMoney) AS OrderMoney FROM " + ShopMssqlHelper.TablePrefix + "Order WHERE OrderStatus=6 " + str + " GROUP BY UserID) AS TEMP3 ON " + ShopMssqlHelper.TablePrefix + "User.ID = TEMP3.UserID) AS PageTable";
            class2.Fields = "[ID],[UserName],[Sex],[RechargeCount],[RechargeMoney],[ApplyCount],[ApplyMoney],[OrderCount],[OrderMoney]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            switch (orderField)
            {
                case "RechargeCount":
                    class2.OrderField = "[RechargeCount],[ID]";
                    break;

                case "RechargeMoney":
                    class2.OrderField = "[RechargeMoney],[ID]";
                    break;

                case "ApplyCount":
                    class2.OrderField = "[ApplyCount],[ID]";
                    break;

                case "ApplyMoney":
                    class2.OrderField = "[ApplyMoney],[ID]";
                    break;

                case "OrderCount":
                    class2.OrderField = "[OrderCount],[ID]";
                    break;

                case "OrderMoney":
                    class2.OrderField = "[OrderMoney],[ID]";
                    break;

                default:
                    class2.OrderField = "[OrderCount],[ID]";
                    break;
            }
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userSearch);
            class2.Count = count;
            count = class2.Count;
            return class2.ExecuteDataTable();
        }

        public DataTable StatisticsUserCount(UserSearchInfo userSearch, DateType dateType)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userSearch);
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar), new SqlParameter("@dateType", SqlDbType.Int) };
            pt[0].Value = mssqlCondition.ToString();
            pt[1].Value = (int) dateType;
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "StatisticsUserCount", pt);
        }

        public DataTable StatisticsUserStatus(UserSearchInfo userSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userSearch);
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "StatisticsUserStatus", pt);
        }

        public void UpdateUser(UserInfo user)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@sex", SqlDbType.Int), new SqlParameter("@introduce", SqlDbType.NText), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@mSN", SqlDbType.NVarChar), new SqlParameter("@qQ", SqlDbType.NVarChar), new SqlParameter("@tel", SqlDbType.NVarChar), new SqlParameter("@mobile", SqlDbType.NVarChar), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@address", SqlDbType.NVarChar), new SqlParameter("@birthday", SqlDbType.NVarChar), new SqlParameter("@status", SqlDbType.Int), new SqlParameter("@email", SqlDbType.NVarChar) };
            pt[0].Value = user.ID;
            pt[1].Value = user.Sex;
            pt[2].Value = user.Introduce;
            pt[3].Value = user.Photo;
            pt[4].Value = user.MSN;
            pt[5].Value = user.QQ;
            pt[6].Value = user.Tel;
            pt[7].Value = user.Mobile;
            pt[8].Value = user.RegionID;
            pt[9].Value = user.Address;
            pt[10].Value = user.Birthday;
            pt[11].Value = user.Status;
            pt[12].Value = user.Email;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUser", pt);
        }

        public void UpdateUserLogin(int id, DateTime lastLoginDate, string lastLoginIP)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@lastLoginDate", SqlDbType.DateTime), new SqlParameter("@lastLoginIP", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = lastLoginDate;
            pt[2].Value = lastLoginIP;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserLogin", pt);
        }

        public DataTable UserIndexStatistics(int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = userID;
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "UserIndexStatistics", pt);
        }
    }
}

