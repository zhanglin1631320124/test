using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SocoShop.Web
{
    /// <summary>
    /// 用户团购实体模型
    /// </summary>
    public sealed class UserGroupBuyInfo
    {
        private int id;
        private int groupBuyID;
        private DateTime date = DateTime.Now;
        private string iP = string.Empty;
        private int buyCount;
        private int orderID;
        private int userID;
        private string userName = string.Empty;
        private string consignee = string.Empty;
        private string regionID = string.Empty;
        private string address = string.Empty;
        private string zipCode = string.Empty;
        private string tel = string.Empty;
        private string email = string.Empty;
        private string mobile = string.Empty;

        public int ID
        {
            set { this.id = value; }
            get { return this.id; }
        }
        public int GroupBuyID
        {
            set { this.groupBuyID = value; }
            get { return this.groupBuyID; }
        }
        public DateTime Date
        {
            set { this.date = value; }
            get { return this.date; }
        }
        public string IP
        {
            set { this.iP = value; }
            get { return this.iP; }
        }
        public int BuyCount
        {
            set { this.buyCount = value; }
            get { return this.buyCount; }
        }
        public int OrderID
        {
            set { this.orderID = value; }
            get { return this.orderID; }
        }
        public int UserID
        {
            set { this.userID = value; }
            get { return this.userID; }
        }
        public string UserName
        {
            set { this.userName = value; }
            get { return this.userName; }
        }
        public string Consignee
        {
            set { this.consignee = value; }
            get { return this.consignee; }
        }
        public string RegionID
        {
            set { this.regionID = value; }
            get { return this.regionID; }
        }
        public string Address
        {
            set { this.address = value; }
            get { return this.address; }
        }
        public string ZipCode
        {
            set { this.zipCode = value; }
            get { return this.zipCode; }
        }
        public string Tel
        {
            set { this.tel = value; }
            get { return this.tel; }
        }
        public string Email
        {
            set { this.email = value; }
            get { return this.email; }
        }
        public string Mobile
        {
            set { this.mobile = value; }
            get { return this.mobile; }
        }

    }
}