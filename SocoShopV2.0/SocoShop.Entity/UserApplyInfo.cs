namespace SocoShop.Entity
{
    using System;

    public sealed class UserApplyInfo
    {
        private string adminNote = string.Empty;
        private DateTime applyDate = DateTime.Now;
        private string applyIP = string.Empty;
        private int id;
        private decimal money;
        private string number = string.Empty;
        private int status;
        private int updateAdminID;
        private string updateAdminName = string.Empty;
        private DateTime updateDate = DateTime.Now;
        private int userID;
        private string userName = string.Empty;
        private string userNote = string.Empty;

        public string AdminNote
        {
            get
            {
                return this.adminNote;
            }
            set
            {
                this.adminNote = value;
            }
        }

        public DateTime ApplyDate
        {
            get
            {
                return this.applyDate;
            }
            set
            {
                this.applyDate = value;
            }
        }

        public string ApplyIP
        {
            get
            {
                return this.applyIP;
            }
            set
            {
                this.applyIP = value;
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

        public int UpdateAdminID
        {
            get
            {
                return this.updateAdminID;
            }
            set
            {
                this.updateAdminID = value;
            }
        }

        public string UpdateAdminName
        {
            get
            {
                return this.updateAdminName;
            }
            set
            {
                this.updateAdminName = value;
            }
        }

        public DateTime UpdateDate
        {
            get
            {
                return this.updateDate;
            }
            set
            {
                this.updateDate = value;
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

        public string UserNote
        {
            get
            {
                return this.userNote;
            }
            set
            {
                this.userNote = value;
            }
        }
    }
}

