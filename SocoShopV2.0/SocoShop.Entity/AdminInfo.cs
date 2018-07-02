namespace SocoShop.Entity
{
    using System;

    public sealed class AdminInfo
    {
        private string email = string.Empty;
        private int groupID;
        private int id;
        private int isCreate;
        private DateTime lastLoginDate = DateTime.Now;
        private string lastLoginIP = string.Empty;
        private int loginTimes;
        private string name = string.Empty;
        private string noteBook = string.Empty;
        private string password = string.Empty;

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

        public int GroupID
        {
            get
            {
                return this.groupID;
            }
            set
            {
                this.groupID = value;
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

        public int IsCreate
        {
            get
            {
                return this.isCreate;
            }
            set
            {
                this.isCreate = value;
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

        public string NoteBook
        {
            get
            {
                return this.noteBook;
            }
            set
            {
                this.noteBook = value;
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
    }
}

