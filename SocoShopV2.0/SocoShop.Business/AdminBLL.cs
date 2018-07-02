namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class AdminBLL
    {
        private static readonly IAdmin dal = FactoryHelper.Instance<IAdmin>(Global.DataProvider, "AdminDAL");

        public static int AddAdmin(AdminInfo admin)
        {
            admin.ID = dal.AddAdmin(admin);
            AdminGroupBLL.ChangeAdminGroupCount(admin.GroupID, ChangeAction.Plus);
            return admin.ID;
        }

        public static void ChangeAdminCount(int id, ChangeAction action)
        {
            dal.ChangeAdminCount(id, action);
        }

        public static void ChangeAdminCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeAdminCountByGeneral(strID, action);
        }

        public static void ChangePassword(int id, string newPassword)
        {
            dal.ChangePassword(id, newPassword);
        }

        public static void ChangePassword(int id, string oldPassword, string newPassword)
        {
            dal.ChangePassword(id, oldPassword, newPassword);
        }

        public static AdminInfo CheckAdminLogin(string loginName, string loginPass)
        {
            return dal.CheckAdminLogin(loginName, loginPass);
        }

        public static void DeleteAdmin(string strID)
        {
            AdminLogBLL.DeleteAdminLogByAdminID(strID);
            AdminGroupBLL.ChangeAdminGroupCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteAdmin(strID);
        }

        public static void DeleteAdminByGroupID(string strGroupID)
        {
            AdminLogBLL.DeleteAdminLogByGroupID(strGroupID);
            dal.DeleteAdminByGroupID(strGroupID);
        }

        public static string NoDelete(object isCreater, object id)
        {
            int num = Convert.ToInt32(isCreater);
            int num2 = Convert.ToInt32(id);
            string str = string.Empty;
            if (num != 1 && num2 != Cookies.Admin.GetAdminID(false)) str = "<input type=\"checkbox\" name=\"SelectID\" value=\"" + id.ToString() + "\" />";
            return str;
        }

        public static string NoPasswordAdd(object id)
        {
            int num = Convert.ToInt32(id);
            string str = string.Empty;
            if (num != Cookies.Admin.GetAdminID(false)) str = "<a href=\"javascript:pop('PasswordAdd.aspx?ID=" + id.ToString() + "',600,250,'修改密码','PasswordAdd" + id.ToString() + "')\"><img src=\"Style/Images/password.gif\" alt=\"修改密码\" title=\"修改密码\" /></a>";
            return str;
        }

        public static AdminInfo ReadAdmin(int id)
        {
            return dal.ReadAdmin(id);
        }

        public static List<AdminInfo> ReadAdminList(int currentPage, int pageSize, ref int count)
        {
            return dal.ReadAdminList(currentPage, pageSize, ref count);
        }

        public static List<AdminInfo> ReadAdminList(int groupID, int currentPage, int pageSize, ref int count)
        {
            return dal.ReadAdminList(groupID, currentPage, pageSize, ref count);
        }

        public static void UpdateAdmin(AdminInfo admin)
        {
            AdminInfo info = ReadAdmin(admin.ID);
            dal.UpdateAdmin(admin);
            if (admin.GroupID != info.GroupID)
            {
                AdminGroupBLL.ChangeAdminGroupCount(info.GroupID, ChangeAction.Minus);
                AdminGroupBLL.ChangeAdminGroupCount(admin.GroupID, ChangeAction.Plus);
            }
        }

        public static void UpdateAdminLogin(int id, DateTime lastLoginTime, string lastLoginIP)
        {
            dal.UpdateAdminLogin(id, lastLoginTime, lastLoginIP);
        }
    }
}

