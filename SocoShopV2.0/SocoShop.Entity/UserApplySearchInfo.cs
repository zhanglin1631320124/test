namespace SocoShop.Entity
{
    using System;

    public sealed class UserApplySearchInfo
    {
        private DateTime endApplyDate = DateTime.MinValue;
        private string number = string.Empty;
        private DateTime startApplyDate = DateTime.MinValue;
        private int status = -2147483648;
        private int userID = -2147483648;
        private string userName = string.Empty;

        public DateTime EndApplyDate
        {
            get
            {
                return this.endApplyDate;
            }
            set
            {
                this.endApplyDate = value;
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

        public DateTime StartApplyDate
        {
            get
            {
                return this.startApplyDate;
            }
            set
            {
                this.startApplyDate = value;
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

