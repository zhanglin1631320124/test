namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class ProductPhotoBLL
    {
        private static readonly IProductPhoto dal = FactoryHelper.Instance<IProductPhoto>(Global.DataProvider, "ProductPhotoDAL");
        public static readonly int TableID = UploadTable.ReadTableID("ProductPhoto");

        public static int AddProductPhoto(ProductPhotoInfo productPhoto)
        {
            productPhoto.ID = dal.AddProductPhoto(productPhoto);
            UploadBLL.UpdateUpload(TableID, productPhoto.ProductID, productPhoto.ID, Cookies.Admin.GetRandomNumber(false));
            ProductBLL.ChangeProductPhotoCount(productPhoto.ProductID, ChangeAction.Plus);
            return productPhoto.ID;
        }

        public static void DeleteProductPhoto(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            ProductBLL.ChangeProductPhotoCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteProductPhoto(strID);
        }

        public static void DeleteProductPhotoByProductID(string strProductID)
        {
            dal.DeleteProductPhotoByProductID(strProductID);
        }

        public static ProductPhotoInfo ReadProductPhoto(int id)
        {
            return dal.ReadProductPhoto(id);
        }

        public static List<ProductPhotoInfo> ReadProductPhotoByProduct(int productID)
        {
            return dal.ReadProductPhotoByProduct(productID);
        }
    }
}

