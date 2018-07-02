namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class SendMessageDAL : ISendMessage
    {
        public int AddSendMessage(SendMessageInfo sendMessage)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@toUserID", SqlDbType.NText), new SqlParameter("@toUserName", SqlDbType.NText), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar), new SqlParameter("@isAdmin", SqlDbType.Int) };
            pt[0].Value = sendMessage.Title;
            pt[1].Value = sendMessage.Content;
            pt[2].Value = sendMessage.Date;
            pt[3].Value = sendMessage.ToUserID;
            pt[4].Value = sendMessage.ToUserName;
            pt[5].Value = sendMessage.UserID;
            pt[6].Value = sendMessage.UserName;
            pt[7].Value = sendMessage.IsAdmin;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddSendMessage", pt));
        }

        public void DeleteSendMessage(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteSendMessage", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, SendMessageSearchInfo sendMessageSearch)
        {
            mssqlCondition.Add("[UserID]", sendMessageSearch.UserID, ConditionType.Equal);
            mssqlCondition.Add("[IsAdmin]", sendMessageSearch.IsAdmin, ConditionType.Equal);
        }

        public void PrepareSendMessageModel(SqlDataReader dr, List<SendMessageInfo> sendMessageList)
        {
            while (dr.Read())
            {
                SendMessageInfo item = new SendMessageInfo();
                item.ID = dr.GetInt32(0);
                item.Title = dr[1].ToString();
                item.Content = dr[2].ToString();
                item.Date = dr.GetDateTime(3);
                item.ToUserID = dr[4].ToString();
                item.ToUserName = dr[5].ToString();
                item.UserID = dr.GetInt32(6);
                item.UserName = dr[7].ToString();
                item.IsAdmin = dr.GetInt32(8);
                sendMessageList.Add(item);
            }
        }

        public SendMessageInfo ReadSendMessage(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            SendMessageInfo info = new SendMessageInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadSendMessage", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Title = reader[1].ToString();
                    info.Content = reader[2].ToString();
                    info.Date = reader.GetDateTime(3);
                    info.ToUserID = reader[4].ToString();
                    info.ToUserName = reader[5].ToString();
                    info.UserID = reader.GetInt32(6);
                    info.UserName = reader[7].ToString();
                    info.IsAdmin = reader.GetInt32(8);
                }
            }
            return info;
        }

        public string ReadSendMessageIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadSendMessageIDList", pt))
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

        public List<SendMessageInfo> SearchSendMessageList(SendMessageSearchInfo sendMessageSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, sendMessageSearch);
            List<SendMessageInfo> sendMessageList = new List<SendMessageInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchSendMessageList", pt))
            {
                this.PrepareSendMessageModel(reader, sendMessageList);
            }
            return sendMessageList;
        }

        public List<SendMessageInfo> SearchSendMessageList(int currentPage, int pageSize, SendMessageSearchInfo sendMessageSearch, ref int count)
        {
            List<SendMessageInfo> sendMessageList = new List<SendMessageInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "SendMessage";
            class2.Fields = "[ID],[Title],[Content],[Date],[ToUserID],[ToUserName],[UserID],[UserName],[IsAdmin]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, sendMessageSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareSendMessageModel(reader, sendMessageList);
            }
            return sendMessageList;
        }

        public void UpdateSendMessage(SendMessageInfo sendMessage)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText) };
            pt[0].Value = sendMessage.ID;
            pt[1].Value = sendMessage.Title;
            pt[2].Value = sendMessage.Content;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateSendMessage", pt);
        }
    }
}

