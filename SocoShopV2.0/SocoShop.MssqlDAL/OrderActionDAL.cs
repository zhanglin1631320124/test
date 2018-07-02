namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class OrderActionDAL : IOrderAction
    {
        public int AddOrderAction(OrderActionInfo orderAction)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@orderOperate", SqlDbType.Int), new SqlParameter("@startOrderStatus", SqlDbType.Int), new SqlParameter("@endOrderStatus", SqlDbType.Int), new SqlParameter("@note", SqlDbType.NVarChar), new SqlParameter("@iP", SqlDbType.NVarChar), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@adminID", SqlDbType.Int), new SqlParameter("@adminName", SqlDbType.NVarChar) };
            pt[0].Value = orderAction.OrderID;
            pt[1].Value = orderAction.OrderOperate;
            pt[2].Value = orderAction.StartOrderStatus;
            pt[3].Value = orderAction.EndOrderStatus;
            pt[4].Value = orderAction.Note;
            pt[5].Value = orderAction.IP;
            pt[6].Value = orderAction.Date;
            pt[7].Value = orderAction.AdminID;
            pt[8].Value = orderAction.AdminName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddOrderAction", pt));
        }

        public void DeleteOrderAction(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteOrderAction", pt);
        }

        public void DeleteOrderActionByOrderID(string strOrderID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strOrderID", SqlDbType.NVarChar) };
            pt[0].Value = strOrderID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteOrderActionByOrderID", pt);
        }

        public void PrepareOrderActionModel(SqlDataReader dr, List<OrderActionInfo> orderActionList)
        {
            while (dr.Read())
            {
                OrderActionInfo item = new OrderActionInfo();
                item.ID = dr.GetInt32(0);
                item.OrderID = dr.GetInt32(1);
                item.OrderOperate = dr.GetInt32(2);
                item.StartOrderStatus = dr.GetInt32(3);
                item.EndOrderStatus = dr.GetInt32(4);
                item.Note = dr[5].ToString();
                item.IP = dr[6].ToString();
                item.Date = dr.GetDateTime(7);
                item.AdminID = dr.GetInt32(8);
                item.AdminName = dr[9].ToString();
                orderActionList.Add(item);
            }
        }

        public OrderActionInfo ReadLatestOrderAction(int orderID, int endOrderStatus)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@endOrderStatus", SqlDbType.Int) };
            pt[0].Value = orderID;
            pt[1].Value = endOrderStatus;
            OrderActionInfo info = new OrderActionInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadLatestOrderAction", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.OrderID = reader.GetInt32(1);
                    info.OrderOperate = reader.GetInt32(2);
                    info.StartOrderStatus = reader.GetInt32(3);
                    info.EndOrderStatus = reader.GetInt32(4);
                    info.Note = reader[5].ToString();
                    info.IP = reader[6].ToString();
                    info.Date = reader.GetDateTime(7);
                    info.AdminID = reader.GetInt32(8);
                    info.AdminName = reader[9].ToString();
                }
            }
            return info;
        }

        public OrderActionInfo ReadOrderAction(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            OrderActionInfo info = new OrderActionInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrderAction", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.OrderID = reader.GetInt32(1);
                    info.OrderOperate = reader.GetInt32(2);
                    info.StartOrderStatus = reader.GetInt32(3);
                    info.EndOrderStatus = reader.GetInt32(4);
                    info.Note = reader[5].ToString();
                    info.IP = reader[6].ToString();
                    info.Date = reader.GetDateTime(7);
                    info.AdminID = reader.GetInt32(8);
                    info.AdminName = reader[9].ToString();
                }
            }
            return info;
        }

        public List<OrderActionInfo> ReadOrderActionByOrder(int orderID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@orderID", SqlDbType.Int) };
            pt[0].Value = orderID;
            List<OrderActionInfo> orderActionList = new List<OrderActionInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadOrderActionByOrder", pt))
            {
                this.PrepareOrderActionModel(reader, orderActionList);
            }
            return orderActionList;
        }
    }
}

