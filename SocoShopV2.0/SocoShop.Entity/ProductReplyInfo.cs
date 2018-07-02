namespace SocoShop.Entity
{
    using System;

    public sealed class ProductReplyInfo
    {
        private int commentID;
        private string content = string.Empty;
        private int id;
        private DateTime postDate = DateTime.Now;
        private int productID;
        private int userID;
        private string userIP = string.Empty;
        private string userName = string.Empty;

        public int CommentID
        {
            get
            {
                return this.commentID;
            }
            set
            {
                this.commentID = value;
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

        public int ProductID
        {
            get
            {
                return this.productID;
            }
            set
            {
                this.productID = value;
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

