namespace SocoShop.Entity
{
    using System;

    public sealed class GiftPackInfo
    {
        private DateTime endDate = DateTime.Now;
        private string giftGroup = string.Empty;
        private int id;
        private string name = string.Empty;
        private string photo = string.Empty;
        private decimal price;
        private DateTime startDate = DateTime.Now;

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

        public string GiftGroup
        {
            get
            {
                return this.giftGroup;
            }
            set
            {
                this.giftGroup = value;
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

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
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
    }
}

