namespace SocoShop.Entity
{
    using System;

    public sealed class ProductCommentSearchInfo
    {
        private string content = string.Empty;
        private DateTime endPostDate = DateTime.MinValue;
        private int productID = -2147483648;
        private string productName = string.Empty;
        private DateTime startPostDate = DateTime.MinValue;
        private int status = -2147483648;
        private string title = string.Empty;
        private int userID = -2147483648;
        private string userIP = string.Empty;

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

        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                this.productName = value;
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
    }
}

