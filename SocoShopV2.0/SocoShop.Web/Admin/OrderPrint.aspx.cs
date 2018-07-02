namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Web;

    public partial class OrderPrint : AdminBasePage
    {
        protected string orderHtml = string.Empty;

        private void ExcelPrint(OrderInfo order, List<OrderDetailInfo> orderDetailList)
        {
            try
            {
                string templetFilePath = base.Server.MapPath("/Admin/Print/Template.xls");
                string path = "/Admin/Print/Order/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + Guid.NewGuid().ToString() + ".xls";
                string outputFilePath = base.Server.MapPath(path);
                OrderExcelHelper helper = new OrderExcelHelper(templetFilePath, outputFilePath);
                helper.Dt = this.ReadDataTable(orderDetailList);
                helper.CellParameters = this.ReadCellParameters(order);
                helper.Rows = 14;
                helper.Left = 1;
                helper.Top = 8;
                helper.DataTableToExcel();
                ResponseHelper.Redirect("http://" + HttpContext.Current.Request.ServerVariables["Http_Host"] + path.Replace(@"\", "/"));
            }
            catch (Exception exception)
            {
                ExceptionHelper.ProcessException(exception, true);
            }
        }

        private void HtmlPrint(OrderInfo order, List<OrderDetailInfo> orderDetailList)
        {
            using (StreamReader reader = new StreamReader(ServerHelper.MapPath("/Admin/Print/Template.htm")))
            {
                this.orderHtml = reader.ReadToEnd();
            }
            this.orderHtml = this.orderHtml.Replace("<$RegionID$>", RegionBLL.RegionNameList(order.RegionID));
            this.orderHtml = this.orderHtml.Replace("<$ShippingName$>", ShippingBLL.ReadShippingCache(order.ShippingID).Name);
            this.orderHtml = this.orderHtml.Replace("<$PrintTime$>", RequestHelper.DateNow.ToString("yyyy-MM-dd"));
            this.orderHtml = this.orderHtml.Replace("<$ActionUser$>", Cookies.Admin.GetAdminName(false));
            this.orderHtml = this.orderHtml.Replace("<$NoPayMoney$>", OrderBLL.ReadNoPayMoney(order).ToString());
            PropertyInfo[] properties = typeof(OrderInfo).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                this.orderHtml = this.orderHtml.Replace("<$" + info.Name + "$>", info.GetValue(order, null).ToString());
            }
            string newValue = string.Empty;
            int num = 1;
            foreach (OrderDetailInfo info2 in orderDetailList)
            {
                newValue = newValue + "<tr align=\"middle\">";
                newValue = newValue + "<td>" + num.ToString() + "</td>";
                newValue = newValue + "<td align=\"left\">" + info2.ProductName + "</td>";
                object obj2 = newValue;
                newValue = string.Concat(new object[] { obj2, "<td>", info2.BuyCount, "</td>" });
                newValue = newValue + "<td>" + info2.ProductPrice.ToString("n") + "</td>";
                newValue = newValue + "<td>" + ((info2.BuyCount * info2.ProductPrice)).ToString("n") + "</td>";
                newValue = newValue + "</tr>";
                num++;
            }
            this.orderHtml = this.orderHtml.Replace("<$OrderDetailList$>", newValue);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int queryString = RequestHelper.GetQueryString<int>("OrderID");
            string str = RequestHelper.GetQueryString<string>("Action");
            if (queryString > 0 && str != string.Empty)
            {
                OrderInfo order = OrderBLL.ReadOrder(queryString, 0);
                List<OrderDetailInfo> orderDetailList = OrderDetailBLL.ReadOrderDetailByOrder(queryString);
                string str2 = str;
                if (str2 != null)
                {
                    if (!(str2 == "Html"))
                    {
                        if (str2 == "Excel") this.ExcelPrint(order, orderDetailList);
                    }
                    else
                        this.HtmlPrint(order, orderDetailList);
                }
            }
        }

        private Dictionary<int[], string> ReadCellParameters(OrderInfo order)
        {
            Dictionary<int[], string> dictionary = new Dictionary<int[], string>();
            dictionary.Add(new int[] { 2, 2 }, order.OrderNumber);
            dictionary.Add(new int[] { 2, 4 }, order.UserName);
            dictionary.Add(new int[] { 2, 6 }, order.AddDate.ToString());
            dictionary.Add(new int[] { 3, 2 }, order.PayName);
            dictionary.Add(new int[] { 3, 4 }, ShippingBLL.ReadShippingCache(order.ShippingID).Name);
            dictionary.Add(new int[] { 3, 6 }, order.ShippingNumber);
            dictionary.Add(new int[] { 4, 2 }, order.Consignee);
            dictionary.Add(new int[] { 4, 4 }, order.ZipCode);
            dictionary.Add(new int[] { 4, 6 }, order.Tel + " " + order.Mobile);
            dictionary.Add(new int[] { 5, 2 }, "[" + RegionBLL.RegionNameList(order.RegionID) + "] " + order.Address);
            dictionary.Add(new int[] { 6, 2 }, order.UserMessage);
            dictionary.Add(new int[] { 0x16, 1 }, string.Concat(new object[] { "产品金额：", order.ProductMoney, " 元 - 优惠金额：", order.FavorableMoney, " 元 + 物流费用：", order.ShippingMoney, " 元 + 其它费用：", order.OtherMoney, " 元 -余额：", order.Balance, "元 -优惠券：", order.CouponMoney, " 元" }));
            dictionary.Add(new int[] { 0x17, 1 }, "应付金额：" + OrderBLL.ReadNoPayMoney(order).ToString() + " 元");
            dictionary.Add(new int[] { 0x18, 1 }, "打印时间：" + RequestHelper.DateNow.ToString("yyyy-MM-dd") + "  操作者：" + Cookies.Admin.GetAdminName(false));
            return dictionary;
        }

        private DataTable ReadDataTable(List<OrderDetailInfo> orderDetailList)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Index", Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("ProductName", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("BuyCount", Type.GetType("System.Int32")));
            table.Columns.Add(new DataColumn("ProductPrice", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("TotalProductPrice", Type.GetType("System.String")));
            int num = 0;
            foreach (OrderDetailInfo info in orderDetailList)
            {
                num++;
                DataRow row = table.NewRow();
                row["Index"] = num;
                row["ProductName"] = info.ProductName;
                row["BuyCount"] = info.BuyCount;
                row["ProductPrice"] = info.ProductPrice.ToString("n");
                row["TotalProductPrice"] = (info.BuyCount * info.ProductPrice).ToString("n");
                table.Rows.Add(row);
            }
            return table;
        }
    }
}

