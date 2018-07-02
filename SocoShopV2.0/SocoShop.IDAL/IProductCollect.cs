namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IProductCollect
    {
        int AddProductCollect(ProductCollectInfo productCollect);
        void DeleteProductCollect(string strID, int userID);
        ProductCollectInfo ReadProductCollect(int id, int userID);
        ProductCollectInfo ReadProductCollectByProductID(int productID, int userID);
        string ReadProductCollectIDList(string strID, int userID);
        List<ProductCollectInfo> ReadProductCollectList(int currentPage, int pageSize, ref int count, int userID);
        void UpdateProductCollect(ProductCollectInfo productCollect);
    }
}

