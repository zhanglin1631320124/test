namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;

    public sealed class AdminLogBLL
    {
        private static readonly IAdminLog dal = FactoryHelper.Instance<IAdminLog>(Global.DataProvider, "AdminLogDAL");

        private static void AddAdminLog(AdminLogInfo adminLog)
        {
            dal.AddAdminLog(adminLog);
        }

        public static void AddAdminLog(string action)
        {
            AddAdminLog(action, string.Empty, string.Empty);
        }

        public static void AddAdminLog(string action, int id)
        {
            AddAdminLog(action, string.Empty, id.ToString());
        }

        public static void AddAdminLog(string action, string fileName)
        {
            if (action.IndexOf("$FileName") > -1) action = action.Replace("$FileName", fileName);
            AdminLogInfo adminLog = new AdminLogInfo();
            adminLog.Action = action;
            adminLog.AddDate = RequestHelper.DateNow;
            adminLog.IP = ClientHelper.IP;
            adminLog.AdminID = Cookies.Admin.GetAdminID(false);
            adminLog.AdminName = Cookies.Admin.GetAdminName(false);
            AddAdminLog(adminLog);
        }

        public static void AddAdminLog(string action, string tableName, int id)
        {
            AddAdminLog(action, tableName, id.ToString());
        }

        public static void AddAdminLog(string action, string tableName, string strID)
        {
            if (action.IndexOf("$TableName") > -1) action = action.Replace("$TableName", tableName);
            if (action.IndexOf("$ID") > -1) action = action.Replace("$ID", strID);
            AdminLogInfo adminLog = new AdminLogInfo();
            adminLog.Action = action;
            adminLog.AddDate = RequestHelper.DateNow;
            adminLog.IP = ClientHelper.IP;
            adminLog.AdminID = Cookies.Admin.GetAdminID(false);
            adminLog.AdminName = Cookies.Admin.GetAdminName(false);
            AddAdminLog(adminLog);
        }

        public static void DeleteAdminLog(string strID, int adminID)
        {
            dal.DeleteAdminLog(strID, adminID);
        }

        public static void DeleteAdminLogByAdminID(string strAdminID)
        {
            dal.DeleteAdminLogByAdminID(strAdminID);
        }

        public static void DeleteAdminLogByGroupID(string strGroupID)
        {
            dal.DeleteAdminLogByGroupID(strGroupID);
        }

        public static AdminLogInfo ReadAdminLog(int id, int adminID)
        {
            return dal.ReadAdminLog(id, adminID);
        }

        public static List<AdminLogInfo> ReadAdminLogList(int currentPage, int pageSize, ref int count, int adminID)
        {
            return dal.ReadAdminLogList(currentPage, pageSize, ref count, adminID);
        }

        public static void UpdateAdminLog(AdminLogInfo adminLog)
        {
            dal.UpdateAdminLog(adminLog);
        }
    }
}

