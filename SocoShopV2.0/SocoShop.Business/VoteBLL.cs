namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class VoteBLL
    {
        private static readonly IVote dal = FactoryHelper.Instance<IVote>(Global.DataProvider, "VoteDAL");

        public static int AddVote(VoteInfo vote)
        {
            vote.ID = dal.AddVote(vote);
            return vote.ID;
        }

        public static void ChangeVoteCount(int id, ChangeAction action)
        {
            dal.ChangeVoteCount(id, action);
        }

        public static void ChangeVoteCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeVoteCountByGeneral(strID, action);
        }

        public static void DeleteVote(string strID)
        {
            VoteItemBLL.DeleteVoteItemByVoteID(strID);
            dal.DeleteVote(strID);
        }

        public static VoteInfo ReadVote(int id)
        {
            return dal.ReadVote(id);
        }

        public static List<VoteInfo> ReadVoteList(int currentPage, int pageSize, ref int count)
        {
            return dal.ReadVoteList(currentPage, pageSize, ref count);
        }

        public static void UpdateVote(VoteInfo vote)
        {
            dal.UpdateVote(vote);
        }
    }
}

