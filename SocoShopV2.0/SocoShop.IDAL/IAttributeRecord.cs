namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IAttributeRecord
    {
        void AddAttributeRecord(AttributeRecordInfo attributeRecord);
        void DeleteAttributeRecordByProductID(string strProductID);
        List<AttributeRecordInfo> ReadAttributeRecordByProduct(int productID);
    }
}

