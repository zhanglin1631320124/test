namespace SocoShop.Entity
{
    using System;
    using System.Collections.Generic;

    public sealed class CartGiftPackVirtualInfo
    {
        private List<CartInfo> cartList = new List<CartInfo>();
        private int giftPackBuyCount;
        private int giftPackID;
        private string giftPackName = string.Empty;
        private string giftPackPhoto = string.Empty;
        private int leftStorageCount;
        private string randNumber = string.Empty;
        private string strCartID = string.Empty;
        private string strProductID = string.Empty;
        private decimal totalPrice;
        private decimal totalProductWeight;
        private int totalSendPoint;

        public List<CartInfo> CartList
        {
            get
            {
                return this.cartList;
            }
            set
            {
                this.cartList = value;
            }
        }

        public int GiftPackBuyCount
        {
            get
            {
                return this.giftPackBuyCount;
            }
            set
            {
                this.giftPackBuyCount = value;
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

        public string GiftPackName
        {
            get
            {
                return this.giftPackName;
            }
            set
            {
                this.giftPackName = value;
            }
        }

        public string GiftPackPhoto
        {
            get
            {
                return this.giftPackPhoto;
            }
            set
            {
                this.giftPackPhoto = value;
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

        public string StrCartID
        {
            get
            {
                return this.strCartID;
            }
            set
            {
                this.strCartID = value;
            }
        }

        public string StrProductID
        {
            get
            {
                return this.strProductID;
            }
            set
            {
                this.strProductID = value;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return this.totalPrice;
            }
            set
            {
                this.totalPrice = value;
            }
        }

        public decimal TotalProductWeight
        {
            get
            {
                return this.totalProductWeight;
            }
            set
            {
                this.totalProductWeight = value;
            }
        }

        public int TotalSendPoint
        {
            get
            {
                return this.totalSendPoint;
            }
            set
            {
                this.totalSendPoint = value;
            }
        }
    }
}

