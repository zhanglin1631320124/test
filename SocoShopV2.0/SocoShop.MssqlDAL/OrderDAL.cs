namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class OrderDAL : IOrder
    {
        public int AddOrder(OrderInfo order)
        {
            SqlParameter[] pt = new SqlParameter[] { 
                new SqlParameter("@orderNumber", SqlDbType.NVarChar), new SqlParameter("@isActivity", SqlDbType.Int), new SqlParameter("@orderStatus", SqlDbType.Int), new SqlParameter("@orderNote", SqlDbType.NVarChar), new SqlParameter("@productMoney", SqlDbType.Decimal), new SqlParameter("@balance", SqlDbType.Decimal), new SqlParameter("@favorableMoney", SqlDbType.Decimal), new SqlParameter("@otherMoney", SqlDbType.Decimal), new SqlParameter("@couponMoney", SqlDbType.Decimal), new SqlParameter("@consignee", SqlDbType.NVarChar), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@address", SqlDbType.NVarChar), new SqlParameter("@zipCode", SqlDbType.NVarChar), new SqlParameter("@tel", SqlDbType.NVarChar), new SqlParameter("@email", SqlDbType.NVarChar), new SqlParameter("@mobile", SqlDbType.NVarChar), 
                new SqlParameter("@shippingID", SqlDbType.Int), new SqlParameter("@shippingDate", SqlDbType.DateTime), new SqlParameter("@shippingNumber", SqlDbType.NVarChar), new SqlParameter("@shippingMoney", SqlDbType.Decimal), new SqlParameter("@payKey", SqlDbType.NVarChar), new SqlParameter("@payName", SqlDbType.NVarChar), new SqlParameter("@payDate", SqlDbType.DateTime), new SqlParameter("@isRefund", SqlDbType.Int), new SqlParameter("@favorableActivityID", SqlDbType.Int), new SqlParameter("@giftID", SqlDbType.Int), new SqlParameter("@invoiceTitle", SqlDbType.NVarChar), new SqlParameter("@invoiceContent", SqlDbType.NVarChar), new SqlParameter("@userMessage", SqlDbType.NVarChar), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@iP", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int), 
                new SqlParameter("@userName", SqlDbType.NVarChar)
             };
            pt[0].Value = order.OrderNumber;
            pt[1].Value = order.IsActivity;
            pt[2].Value = order.OrderStatus;
            pt[3].Value = order.OrderNote;
            pt[4].Value = order.ProductMoney;
            pt[5].Value = order.Balance;
            pt[6].Value = order.FavorableMoney;
            pt[7].Value = order.OtherMoney;
            pt[8].Value = order.CouponMoney;
            pt[9].Value = order.Consignee;
            pt[10].Value = order.RegionID;
            pt[11].Value = order.Address;
            pt[12].Value = order.ZipCode;
            pt[13].Value = order.Tel;
            pt[14].Value = order.Email;
            pt[15].Value = order.Mobile;
            pt[0x10].Value = order.ShippingID;
            pt[0x11].Value = order.ShippingDate;
            pt[0x12].Value = order.ShippingNumber;
            pt[0x13].Value = order.ShippingMoney;
            pt[20].Value = order.PayKey;
            pt[0x15].Value = order.PayName;
            pt[0x16].Value = order.PayDate;
            pt[0x17].Value = order.IsRefund;
            pt[0x18].Value = order.FavorableActivityID;
            pt[0x19].Value = order.GiftID;
            pt[0x1a].Value = order.InvoiceTitle;
            pt[0x1b].Value = order.InvoiceContent;
            pt[0x1c].Value = order.UserMessage;
            pt[0x1d].Value = order.AddDate;
            pt[30].Value = order.IP;
            pt[0x1f].Value = order.UserID;
            pt[0x20].Value = order.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddOrder", pt));
        }

        public void DeleteOrder(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteOrder", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, OrderSearchInfo orderSearch)
        {
            mssqlCondition.Add("[OrderNumber]", orderSearch.OrderNumber, ConditionType.Like);
            mssqlCondition.Add("[OrderStatus]", orderSearch.OrderStatus, ConditionType.Equal);
            mssqlCondition.Add("[Consignee]", orderSearch.Consignee, ConditionType.Like);
            mssqlCondition.Add("[RegionID]", orderSearch.RegionID, ConditionType.Like);
            mssqlCondition.Add("[ShippingID]", orderSearch.ShippingID, ConditionType.Equal);
            mssqlCondition.Add("[AddDate]", orderSearch.StartAddDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[AddDate]", orderSearch.EndAddDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[UserID]", orderSearch.UserID, ConditionType.Equal);
            mssqlCondition.Add("[UserName]", orderSearch.UserName, ConditionType.Like);
        }

        public void PrepareOrderModel(SqlDataReader dr, List<OrderInfo> orderList)
        {
            while (dr.Read())
            {
                OrderInfo item = new OrderInfo();
                item.ID = dr.GetInt32(0);
                item.OrderNumber = dr[1].ToString();
                item.IsActivity = dr.GetInt32(2);
                item.OrderStatus = dr.GetInt32(3);
                item.OrderNote = dr[4].ToString();
                item.ProductMoney = dr.GetDecimal(5);
                item.Balance = dr.GetDecimal(6);
                item.FavorableMoney = dr.GetDecimal(7);
                item.OtherMoney = dr.GetDecimal(8);
                item.CouponMoney = dr.GetDecimal(9);
                item.Consignee = dr[10].ToString();
                item.RegionID = dr[11].ToString();
                item.Address = dr[12].ToString();
                item.ZipCode = dr[13].ToString();
                item.Tel = dr[14].ToString();
                item.Email = dr[15].ToString();
                item.Mobile = dr[0x10].ToString();
                item.ShippingID = dr.GetInt32(0x11);
                item.ShippingDate = dr.GetDateTime(0x12);
                item.ShippingNumber = dr[0x13].ToString();
                item.ShippingMoney = dr.GetDecimal(20);
                item.PayKey = dr[0x15].ToString();
                item.PayName = dr[0x16].ToString();
                item.PayDate = dr.GetDateTime(0x17);
                item.IsRefund = dr.GetInt32(0x18);
                item.FavorableActivityID = dr.GetInt32(0x19);
                item.GiftID = dr.GetInt32(0x1a);
                item.InvoiceTitle = dr[0x1b].ToString();
                item.InvoiceContent = dr[0x1c].ToString();
                item.UserMessage = dr[0x1d].ToString();
                item.AddDate = dr.GetDateTime(30);
                item.IP = dr[0x1f].ToString();
                item.UserID = dr.GetInt32(0x20);
                item.UserName = dr[0x21].ToString();
                orderList.Add(item);
            }
        }

        public OrderInfo ReadOrder(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            OrderInfo info = new OrderInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrder", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.OrderNumber = reader[1].ToString();
                    info.IsActivity = reader.GetInt32(2);
                    info.OrderStatus = reader.GetInt32(3);
                    info.OrderNote = reader[4].ToString();
                    info.ProductMoney = reader.GetDecimal(5);
                    info.Balance = reader.GetDecimal(6);
                    info.FavorableMoney = reader.GetDecimal(7);
                    info.OtherMoney = reader.GetDecimal(8);
                    info.CouponMoney = reader.GetDecimal(9);
                    info.Consignee = reader[10].ToString();
                    info.RegionID = reader[11].ToString();
                    info.Address = reader[12].ToString();
                    info.ZipCode = reader[13].ToString();
                    info.Tel = reader[14].ToString();
                    info.Email = reader[15].ToString();
                    info.Mobile = reader[0x10].ToString();
                    info.ShippingID = reader.GetInt32(0x11);
                    info.ShippingDate = reader.GetDateTime(0x12);
                    info.ShippingNumber = reader[0x13].ToString();
                    info.ShippingMoney = reader.GetDecimal(20);
                    info.PayKey = reader[0x15].ToString();
                    info.PayName = reader[0x16].ToString();
                    info.PayDate = reader.GetDateTime(0x17);
                    info.IsRefund = reader.GetInt32(0x18);
                    info.FavorableActivityID = reader.GetInt32(0x19);
                    info.GiftID = reader.GetInt32(0x1a);
                    info.InvoiceTitle = reader[0x1b].ToString();
                    info.InvoiceContent = reader[0x1c].ToString();
                    info.UserMessage = reader[0x1d].ToString();
                    info.AddDate = reader.GetDateTime(30);
                    info.IP = reader[0x1f].ToString();
                    info.UserID = reader.GetInt32(0x20);
                    info.UserName = reader[0x21].ToString();
                }
            }
            return info;
        }

        public OrderInfo ReadOrderByNumber(string orderNumber, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderNumber", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = orderNumber;
            pt[1].Value = userID;
            OrderInfo info = new OrderInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrderByNumber", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.OrderNumber = reader[1].ToString();
                    info.IsActivity = reader.GetInt32(2);
                    info.OrderStatus = reader.GetInt32(3);
                    info.OrderNote = reader[4].ToString();
                    info.ProductMoney = reader.GetDecimal(5);
                    info.Balance = reader.GetDecimal(6);
                    info.FavorableMoney = reader.GetDecimal(7);
                    info.OtherMoney = reader.GetDecimal(8);
                    info.CouponMoney = reader.GetDecimal(9);
                    info.Consignee = reader[10].ToString();
                    info.RegionID = reader[11].ToString();
                    info.Address = reader[12].ToString();
                    info.ZipCode = reader[13].ToString();
                    info.Tel = reader[14].ToString();
                    info.Email = reader[15].ToString();
                    info.Mobile = reader[0x10].ToString();
                    info.ShippingID = reader.GetInt32(0x11);
                    info.ShippingDate = reader.GetDateTime(0x12);
                    info.ShippingNumber = reader[0x13].ToString();
                    info.ShippingMoney = reader.GetDecimal(20);
                    info.PayKey = reader[0x15].ToString();
                    info.PayName = reader[0x16].ToString();
                    info.PayDate = reader.GetDateTime(0x17);
                    info.IsRefund = reader.GetInt32(0x18);
                    info.FavorableActivityID = reader.GetInt32(0x19);
                    info.GiftID = reader.GetInt32(0x1a);
                    info.InvoiceTitle = reader[0x1b].ToString();
                    info.InvoiceContent = reader[0x1c].ToString();
                    info.UserMessage = reader[0x1d].ToString();
                    info.AddDate = reader.GetDateTime(30);
                    info.IP = reader[0x1f].ToString();
                    info.UserID = reader.GetInt32(0x20);
                    info.UserName = reader[0x21].ToString();
                }
            }
            return info;
        }

        public string ReadOrderIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrderIDList", pt))
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

        public List<OrderInfo> ReadPreNextOrder(int id)
        {
            List<OrderInfo> list = new List<OrderInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadPreNextOrder", pt))
            {
                while (reader.Read())
                {
                    OrderInfo item = new OrderInfo();
                    item.ID = reader.GetInt32(0);
                    list.Add(item);
                }
            }
            return list;
        }

        public List<OrderInfo> SearchOrderList(OrderSearchInfo orderSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, orderSearch);
            List<OrderInfo> orderList = new List<OrderInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchOrderList", pt))
            {
                this.PrepareOrderModel(reader, orderList);
            }
            return orderList;
        }

        public List<OrderInfo> SearchOrderList(int currentPage, int pageSize, OrderSearchInfo orderSearch, ref int count)
        {
            List<OrderInfo> orderList = new List<OrderInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Order";
            class2.Fields = "[ID],[OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, orderSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareOrderModel(reader, orderList);
            }
            return orderList;
        }

        public DataTable StatisticsOrderArea(OrderSearchInfo orderSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, orderSearch);
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "StatisticsOrderArea", pt);
        }

        public DataTable StatisticsOrderCount(OrderSearchInfo orderSearch, DateType dateType)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, orderSearch);
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar), new SqlParameter("@dateType", SqlDbType.Int) };
            pt[0].Value = mssqlCondition.ToString();
            pt[1].Value = (int) dateType;
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "StatisticsOrderCount", pt);
        }

        public DataTable StatisticsOrderStatus(OrderSearchInfo orderSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, orderSearch);
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "StatisticsOrderStatus", pt);
        }

        public DataTable StatisticsSaleStop(string productIDList)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productIDList", SqlDbType.NVarChar) };
            pt[0].Value = productIDList;
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "StatisticsSaleStop", pt);
        }

        public DataTable StatisticsSaleTotal(OrderSearchInfo orderSearch, DateType dateType)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, orderSearch);
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar), new SqlParameter("@dateType", SqlDbType.Int) };
            pt[0].Value = mssqlCondition.ToString();
            pt[1].Value = (int) dateType;
            return ShopMssqlHelper.ExecuteDataTable(ShopMssqlHelper.TablePrefix + "StatisticsSaleTotal", pt);
        }

        public void UpdateOrder(OrderInfo order)
        {
            SqlParameter[] pt = new SqlParameter[] { 
                new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@orderStatus", SqlDbType.Int), new SqlParameter("@orderNote", SqlDbType.NVarChar), new SqlParameter("@productMoney", SqlDbType.Decimal), new SqlParameter("@balance", SqlDbType.Decimal), new SqlParameter("@favorableMoney", SqlDbType.Decimal), new SqlParameter("@otherMoney", SqlDbType.Decimal), new SqlParameter("@couponMoney", SqlDbType.Decimal), new SqlParameter("@consignee", SqlDbType.NVarChar), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@address", SqlDbType.NVarChar), new SqlParameter("@zipCode", SqlDbType.NVarChar), new SqlParameter("@tel", SqlDbType.NVarChar), new SqlParameter("@email", SqlDbType.NVarChar), new SqlParameter("@mobile", SqlDbType.NVarChar), new SqlParameter("@shippingID", SqlDbType.Int), 
                new SqlParameter("@shippingDate", SqlDbType.DateTime), new SqlParameter("@shippingNumber", SqlDbType.NVarChar), new SqlParameter("@shippingMoney", SqlDbType.Decimal), new SqlParameter("@payKey", SqlDbType.NVarChar), new SqlParameter("@payName", SqlDbType.NVarChar), new SqlParameter("@payDate", SqlDbType.DateTime), new SqlParameter("@isRefund", SqlDbType.Int), new SqlParameter("@favorableActivityID", SqlDbType.Int), new SqlParameter("@giftID", SqlDbType.Int), new SqlParameter("@invoiceTitle", SqlDbType.NVarChar), new SqlParameter("@invoiceContent", SqlDbType.NVarChar), new SqlParameter("@userMessage", SqlDbType.NVarChar)
             };
            pt[0].Value = order.ID;
            pt[1].Value = order.OrderStatus;
            pt[2].Value = order.OrderNote;
            pt[3].Value = order.ProductMoney;
            pt[4].Value = order.Balance;
            pt[5].Value = order.FavorableMoney;
            pt[6].Value = order.OtherMoney;
            pt[7].Value = order.CouponMoney;
            pt[8].Value = order.Consignee;
            pt[9].Value = order.RegionID;
            pt[10].Value = order.Address;
            pt[11].Value = order.ZipCode;
            pt[12].Value = order.Tel;
            pt[13].Value = order.Email;
            pt[14].Value = order.Mobile;
            pt[15].Value = order.ShippingID;
            pt[0x10].Value = order.ShippingDate;
            pt[0x11].Value = order.ShippingNumber;
            pt[0x12].Value = order.ShippingMoney;
            pt[0x13].Value = order.PayKey;
            pt[20].Value = order.PayName;
            pt[0x15].Value = order.PayDate;
            pt[0x16].Value = order.IsRefund;
            pt[0x17].Value = order.FavorableActivityID;
            pt[0x18].Value = order.GiftID;
            pt[0x19].Value = order.InvoiceTitle;
            pt[0x1a].Value = order.InvoiceContent;
            pt[0x1b].Value = order.UserMessage;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateOrder", pt);
        }
    }
}

