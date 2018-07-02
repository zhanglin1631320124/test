namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class ProductReplyBLL
    {
        private static readonly IProductReply dal = FactoryHelper.Instance<IProductReply>(Global.DataProvider, "ProductReplyDAL");

        public static int AddProductReply(ProductReplyInfo productReply)
        {
            productReply.ID = dal.AddProductReply(productReply);
            ProductCommentBLL.ChangeProductCommentCount(productReply.CommentID, ChangeAction.Plus);
            return productReply.ID;
        }

        public static void DeleteProductReply(string strID, int userID)
        {
            ProductCommentBLL.ChangeProductCommentCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteProductReply(strID, userID);
        }

        public static void DeleteProductReplyByCommentID(string strCommentID)
        {
            dal.DeleteProductReplyByCommentID(strCommentID);
        }

        public static void DeleteProductReplyByProductID(string strProductID)
        {
            dal.DeleteProductReplyByProductID(strProductID);
        }

        public static ProductReplyInfo ReadProductReply(int id, int userID)
        {
            return dal.ReadProductReply(id, userID);
        }

        public static List<ProductReplyInfo> ReadProductReplyList(int currentPage, int pageSize, ref int count, int userID)
        {
            return dal.ReadProductReplyList(currentPage, pageSize, ref count, userID);
        }

        public static List<ProductReplyInfo> ReadProductReplyList(int commentID, int currentPage, int pageSize, ref int count, int userID)
        {
            return dal.ReadProductReplyList(commentID, currentPage, pageSize, ref count, userID);
        }

        public static void UpdateProductReply(ProductReplyInfo productReply)
        {
            ProductReplyInfo info = ReadProductReply(productReply.ID, 0);
            dal.UpdateProductReply(productReply);
            if (productReply.CommentID != info.CommentID)
            {
                ProductCommentBLL.ChangeProductCommentCount(info.CommentID, ChangeAction.Minus);
                ProductCommentBLL.ChangeProductCommentCount(productReply.CommentID, ChangeAction.Plus);
            }
        }
    }
}

