namespace SocoShop.Entity
{
    using System;

    public sealed class OrderActionInfo
    {
        private int adminID;
        private string adminName = string.Empty;
        private DateTime date = DateTime.Now;
        private int endOrderStatus;
        private int id;
        private string iP = string.Empty;
        private string note = string.Empty;
        private int orderID;
        private int orderOperate;
        private int startOrderStatus;

        public int AdminID
        {
            get
            {
                return this.adminID;
            }
            set
            {
                this.adminID = value;
            }
        }

        public string AdminName
        {
            get
            {
                return this.adminName;
            }
            set
            {
                this.adminName = value;
            }
        }

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

        public int EndOrderStatus
        {
            get
            {
                return this.endOrderStatus;
            }
            set
            {
                this.endOrderStatus = value;
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

        public string Note
        {
            get
            {
                return this.note;
            }
            set
            {
                this.note = value;
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

        public int OrderOperate
        {
            get
            {
                return this.orderOperate;
            }
            set
            {
                this.orderOperate = value;
            }
        }

        public int StartOrderStatus
        {
            get
            {
                return this.startOrderStatus;
            }
            set
            {
                this.startOrderStatus = value;
            }
        }
    }
}

