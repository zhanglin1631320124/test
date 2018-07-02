namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;
    using SkyCES.EntLib;

    public sealed class MemberPriceBLL
    {
        private static readonly IMemberPrice dal = FactoryHelper.Instance<IMemberPrice>(Global.DataProvider, "MemberPriceDAL");

        public static void AddMemberPrice(MemberPriceInfo memberPrice)
        {
            dal.AddMemberPrice(memberPrice);
        }

        public static void DeleteMemberPriceByGradeID(string strGradeID)
        {
            dal.DeleteMemberPriceByGradeID(strGradeID);
        }

        public static void DeleteMemberPriceByProductID(string strProductID)
        {
            dal.DeleteMemberPriceByProductID(strProductID);
        }

        public static decimal ReadCurrentMemberPrice(List<MemberPriceInfo> MemberPriceList, int gradeID, ProductInfo product)
        {
            decimal d = product.MarketPrice * UserGradeBLL.ReadUserGradeCache(gradeID).Discount / 100M;
            foreach (MemberPriceInfo info in MemberPriceList)
            {
                if (info.GradeID == gradeID && info.ProductID == product.ID)
                {
                    d = info.Price;
                    break;
                }
            }
            return Math.Round(d, 2);
        }

        public static decimal ReadMemberPrice(UserGradeInfo userGrade, List<MemberPriceInfo> memberPriceList, ProductInfo product)
        {
            decimal d = product.MarketPrice * userGrade.Discount / 100M;
            foreach (MemberPriceInfo info in memberPriceList)
            {
                if (info.GradeID == userGrade.ID)
                {
                    d = info.Price;
                    break;
                }
            }
            return Math.Round(d, 2);
        }

        public static List<MemberPriceInfo> ReadMemberPriceByProduct(int productID)
        {
            return dal.ReadMemberPriceByProduct(productID);
        }

        public static List<MemberPriceInfo> ReadMemberPriceByProduct(string strProductID)
        {
            return dal.ReadMemberPriceByProduct(strProductID);
        }

        public static List<MemberPriceInfo> ReadMemberPriceByProductGrade(string strProductID, int gradeID)
        {
            return dal.ReadMemberPriceByProductGrade(strProductID, gradeID);
        }
    }
}

