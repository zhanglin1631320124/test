namespace SocoShop.Entity
{
    using System;

    public sealed class TagsSearchInfo
    {
        private int isTop = -2147483648;
        private int productID = -2147483648;
        private string productName = string.Empty;
        private int userID = -2147483648;
        private string word = string.Empty;

        public int IsTop
        {
            get
            {
                return this.isTop;
            }
            set
            {
                this.isTop = value;
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

        public string Word
        {
            get
            {
                return this.word;
            }
            set
            {
                this.word = value;
            }
        }
    }
}

