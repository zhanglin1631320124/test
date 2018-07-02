namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IProductPhoto
    {
        int AddProductPhoto(ProductPhotoInfo productPhoto);
        void DeleteProductPhoto(string strID);
        void DeleteProductPhotoByProductID(string strProductID);
        ProductPhotoInfo ReadProductPhoto(int id);
        List<ProductPhotoInfo> ReadProductPhotoByProduct(int productID);
    }
}

