namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class ArticleClassInfo
    {
        private string className = string.Empty;
        private string description = string.Empty;
        private int fatherID;
        private int id;
        private int isSystem;
        private int orderID;

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

        public int IsSystem
        {
            get
            {
                return this.isSystem;
            }
            set
            {
                this.isSystem = value;
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
    }
}

