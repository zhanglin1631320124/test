namespace SocoShop.Entity
{
    using System;

    public sealed class BookingProductInfo
    {
        private DateTime bookingDate = DateTime.Now;
        private string bookingIP = string.Empty;
        private string email = string.Empty;
        private int handlerAdminID;
        private string handlerAdminName = string.Empty;
        private DateTime handlerDate = DateTime.Now;
        private string handlerNote = string.Empty;
        private int id;
        private int isHandler;
        private int productID;
        private string productName = string.Empty;
        private string relationUser = string.Empty;
        private string tel = string.Empty;
        private int userID;
        private string userName = string.Empty;
        private string userNote = string.Empty;

        public DateTime BookingDate
        {
            get
            {
                return this.bookingDate;
            }
            set
            {
                this.bookingDate = value;
            }
        }

        public string BookingIP
        {
            get
            {
                return this.bookingIP;
            }
            set
            {
                this.bookingIP = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public int HandlerAdminID
        {
            get
            {
                return this.handlerAdminID;
            }
            set
            {
                this.handlerAdminID = value;
            }
        }

        public string HandlerAdminName
        {
            get
            {
                return this.handlerAdminName;
            }
            set
            {
                this.handlerAdminName = value;
            }
        }

        public DateTime HandlerDate
        {
            get
            {
                return this.handlerDate;
            }
            set
            {
                this.handlerDate = value;
            }
        }

        public string HandlerNote
        {
            get
            {
                return this.handlerNote;
            }
            set
            {
                this.handlerNote = value;
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

        public int IsHandler
        {
            get
            {
                return this.isHandler;
            }
            set
            {
                this.isHandler = value;
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

        public string RelationUser
        {
            get
            {
                return this.relationUser;
            }
            set
            {
                this.relationUser = value;
            }
        }

        public string Tel
        {
            get
            {
                return this.tel;
            }
            set
            {
                this.tel = value;
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

        public string UserNote
        {
            get
            {
                return this.userNote;
            }
            set
            {
                this.userNote = value;
            }
        }
    }
}

