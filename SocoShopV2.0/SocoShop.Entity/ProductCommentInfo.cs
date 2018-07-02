namespace SocoShop.Entity
{
    using System;

    public sealed class ProductCommentInfo
    {
        private string adminReplyContent = string.Empty;
        private DateTime adminReplyDate = DateTime.Now;
        private int against;
        private string content = string.Empty;
        private int id;
        private DateTime postDate = DateTime.Now;
        private ProductInfo product = new ProductInfo();
        private int productID;
        private int rank;
        private int replyCount;
        private int status;
        private int support;
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

        public int Against
        {
            get
            {
                return this.against;
            }
            set
            {
                this.against = value;
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

        public ProductInfo Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
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

        public int Rank
        {
            get
            {
                return this.rank;
            }
            set
            {
                this.rank = value;
            }
        }

        public int ReplyCount
        {
            get
            {
                return this.replyCount;
            }
            set
            {
                this.replyCount = value;
            }
        }

        public int Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public int Support
        {
            get
            {
                return this.support;
            }
            set
            {
                this.support = value;
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

