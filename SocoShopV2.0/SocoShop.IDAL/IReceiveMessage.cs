namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IReceiveMessage
    {
        int AddReceiveMessage(ReceiveMessageInfo receiveMessage);
        void DeleteReceiveMessage(string strID, int userID);
        ReceiveMessageInfo ReadReceiveMessage(int id, int userID);
        string ReadReceiveMessageIDList(string strID, int userID);
        List<ReceiveMessageInfo> SearchReceiveMessageList(ReceiveMessageSearchInfo receiveMessage);
        List<ReceiveMessageInfo> SearchReceiveMessageList(int currentPage, int pageSize, ReceiveMessageSearchInfo receiveMessage, ref int count);
        void UpdateReceiveMessage(ReceiveMessageInfo receiveMessage);
    }
}

