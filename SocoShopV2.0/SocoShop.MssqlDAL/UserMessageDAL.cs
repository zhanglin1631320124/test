namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserMessageDAL : IUserMessage
    {
        public int AddUserMessage(UserMessageInfo userMessage)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@messageClass", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@userIP", SqlDbType.NVarChar), new SqlParameter("@postDate", SqlDbType.DateTime), new SqlParameter("@isHandler", SqlDbType.Int), new SqlParameter("@adminReplyContent", SqlDbType.NText), new SqlParameter("@adminReplyDate", SqlDbType.DateTime), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userMessage.MessageClass;
            pt[1].Value = userMessage.Title;
            pt[2].Value = userMessage.Content;
            pt[3].Value = userMessage.UserIP;
            pt[4].Value = userMessage.PostDate;
            pt[5].Value = userMessage.IsHandler;
            pt[6].Value = userMessage.AdminReplyContent;
            pt[7].Value = userMessage.AdminReplyDate;
            pt[8].Value = userMessage.UserID;
            pt[9].Value = userMessage.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserMessage", pt));
        }

        public void DeleteUserMessage(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserMessage", pt);
        }

        public void DeleteUserMessageByUserID(string strUserID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strUserID", SqlDbType.NVarChar) };
            pt[0].Value = strUserID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserMessageByUserID", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, UserMessageSeachInfo userMessageSearch)
        {
            mssqlCondition.Add("[MessageClass]", userMessageSearch.MessageClass, ConditionType.Equal);
            mssqlCondition.Add("[Title]", userMessageSearch.Title, ConditionType.Like);
            mssqlCondition.Add("[PostDate]", userMessageSearch.StartPostDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[PostDate]", userMessageSearch.EndPostDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[UserName]", userMessageSearch.UserName, ConditionType.Like);
            mssqlCondition.Add("[UserID]", userMessageSearch.UserID, ConditionType.Equal);
            mssqlCondition.Add("[IsHandler]", userMessageSearch.IsHandler, ConditionType.Equal);
        }

        public void PrepareUserMessageModel(SqlDataReader dr, List<UserMessageInfo> userMessageList)
        {
            while (dr.Read())
            {
                UserMessageInfo item = new UserMessageInfo();
                item.ID = dr.GetInt32(0);
                item.MessageClass = dr.GetInt32(1);
                item.Title = dr[2].ToString();
                item.Content = dr[3].ToString();
                item.UserIP = dr[4].ToString();
                item.PostDate = dr.GetDateTime(5);
                item.IsHandler = dr.GetInt32(6);
                item.AdminReplyContent = dr[7].ToString();
                item.AdminReplyDate = dr.GetDateTime(8);
                item.UserID = dr.GetInt32(9);
                item.UserName = dr[10].ToString();
                userMessageList.Add(item);
            }
        }

        public UserMessageInfo ReadUserMessage(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            UserMessageInfo info = new UserMessageInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserMessage", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.MessageClass = reader.GetInt32(1);
                    info.Title = reader[2].ToString();
                    info.Content = reader[3].ToString();
                    info.UserIP = reader[4].ToString();
                    info.PostDate = reader.GetDateTime(5);
                    info.IsHandler = reader.GetInt32(6);
                    info.AdminReplyContent = reader[7].ToString();
                    info.AdminReplyDate = reader.GetDateTime(8);
                    info.UserID = reader.GetInt32(9);
                    info.UserName = reader[10].ToString();
                }
            }
            return info;
        }

        public string ReadUserMessageIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserMessageIDList", pt))
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

        public List<UserMessageInfo> SearchUserMessageList(UserMessageSeachInfo userMessageSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, userMessageSearch);
            List<UserMessageInfo> userMessageList = new List<UserMessageInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchUserMessageList", pt))
            {
                this.PrepareUserMessageModel(reader, userMessageList);
            }
            return userMessageList;
        }

        public List<UserMessageInfo> SearchUserMessageList(int currentPage, int pageSize, UserMessageSeachInfo userMessageSearch, ref int count)
        {
            List<UserMessageInfo> userMessageList = new List<UserMessageInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "UserMessage";
            class2.Fields = "[ID],[MessageClass],[Title],[Content],[UserIP],[PostDate],[IsHandler],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, userMessageSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareUserMessageModel(reader, userMessageList);
            }
            return userMessageList;
        }

        public void UpdateUserMessage(UserMessageInfo userMessage)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@isHandler", SqlDbType.Int), new SqlParameter("@adminReplyContent", SqlDbType.NText), new SqlParameter("@adminReplyDate", SqlDbType.DateTime) };
            pt[0].Value = userMessage.ID;
            pt[1].Value = userMessage.IsHandler;
            pt[2].Value = userMessage.AdminReplyContent;
            pt[3].Value = userMessage.AdminReplyDate;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserMessage", pt);
        }
    }
}

