namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;

    public class OrderAjax : UserAjaxBasePage
    {
        private void OrderOperate()
        {
            string content = string.Empty;
            int queryString = RequestHelper.GetQueryString<int>("OrderID");
            int num2 = RequestHelper.GetQueryString<int>("OrderStatus");
            switch (num2)
            {
                case 1:
                case 2:
                case 5:
                {
                    OrderInfo order = OrderBLL.ReadOrder(queryString, base.UserID);
                    if (order.ID == 0)
                        content = "不是属于当前用户的订单";
                    else
                    {
                        int orderStatus;
                        if (num2 == 2 || num2 == 1)
                        {
                            orderStatus = order.OrderStatus;
                            order.OrderStatus = 3;
                            ProductBLL.ChangeProductOrderCountByOrder(queryString, ChangeAction.Minus);
                            OrderBLL.UserUpdateOrderAddAction(order, "用户取消订单", 3, orderStatus);
                        }
                        else
                        {
                            int point = OrderBLL.ReadOrderSendPoint(order.ID);
                            if (point > 0) UserAccountRecordBLL.AddUserAccountRecord(0M, point, ShopLanguage.ReadLanguage("OrderReceived").Replace("$OrderNumber", order.OrderNumber), order.UserID, order.UserName);
                            orderStatus = order.OrderStatus;
                            order.OrderStatus = 6;
                            OrderBLL.UserUpdateOrderAddAction(order, "用户确认收货", 5, orderStatus);
                        }
                    }
                    break;
                }
                default:
                    content = "订单状态错误";
                    break;
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != null && queryString == "OrderOperate") this.OrderOperate();
        }
    }
}

