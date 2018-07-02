namespace SocoShop.Entity
{
    using System;

    public sealed class ReceiveMessageInfo
    {
        private string content = string.Empty;
        private DateTime date = DateTime.Now;
        private int fromUserID;
        private string fromUserName = string.Empty;
        private int id;
        private int isAdmin;
        private int isRead;
        private string title = string.Empty;
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

        public int FromUserID
        {
            get
            {
                return this.fromUserID;
            }
            set
            {
                this.fromUserID = value;
            }
        }

        public string FromUserName
        {
            get
            {
                return this.fromUserName;
            }
            set
            {
                this.fromUserName = value;
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

        public int IsRead
        {
            get
            {
                return this.isRead;
            }
            set
            {
                this.isRead = value;
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

