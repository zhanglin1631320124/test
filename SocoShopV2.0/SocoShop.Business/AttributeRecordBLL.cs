namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class AttributeRecordBLL
    {
        private static readonly IAttributeRecord dal = FactoryHelper.Instance<IAttributeRecord>(Global.DataProvider, "AttributeRecordDAL");

        public static void AddAttributeRecord(AttributeRecordInfo attributeRecord)
        {
            dal.AddAttributeRecord(attributeRecord);
        }

        public static void DeleteAttributeRecordByProductID(string strProductID)
        {
            dal.DeleteAttributeRecordByProductID(strProductID);
        }

        public static List<AttributeRecordInfo> ReadAttributeRecordByProduct(int productID)
        {
            return dal.ReadAttributeRecordByProduct(productID);
        }
    }
}

