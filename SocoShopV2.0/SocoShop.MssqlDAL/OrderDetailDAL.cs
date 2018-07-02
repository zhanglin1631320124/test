namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class OrderDetailDAL : IOrderDetail
    {
        public int AddOrderDetail(OrderDetailInfo orderDetail)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@productName", SqlDbType.NVarChar), new SqlParameter("@productWeight", SqlDbType.Decimal), new SqlParameter("@sendPoint", SqlDbType.Int), new SqlParameter("@productPrice", SqlDbType.Decimal), new SqlParameter("@buyCount", SqlDbType.Int), new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@randNumber", SqlDbType.NVarChar), new SqlParameter("@giftPackID", SqlDbType.Int) };
            pt[0].Value = orderDetail.OrderID;
            pt[1].Value = orderDetail.ProductID;
            pt[2].Value = orderDetail.ProductName;
            pt[3].Value = orderDetail.ProductWeight;
            pt[4].Value = orderDetail.SendPoint;
            pt[5].Value = orderDetail.ProductPrice;
            pt[6].Value = orderDetail.BuyCount;
            pt[7].Value = orderDetail.FatherID;
            pt[8].Value = orderDetail.RandNumber;
            pt[9].Value = orderDetail.GiftPackID;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddOrderDetail", pt));
        }

        public void ChangeOrderGiftPackBuyCount(int orderID, string randNumber, int buyCount)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@randNumber", SqlDbType.NVarChar), new SqlParameter("@buyCount", SqlDbType.Int) };
            pt[0].Value = orderID;
            pt[1].Value = randNumber;
            pt[2].Value = buyCount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeOrderGiftPackBuyCount", pt);
        }

        public void ChangeOrderProductBuyCount(string strID, int buyCount)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@buyCount", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = buyCount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeOrderProductBuyCount", pt);
        }

        public void DeleteOrderDetail(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteOrderDetail", pt);
        }

        public void DeleteOrderDetailByOrderID(string strOrderID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strOrderID", SqlDbType.NVarChar) };
            pt[0].Value = strOrderID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteOrderDetailByOrderID", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, OrderSearchInfo orderSearch, ProductSearchInfo productSearch)
        {
            mssqlCondition.Add("[OrderNumber]", orderSearch.OrderNumber, ConditionType.Like);
            mssqlCondition.Add("[AddDate]", orderSearch.StartAddDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[AddDate]", orderSearch.EndAddDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[UserName]", orderSearch.UserName, ConditionType.Like);
            mssqlCondition.Add("[Name]", productSearch.Name, ConditionType.Like);
            mssqlCondition.Add("[BrandID]", productSearch.BrandID, ConditionType.Equal);
            mssqlCondition.Add("[ClassID]", productSearch.ClassID, ConditionType.Like);
            mssqlCondition.Add("[ProductID]", productSearch.InProductID, ConditionType.In);
        }

        public void PrepareOrderDetailModel(SqlDataReader dr, List<OrderDetailInfo> orderDetailList)
        {
            while (dr.Read())
            {
                OrderDetailInfo item = new OrderDetailInfo();
                item.ID = dr.GetInt32(0);
                item.OrderID = dr.GetInt32(1);
                item.ProductID = dr.GetInt32(2);
                item.ProductName = dr[3].ToString();
                item.ProductWeight = dr.GetDecimal(4);
                item.SendPoint = dr.GetInt32(5);
                item.ProductPrice = dr.GetDecimal(6);
                item.BuyCount = dr.GetInt32(7);
                item.FatherID = dr.GetInt32(8);
                item.RandNumber = dr[9].ToString();
                item.GiftPackID = dr.GetInt32(10);
                orderDetailList.Add(item);
            }
        }

        public OrderDetailInfo ReadOrderDetail(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            OrderDetailInfo info = new OrderDetailInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrderDetail", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.OrderID = reader.GetInt32(1);
                    info.ProductID = reader.GetInt32(2);
                    info.ProductName = reader[3].ToString();
                    info.ProductWeight = reader.GetDecimal(4);
                    info.SendPoint = reader.GetInt32(5);
                    info.ProductPrice = reader.GetDecimal(6);
                    info.BuyCount = reader.GetInt32(7);
                    info.FatherID = reader.GetInt32(8);
                    info.RandNumber = reader[9].ToString();
                    info.GiftPackID = reader.GetInt32(10);
                }
            }
            return info;
        }

        public List<OrderDetailInfo> ReadOrderDetailByOrder(int orderID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int) };
            pt[0].Value = orderID;
            List<OrderDetailInfo> orderDetailList = new List<OrderDetailInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrderDetailByOrder", pt))
            {
                this.PrepareOrderDetailModel(reader, orderDetailList);
            }
            return orderDetailList;
        }

        public List<OrderDetailInfo> ReadOrderDetailByProductID(int productID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int) };
            pt[0].Value = productID;
            List<OrderDetailInfo> orderDetailList = new List<OrderDetailInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrderDetailByProductID", pt))
            {
                this.PrepareOrderDetailModel(reader, orderDetailList);
            }
            return orderDetailList;
        }

        public DataTable StatisticsSaleDetail(int currentPage, int pageSize, OrderSearchInfo orderSearch, ProductSearchInfo productSearch, ref int count)
        {
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "View_SaleDetail";
            class2.Fields = "[Name],[ClassID],[BrandID],[BuyCount],[Money],[OrderNumber],[AddDate],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[AddDate],[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, orderSearch, productSearch);
            class2.Count = count;
            count = class2.Count;
            return class2.ExecuteDataTable();
        }

        public void UpdateOrderDetail(OrderDetailInfo orderDetail)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@buyCount", SqlDbType.Int) };
            pt[0].Value = orderDetail.ID;
            pt[1].Value = orderDetail.BuyCount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateOrderDetail", pt);
        }
    }
}

