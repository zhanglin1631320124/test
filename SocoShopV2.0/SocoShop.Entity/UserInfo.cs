namespace SocoShop.Entity
{
    using System;

    public sealed class UserInfo
    {
        private string address = string.Empty;
        private string birthday = string.Empty;
        private string email = string.Empty;
        private DateTime findDate = DateTime.Now;
        private int id;
        private string introduce = string.Empty;
        private DateTime lastLoginDate = DateTime.Now;
        private string lastLoginIP = string.Empty;
        private int loginTimes;
        private string mobile = string.Empty;
        private decimal moneyLeft = 0M;
        private decimal moneyUsed = 0M;
        private string mSN = string.Empty;
        private string openID = string.Empty;
        private string photo = string.Empty;
        private int pointLeft = 0;
        private string qQ = string.Empty;
        private string regionID = string.Empty;
        private DateTime registerDate = DateTime.Now;
        private string registerIP = string.Empty;
        private string safeCode = string.Empty;
        private int sex;
        private int status;
        private string tel = string.Empty;
        private string userName = string.Empty;
        private string userPassword = string.Empty;

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public string Birthday
        {
            get
            {
                return this.birthday;
            }
            set
            {
                this.birthday = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public DateTime FindDate
        {
            get
            {
                return this.findDate;
            }
            set
            {
                this.findDate = value;
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

        public string Introduce
        {
            get
            {
                return this.introduce;
            }
            set
            {
                this.introduce = value;
            }
        }

        public DateTime LastLoginDate
        {
            get
            {
                return this.lastLoginDate;
            }
            set
            {
                this.lastLoginDate = value;
            }
        }

        public string LastLoginIP
        {
            get
            {
                return this.lastLoginIP;
            }
            set
            {
                this.lastLoginIP = value;
            }
        }

        public int LoginTimes
        {
            get
            {
                return this.loginTimes;
            }
            set
            {
                this.loginTimes = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
            }
        }

        public decimal MoneyLeft
        {
            get
            {
                return this.moneyLeft;
            }
            set
            {
                this.moneyLeft = value;
            }
        }

        public decimal MoneyUsed
        {
            get
            {
                return this.moneyUsed;
            }
            set
            {
                this.moneyUsed = value;
            }
        }

        public string MSN
        {
            get
            {
                return this.mSN;
            }
            set
            {
                this.mSN = value;
            }
        }

        public string OpenID
        {
            get
            {
                return this.openID;
            }
            set
            {
                this.openID = value;
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

        public int PointLeft
        {
            get
            {
                return this.pointLeft;
            }
            set
            {
                this.pointLeft = value;
            }
        }

        public string QQ
        {
            get
            {
                return this.qQ;
            }
            set
            {
                this.qQ = value;
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

        public DateTime RegisterDate
        {
            get
            {
                return this.registerDate;
            }
            set
            {
                this.registerDate = value;
            }
        }

        public string RegisterIP
        {
            get
            {
                return this.registerIP;
            }
            set
            {
                this.registerIP = value;
            }
        }

        public string SafeCode
        {
            get
            {
                return this.safeCode;
            }
            set
            {
                this.safeCode = value;
            }
        }

        public int Sex
        {
            get
            {
                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }

        public int Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string Tel
        {
            get
            {
                return this.tel;
            }
            set
            {
                this.tel = value;
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

        public string UserPassword
        {
            get
            {
                return this.userPassword;
            }
            set
            {
                this.userPassword = value;
            }
        }
    }
}

