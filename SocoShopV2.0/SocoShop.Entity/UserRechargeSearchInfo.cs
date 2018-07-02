namespace SocoShop.Entity
{
    using System;

    public sealed class UserRechargeSearchInfo
    {
        private DateTime endRechargeDate = DateTime.MinValue;
        private int isFinish = -2147483648;
        private string number = string.Empty;
        private DateTime startRechargeDate = DateTime.MinValue;
        private int userID = -2147483648;
        private string userName = string.Empty;

        public DateTime EndRechargeDate
        {
            get
            {
                return this.endRechargeDate;
            }
            set
            {
                this.endRechargeDate = value;
            }
        }

        public int IsFinish
        {
            get
            {
                return this.isFinish;
            }
            set
            {
                this.isFinish = value;
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

        public DateTime StartRechargeDate
        {
            get
            {
                return this.startRechargeDate;
            }
            set
            {
                this.startRechargeDate = value;
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

