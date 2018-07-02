namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IMenu
    {
        int AddMenu(MenuInfo Menu);
        void DeleteMenu(int id);
        void MoveDownMenu(int id);
        void MoveUpMenu(int id);
        List<MenuInfo> ReadMenuAllList();
        void UpdateMenu(MenuInfo Menu);
    }
}

