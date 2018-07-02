namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IUserAddress
    {
        int AddUserAddress(UserAddressInfo userAddress);
        void DeleteUserAddress(string strID, int userID);
        void DeleteUserAddressByUserID(string strUserID);
        UserAddressInfo ReadUserAddress(int id, int userID);
        List<UserAddressInfo> ReadUserAddressByUser(int userID);
        void UpdateUserAddress(UserAddressInfo userAddress);
        void UpdateUserAddressIsDefault(int isDefault, int userID);
    }
}

