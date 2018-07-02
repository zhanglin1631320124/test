namespace SocoShop.Entity
{
    using System;

    public sealed class FlashPhotoInfo
    {
        private DateTime date = DateTime.Now;
        private string fileName = string.Empty;
        private int flashID;
        private int id;
        private int orderID;
        private string title = string.Empty;
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

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public int FlashID
        {
            get
            {
                return this.flashID;
            }
            set
            {
                this.flashID = value;
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

