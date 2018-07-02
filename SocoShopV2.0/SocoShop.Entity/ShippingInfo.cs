namespace SocoShop.Entity
{
    using System;

    public sealed class ShippingInfo
    {
        private int againWeight;
        private string description = string.Empty;
        private int firstWeight;
        private int id;
        private int isEnabled;
        private string name = string.Empty;
        private int orderID;
        private int shippingType;

        public int AgainWeight
        {
            get
            {
                return this.againWeight;
            }
            set
            {
                this.againWeight = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public int FirstWeight
        {
            get
            {
                return this.firstWeight;
            }
            set
            {
                this.firstWeight = value;
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

        public int IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;
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

        public int ShippingType
        {
            get
            {
                return this.shippingType;
            }
            set
            {
                this.shippingType = value;
            }
        }
    }
}

