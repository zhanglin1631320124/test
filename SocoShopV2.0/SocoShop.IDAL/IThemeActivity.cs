namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IThemeActivity
    {
        int AddThemeActivity(ThemeActivityInfo themeActivity);
        void DeleteThemeActivity(string strID);
        ThemeActivityInfo ReadThemeActivity(int id);
        List<ThemeActivityInfo> ReadThemeActivityList(int currentPage, int pageSize, ref int count);
        void UpdateThemeActivity(ThemeActivityInfo themeActivity);
    }
}

