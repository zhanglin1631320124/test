namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class UserAddressBLL
    {
        private static readonly IUserAddress dal = FactoryHelper.Instance<IUserAddress>(Global.DataProvider, "UserAddressDAL");

        public static int AddUserAddress(UserAddressInfo userAddress)
        {
            userAddress.ID = dal.AddUserAddress(userAddress);
            return userAddress.ID;
        }

        public static void DeleteUserAddress(string strID, int userID)
        {
            dal.DeleteUserAddress(strID, userID);
        }

        public static void DeleteUserAddressByUserID(string strUserID)
        {
            dal.DeleteUserAddressByUserID(strUserID);
        }

        public static UserAddressInfo ReadUserAddress(int id, int userID)
        {
            return dal.ReadUserAddress(id, userID);
        }

        public static List<UserAddressInfo> ReadUserAddressByUser(int userID)
        {
            return dal.ReadUserAddressByUser(userID);
        }

        public static void UpdateUserAddress(UserAddressInfo userAddress)
        {
            dal.UpdateUserAddress(userAddress);
        }

        public static void UpdateUserAddressIsDefault(int isDefault, int userID)
        {
            dal.UpdateUserAddressIsDefault(isDefault, userID);
        }
    }
}

