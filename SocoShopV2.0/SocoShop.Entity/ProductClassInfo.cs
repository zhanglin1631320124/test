namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class ProductClassInfo
    {
        private string className = string.Empty;
        private string description = string.Empty;
        private int fatherID;
        private int id;
        private string keywords = string.Empty;
        private int orderID;
        private long taobaoID;

        public string ClassName
        {
            get
            {
                return this.className;
            }
            set
            {
                this.className = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public int FatherID
        {
            get
            {
                return this.fatherID;
            }
            set
            {
                this.fatherID = value;
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

        public string Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
            }
        }

        public int OrderID
        {
            get
            {
                return this.orderID;
            }
            set
            {
                this.orderID = value;
            }
        }

        public long TaobaoID
        {
            get
            {
                return this.taobaoID;
            }
            set
            {
                this.taobaoID = value;
            }
        }
    }
}

