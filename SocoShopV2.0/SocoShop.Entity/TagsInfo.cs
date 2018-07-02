namespace SocoShop.Entity
{
    using System;

    public sealed class TagsInfo
    {
        private string color = string.Empty;
        private int id;
        private int isTop;
        private ProductInfo product = new ProductInfo();
        private int productID;
        private int size;
        private int userID;
        private string userName = string.Empty;
        private string word = string.Empty;

        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
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

        public int Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
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

