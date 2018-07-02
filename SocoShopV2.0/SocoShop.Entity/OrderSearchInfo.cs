namespace SocoShop.Entity
{
    using System;

    public sealed class OrderSearchInfo
    {
        private string consignee = string.Empty;
        private DateTime endAddDate = DateTime.MinValue;
        private string orderNumber = string.Empty;
        private int orderStatus = -2147483648;
        private string regionID = string.Empty;
        private int shippingID = -2147483648;
        private DateTime startAddDate = DateTime.MinValue;
        private int userID = -2147483648;
        private string userName = string.Empty;

        public string Consignee
        {
            get
            {
                return this.consignee;
            }
            set
            {
                this.consignee = value;
            }
        }

        public DateTime EndAddDate
        {
            get
            {
                return this.endAddDate;
            }
            set
            {
                this.endAddDate = value;
            }
        }

        public string OrderNumber
        {
            get
            {
                return this.orderNumber;
            }
            set
            {
                this.orderNumber = value;
            }
        }

        public int OrderStatus
        {
            get
            {
                return this.orderStatus;
            }
            set
            {
                this.orderStatus = value;
            }
        }

        public string RegionID
        {
            get
            {
                return this.regionID;
            }
            set
            {
                this.regionID = value;
            }
        }

        public int ShippingID
        {
            get
            {
                return this.shippingID;
            }
            set
            {
                this.shippingID = value;
            }
        }

        public DateTime StartAddDate
        {
            get
            {
                return this.startAddDate;
            }
            set
            {
                this.startAddDate = value;
            }
        }

        public int UserID
        {
            get
            {
                return this.userID;
            }
            set
            {
                this.userID = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
    }
}

