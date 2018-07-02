namespace SocoShop.Entity
{
    using System;

    public sealed class LinkInfo
    {
        private string display = string.Empty;
        private int id;
        private int linkClass;
        private int orderID;
        private string remark = string.Empty;
        private string uRL = string.Empty;

        public string Display
        {
            get
            {
                return this.display;
            }
            set
            {
                this.display = value;
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

        public int LinkClass
        {
            get
            {
                return this.linkClass;
            }
            set
            {
                this.linkClass = value;
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

        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
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

