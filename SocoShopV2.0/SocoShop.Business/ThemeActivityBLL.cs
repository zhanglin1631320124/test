namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class ThemeActivityBLL
    {
        private static readonly IThemeActivity dal = FactoryHelper.Instance<IThemeActivity>(Global.DataProvider, "ThemeActivityDAL");
        public static readonly int TableID = UploadTable.ReadTableID("ThemeActivity");

        public static int AddThemeActivity(ThemeActivityInfo themeActivity)
        {
            themeActivity.ID = dal.AddThemeActivity(themeActivity);
            UploadBLL.UpdateUpload(TableID, 0, themeActivity.ID, Cookies.Admin.GetRandomNumber(false));
            return themeActivity.ID;
        }

        public static void DeleteThemeActivity(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteThemeActivity(strID);
        }

        public static ThemeActivityInfo ReadThemeActivity(int id)
        {
            return dal.ReadThemeActivity(id);
        }

        public static List<ThemeActivityInfo> ReadThemeActivityList(int currentPage, int pageSize, ref int count)
        {
            return dal.ReadThemeActivityList(currentPage, pageSize, ref count);
        }

        public static void UpdateThemeActivity(ThemeActivityInfo themeActivity)
        {
            dal.UpdateThemeActivity(themeActivity);
            UploadBLL.UpdateUpload(TableID, 0, themeActivity.ID, Cookies.Admin.GetRandomNumber(false));
        }
    }
}

