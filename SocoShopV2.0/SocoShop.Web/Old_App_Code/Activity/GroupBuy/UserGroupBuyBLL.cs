using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using SocoShop.Business;
using SocoShop.Common;
using SkyCES.EntLib;

namespace SocoShop.Web
{
    /// <summary>
    /// 用户团购业务逻辑。
    /// </summary>
    public sealed class UserGroupBuyBLL
    {
        /// <summary>
        /// 增加一条用户团购数据
        /// </summary>
        /// <param name="userGroupBuy">用户团购模型变量</param>
        public static int AddUserGroupBuy(UserGroupBuyInfo userGroupBuy)
        {
            string sql = "INSERT INTO " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy([GroupBuyID],[Date],[IP],[BuyCount],[OrderID],[UserID],[UserName],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile]) VALUES (@groupBuyID,@date,@iP,@buyCount,@orderID,@userID,@userName,@consignee,@regionID,@address,@zipCode,@tel,@email,@mobile)";
            OleDbParameter[] parameters = {
				new OleDbParameter("@groupBuyID",OleDbType.Integer),
				new OleDbParameter("@date",OleDbType.VarWChar),
				new OleDbParameter("@iP",OleDbType.VarWChar),
				new OleDbParameter("@buyCount",OleDbType.Integer),
				new OleDbParameter("@orderID",OleDbType.Integer),
				new OleDbParameter("@userID",OleDbType.Integer),
				new OleDbParameter("@userName",OleDbType.VarWChar),
				new OleDbParameter("@consignee",OleDbType.VarWChar),
				new OleDbParameter("@regionID",OleDbType.VarWChar),
				new OleDbParameter("@address",OleDbType.VarWChar),
				new OleDbParameter("@zipCode",OleDbType.VarWChar),
				new OleDbParameter("@tel",OleDbType.VarWChar),
				new OleDbParameter("@email",OleDbType.VarWChar),
				new OleDbParameter("@mobile",OleDbType.VarWChar)
			};
            parameters[0].Value = userGroupBuy.GroupBuyID;
            parameters[1].Value = userGroupBuy.Date;
            parameters[2].Value = userGroupBuy.IP;
            parameters[3].Value = userGroupBuy.BuyCount;
            parameters[4].Value = userGroupBuy.OrderID;
            parameters[5].Value = userGroupBuy.UserID;
            parameters[6].Value = userGroupBuy.UserName;
            parameters[7].Value = userGroupBuy.Consignee;
            parameters[8].Value = userGroupBuy.RegionID;
            parameters[9].Value = userGroupBuy.Address;
            parameters[10].Value = userGroupBuy.ZipCode;
            parameters[11].Value = userGroupBuy.Tel;
            parameters[12].Value = userGroupBuy.Email;
            parameters[13].Value = userGroupBuy.Mobile;
            GroupBuyAccessHelper.ExecuteNonQuery(sql, parameters);
            Object id = GroupBuyAccessHelper.ExecuteScalar("SELECT MAX([ID]) FROM " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy");
            return (Convert.ToInt32(id));
        }


        /// <summary>
        /// 更新一条用户团购数据
        /// </summary>
        /// <param name="userGroupBuyID">用户团购ID</param>
        /// <param name="orderID">订单ID号</param>
        /// <param name="userID">用户ID</param>
        public static void UpdateUserGroupBuy(int userGroupBuyID, int orderID)
        {
            string sql = "UPDATE " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy SET [OrderID]=" + orderID.ToString() + " WHERE [ID]=" + userGroupBuyID.ToString();
            GroupBuyAccessHelper.ExecuteNonQuery(sql);
        }   

        /// <summary>
        /// 删除多条用户团购数据
        /// </summary>
        /// <param name="strID">用户团购的主键值,以,号分隔</param>
        public static void DeleteUserGroupBuy(string strID)
        {
            if (strID == string.Empty)
            {
                return;
            }
            string sql = "DELETE FROM " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy WHERE [ID] IN(" + strID + ")";
            GroupBuyAccessHelper.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 按分类删除用户团购数据
        /// </summary>
        /// <param name="strGroupBuyID">分类ID,以,号分隔</param>
        public static void DeleteUserGroupBuyByGroupBuyID(string strGroupBuyID)
        {
            if (strGroupBuyID == string.Empty)
            {
                return;
            }
            string sql = "DELETE FROM " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy WHERE [GroupBuyID] IN(" + strGroupBuyID + ")";
            GroupBuyAccessHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 准备用户团购模型
        /// </summary>
        /// <param name="dr">Datareader</param>
        /// <param name="userGroupBuyList">用户团购的数据列表</param>
        public static void PrepareUserGroupBuyModel(OleDbDataReader dr, List<UserGroupBuyInfo> userGroupBuyList)
        {
            while (dr.Read())
            {
                UserGroupBuyInfo userGroupBuy = new UserGroupBuyInfo();
                userGroupBuy.ID = dr.GetInt32(0);
                userGroupBuy.GroupBuyID = dr.GetInt32(1);
                userGroupBuy.Date = dr.GetDateTime(2);
                userGroupBuy.IP = dr[3].ToString();
                userGroupBuy.BuyCount = dr.GetInt32(4);
                userGroupBuy.OrderID = dr.GetInt32(5);
                userGroupBuy.UserID = dr.GetInt32(6);
                userGroupBuy.UserName = dr[7].ToString();
                userGroupBuy.Consignee = dr[8].ToString();
                userGroupBuy.RegionID = dr[9].ToString();
                userGroupBuy.Address = dr[10].ToString();
                userGroupBuy.ZipCode = dr[11].ToString();
                userGroupBuy.Tel = dr[12].ToString();
                userGroupBuy.Email = dr[13].ToString();
                userGroupBuy.Mobile = dr[14].ToString();
                userGroupBuyList.Add(userGroupBuy);
            }
        }

        /// <summary>
        /// 获得用户团购数据列表
        /// </summary>
        /// <param name="groupBuyID">分类ID</param>
        /// <param name="currentPage">当前的页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="count">总数量</param>
        /// <returns>用户团购数据列表</returns>
        public static List<UserGroupBuyInfo> ReadUserGroupBuyList(int groupBuyID, int currentPage, int pageSize, ref int count)
        {
            List<UserGroupBuyInfo> userGroupBuyList = new List<UserGroupBuyInfo>();
            GroupBuyAccessPagerClass pc = new GroupBuyAccessPagerClass();
            pc.TableName = GroupBuyAccessHelper.TablePrefix + "UserGroupBuy";
            pc.Fields = "[ID],[GroupBuyID],[Date],[IP],[BuyCount],[OrderID],[UserID],[UserName],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile]";
            pc.CurrentPage = currentPage;
            pc.PageSize = pageSize;
            pc.OrderField = "[ID]";
            pc.OrderType = OrderType.Desc;
            pc.AccessCondition.Add("[GroupBuyID]", groupBuyID, ConditionType.Equal);
            pc.Count = count;
            count = pc.Count;
            using (OleDbDataReader dr = pc.ExecuteReader())
            {
                PrepareUserGroupBuyModel(dr, userGroupBuyList);
            }
            return userGroupBuyList;
        }


        /// <summary>
        /// 获得用户团购数据列表
        /// </summary>
        /// <param name="groupBuyID">分类ID</param>
        /// <returns>用户团购数据列表</returns>
        public static List<UserGroupBuyInfo> ReadUserGroupBuyList(int groupBuyID)
        {
            List<UserGroupBuyInfo> userGroupBuyList = new List<UserGroupBuyInfo>();
            string sql = "SELECT * FROM " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy WHERE [GroupBuyID]=" + groupBuyID;
            using (OleDbDataReader dr = GroupBuyAccessHelper.ExecuteReader(sql))
            {
                PrepareUserGroupBuyModel(dr, userGroupBuyList);
            }
            return userGroupBuyList;
        }


        /// <summary>
        /// 获得用户团购数据的产品数量
        /// </summary>
        /// <param name="groupBuyID">分类ID</param>
        public static int ReadUserGroupBuyCount(int groupBuyID)
        {
            string sql = "SELECT SUM(BuyCount) FROM " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy WHERE [GroupBuyID]=" + groupBuyID;
            object count = GroupBuyAccessHelper.ExecuteScalar(sql);
            if (count != null && count != DBNull.Value)
            {
                return (Convert.ToInt32(count));
            }
            else
            {
                return 0;
            }
            
        }

        /// <summary>
        /// 获得用户团购数据的产品数量
        /// </summary>
        /// <param name="groupBuyID">分类ID</param>
        public static Dictionary<int,int> ReadUserGroupBuyCount(string groupBuyIDList)
        {
            Dictionary<int, int> dicCount = new Dictionary<int, int>();
            string sql = "SELECT [GroupBuyID],SUM(BuyCount) FROM " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy WHERE [GroupBuyID] IN(" + groupBuyIDList + ") GROUP BY [GroupBuyID]";
            using (OleDbDataReader dr = GroupBuyAccessHelper.ExecuteReader(sql))
            {
                while (dr.Read())
                {
                    dicCount.Add(dr.GetInt32(0), Convert.ToInt32(dr[1].ToString()));
                }
            }
            return dicCount;
        }


        /// <summary>
        /// 读取一条用户团购数据
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户团购用户团购数据模型</returns>
        public static UserGroupBuyInfo ReadUserGroupBuyByUser(int groupBuyID, int userID)
        {
            string sql = "SELECT [ID],[GroupBuyID],[Date],[IP],[BuyCount],[OrderID],[UserID],[UserName],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile] FROM " + GroupBuyAccessHelper.TablePrefix + "UserGroupBuy WHERE [GroupBuyID]=" + groupBuyID.ToString() + " AND [UserID]=" + userID.ToString();
            UserGroupBuyInfo userGroupBuy = new UserGroupBuyInfo();
            using (OleDbDataReader dr = GroupBuyAccessHelper.ExecuteReader(sql))
            {
                if (dr.Read())
                {
                    userGroupBuy.ID = dr.GetInt32(0);
                    userGroupBuy.GroupBuyID = dr.GetInt32(1);
                    userGroupBuy.Date = dr.GetDateTime(2);
                    userGroupBuy.IP = dr[3].ToString();
                    userGroupBuy.BuyCount = dr.GetInt32(4);
                    userGroupBuy.OrderID = dr.GetInt32(5);
                    userGroupBuy.UserID = dr.GetInt32(6);
                    userGroupBuy.UserName = dr[7].ToString();
                    userGroupBuy.Consignee = dr[8].ToString();
                    userGroupBuy.RegionID = dr[9].ToString();
                    userGroupBuy.Address = dr[10].ToString();
                    userGroupBuy.ZipCode = dr[11].ToString();
                    userGroupBuy.Tel = dr[12].ToString();
                    userGroupBuy.Email = dr[13].ToString();
                    userGroupBuy.Mobile = dr[14].ToString();
                }
            }
            return userGroupBuy;
        }
    }
}
