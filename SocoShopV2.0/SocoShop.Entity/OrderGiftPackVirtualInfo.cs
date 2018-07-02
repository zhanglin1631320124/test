namespace SocoShop.Entity
{
    using System;
    using System.Collections.Generic;

    public sealed class OrderGiftPackVirtualInfo
    {
        private int giftPackBuyCount;
        private int giftPackID;
        private string giftPackName = string.Empty;
        private string giftPackPhoto = string.Empty;
        private List<OrderDetailInfo> orderDetailList = new List<OrderDetailInfo>();
        private string randNumber = string.Empty;
        private string strOrderDetailID = string.Empty;
        private string strProductID = string.Empty;
        private decimal totalPrice;
        private decimal totalProductWeight;
        private int totalSendPoint;

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

        public List<OrderDetailInfo> OrderDetailList
        {
            get
            {
                return this.orderDetailList;
            }
            set
            {
                this.orderDetailList = value;
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

        public string StrOrderDetailID
        {
            get
            {
                return this.strOrderDetailID;
            }
            set
            {
                this.strOrderDetailID = value;
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

