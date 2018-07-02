namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IAdmin
    {
        int AddAdmin(AdminInfo admin);
        void ChangeAdminCount(int id, ChangeAction action);
        void ChangeAdminCountByGeneral(string strID, ChangeAction action);
        void ChangePassword(int id, string newPassword);
        void ChangePassword(int id, string oldPassword, string newPassword);
        AdminInfo CheckAdminLogin(string loginName, string loginPass);
        void DeleteAdmin(string strID);
        void DeleteAdminByGroupID(string strGroupID);
        AdminInfo ReadAdmin(int id);
        List<AdminInfo> ReadAdminList(int currentPage, int pageSize, ref int count);
        List<AdminInfo> ReadAdminList(int groupID, int currentPage, int pageSize, ref int count);
        void UpdateAdmin(AdminInfo admin);
        void UpdateAdminLogin(int id, DateTime lastLoginDate, string lastLoginIP);
    }
}

