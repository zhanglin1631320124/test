namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class RegionInfo
    {
        private int fatherID;
        private int id;
        private int orderID;
        private string regionName = string.Empty;

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

        public string RegionName
        {
            get
            {
                return this.regionName;
            }
            set
            {
                this.regionName = value;
            }
        }
    }
}

