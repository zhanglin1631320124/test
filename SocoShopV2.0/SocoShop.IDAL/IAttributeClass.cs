namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IAttributeClass
    {
        int AddAttributeClass(AttributeClassInfo attributeClass);
        void ChangeAttributeClassCount(int id, ChangeAction action);
        void ChangeAttributeClassCountByGeneral(string strID, ChangeAction action);
        void DeleteAttributeClass(string strID);
        List<AttributeClassInfo> ReadAttributeClassAllList();
        void UpdateAttributeClass(AttributeClassInfo attributeClass);
    }
}

