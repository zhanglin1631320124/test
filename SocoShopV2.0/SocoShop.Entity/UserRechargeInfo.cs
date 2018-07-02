namespace SocoShop.Entity
{
    using System;

    public sealed class UserRechargeInfo
    {
        private int id;
        private int isFinish;
        private decimal money;
        private string number = string.Empty;
        private string payKey = string.Empty;
        private string payName = string.Empty;
        private DateTime rechargeDate = DateTime.Now;
        private string rechargeIP = string.Empty;
        private int userID;
        private string userName = string.Empty;

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

        public string PayKey
        {
            get
            {
                return this.payKey;
            }
            set
            {
                this.payKey = value;
            }
        }

        public string PayName
        {
            get
            {
                return this.payName;
            }
            set
            {
                this.payName = value;
            }
        }

        public DateTime RechargeDate
        {
            get
            {
                return this.rechargeDate;
            }
            set
            {
                this.rechargeDate = value;
            }
        }

        public string RechargeIP
        {
            get
            {
                return this.rechargeIP;
            }
            set
            {
                this.rechargeIP = value;
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

