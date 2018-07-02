namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IProduct
    {
        int AddProduct(ProductInfo product);
        void ChangeProductCollectCount(int id, ChangeAction action);
        void ChangeProductCollectCountByGeneral(string strID, ChangeAction action);
        void ChangeProductCommentCountAndRank(int id, int rank, ChangeAction action);
        void ChangeProductCommentCountAndRankByGeneral(string strID, ChangeAction action);
        void ChangeProductOrderCount(string strProductID, int changeCount);
        void ChangeProductOrderCountByOrder(int orderID, ChangeAction changeAction);
        void ChangeProductPhotoCount(int id, ChangeAction action);
        void ChangeProductPhotoCountByGeneral(string strID, ChangeAction action);
        void ChangeProductSendCountByOrder(int orderID, ChangeAction changeAction);
        void ChangeProductViewCount(int productID, int changeCount);
        void ChangProductAllowComment(int id, int status);
        void ChangProductIsHot(int id, int status);
        void ChangProductIsNew(int id, int status);
        void ChangProductIsSpecial(int id, int status);
        void ChangProductIsTop(int id, int status);
        void DeleteProduct(string strID);
        DataTable NoHandlerStatistics();
        void OffSaleProduct(string strID);
        void OnSaleProduct(string strID);
        ProductInfo ReadProduct(int id);
        List<ProductInfo> SearchProductList(ProductSearchInfo productSearch);
        List<ProductInfo> SearchProductList(int currentPage, int pageSize, ProductSearchInfo product, ref int count);
        List<ProductInfo> SearchProductList(int currentPage, int pageSize, ProductSearchInfo productSearch, ref int count, int gradeID, decimal disCount);
        DataTable StatisticsProductSale(int currentPage, int pageSize, ProductSearchInfo productSearch, ref int count, DateTime startDate, DateTime endDate);
        void TaobaoProduct(ProductInfo product);
        void UnionUpdateProduct(string productIDList, ProductInfo product);
        void UpdateProduct(ProductInfo product);
        void UpdateProductCoverPhoto(int id, string photo);
        void UpdateProductStandardType(string strID, int standardType, int id);
    }
}

