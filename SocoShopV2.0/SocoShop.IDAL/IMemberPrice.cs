namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IMemberPrice
    {
        void AddMemberPrice(MemberPriceInfo memberPrice);
        void DeleteMemberPriceByGradeID(string strGradeID);
        void DeleteMemberPriceByProductID(string strProductID);
        List<MemberPriceInfo> ReadMemberPriceByProduct(int productID);
        List<MemberPriceInfo> ReadMemberPriceByProduct(string strProductID);
        List<MemberPriceInfo> ReadMemberPriceByProductGrade(string strProductID, int gradeID);
    }
}

