namespace SocoShop.Entity
{
    using System;

    public sealed class CouponSearchInfo
    {
        private string inCouponID = string.Empty;
        private string name = string.Empty;

        public string InCouponID
        {
            get
            {
                return this.inCouponID;
            }
            set
            {
                this.inCouponID = value;
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
    }
}

