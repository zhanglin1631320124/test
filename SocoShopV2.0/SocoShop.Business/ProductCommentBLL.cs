namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class ProductCommentBLL
    {
        private static readonly IProductComment dal = FactoryHelper.Instance<IProductComment>(Global.DataProvider, "ProductCommentDAL");

        public static int AddProductComment(ProductCommentInfo productComment)
        {
            productComment.ID = dal.AddProductComment(productComment);
            ProductBLL.ChangeProductCommentCountAndRank(productComment.ProductID, productComment.Rank, ChangeAction.Plus);
            return productComment.ID;
        }

        public static void ChangeProductCommentAgainstCount(string strID, ChangeAction action)
        {
            dal.ChangeProductCommentAgainstCount(strID, action);
        }

        public static void ChangeProductCommentCount(int id, ChangeAction action)
        {
            dal.ChangeProductCommentCount(id, action);
        }

        public static void ChangeProductCommentCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeProductCommentCountByGeneral(strID, action);
        }

        public static void ChangeProductCommentStatus(string strID, int status)
        {
            dal.ChangeProductCommentStatus(strID, status);
        }

        public static void ChangeProductCommentSupportCount(string strID, ChangeAction action)
        {
            dal.ChangeProductCommentSupportCount(strID, action);
        }

        public static void DeleteProductComment(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadProductCommentIDList(strID, userID);
            ProductReplyBLL.DeleteProductReplyByCommentID(strID);
            ProductBLL.ChangeProductCommentCountAndRankByGeneral(strID, ChangeAction.Minus);
            dal.DeleteProductComment(strID, userID);
        }

        public static void DeleteProductCommentByProductID(string strProductID)
        {
            ProductReplyBLL.DeleteProductReplyByProductID(strProductID);
            dal.DeleteProductCommentByProductID(strProductID);
        }

        public static string ReadCommentStatus(int commentStatus)
        {
            string str = string.Empty;
            switch (commentStatus)
            {
                case 1:
                    return "未处理";

                case 2:
                    return "显示";

                case 3:
                    return "不显示";
            }
            return str;
        }

        public static ProductCommentInfo ReadProductComment(int id, int userID)
        {
            return dal.ReadProductComment(id, userID);
        }

        public static List<ProductCommentInfo> SearchProductCommentInnerList(int currentPage, int pageSize, ProductCommentSearchInfo productComment, ref int count)
        {
            return dal.SearchProductCommentInnerList(currentPage, pageSize, productComment, ref count);
        }

        public static List<ProductCommentInfo> SearchProductCommentList(ProductCommentSearchInfo productComment)
        {
            return dal.SearchProductCommentList(productComment);
        }

        public static List<ProductCommentInfo> SearchProductCommentList(int currentPage, int pageSize, ProductCommentSearchInfo productComment, ref int count)
        {
            return dal.SearchProductCommentList(currentPage, pageSize, productComment, ref count);
        }

        public static void UpdateProductComment(ProductCommentInfo productComment)
        {
            ProductCommentInfo info = ReadProductComment(productComment.ID, 0);
            dal.UpdateProductComment(productComment);
            if (productComment.ProductID != info.ProductID)
            {
                ProductBLL.ChangeProductCommentCountAndRank(info.ProductID, info.Rank, ChangeAction.Minus);
                ProductBLL.ChangeProductCommentCountAndRank(productComment.ProductID, productComment.Rank, ChangeAction.Plus);
            }
        }
    }
}

