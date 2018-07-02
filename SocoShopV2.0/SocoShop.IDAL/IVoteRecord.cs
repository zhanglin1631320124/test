namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IVoteRecord
    {
        int AddVoteRecord(VoteRecordInfo voteRecord);
        void DeleteVoteRecord(string strID);
        void DeleteVoteRecordByItemID(string strItemID);
        void DeleteVoteRecordByVoteID(string strVoteID);
        VoteRecordInfo ReadVoteRecord(int id);
        List<VoteRecordInfo> ReadVoteRecordList(int voteID, int currentPage, int pageSize, ref int count);
    }
}

