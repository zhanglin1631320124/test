namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class VoteRecordBLL
    {
        private static readonly IVoteRecord dal = FactoryHelper.Instance<IVoteRecord>(Global.DataProvider, "VoteRecordDAL");

        public static int AddVoteRecord(VoteRecordInfo voteRecord)
        {
            voteRecord.ID = dal.AddVoteRecord(voteRecord);
            VoteItemBLL.ChangeVoteItemCount(voteRecord.ItemID, ChangeAction.Plus);
            return voteRecord.ID;
        }

        public static void DeleteVoteRecord(string strID)
        {
            VoteItemBLL.ChangeVoteItemCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteVoteRecord(strID);
        }

        public static void DeleteVoteRecordByItemID(string strItemID)
        {
            dal.DeleteVoteRecordByItemID(strItemID);
        }

        public static void DeleteVoteRecordByVoteID(string strVoteID)
        {
            dal.DeleteVoteRecordByVoteID(strVoteID);
        }

        public static VoteRecordInfo ReadVoteRecord(int id)
        {
            return dal.ReadVoteRecord(id);
        }

        public static List<VoteRecordInfo> ReadVoteRecordList(int voteID, int currentPage, int pageSize, ref int count)
        {
            return dal.ReadVoteRecordList(voteID, currentPage, pageSize, ref count);
        }
    }
}

