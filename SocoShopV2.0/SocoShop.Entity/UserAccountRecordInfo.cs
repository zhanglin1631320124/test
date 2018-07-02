namespace SocoShop.Entity
{
    using System;

    public sealed class UserAccountRecordInfo
    {
        private DateTime date = DateTime.Now;
        private int id;
        private string iP = string.Empty;
        private decimal money;
        private string note = string.Empty;
        private int point;
        private int userID;
        private string userName = string.Empty;

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

        public string Note
        {
            get
            {
                return this.note;
            }
            set
            {
                this.note = value;
            }
        }

        public int Point
        {
            get
            {
                return this.point;
            }
            set
            {
                this.point = value;
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

