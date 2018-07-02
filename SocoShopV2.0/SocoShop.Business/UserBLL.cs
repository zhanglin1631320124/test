namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.Security;

    public sealed class UserBLL
    {
        private static readonly IUser dal = FactoryHelper.Instance<IUser>(Global.DataProvider, "UserDAL");
        public static readonly int TableID = UploadTable.ReadTableID("User");

        public static int AddUser(UserInfo user)
        {
            user.ID = dal.AddUser(user);
            UploadBLL.UpdateUpload(TableID, 0, user.ID, Cookies.Admin.GetRandomNumber(false));
            return user.ID;
        }

        public static void ChangePassword(int id, string newPassword)
        {
            dal.ChangePassword(id, newPassword);
        }

        public static void ChangePassword(int id, string oldPassword, string newPassword)
        {
            dal.ChangePassword(id, oldPassword, newPassword);
        }

        public static void ChangeUserSafeCode(int userID, string safeCode, DateTime findDate)
        {
            dal.ChangeUserSafeCode(userID, safeCode, findDate);
        }

        public static void ChangeUserStatus(string strID, int status)
        {
            dal.ChangeUserStatus(strID, status);
        }

        public static bool CheckEmail(string email)
        {
            return dal.CheckEmail(email);
        }

        public static UserInfo CheckUserLogin(string loginName, string loginPass)
        {
            return dal.CheckUserLogin(loginName, loginPass);
        }

        public static int CheckUserName(string userName)
        {
            return dal.CheckUserName(userName);
        }

        public static void DeleteUser(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteUser(strID);
        }

        public static UserInfo ReadUser(int id)
        {
            return dal.ReadUser(id);
        }

        public static UserInfo ReadUserByOpenID(string openID)
        {
            return dal.ReadUserByOpenID(openID);
        }

        public static UserInfo ReadUserByUserList(List<UserInfo> userList, int userID)
        {
            UserInfo info = new UserInfo();
            foreach (UserInfo info2 in userList)
            {
                if (info2.ID == userID) info = info2;
            }
            return info;
        }

        public static UserInfo ReadUserByUserName(string userName)
        {
            return dal.ReadUserByUserName(userName);
        }

        public static List<string> ReadUserEmailByMoneyUsed(Dictionary<decimal, decimal> moneyUsed)
        {
            return dal.ReadUserEmailByMoneyUsed(moneyUsed);
        }

        public static UserInfo ReadUserMore(int id)
        {
            return dal.ReadUserMore(id);
        }

        public static string ReadUserPhoto(string size)
        {
            string str = CookiesHelper.ReadCookieValue("UserPhoto");
            if (str == string.Empty) return ("/Plugins/Template/" + ShopConfig.ReadConfigInfo().TemplatePath + "/Style/Images/NoImage.gif");
            return str.Replace("Original", size);
        }

        public static List<UserInfo> SearchUserList(UserSearchInfo user)
        {
            return dal.SearchUserList(user);
        }

        public static List<UserInfo> SearchUserList(int currentPage, int pageSize, UserSearchInfo user, ref int count)
        {
            return dal.SearchUserList(currentPage, pageSize, user, ref count);
        }

        public static DataTable StatisticsUserActive(int currentPage, int pageSize, UserSearchInfo userSearch, ref int count, string orderField)
        {
            return dal.StatisticsUserActive(currentPage, pageSize, userSearch, ref count, orderField);
        }

        public static DataTable StatisticsUserConsume(int currentPage, int pageSize, UserSearchInfo userSearch, ref int count, string orderField, DateTime startDate, DateTime endDate)
        {
            return dal.StatisticsUserConsume(currentPage, pageSize, userSearch, ref count, orderField, startDate, endDate);
        }

        public static DataTable StatisticsUserCount(UserSearchInfo userSearch, DateType dateType)
        {
            return dal.StatisticsUserCount(userSearch, dateType);
        }

        public static DataTable StatisticsUserStatus(UserSearchInfo userSearch)
        {
            return dal.StatisticsUserStatus(userSearch);
        }

        public static void UpdateUser(UserInfo user)
        {
            dal.UpdateUser(user);
            UploadBLL.UpdateUpload(TableID, 0, user.ID, Cookies.Admin.GetRandomNumber(false));
        }

        public static void UpdateUserLogin(int id, DateTime lastLoginDate, string lastLoginIP)
        {
            dal.UpdateUserLogin(id, lastLoginDate, lastLoginIP);
        }

        public static DataTable UserIndexStatistics(int userID)
        {
            return dal.UserIndexStatistics(userID);
        }

        public static void UserLoginInit(UserInfo user)
        {
            int iD = UserGradeBLL.ReadUserGradeByMoney(user.MoneyUsed).ID;
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(user.ID.ToString() + HttpContext.Current.Server.UrlEncode(user.UserName) + user.MoneyUsed.ToString() + iD.ToString() + ShopConfig.ReadConfigInfo().SecureKey + ClientHelper.Agent, "MD5");
            string str2 = string.Concat(new object[] { str, "|", user.ID.ToString(), "|", HttpContext.Current.Server.UrlEncode(user.UserName), "|", user.MoneyUsed, "|", iD });
            CookiesHelper.AddCookie(ShopConfig.ReadConfigInfo().UserCookies, str2);
            CookiesHelper.AddCookie("UserPhoto", user.Photo);
            CookiesHelper.AddCookie("UserEmail", user.Email);
            CartBLL.CookiesImportDataBase(user.ID, user.UserName);
            CartBLL.StaticsCart(user.ID, iD);
        }
    }
}

