namespace SocoShop.Entity
{
    using System;

    public sealed class UserMessageSeachInfo
    {
        private DateTime endPostDate = DateTime.MinValue;
        private int isHandler = -2147483648;
        private int messageClass = -2147483648;
        private DateTime startPostDate = DateTime.MinValue;
        private string title = string.Empty;
        private int userID = -2147483648;
        private string userName = string.Empty;

        public DateTime EndPostDate
        {
            get
            {
                return this.endPostDate;
            }
            set
            {
                this.endPostDate = value;
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

        public DateTime StartPostDate
        {
            get
            {
                return this.startPostDate;
            }
            set
            {
                this.startPostDate = value;
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

