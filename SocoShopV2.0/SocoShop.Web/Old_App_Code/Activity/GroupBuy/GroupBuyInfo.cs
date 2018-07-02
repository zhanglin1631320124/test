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
    /// 团购实体模型
    /// </summary>
    public sealed class GroupBuyInfo
    {
        private int id;
        private string name = string.Empty;
        private string photo = string.Empty;
        private string description = string.Empty;
        private int productID;
        private DateTime startDate = DateTime.Now;
        private DateTime endDate = DateTime.Now;
        private decimal price;
        private int minCount;
        private int maxCount;
        private int eachNumber;

        public int ID
        {
            set { this.id = value; }
            get { return this.id; }
        }
        public string Name
        {
            set { this.name = value; }
            get { return this.name; }
        }
        public string Photo
        {
            set { this.photo = value; }
            get { return this.photo; }
        }
        public string Description
        {
            set { this.description = value; }
            get { return this.description; }
        }
        public int ProductID
        {
            set { this.productID = value; }
            get { return this.productID; }
        }
        public DateTime StartDate
        {
            set { this.startDate = value; }
            get { return this.startDate; }
        }
        public DateTime EndDate
        {
            set { this.endDate = value; }
            get { return this.endDate; }
        }
        public decimal Price
        {
            set { this.price = value; }
            get { return this.price; }
        }
        public int MinCount
        {
            set { this.minCount = value; }
            get { return this.minCount; }
        }
        public int MaxCount
        {
            set { this.maxCount = value; }
            get { return this.maxCount; }
        }
        public int EachNumber
        {
            set { this.eachNumber = value; }
            get { return this.eachNumber; }
        } 
    }
}