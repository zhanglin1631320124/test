namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class MenuInfo
    {
        private DateTime date = DateTime.Now;
        private int fatherID;
        private int id;
        private string iP = string.Empty;
        private int menuImage;
        private string menuName = string.Empty;
        private int orderID;
        private string uRL = string.Empty;

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

        public string IP
        {
            get
            {
                return this.iP;
            }
            set
            {
                this.iP = value;
            }
        }

        public int MenuImage
        {
            get
            {
                return this.menuImage;
            }
            set
            {
                this.menuImage = value;
            }
        }

        public string MenuName
        {
            get
            {
                return this.menuName;
            }
            set
            {
                this.menuName = value;
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

        public string URL
        {
            get
            {
                return this.uRL;
            }
            set
            {
                this.uRL = value;
            }
        }
    }
}

