namespace SocoShop.Entity
{
    using System;

    public sealed class UserMessageInfo
    {
        private string adminReplyContent = string.Empty;
        private DateTime adminReplyDate = DateTime.Now;
        private string content = string.Empty;
        private int id;
        private int isHandler;
        private int messageClass;
        private DateTime postDate = DateTime.Now;
        private string title = string.Empty;
        private int userID;
        private string userIP = string.Empty;
        private string userName = string.Empty;

        public string AdminReplyContent
        {
            get
            {
                return this.adminReplyContent;
            }
            set
            {
                this.adminReplyContent = value;
            }
        }

        public DateTime AdminReplyDate
        {
            get
            {
                return this.adminReplyDate;
            }
            set
            {
                this.adminReplyDate = value;
            }
        }

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

        public int IsHandler
        {
            get
            {
                return this.isHandler;
            }
            set
            {
                this.isHandler = value;
            }
        }

        public int MessageClass
        {
            get
            {
                return this.messageClass;
            }
            set
            {
                this.messageClass = value;
            }
        }

        public DateTime PostDate
        {
            get
            {
                return this.postDate;
            }
            set
            {
                this.postDate = value;
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
    }
}

