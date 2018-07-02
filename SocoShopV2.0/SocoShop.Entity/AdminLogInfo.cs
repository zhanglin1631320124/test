namespace SocoShop.Entity
{
    using System;

    public sealed class AdminLogInfo
    {
        private string action = string.Empty;
        private DateTime addDate = DateTime.Now;
        private int adminID;
        private string adminName = string.Empty;
        private int groupID;
        private int id;
        private string iP = string.Empty;

        public string Action
        {
            get
            {
                return this.action;
            }
            set
            {
                this.action = value;
            }
        }

        public DateTime AddDate
        {
            get
            {
                return this.addDate;
            }
            set
            {
                this.addDate = value;
            }
        }

        public int AdminID
        {
            get
            {
                return this.adminID;
            }
            set
            {
                this.adminID = value;
            }
        }

        public string AdminName
        {
            get
            {
                return this.adminName;
            }
            set
            {
                this.adminName = value;
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
    }
}

