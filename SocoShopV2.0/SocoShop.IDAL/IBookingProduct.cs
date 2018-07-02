namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IBookingProduct
    {
        int AddBookingProduct(BookingProductInfo bookingProduct);
        void DeleteBookingProduct(string strID, int userID);
        BookingProductInfo ReadBookingProduct(int id, int userID);
        string ReadBookingProductIDList(string strID, int userID);
        List<BookingProductInfo> SearchBookingProductList(BookingProductSearchInfo bookingProduct);
        List<BookingProductInfo> SearchBookingProductList(int currentPage, int pageSize, BookingProductSearchInfo bookingProduct, ref int count);
        void UpdateBookingProduct(BookingProductInfo bookingProduct);
    }
}

