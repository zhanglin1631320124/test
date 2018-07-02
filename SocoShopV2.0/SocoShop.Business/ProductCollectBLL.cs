namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class ProductCollectBLL
    {
        private static readonly IProductCollect dal = FactoryHelper.Instance<IProductCollect>(Global.DataProvider, "ProductCollectDAL");

        public static int AddProductCollect(ProductCollectInfo productCollect)
        {
            productCollect.ID = dal.AddProductCollect(productCollect);
            ProductBLL.ChangeProductCollectCount(productCollect.ProductID, ChangeAction.Plus);
            return productCollect.ID;
        }

        public static void DeleteProductCollect(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadProductCollectIDList(strID, userID);
            ProductBLL.ChangeProductCollectCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteProductCollect(strID, userID);
        }

        public static ProductCollectInfo ReadProductCollect(int id, int userID)
        {
            return dal.ReadProductCollect(id, userID);
        }

        public static ProductCollectInfo ReadProductCollectByProductID(int productID, int userID)
        {
            return dal.ReadProductCollectByProductID(productID, userID);
        }

        public static List<ProductCollectInfo> ReadProductCollectList(int currentPage, int pageSize, ref int count, int userID)
        {
            return dal.ReadProductCollectList(currentPage, pageSize, ref count, userID);
        }

        public static void UpdateProductCollect(ProductCollectInfo productCollect)
        {
            dal.UpdateProductCollect(productCollect);
        }
    }
}

