namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IFlashPhoto
    {
        int AddFlashPhoto(FlashPhotoInfo flashPhoto);
        void ChangeFlashPhotoOrder(ChangeAction action, int id);
        void DeleteFlashPhoto(string strID);
        void DeleteFlashPhotoByFlashID(string strFlashID);
        FlashPhotoInfo ReadFlashPhoto(int id);
        List<FlashPhotoInfo> ReadFlashPhotoByFlash(int flashID);
        void UpdateFlashPhoto(FlashPhotoInfo flashPhoto);
    }
}

