namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;
    using SkyCES.EntLib;

    public sealed class ReceiveMessageBLL
    {
        private static readonly IReceiveMessage dal = FactoryHelper.Instance<IReceiveMessage>(Global.DataProvider, "ReceiveMessageDAL");

        public static int AddReceiveMessage(ReceiveMessageInfo receiveMessage)
        {
            receiveMessage.ID = dal.AddReceiveMessage(receiveMessage);
            return receiveMessage.ID;
        }

        public static void DeleteReceiveMessage(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadReceiveMessageIDList(strID, userID);
            dal.DeleteReceiveMessage(strID, userID);
        }

        public static ReceiveMessageInfo ReadReceiveMessage(int id, int userID)
        {
            return dal.ReadReceiveMessage(id, userID);
        }

        public static List<ReceiveMessageInfo> SearchReceiveMessageList(ReceiveMessageSearchInfo receiveMessage)
        {
            return dal.SearchReceiveMessageList(receiveMessage);
        }

        public static List<ReceiveMessageInfo> SearchReceiveMessageList(int currentPage, int pageSize, ReceiveMessageSearchInfo receiveMessage, ref int count)
        {
            return dal.SearchReceiveMessageList(currentPage, pageSize, receiveMessage, ref count);
        }

        public static void UpdateReceiveMessage(ReceiveMessageInfo receiveMessage)
        {
            dal.UpdateReceiveMessage(receiveMessage);
        }
    }
}

