namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class BookingProductBLL
    {
        private static readonly IBookingProduct dal = FactoryHelper.Instance<IBookingProduct>(Global.DataProvider, "BookingProductDAL");

        public static int AddBookingProduct(BookingProductInfo bookingProduct)
        {
            bookingProduct.ID = dal.AddBookingProduct(bookingProduct);
            return bookingProduct.ID;
        }

        public static void DeleteBookingProduct(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadBookingProductIDList(strID, userID);
            dal.DeleteBookingProduct(strID, userID);
        }

        public static BookingProductInfo ReadBookingProduct(int id, int userID)
        {
            return dal.ReadBookingProduct(id, userID);
        }

        public static List<BookingProductInfo> SearchBookingProductList(BookingProductSearchInfo bookingProduct)
        {
            return dal.SearchBookingProductList(bookingProduct);
        }

        public static List<BookingProductInfo> SearchBookingProductList(int currentPage, int pageSize, BookingProductSearchInfo bookingProduct, ref int count)
        {
            return dal.SearchBookingProductList(currentPage, pageSize, bookingProduct, ref count);
        }

        public static void UpdateBookingProduct(BookingProductInfo bookingProduct)
        {
            dal.UpdateBookingProduct(bookingProduct);
        }
    }
}

