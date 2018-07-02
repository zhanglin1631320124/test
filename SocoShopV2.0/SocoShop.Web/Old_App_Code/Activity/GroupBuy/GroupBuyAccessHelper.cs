using System;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections.Generic;
using SkyCES.EntLib;

namespace SocoShop.Web
{
    /// <summary>
    /// Aceess数据库操作类
    /// </summary>
    public sealed class GroupBuyAccessHelper
    {
        private static string tablePrefix = string.Empty;
        /// <summary>
        /// 数据库前缀
        /// </summary>
        /// <returns></returns>
        public static string TablePrefix
        {
            get { return tablePrefix; }
            set { tablePrefix = value; }
        }
        private static AccessHelper accessHelper;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static GroupBuyAccessHelper()
        {
            accessHelper = new AccessHelper();
            accessHelper.ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source = " + ServerHelper.MapPath("/Plugins/Activity/GroupBuy/GroupBuy.mdb");
            tablePrefix = "SocoShop_";
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        public static void ExecuteNonQuery(string commandText)
        {
            accessHelper.ExecuteNonQuery(commandText);
        }
        public static void ExecuteNonQuery(string commandText, OleDbParameter[] pt)
        {
            accessHelper.ExecuteNonQuery(commandText, pt);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        public static void ExecuteNonQuery(List<string> commandTexts)
        {
            accessHelper.ExecuteNonQuery(commandTexts);
        }
        /// <summary>
        /// 返回SCALAR对像
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandText)
        {
            return accessHelper.ExecuteScalar(commandText);
        }
        public static object ExecuteScalar(string commandText, OleDbParameter[] pt)
        {
            return accessHelper.ExecuteScalar(commandText, pt);
        }
        /// <summary>
        /// 返回DataReader对像
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(string commandText)
        {
            return accessHelper.ExecuteReader(commandText);
        }
        public static OleDbDataReader ExecuteReader(string commandText, OleDbParameter[] pt)
        {
            return accessHelper.ExecuteReader(commandText, pt);
        }
        /// <summary>
        /// 返回DataTable对像
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string commandText)
        {
            return accessHelper.ExecuteDataTable(commandText);
        }
        public static DataTable ExecuteDataTable(string commandText, OleDbParameter[] pt)
        {
            return accessHelper.ExecuteDataTable(commandText, pt);
        }
    }
}
