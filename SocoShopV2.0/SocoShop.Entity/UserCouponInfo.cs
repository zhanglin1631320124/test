namespace SocoShop.Entity
{
    using System;

    public sealed class UserCouponInfo
    {
        private CouponInfo coupon = new CouponInfo();
        private int couponID;
        private int getType;
        private int id;
        private int isUse;
        private string number = string.Empty;
        private int orderID;
        private string password = string.Empty;
        private int userID;
        private string userName = string.Empty;

        public CouponInfo Coupon
        {
            get
            {
                return this.coupon;
            }
            set
            {
                this.coupon = value;
            }
        }

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

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
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

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }
    }
}

