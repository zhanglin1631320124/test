namespace SocoShop.Entity
{
    using System;

    public sealed class VoteRecordInfo
    {
        private DateTime addDate = DateTime.Now;
        private int id;
        private string itemID = string.Empty;
        private int userID;
        private string userIP = string.Empty;
        private string userName = string.Empty;
        private int voteID;

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

        public string ItemID
        {
            get
            {
                return this.itemID;
            }
            set
            {
                this.itemID = value;
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

        public string UserIP
        {
            get
            {
                return this.userIP;
            }
            set
            {
                this.userIP = value;
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

        public int VoteID
        {
            get
            {
                return this.voteID;
            }
            set
            {
                this.voteID = value;
            }
        }
    }
}

