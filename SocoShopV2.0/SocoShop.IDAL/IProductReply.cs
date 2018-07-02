namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IProductReply
    {
        int AddProductReply(ProductReplyInfo productReply);
        void DeleteProductReply(string strID, int userID);
        void DeleteProductReplyByCommentID(string strCommentID);
        void DeleteProductReplyByProductID(string strProductID);
        ProductReplyInfo ReadProductReply(int id, int userID);
        List<ProductReplyInfo> ReadProductReplyList(int currentPage, int pageSize, ref int count, int userID);
        List<ProductReplyInfo> ReadProductReplyList(int commentID, int currentPage, int pageSize, ref int count, int userID);
        void UpdateProductReply(ProductReplyInfo productReply);
    }
}

