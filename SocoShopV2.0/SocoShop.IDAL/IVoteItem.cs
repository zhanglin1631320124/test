namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IVoteItem
    {
        int AddVoteItem(VoteItemInfo voteItem);
        void ChangeVoteItemCount(string strID, ChangeAction action);
        void ChangeVoteItemCountByGeneral(string strID, ChangeAction action);
        void ChangeVoteItemOrder(ChangeAction action, int id);
        void DeleteVoteItem(string strID);
        void DeleteVoteItemByVoteID(string strVoteID);
        VoteItemInfo ReadVoteItem(int id);
        List<VoteItemInfo> ReadVoteItemAllList();
        List<VoteItemInfo> ReadVoteItemByVote(int voteID);
        void UpdateVoteItem(VoteItemInfo voteItem);
    }
}

