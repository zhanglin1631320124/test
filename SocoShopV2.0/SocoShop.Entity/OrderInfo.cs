namespace SocoShop.Entity
{
    using System;

    public sealed class OrderInfo
    {
        private DateTime addDate = DateTime.Now;
        private string address = string.Empty;
        private decimal balance;
        private string consignee = string.Empty;
        private decimal couponMoney;
        private string email = string.Empty;
        private int favorableActivityID;
        private decimal favorableMoney;
        private int giftID;
        private int id;
        private string invoiceContent = string.Empty;
        private string invoiceTitle = string.Empty;
        private string iP = string.Empty;
        private int isActivity;
        private int isRefund;
        private string mobile = string.Empty;
        private string orderNote = string.Empty;
        private string orderNumber = string.Empty;
        private int orderStatus;
        private decimal otherMoney;
        private DateTime payDate = DateTime.Now;
        private string payKey = string.Empty;
        private string payName = string.Empty;
        private decimal productMoney;
        private string regionID = string.Empty;
        private DateTime shippingDate = DateTime.Now;
        private int shippingID;
        private decimal shippingMoney;
        private string shippingNumber = string.Empty;
        private string tel = string.Empty;
        private int userID;
        private string userMessage = string.Empty;
        private string userName = string.Empty;
        private string zipCode = string.Empty;

        public DateTime AddDate
        {
            get
            {
                return this.addDate;
            }
            set
            {
                this.addDate = value;
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return this.balance;
            }
            set
            {
                this.balance = value;
            }
        }

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

        public decimal CouponMoney
        {
            get
            {
                return this.couponMoney;
            }
            set
            {
                this.couponMoney = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public int FavorableActivityID
        {
            get
            {
                return this.favorableActivityID;
            }
            set
            {
                this.favorableActivityID = value;
            }
        }

        public decimal FavorableMoney
        {
            get
            {
                return this.favorableMoney;
            }
            set
            {
                this.favorableMoney = value;
            }
        }

        public int GiftID
        {
            get
            {
                return this.giftID;
            }
            set
            {
                this.giftID = value;
            }
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string InvoiceContent
        {
            get
            {
                return this.invoiceContent;
            }
            set
            {
                this.invoiceContent = value;
            }
        }

        public string InvoiceTitle
        {
            get
            {
                return this.invoiceTitle;
            }
            set
            {
                this.invoiceTitle = value;
            }
        }

        public string IP
        {
            get
            {
                return this.iP;
            }
            set
            {
                this.iP = value;
            }
        }

        public int IsActivity
        {
            get
            {
                return this.isActivity;
            }
            set
            {
                this.isActivity = value;
            }
        }

        public int IsRefund
        {
            get
            {
                return this.isRefund;
            }
            set
            {
                this.isRefund = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
            }
        }

        public string OrderNote
        {
            get
            {
                return this.orderNote;
            }
            set
            {
                this.orderNote = value;
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

        public decimal OtherMoney
        {
            get
            {
                return this.otherMoney;
            }
            set
            {
                this.otherMoney = value;
            }
        }

        public DateTime PayDate
        {
            get
            {
                return this.payDate;
            }
            set
            {
                this.payDate = value;
            }
        }

        public string PayKey
        {
            get
            {
                return this.payKey;
            }
            set
            {
                this.payKey = value;
            }
        }

        public string PayName
        {
            get
            {
                return this.payName;
            }
            set
            {
                this.payName = value;
            }
        }

        public decimal ProductMoney
        {
            get
            {
                return this.productMoney;
            }
            set
            {
                this.productMoney = value;
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

        public DateTime ShippingDate
        {
            get
            {
                return this.shippingDate;
            }
            set
            {
                this.shippingDate = value;
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

        public decimal ShippingMoney
        {
            get
            {
                return this.shippingMoney;
            }
            set
            {
                this.shippingMoney = value;
            }
        }

        public string ShippingNumber
        {
            get
            {
                return this.shippingNumber;
            }
            set
            {
                this.shippingNumber = value;
            }
        }

        public string Tel
        {
            get
            {
                return this.tel;
            }
            set
            {
                this.tel = value;
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

        public string UserMessage
        {
            get
            {
                return this.userMessage;
            }
            set
            {
                this.userMessage = value;
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

        public string ZipCode
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                this.zipCode = value;
            }
        }
    }
}

