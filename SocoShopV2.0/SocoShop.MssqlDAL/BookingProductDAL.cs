namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class BookingProductDAL : IBookingProduct
    {
        public int AddBookingProduct(BookingProductInfo bookingProduct)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@productName", SqlDbType.NVarChar), new SqlParameter("@relationUser", SqlDbType.NVarChar), new SqlParameter("@email", SqlDbType.NVarChar), new SqlParameter("@tel", SqlDbType.NVarChar), new SqlParameter("@userNote", SqlDbType.NVarChar), new SqlParameter("@bookingDate", SqlDbType.DateTime), new SqlParameter("@bookingIP", SqlDbType.NVarChar), new SqlParameter("@isHandler", SqlDbType.Int), new SqlParameter("@handlerDate", SqlDbType.DateTime), new SqlParameter("@handlerAdminID", SqlDbType.Int), new SqlParameter("@handlerAdminName", SqlDbType.NVarChar), new SqlParameter("@handlerNote", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = bookingProduct.ProductID;
            pt[1].Value = bookingProduct.ProductName;
            pt[2].Value = bookingProduct.RelationUser;
            pt[3].Value = bookingProduct.Email;
            pt[4].Value = bookingProduct.Tel;
            pt[5].Value = bookingProduct.UserNote;
            pt[6].Value = bookingProduct.BookingDate;
            pt[7].Value = bookingProduct.BookingIP;
            pt[8].Value = bookingProduct.IsHandler;
            pt[9].Value = bookingProduct.HandlerDate;
            pt[10].Value = bookingProduct.HandlerAdminID;
            pt[11].Value = bookingProduct.HandlerAdminName;
            pt[12].Value = bookingProduct.HandlerNote;
            pt[13].Value = bookingProduct.UserID;
            pt[14].Value = bookingProduct.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddBookingProduct", pt));
        }

        public void DeleteBookingProduct(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteBookingProduct", pt);
        }

        public void PrepareBookingProductModel(SqlDataReader dr, List<BookingProductInfo> bookingProductList)
        {
            while (dr.Read())
            {
                BookingProductInfo item = new BookingProductInfo();
                item.ID = dr.GetInt32(0);
                item.ProductID = dr.GetInt32(1);
                item.ProductName = dr[2].ToString();
                item.RelationUser = dr[3].ToString();
                item.Email = dr[4].ToString();
                item.Tel = dr[5].ToString();
                item.UserNote = dr[6].ToString();
                item.BookingDate = dr.GetDateTime(7);
                item.BookingIP = dr[8].ToString();
                item.IsHandler = dr.GetInt32(9);
                item.HandlerDate = dr.GetDateTime(10);
                item.HandlerAdminID = dr.GetInt32(11);
                item.HandlerAdminName = dr[12].ToString();
                item.HandlerNote = dr[13].ToString();
                item.UserID = dr.GetInt32(14);
                item.UserName = dr[15].ToString();
                bookingProductList.Add(item);
            }
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, BookingProductSearchInfo bookingProductSearch)
        {
            mssqlCondition.Add("[ProductName]", bookingProductSearch.ProductName, ConditionType.Like);
            mssqlCondition.Add("[RelationUser]", bookingProductSearch.RelationUser, ConditionType.Like);
            mssqlCondition.Add("[Email]", bookingProductSearch.Email, ConditionType.Like);
            mssqlCondition.Add("[Tel]", bookingProductSearch.Tel, ConditionType.Like);
            mssqlCondition.Add("[IsHandler]", bookingProductSearch.IsHandler, ConditionType.Equal);
            mssqlCondition.Add("[UserID]", bookingProductSearch.UserID, ConditionType.Equal);
        }

        public BookingProductInfo ReadBookingProduct(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            BookingProductInfo info = new BookingProductInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadBookingProduct", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.ProductID = reader.GetInt32(1);
                    info.ProductName = reader[2].ToString();
                    info.RelationUser = reader[3].ToString();
                    info.Email = reader[4].ToString();
                    info.Tel = reader[5].ToString();
                    info.UserNote = reader[6].ToString();
                    info.BookingDate = reader.GetDateTime(7);
                    info.BookingIP = reader[8].ToString();
                    info.IsHandler = reader.GetInt32(9);
                    info.HandlerDate = reader.GetDateTime(10);
                    info.HandlerAdminID = reader.GetInt32(11);
                    info.HandlerAdminName = reader[12].ToString();
                    info.HandlerNote = reader[13].ToString();
                    info.UserID = reader.GetInt32(14);
                    info.UserName = reader[15].ToString();
                }
            }
            return info;
        }

        public string ReadBookingProductIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadBookingProductIDList", pt))
            {
                while (reader.Read())
                {
                    if (str == string.Empty)
                        str = reader.GetInt32(0).ToString();
                    else
                        str = str + "," + reader.GetInt32(0).ToString();
                }
            }
            return str;
        }

        public List<BookingProductInfo> SearchBookingProductList(BookingProductSearchInfo bookingProductSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, bookingProductSearch);
            List<BookingProductInfo> bookingProductList = new List<BookingProductInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchBookingProductList", pt))
            {
                this.PrepareBookingProductModel(reader, bookingProductList);
            }
            return bookingProductList;
        }

        public List<BookingProductInfo> SearchBookingProductList(int currentPage, int pageSize, BookingProductSearchInfo bookingProductSearch, ref int count)
        {
            List<BookingProductInfo> bookingProductList = new List<BookingProductInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "BookingProduct";
            class2.Fields = "[ID],[ProductID],[ProductName],[RelationUser],[Email],[Tel],[UserNote],[BookingDate],[BookingIP],[IsHandler],[HandlerDate],[HandlerAdminID],[HandlerAdminName],[HandlerNote],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, bookingProductSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareBookingProductModel(reader, bookingProductList);
            }
            return bookingProductList;
        }

        public void UpdateBookingProduct(BookingProductInfo bookingProduct)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@isHandler", SqlDbType.Int), new SqlParameter("@handlerDate", SqlDbType.DateTime), new SqlParameter("@handlerAdminID", SqlDbType.Int), new SqlParameter("@handlerAdminName", SqlDbType.NVarChar), new SqlParameter("@handlerNote", SqlDbType.NVarChar) };
            pt[0].Value = bookingProduct.ID;
            pt[1].Value = bookingProduct.IsHandler;
            pt[2].Value = bookingProduct.HandlerDate;
            pt[3].Value = bookingProduct.HandlerAdminID;
            pt[4].Value = bookingProduct.HandlerAdminName;
            pt[5].Value = bookingProduct.HandlerNote;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateBookingProduct", pt);
        }
    }
}

