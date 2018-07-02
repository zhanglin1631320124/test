namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using SkyCES.EntLib;

    public sealed class ProductBLL
    {
        private static readonly IProduct dal = FactoryHelper.Instance<IProduct>(Global.DataProvider, "ProductDAL");
        public static readonly int TableID = UploadTable.ReadTableID("Product");

        public static int AddProduct(ProductInfo product)
        {
            product.ID = dal.AddProduct(product);
            UploadBLL.UpdateUpload(TableID, 0, product.ID, Cookies.Admin.GetRandomNumber(false));
            return product.ID;
        }

        public static void ChangeProductCollectCount(int id, ChangeAction action)
        {
            dal.ChangeProductCollectCount(id, action);
        }

        public static void ChangeProductCollectCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeProductCollectCountByGeneral(strID, action);
        }

        public static void ChangeProductCommentCountAndRank(int id, int rank, ChangeAction action)
        {
            dal.ChangeProductCommentCountAndRank(id, rank, action);
        }

        public static void ChangeProductCommentCountAndRankByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeProductCommentCountAndRankByGeneral(strID, action);
        }

        public static void ChangeProductOrderCount(string strProductID, int changeCount)
        {
            dal.ChangeProductOrderCount(strProductID, changeCount);
        }

        public static void ChangeProductOrderCountByOrder(int orderID, ChangeAction changeAction)
        {
            dal.ChangeProductOrderCountByOrder(orderID, changeAction);
        }

        public static void ChangeProductPhotoCount(int id, ChangeAction action)
        {
            dal.ChangeProductPhotoCount(id, action);
        }

        public static void ChangeProductPhotoCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeProductPhotoCountByGeneral(strID, action);
        }

        public static void ChangeProductSendCountByOrder(int orderID, ChangeAction changeAction)
        {
            dal.ChangeProductSendCountByOrder(orderID, changeAction);
        }

        public static void ChangeProductViewCount(int productID, int changeCount)
        {
            dal.ChangeProductViewCount(productID, changeCount);
        }

        public static void ChangProductAllowComment(int id, int status)
        {
            dal.ChangProductAllowComment(id, status);
        }

        public static void ChangProductIsHot(int id, int status)
        {
            dal.ChangProductIsHot(id, status);
        }

        public static void ChangProductIsNew(int id, int status)
        {
            dal.ChangProductIsNew(id, status);
        }

        public static void ChangProductIsSpecial(int id, int status)
        {
            dal.ChangProductIsSpecial(id, status);
        }

        public static void ChangProductIsTop(int id, int status)
        {
            dal.ChangProductIsTop(id, status);
        }

        public static void DeleteProduct(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            ProductBrandBLL.ChangeProductBrandCountByGeneral(strID, ChangeAction.Minus);
            ProductPhotoBLL.DeleteProductPhotoByProductID(strID);
            dal.DeleteProduct(strID);
        }

        public static DataTable NoHandlerStatistics()
        {
            return dal.NoHandlerStatistics();
        }

        public static void OffSaleProduct(string strID)
        {
            dal.OffSaleProduct(strID);
        }

        public static void OnSaleProduct(string strID)
        {
            dal.OnSaleProduct(strID);
        }

        public static ProductInfo ReadProduct(int id)
        {
            return dal.ReadProduct(id);
        }

        public static ProductInfo ReadProductByProductList(List<ProductInfo> productList, int productID)
        {
            ProductInfo info = new ProductInfo();
            foreach (ProductInfo info2 in productList)
            {
                if (info2.ID == productID) info = info2;
            }
            return info;
        }

        public static List<ProductInfo> SearchProductList(ProductSearchInfo productSearch)
        {
            return dal.SearchProductList(productSearch);
        }

        public static List<ProductInfo> SearchProductList(int currentPage, int pageSize, ProductSearchInfo product, ref int count)
        {
            return dal.SearchProductList(currentPage, pageSize, product, ref count);
        }

        public static List<ProductInfo> SearchProductList(int currentPage, int pageSize, ProductSearchInfo productSearch, ref int count, int gradeID, decimal disCount)
        {
            return dal.SearchProductList(currentPage, pageSize, productSearch, ref count, gradeID, disCount);
        }

        public static DataTable StatisticsProductSale(int currentPage, int pageSize, ProductSearchInfo productSearch, ref int count, DateTime startDate, DateTime endDate)
        {
            return dal.StatisticsProductSale(currentPage, pageSize, productSearch, ref count, startDate, endDate);
        }

        public static void TaobaoProduct(ProductInfo product)
        {
            dal.TaobaoProduct(product);
        }

        public static void UnionUpdateProduct(string productIDList, ProductInfo product)
        {
            dal.UnionUpdateProduct(productIDList, product);
        }

        public static void UpdateProduct(ProductInfo product)
        {
            dal.UpdateProduct(product);
            UploadBLL.UpdateUpload(TableID, 0, product.ID, Cookies.Admin.GetRandomNumber(false));
        }

        public static void UpdateProductCoverPhoto(int id, string photo)
        {
            dal.UpdateProductCoverPhoto(id, photo);
        }

        public static void UpdateProductStandardType(string strID, int standardType, int id)
        {
            dal.UpdateProductStandardType(strID, standardType, id);
        }
    }
}

