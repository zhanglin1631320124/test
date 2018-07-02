namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IProductComment
    {
        int AddProductComment(ProductCommentInfo productComment);
        void ChangeProductCommentAgainstCount(string strID, ChangeAction action);
        void ChangeProductCommentCount(int id, ChangeAction action);
        void ChangeProductCommentCountByGeneral(string strID, ChangeAction action);
        void ChangeProductCommentStatus(string strID, int status);
        void ChangeProductCommentSupportCount(string strID, ChangeAction action);
        void DeleteProductComment(string strID, int userID);
        void DeleteProductCommentByProductID(string strProductID);
        ProductCommentInfo ReadProductComment(int id, int userID);
        string ReadProductCommentIDList(string strID, int userID);
        List<ProductCommentInfo> SearchProductCommentInnerList(int currentPage, int pageSize, ProductCommentSearchInfo productComment, ref int count);
        List<ProductCommentInfo> SearchProductCommentList(ProductCommentSearchInfo productComment);
        List<ProductCommentInfo> SearchProductCommentList(int currentPage, int pageSize, ProductCommentSearchInfo productComment, ref int count);
        void UpdateProductComment(ProductCommentInfo productComment);
    }
}

