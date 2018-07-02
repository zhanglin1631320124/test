namespace SocoShop.Entity
{
    using System;

    public sealed class UserSearchInfo
    {
        private string email = string.Empty;
        private DateTime endRegisterDate = DateTime.MinValue;
        private string inUserID = string.Empty;
        private int sex = -2147483648;
        private DateTime startRegisterDate = DateTime.MinValue;
        private int status = -2147483648;
        private string userName = string.Empty;

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

        public DateTime EndRegisterDate
        {
            get
            {
                return this.endRegisterDate;
            }
            set
            {
                this.endRegisterDate = value;
            }
        }

        public string InUserID
        {
            get
            {
                return this.inUserID;
            }
            set
            {
                this.inUserID = value;
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

        public DateTime StartRegisterDate
        {
            get
            {
                return this.startRegisterDate;
            }
            set
            {
                this.startRegisterDate = value;
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

