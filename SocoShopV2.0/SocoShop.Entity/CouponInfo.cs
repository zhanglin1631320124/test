namespace SocoShop.Entity
{
    using System;

    public sealed class CouponInfo
    {
        private int id;
        private decimal money;
        private string name = string.Empty;
        private DateTime useEndDate = DateTime.Now;
        private decimal useMinAmount;
        private DateTime useStartDate = DateTime.Now;

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

        public decimal Money
        {
            get
            {
                return this.money;
            }
            set
            {
                this.money = value;
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

        public DateTime UseEndDate
        {
            get
            {
                return this.useEndDate;
            }
            set
            {
                this.useEndDate = value;
            }
        }

        public decimal UseMinAmount
        {
            get
            {
                return this.useMinAmount;
            }
            set
            {
                this.useMinAmount = value;
            }
        }

        public DateTime UseStartDate
        {
            get
            {
                return this.useStartDate;
            }
            set
            {
                this.useStartDate = value;
            }
        }
    }
}

