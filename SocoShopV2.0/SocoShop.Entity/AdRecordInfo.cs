namespace SocoShop.Entity
{
    using System;

    public sealed class AdRecordInfo
    {
        private int adID;
        private DateTime date = DateTime.Now;
        private int id;
        private string iP = string.Empty;
        private string page = string.Empty;
        private int userID;
        private string userName = string.Empty;

        public int AdID
        {
            get
            {
                return this.adID;
            }
            set
            {
                this.adID = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
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

        public string IP
        {
            get
            {
                return this.iP;
            }
            set
            {
                this.iP = value;
            }
        }

        public string Page
        {
            get
            {
                return this.page;
            }
            set
            {
                this.page = value;
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

