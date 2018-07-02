namespace SocoShop.Entity
{
    using System;

    public sealed class OrderDetailInfo
    {
        private int buyCount;
        private int fatherID;
        private int giftPackID;
        private int id;
        private int orderID;
        private int productID;
        private string productName = string.Empty;
        private decimal productPrice;
        private decimal productWeight;
        private string randNumber = string.Empty;
        private int sendPoint;

        public int BuyCount
        {
            get
            {
                return this.buyCount;
            }
            set
            {
                this.buyCount = value;
            }
        }

        public int FatherID
        {
            get
            {
                return this.fatherID;
            }
            set
            {
                this.fatherID = value;
            }
        }

        public int GiftPackID
        {
            get
            {
                return this.giftPackID;
            }
            set
            {
                this.giftPackID = value;
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

        public int OrderID
        {
            get
            {
                return this.orderID;
            }
            set
            {
                this.orderID = value;
            }
        }

        public int ProductID
        {
            get
            {
                return this.productID;
            }
            set
            {
                this.productID = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                this.productName = value;
            }
        }

        public decimal ProductPrice
        {
            get
            {
                return this.productPrice;
            }
            set
            {
                this.productPrice = value;
            }
        }

        public decimal ProductWeight
        {
            get
            {
                return this.productWeight;
            }
            set
            {
                this.productWeight = value;
            }
        }

        public string RandNumber
        {
            get
            {
                return this.randNumber;
            }
            set
            {
                this.randNumber = value;
            }
        }

        public int SendPoint
        {
            get
            {
                return this.sendPoint;
            }
            set
            {
                this.sendPoint = value;
            }
        }
    }
}

