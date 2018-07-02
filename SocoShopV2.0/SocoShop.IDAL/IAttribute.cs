namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IAttribute
    {
        int AddAttribute(AttributeInfo attribute);
        void ChangeAttributeOrder(ChangeAction action, int id);
        void DeleteAttribute(string strID);
        void DeleteAttributeByAttributeClassID(string strAttributeClassID);
        List<AttributeInfo> ReadAttributeAllList();
        void UpdateAttribute(AttributeInfo attribute);
    }
}

