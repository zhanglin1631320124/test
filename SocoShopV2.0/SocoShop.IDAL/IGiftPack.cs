namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IGiftPack
    {
        int AddGiftPack(GiftPackInfo giftPack);
        void DeleteGiftPack(string strID);
        GiftPackInfo ReadGiftPack(int id);
        List<GiftPackInfo> ReadGiftPackList(int currentPage, int pageSize, ref int count);
        void UpdateGiftPack(GiftPackInfo giftPack);
    }
}

