namespace SocoShop.Entity
{
    using System;

    public sealed class CartInfo
    {
        private int buyCount;
        private int fatherID;
        private int giftPackID;
        private int id;
        private int leftStorageCount;
        private int productID;
        private string productName = string.Empty;
        private decimal productPrice;
        private decimal productWeight;
        private string randNumber = string.Empty;
        private int sendPoint;
        private int userID;
        private string userName = string.Empty;

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

        public int LeftStorageCount
        {
            get
            {
                return this.leftStorageCount;
            }
            set
            {
                this.leftStorageCount = value;
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

