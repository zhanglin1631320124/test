namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ReceiveMessageDAL : IReceiveMessage
    {
        public int AddReceiveMessage(ReceiveMessageInfo receiveMessage)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@isRead", SqlDbType.Int), new SqlParameter("@isAdmin", SqlDbType.Int), new SqlParameter("@fromUserID", SqlDbType.Int), new SqlParameter("@fromUserName", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = receiveMessage.Title;
            pt[1].Value = receiveMessage.Content;
            pt[2].Value = receiveMessage.Date;
            pt[3].Value = receiveMessage.IsRead;
            pt[4].Value = receiveMessage.IsAdmin;
            pt[5].Value = receiveMessage.FromUserID;
            pt[6].Value = receiveMessage.FromUserName;
            pt[7].Value = receiveMessage.UserID;
            pt[8].Value = receiveMessage.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddReceiveMessage", pt));
        }

        public void DeleteReceiveMessage(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteReceiveMessage", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, ReceiveMessageSearchInfo receiveMessageSearch)
        {
            mssqlCondition.Add("[IsRead]", receiveMessageSearch.IsRead, ConditionType.Equal);
            mssqlCondition.Add("[IsAdmin]", receiveMessageSearch.IsAdmin, ConditionType.Equal);
            mssqlCondition.Add("[UserID]", receiveMessageSearch.UserID, ConditionType.Equal);
        }

        public void PrepareReceiveMessageModel(SqlDataReader dr, List<ReceiveMessageInfo> receiveMessageList)
        {
            while (dr.Read())
            {
                ReceiveMessageInfo item = new ReceiveMessageInfo();
                item.ID = dr.GetInt32(0);
                item.Title = dr[1].ToString();
                item.Content = dr[2].ToString();
                item.Date = dr.GetDateTime(3);
                item.IsRead = dr.GetInt32(4);
                item.IsAdmin = dr.GetInt32(5);
                item.FromUserID = dr.GetInt32(6);
                item.FromUserName = dr[7].ToString();
                item.UserID = dr.GetInt32(8);
                item.UserName = dr[9].ToString();
                receiveMessageList.Add(item);
            }
        }

        public ReceiveMessageInfo ReadReceiveMessage(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            ReceiveMessageInfo info = new ReceiveMessageInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadReceiveMessage", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Title = reader[1].ToString();
                    info.Content = reader[2].ToString();
                    info.Date = reader.GetDateTime(3);
                    info.IsRead = reader.GetInt32(4);
                    info.IsAdmin = reader.GetInt32(5);
                    info.FromUserID = reader.GetInt32(6);
                    info.FromUserName = reader[7].ToString();
                    info.UserID = reader.GetInt32(8);
                    info.UserName = reader[9].ToString();
                }
            }
            return info;
        }

        public string ReadReceiveMessageIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadReceiveMessageIDList", pt))
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

        public List<ReceiveMessageInfo> SearchReceiveMessageList(ReceiveMessageSearchInfo receiveMessageSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, receiveMessageSearch);
            List<ReceiveMessageInfo> receiveMessageList = new List<ReceiveMessageInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchReceiveMessageList", pt))
            {
                this.PrepareReceiveMessageModel(reader, receiveMessageList);
            }
            return receiveMessageList;
        }

        public List<ReceiveMessageInfo> SearchReceiveMessageList(int currentPage, int pageSize, ReceiveMessageSearchInfo receiveMessageSearch, ref int count)
        {
            List<ReceiveMessageInfo> receiveMessageList = new List<ReceiveMessageInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "ReceiveMessage";
            class2.Fields = "[ID],[Title],[Content],[Date],[IsRead],[IsAdmin],[FromUserID],[FromUserName],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, receiveMessageSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareReceiveMessageModel(reader, receiveMessageList);
            }
            return receiveMessageList;
        }

        public void UpdateReceiveMessage(ReceiveMessageInfo receiveMessage)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@isRead", SqlDbType.Int) };
            pt[0].Value = receiveMessage.ID;
            pt[1].Value = receiveMessage.IsRead;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateReceiveMessage", pt);
        }
    }
}

