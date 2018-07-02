namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IStandardRecord
    {
        void AddStandardRecord(StandardRecordInfo standardRecord);
        void DeleteStandardRecord(string strID);
        void DeleteStandardRecordByProductID(string strProductID);
        List<StandardRecordInfo> ReadStandardRecordByProduct(int productID, int standardType);
    }
}

