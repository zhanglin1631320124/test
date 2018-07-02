namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class SendMessageBLL
    {
        private static readonly ISendMessage dal = FactoryHelper.Instance<ISendMessage>(Global.DataProvider, "SendMessageDAL");

        public static int AddSendMessage(SendMessageInfo sendMessage)
        {
            sendMessage.ID = dal.AddSendMessage(sendMessage);
            return sendMessage.ID;
        }

        public static void DeleteSendMessage(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadSendMessageIDList(strID, userID);
            dal.DeleteSendMessage(strID, userID);
        }

        public static SendMessageInfo ReadSendMessage(int id, int userID)
        {
            return dal.ReadSendMessage(id, userID);
        }

        public static List<SendMessageInfo> SearchSendMessageList(SendMessageSearchInfo sendMessage)
        {
            return dal.SearchSendMessageList(sendMessage);
        }

        public static List<SendMessageInfo> SearchSendMessageList(int currentPage, int pageSize, SendMessageSearchInfo sendMessage, ref int count)
        {
            return dal.SearchSendMessageList(currentPage, pageSize, sendMessage, ref count);
        }

        public static void UpdateSendMessage(SendMessageInfo sendMessage)
        {
            dal.UpdateSendMessage(sendMessage);
        }
    }
}

