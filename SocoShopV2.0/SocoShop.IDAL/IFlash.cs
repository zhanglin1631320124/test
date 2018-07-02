namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IFlash
    {
        int AddFlash(FlashInfo flash);
        void ChangeFlashCount(int id, ChangeAction action);
        void ChangeFlashCountByGeneral(string strID, ChangeAction action);
        void DeleteFlash(string strID);
        FlashInfo ReadFlash(int id);
        List<FlashInfo> ReadFlashList(int currentPage, int pageSize, ref int count);
        void UpdateFlash(FlashInfo flash);
    }
}

