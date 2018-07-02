namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class VoteItemBLL
    {
        private static readonly IVoteItem dal = FactoryHelper.Instance<IVoteItem>(Global.DataProvider, "VoteItemDAL");

        public static int AddVoteItem(VoteItemInfo voteItem)
        {
            voteItem.ID = dal.AddVoteItem(voteItem);
            VoteBLL.ChangeVoteCount(voteItem.VoteID, ChangeAction.Plus);
            return voteItem.ID;
        }

        public static void ChangeVoteItemCount(string strID, ChangeAction action)
        {
            dal.ChangeVoteItemCount(strID, action);
        }

        public static void ChangeVoteItemCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeVoteItemCountByGeneral(strID, action);
        }

        public static void ChangeVoteItemOrder(ChangeAction action, int id)
        {
            dal.ChangeVoteItemOrder(action, id);
        }

        public static void DeleteVoteItem(string strID)
        {
            VoteBLL.ChangeVoteCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteVoteItem(strID);
        }

        public static void DeleteVoteItemByVoteID(string strVoteID)
        {
            VoteRecordBLL.DeleteVoteRecordByVoteID(strVoteID);
            dal.DeleteVoteItemByVoteID(strVoteID);
        }

        public static string ReadItemName(string strID, List<VoteItemInfo> voteItemLsit)
        {
            string str = string.Empty;
            if (strID != string.Empty)
            {
                foreach (string str2 in strID.Split(new char[] { ',' }))
                {
                    foreach (VoteItemInfo info in voteItemLsit)
                    {
                        if (info.ID == Convert.ToInt32(str2)) str = str + info.ItemName + ",";
                    }
                }
            }
            if (str.EndsWith(",")) str = str.Substring(0, str.Length - 1);
            return str;
        }

        public static VoteItemInfo ReadVoteItem(int id)
        {
            return dal.ReadVoteItem(id);
        }

        public static List<VoteItemInfo> ReadVoteItemAllList()
        {
            return dal.ReadVoteItemAllList();
        }

        public static List<VoteItemInfo> ReadVoteItemByVote(int voteID)
        {
            return dal.ReadVoteItemByVote(voteID);
        }

        public static void UpdateVoteItem(VoteItemInfo voteItem)
        {
            VoteItemInfo info = ReadVoteItem(voteItem.ID);
            dal.UpdateVoteItem(voteItem);
            if (voteItem.VoteID != info.VoteID)
            {
                VoteBLL.ChangeVoteCount(info.VoteID, ChangeAction.Minus);
                VoteBLL.ChangeVoteCount(voteItem.VoteID, ChangeAction.Plus);
            }
        }
    }
}

