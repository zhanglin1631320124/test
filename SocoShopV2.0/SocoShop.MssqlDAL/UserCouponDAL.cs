namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserCouponDAL : IUserCoupon
    {
        public int AddUserCoupon(UserCouponInfo userCoupon)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@couponID", SqlDbType.Int), new SqlParameter("@getType", SqlDbType.Int), new SqlParameter("@number", SqlDbType.NVarChar), new SqlParameter("@password", SqlDbType.NVarChar), new SqlParameter("@isUse", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userCoupon.CouponID;
            pt[1].Value = userCoupon.GetType;
            pt[2].Value = userCoupon.Number;
            pt[3].Value = userCoupon.Password;
            pt[4].Value = userCoupon.IsUse;
            pt[5].Value = userCoupon.OrderID;
            pt[6].Value = userCoupon.UserID;
            pt[7].Value = userCoupon.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserCoupon", pt));
        }

        public void DeleteUserCoupon(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserCoupon", pt);
        }

        public void DeleteUserCouponByCouponID(string strCouponID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strCouponID", SqlDbType.NVarChar) };
            pt[0].Value = strCouponID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserCouponByCouponID", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, UserCouponSearchInfo userCouponSearch)
        {
            mssqlCondition.Add("[CouponID]", userCouponSearch.CouponID, ConditionType.Equal);
            mssqlCondition.Add("[GetType]", userCouponSearch.GetType, ConditionType.Equal);
            mssqlCondition.Add("[Number]", userCouponSearch.Number, ConditionType.Like);
            mssqlCondition.Add("[IsUse]", userCouponSearch.IsUse, ConditionType.Equal);
            mssqlCondition.Add("[UserID]", userCouponSearch.UserID, ConditionType.Equal);
        }

        public void PrepareUserCouponModel(SqlDataReader dr, List<UserCouponInfo> userCouponList)
        {
            while (dr.Read())
            {
                UserCouponInfo item = new UserCouponInfo();
                item.ID = dr.GetInt32(0);
                item.CouponID = dr.GetInt32(1);
                item.GetType = dr.GetInt32(2);
                item.Number = dr[3].ToString();
                item.Password = dr[4].ToString();
                item.IsUse = dr.GetInt32(5);
                item.OrderID = dr.GetInt32(6);
                item.UserID = dr.GetInt32(7);
                item.UserName = dr[8].ToString();
                userCouponList.Add(item);
            }
        }

        public UserCouponInfo ReadTopUserCoupon(int couponID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@couponID", SqlDbType.Int) };
            pt[0].Value = couponID;
            UserCouponInfo info = new UserCouponInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadTopUserCoupon", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.CouponID = reader.GetInt32(1);
                    info.GetType = reader.GetInt32(2);
                    info.Number = reader[3].ToString();
                    info.Password = reader[4].ToString();
                    info.IsUse = reader.GetInt32(5);
                    info.OrderID = reader.GetInt32(6);
                    info.UserID = reader.GetInt32(7);
                    info.UserName = reader[8].ToString();
                }
            }
            return info;
        }

        public UserCouponInfo ReadUserCoupon(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            UserCouponInfo info = new UserCouponInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserCoupon", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.CouponID = reader.GetInt32(1);
                    info.GetType = reader.GetInt32(2);
                    info.Number = reader[3].ToString();
                    info.Password = reader[4].ToString();
                    info.IsUse = reader.GetInt32(5);
                    info.OrderID = reader.GetInt32(6);
                    info.UserID = reader.GetInt32(7);
                    info.UserName = reader[8].ToString();
                }
            }
            return info;
        }

        public UserCouponInfo ReadUserCouponByNumber(string number, string password)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@number", SqlDbType.NVarChar), new SqlParameter("@password", SqlDbType.NVarChar) };
            pt[0].Value = number;
            pt[1].Value = password;
            UserCouponInfo info = new UserCouponInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserCouponByNumber", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.CouponID = reader.GetInt32(1);
                    info.GetType = reader.GetInt32(2);
                    info.Number = reader[3].ToString();
                    info.Password = reader[4].ToString();
                    info.IsUse = reader.GetInt32(5);
                    info.OrderID = reader.GetInt32(6);
                    info.UserID = reader.GetInt32(7);
                    info.UserName = reader[8].ToString();
                }
            }
            return info;
        }

        public UserCouponInfo ReadUserCouponByOrder(int orderID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int) };
            pt[0].Value = orderID;
            UserCouponInfo info = new UserCouponInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserCouponByOrder", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.CouponID = reader.GetInt32(1);
                    info.GetType = reader.GetInt32(2);
                    info.Number = reader[3].ToString();
                    info.Password = reader[4].ToString();
                    info.IsUse = reader.GetInt32(5);
                    info.OrderID = reader.GetInt32(6);
                    info.UserID = reader.GetInt32(7);
                    info.UserName = reader[8].ToString();
                    info.Coupon.Money = reader.GetDecimal(9);
                    info.Coupon.UseMinAmount = reader.GetDecimal(10);
                }
            }
            return info;
        }

        public List<UserCouponInfo> ReadUserCouponCanUse(int userID)
        {
            List<UserCouponInfo> list = new List<UserCouponInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserCouponCanUse", pt))
            {
                while (reader.Read())
                {
                    UserCouponInfo item = new UserCouponInfo();
                    item.ID = reader.GetInt32(0);
                    item.CouponID = reader.GetInt32(1);
                    item.GetType = reader.GetInt32(2);
                    item.Number = reader[3].ToString();
                    item.Password = reader[4].ToString();
                    item.IsUse = reader.GetInt32(5);
                    item.OrderID = reader.GetInt32(6);
                    item.UserID = reader.GetInt32(7);
                    item.UserName = reader[8].ToString();
                    item.Coupon.Money = reader.GetDecimal(9);
                    item.Coupon.UseMinAmount = reader.GetDecimal(10);
                    list.Add(item);
                }
            }
            return list;
        }

        public List<UserCouponInfo> SearchUserCouponList(UserCouponSearchInfo userCouponSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userCouponSearch);
            List<UserCouponInfo> userCouponList = new List<UserCouponInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchUserCouponList", pt))
            {
                this.PrepareUserCouponModel(reader, userCouponList);
            }
            return userCouponList;
        }

        public List<UserCouponInfo> SearchUserCouponList(int currentPage, int pageSize, UserCouponSearchInfo userCouponSearch, ref int count)
        {
            List<UserCouponInfo> userCouponList = new List<UserCouponInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "UserCoupon";
            class2.Fields = "[ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userCouponSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareUserCouponModel(reader, userCouponList);
            }
            return userCouponList;
        }

        public void UpdateUserCoupon(UserCouponInfo userCoupon)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@isUse", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userCoupon.ID;
            pt[1].Value = userCoupon.IsUse;
            pt[2].Value = userCoupon.OrderID;
            pt[3].Value = userCoupon.UserID;
            pt[4].Value = userCoupon.UserName;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserCoupon", pt);
        }
    }
}

