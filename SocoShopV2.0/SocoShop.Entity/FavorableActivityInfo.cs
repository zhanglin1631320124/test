namespace SocoShop.Entity
{
    using System;

    public sealed class FavorableActivityInfo
    {
        private string content = string.Empty;
        private DateTime endDate = DateTime.Now;
        private string giftID = string.Empty;
        private int id;
        private string name = string.Empty;
        private decimal orderProductMoney;
        private string photo = string.Empty;
        private decimal reduceDiscount;
        private decimal reduceMoney;
        private int reduceWay;
        private string regionID = string.Empty;
        private int shippingWay;
        private DateTime startDate = DateTime.Now;
        private string userGrade = string.Empty;

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }

        public string GiftID
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

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public decimal OrderProductMoney
        {
            get
            {
                return this.orderProductMoney;
            }
            set
            {
                this.orderProductMoney = value;
            }
        }

        public string Photo
        {
            get
            {
                return this.photo;
            }
            set
            {
                this.photo = value;
            }
        }

        public decimal ReduceDiscount
        {
            get
            {
                return this.reduceDiscount;
            }
            set
            {
                this.reduceDiscount = value;
            }
        }

        public decimal ReduceMoney
        {
            get
            {
                return this.reduceMoney;
            }
            set
            {
                this.reduceMoney = value;
            }
        }

        public int ReduceWay
        {
            get
            {
                return this.reduceWay;
            }
            set
            {
                this.reduceWay = value;
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

        public int ShippingWay
        {
            get
            {
                return this.shippingWay;
            }
            set
            {
                this.shippingWay = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
            }
        }

        public string UserGrade
        {
            get
            {
                return this.userGrade;
            }
            set
            {
                this.userGrade = value;
            }
        }
    }
}

