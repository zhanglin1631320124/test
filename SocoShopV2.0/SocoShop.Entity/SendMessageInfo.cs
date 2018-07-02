namespace SocoShop.Entity
{
    using System;

    public sealed class SendMessageInfo
    {
        private string content = string.Empty;
        private DateTime date = DateTime.Now;
        private int id;
        private int isAdmin;
        private string title = string.Empty;
        private string toUserID = string.Empty;
        private string toUserName = string.Empty;
        private int userID;
        private string userName = string.Empty;

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
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

        public int IsAdmin
        {
            get
            {
                return this.isAdmin;
            }
            set
            {
                this.isAdmin = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public string ToUserID
        {
            get
            {
                return this.toUserID;
            }
            set
            {
                this.toUserID = value;
            }
        }

        public string ToUserName
        {
            get
            {
                return this.toUserName;
            }
            set
            {
                this.toUserName = value;
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

