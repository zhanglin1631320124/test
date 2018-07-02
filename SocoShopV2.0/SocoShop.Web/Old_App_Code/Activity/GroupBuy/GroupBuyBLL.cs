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
    /// 团购逻辑。
    /// </summary>
    public partial class GroupBuyBLL
    {
        public static readonly int TableID = UploadTable.ReadTableID("GroupBuy");
        /// <summary>
        /// 增加一条团购数据
        /// </summary>
        /// <param name="groupBuy">团购模型变量</param>
        public static int AddGroupBuy(GroupBuyInfo groupBuy)
        {
            string sql = "INSERT INTO " + GroupBuyAccessHelper.TablePrefix + "GroupBuy([Name],[Photo],[Description],[ProductID],[StartDate],[EndDate],[Price],[MinCount],[MaxCount],[EachNumber]) VALUES (@name,@photo,@description,@productID,@startDate,@endDate,@price,@minCount,@maxCount,@eachNumber)";
            OleDbParameter[] parameters = {
				new OleDbParameter("@name",OleDbType.VarWChar),
				new OleDbParameter("@photo",OleDbType.VarWChar),
				new OleDbParameter("@description",OleDbType.VarWChar),
				new OleDbParameter("@productID",OleDbType.Integer),
				new OleDbParameter("@startDate",OleDbType.VarWChar),
				new OleDbParameter("@endDate",OleDbType.VarWChar),
				new OleDbParameter("@price",OleDbType.Decimal),
				new OleDbParameter("@minCount",OleDbType.Integer),
				new OleDbParameter("@maxCount",OleDbType.Integer),
				new OleDbParameter("@eachNumber",OleDbType.Integer)
			};
            parameters[0].Value = groupBuy.Name;
            parameters[1].Value = groupBuy.Photo;
            parameters[2].Value = groupBuy.Description;
            parameters[3].Value = groupBuy.ProductID;
            parameters[4].Value = groupBuy.StartDate;
            parameters[5].Value = groupBuy.EndDate;
            parameters[6].Value = groupBuy.Price;
            parameters[7].Value = groupBuy.MinCount;
            parameters[8].Value = groupBuy.MaxCount;
            parameters[9].Value = groupBuy.EachNumber;
            GroupBuyAccessHelper.ExecuteNonQuery(sql, parameters);
            Object id = GroupBuyAccessHelper.ExecuteScalar("SELECT MAX([ID]) FROM " + GroupBuyAccessHelper.TablePrefix + "GroupBuy");
            groupBuy.ID=Convert.ToInt32(id);
            UploadBLL.UpdateUpload(TableID, 0, groupBuy.ID, Cookies.Admin.GetRandomNumber(false));
            return groupBuy.ID;
        }


        /// <summary>
        /// 更新一条团购数据
        /// </summary>
        /// <param name="groupBuy">团购模型变量</param>
        public static void UpdateGroupBuy(GroupBuyInfo groupBuy)
        {
            string sql = "UPDATE " + GroupBuyAccessHelper.TablePrefix + "GroupBuy SET [Name]=@name,[Photo]=@photo,[Description]=@description,[ProductID]=@productID,[StartDate]=@startDate,[EndDate]=@endDate,[Price]=@price,[MinCount]=@minCount,[MaxCount]=@maxCount,[EachNumber]=@eachNumber WHERE [ID]=" + groupBuy.ID.ToString();
            OleDbParameter[] parameters = {
				new OleDbParameter("@name",OleDbType.VarWChar),
				new OleDbParameter("@photo",OleDbType.VarWChar),
				new OleDbParameter("@description",OleDbType.VarWChar),
				new OleDbParameter("@productID",OleDbType.Integer),
				new OleDbParameter("@startDate",OleDbType.VarWChar),
				new OleDbParameter("@endDate",OleDbType.VarWChar),
				new OleDbParameter("@price",OleDbType.Decimal),
				new OleDbParameter("@minCount",OleDbType.Integer),
				new OleDbParameter("@maxCount",OleDbType.Integer),
				new OleDbParameter("@eachNumber",OleDbType.Integer)
			};
            parameters[0].Value = groupBuy.Name;
            parameters[1].Value = groupBuy.Photo;
            parameters[2].Value = groupBuy.Description;
            parameters[3].Value = groupBuy.ProductID;
            parameters[4].Value = groupBuy.StartDate;
            parameters[5].Value = groupBuy.EndDate;
            parameters[6].Value = groupBuy.Price;
            parameters[7].Value = groupBuy.MinCount;
            parameters[8].Value = groupBuy.MaxCount;
            parameters[9].Value = groupBuy.EachNumber;
            GroupBuyAccessHelper.ExecuteNonQuery(sql, parameters);
            UploadBLL.UpdateUpload(TableID, 0, groupBuy.ID, Cookies.Admin.GetRandomNumber(false));
        }

        /// <summary>
        /// 删除多条团购数据
        /// </summary>
        /// <param name="strID">团购的主键值,以,号分隔</param>
        public static void DeleteGroupBuy(string strID)
        {
            if (strID == string.Empty)
            {
                return;
            }
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            UserGroupBuyBLL.DeleteUserGroupBuyByGroupBuyID(strID);
            string sql = "DELETE FROM " + GroupBuyAccessHelper.TablePrefix + "GroupBuy WHERE [ID] IN(" + strID + ")";
            GroupBuyAccessHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取一条团购数据
        /// </summary>
        /// <param name="id">团购的主键值</param>
        /// <returns>团购数据模型</returns>
        public static GroupBuyInfo ReadGroupBuy(int id)
        {
            string sql = "SELECT [ID],[Name],[Photo],[Description],[ProductID],[StartDate],[EndDate],[Price],[MinCount],[MaxCount],[EachNumber] FROM " + GroupBuyAccessHelper.TablePrefix + "GroupBuy WHERE [ID]=" + id.ToString();
            GroupBuyInfo groupBuy = new GroupBuyInfo();
            using (OleDbDataReader dr = GroupBuyAccessHelper.ExecuteReader(sql))
            {
                if (dr.Read())
                {
                    groupBuy.ID = dr.GetInt32(0);
                    groupBuy.Name = dr[1].ToString();
                    groupBuy.Photo = dr[2].ToString();
                    groupBuy.Description = dr[3].ToString();
                    groupBuy.ProductID = dr.GetInt32(4);
                    groupBuy.StartDate = dr.GetDateTime(5);
                    groupBuy.EndDate = dr.GetDateTime(6);
                    groupBuy.Price = dr.GetDecimal(7);
                    groupBuy.MinCount = dr.GetInt32(8);
                    groupBuy.MaxCount = dr.GetInt32(9);
                    groupBuy.EachNumber = dr.GetInt32(10);
                }
            }
            return groupBuy;
        }

        /// <summary>
        /// 准备团购模型
        /// </summary>
        /// <param name="dr">Datareader</param>
        /// <param name="groupBuyList">团购的数据列表</param>
        public static void PrepareGroupBuyModel(OleDbDataReader dr, List<GroupBuyInfo> groupBuyList)
        {
            while (dr.Read())
            {
                GroupBuyInfo groupBuy = new GroupBuyInfo();
                groupBuy.ID = dr.GetInt32(0);
                groupBuy.Name = dr[1].ToString();
                groupBuy.Photo = dr[2].ToString();
                groupBuy.Description = dr[3].ToString();
                groupBuy.ProductID = dr.GetInt32(4);
                groupBuy.StartDate = dr.GetDateTime(5);
                groupBuy.EndDate = dr.GetDateTime(6);
                groupBuy.Price = dr.GetDecimal(7);
                groupBuy.MinCount = dr.GetInt32(8);
                groupBuy.MaxCount = dr.GetInt32(9);
                groupBuy.EachNumber = dr.GetInt32(10);
                groupBuyList.Add(groupBuy);
            }
        }


        /// <summary>
        /// 获得团购数据列表
        /// </summary>
        /// <param name="currentPage">当前的页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="count">总数量</param>
        /// <returns>团购数据列表</returns>
        public static List<GroupBuyInfo> ReadGroupBuyList(int currentPage, int pageSize, ref int count)
        {
            List<GroupBuyInfo> groupBuyList = new List<GroupBuyInfo>();
            GroupBuyAccessPagerClass pc = new GroupBuyAccessPagerClass();
            pc.TableName = GroupBuyAccessHelper.TablePrefix + "GroupBuy";
            pc.Fields = "[ID],[Name],[Photo],[Description],[ProductID],[StartDate],[EndDate],[Price],[MinCount],[MaxCount],[EachNumber]";
            pc.CurrentPage = currentPage;
            pc.PageSize = pageSize;
            pc.OrderField = "[ID]";
            pc.OrderType = OrderType.Desc;
            pc.Count = count;
            count = pc.Count;
            using (OleDbDataReader dr = pc.ExecuteReader())
            {
                PrepareGroupBuyModel(dr, groupBuyList);
            }
            return groupBuyList;
        }
    }
}
