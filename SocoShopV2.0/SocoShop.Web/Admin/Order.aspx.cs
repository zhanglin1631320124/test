namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Order : AdminBasePage
    {
        protected int intOrderStatus = 0;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteOrder", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                OrderBLL.DeleteOrder(intsForm, 0);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Order"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadOrder", PowerCheckType.Single);
                this.OrderNumber.Text = RequestHelper.GetQueryString<string>("OrderNumber");
                this.OrderStatus.Text = RequestHelper.GetQueryString<string>("OrderStatus");
                this.Consignee.Text = RequestHelper.GetQueryString<string>("Consignee");
                this.StartAddDate.Text = RequestHelper.GetQueryString<string>("StartAddDate");
                this.EndAddDate.Text = RequestHelper.GetQueryString<string>("EndAddDate");
                OrderSearchInfo order = new OrderSearchInfo();
                order.OrderNumber = RequestHelper.GetQueryString<string>("OrderNumber");
                order.OrderStatus = RequestHelper.GetQueryString<int>("OrderStatus");
                order.Consignee = RequestHelper.GetQueryString<string>("Consignee");
                order.StartAddDate = RequestHelper.GetQueryString<DateTime>("StartAddDate");
                order.EndAddDate = RequestHelper.GetQueryString<DateTime>("EndAddDate");
                base.BindControl(OrderBLL.SearchOrderList(base.CurrentPage, base.PageSize, order, ref this.Count), this.RecordList, this.MyPager);
                this.intOrderStatus = order.OrderStatus;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((("Order.aspx?Action=search&" + "OrderNumber=" + this.OrderNumber.Text + "&") + "OrderStatus=" + this.OrderStatus.Text + "&") + "Consignee=" + this.Consignee.Text + "&") + "StartAddDate=" + this.StartAddDate.Text + "&") + "EndAddDate=" + this.EndAddDate.Text);
        }
    }
}

