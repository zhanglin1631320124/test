namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IVote
    {
        int AddVote(VoteInfo vote);
        void ChangeVoteCount(int id, ChangeAction action);
        void ChangeVoteCountByGeneral(string strID, ChangeAction action);
        void DeleteVote(string strID);
        VoteInfo ReadVote(int id);
        List<VoteInfo> ReadVoteList(int currentPage, int pageSize, ref int count);
        void UpdateVote(VoteInfo vote);
    }
}

