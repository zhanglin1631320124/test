namespace SocoShop.Entity
{
    using System;

    public sealed class UserCouponSearchInfo
    {
        private int couponID = -2147483648;
        private int getType = -2147483648;
        private int isUse = -2147483648;
        private string number = string.Empty;
        private int userID = -2147483648;

        public int CouponID
        {
            get
            {
                return this.couponID;
            }
            set
            {
                this.couponID = value;
            }
        }

        public new int GetType
        {
            get
            {
                return this.getType;
            }
            set
            {
                this.getType = value;
            }
        }

        public int IsUse
        {
            get
            {
                return this.isUse;
            }
            set
            {
                this.isUse = value;
            }
        }

        public string Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
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
    }
}

