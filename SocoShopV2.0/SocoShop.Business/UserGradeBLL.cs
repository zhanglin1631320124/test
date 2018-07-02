namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class UserGradeBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("UserGrade");
        private static readonly IUserGrade dal = FactoryHelper.Instance<IUserGrade>(Global.DataProvider, "UserGradeDAL");

        public static int AddUserGrade(UserGradeInfo userGrade)
        {
            userGrade.ID = dal.AddUserGrade(userGrade);
            CacheHelper.Remove(cacheKey);
            return userGrade.ID;
        }

        public static void DeleteUserGrade(string strID)
        {
            dal.DeleteUserGrade(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static List<UserGradeInfo> JoinUserGrade(int productID)
        {
            List<MemberPriceInfo> list = MemberPriceBLL.ReadMemberPriceByProduct(productID);
            List<UserGradeInfo> list2 = ReadUserGradeCacheList();
            List<UserGradeInfo> list3 = new List<UserGradeInfo>();
            foreach (UserGradeInfo info in list2)
            {
                bool flag = false;
                foreach (MemberPriceInfo info2 in list)
                {
                    if (info.ID == info2.GradeID)
                    {
                        UserGradeInfo item = new UserGradeInfo();
                        item = (UserGradeInfo) ServerHelper.CopyClass(info);
                        item.MemberPrice = info2;
                        flag = true;
                        list3.Add(item);
                        break;
                    }
                }
                if (!flag)
                {
                    info.MemberPrice.Price = -1M;
                    list3.Add(info);
                }
            }
            return list3;
        }

        public static UserGradeInfo ReadUserGradeByMoney(decimal money)
        {
            UserGradeInfo info = new UserGradeInfo();
            List<UserGradeInfo> list = ReadUserGradeCacheList();
            foreach (UserGradeInfo info2 in list)
            {
                if (money >= info2.MinMoney && money < info2.MaxMoney) return info2;
            }
            return info;
        }

        public static UserGradeInfo ReadUserGradeCache(int id)
        {
            UserGradeInfo info = new UserGradeInfo();
            List<UserGradeInfo> list = ReadUserGradeCacheList();
            foreach (UserGradeInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<UserGradeInfo> ReadUserGradeCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadUserGradeAllList());
            return (List<UserGradeInfo>) CacheHelper.Read(cacheKey);
        }

        public static void UpdateUserGrade(UserGradeInfo userGrade)
        {
            dal.UpdateUserGrade(userGrade);
            CacheHelper.Remove(cacheKey);
        }
    }
}

