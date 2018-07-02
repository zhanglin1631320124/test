namespace SocoShop.Entity
{
    using System;

    public sealed class ShippingRegionInfo
    {
        private decimal againMoney;
        private decimal anotherMoeny;
        private decimal firstMoney;
        private decimal fixedMoeny;
        private int id;
        private string name = string.Empty;
        private decimal oneMoeny;
        private string regionID = string.Empty;
        private int shippingID;

        public decimal AgainMoney
        {
            get
            {
                return this.againMoney;
            }
            set
            {
                this.againMoney = value;
            }
        }

        public decimal AnotherMoeny
        {
            get
            {
                return this.anotherMoeny;
            }
            set
            {
                this.anotherMoeny = value;
            }
        }

        public decimal FirstMoney
        {
            get
            {
                return this.firstMoney;
            }
            set
            {
                this.firstMoney = value;
            }
        }

        public decimal FixedMoeny
        {
            get
            {
                return this.fixedMoeny;
            }
            set
            {
                this.fixedMoeny = value;
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

        public decimal OneMoeny
        {
            get
            {
                return this.oneMoeny;
            }
            set
            {
                this.oneMoeny = value;
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
    }
}

