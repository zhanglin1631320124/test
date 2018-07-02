namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IUserMessage
    {
        int AddUserMessage(UserMessageInfo userMessage);
        void DeleteUserMessage(string strID, int userID);
        void DeleteUserMessageByUserID(string strUserID);
        UserMessageInfo ReadUserMessage(int id, int userID);
        string ReadUserMessageIDList(string strID, int userID);
        List<UserMessageInfo> SearchUserMessageList(UserMessageSeachInfo userMessage);
        List<UserMessageInfo> SearchUserMessageList(int currentPage, int pageSize, UserMessageSeachInfo userMessage, ref int count);
        void UpdateUserMessage(UserMessageInfo userMessage);
    }
}

