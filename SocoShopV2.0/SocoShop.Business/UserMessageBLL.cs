namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class UserMessageBLL
    {
        private static readonly IUserMessage dal = FactoryHelper.Instance<IUserMessage>(Global.DataProvider, "UserMessageDAL");

        public static int AddUserMessage(UserMessageInfo userMessage)
        {
            userMessage.ID = dal.AddUserMessage(userMessage);
            return userMessage.ID;
        }

        public static void DeleteUserMessage(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadUserMessageIDList(strID, userID);
            dal.DeleteUserMessage(strID, userID);
        }

        public static void DeleteUserMessageByUserID(string strUserID)
        {
            dal.DeleteUserMessageByUserID(strUserID);
        }

        public static string ReadMessageType(int messageType)
        {
            string str = string.Empty;
            switch (messageType)
            {
                case 1:
                    return "留言";

                case 2:
                    return "投诉";

                case 3:
                    return "询问";

                case 4:
                    return "售后";

                case 5:
                    return "求购";
            }
            return str;
        }

        public static UserMessageInfo ReadUserMessage(int id, int userID)
        {
            return dal.ReadUserMessage(id, userID);
        }

        public static List<UserMessageInfo> SearchUserMessageList(UserMessageSeachInfo userMessage)
        {
            return dal.SearchUserMessageList(userMessage);
        }

        public static List<UserMessageInfo> SearchUserMessageList(int currentPage, int pageSize, UserMessageSeachInfo userMessage, ref int count)
        {
            return dal.SearchUserMessageList(currentPage, pageSize, userMessage, ref count);
        }

        public static void UpdateUserMessage(UserMessageInfo userMessage)
        {
            dal.UpdateUserMessage(userMessage);
        }
    }
}

