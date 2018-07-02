namespace SocoShop.Entity
{
    using System;

    public sealed class BookingProductSearchInfo
    {
        private string email = string.Empty;
        private int isHandler = -2147483648;
        private string productName = string.Empty;
        private string relationUser = string.Empty;
        private string tel = string.Empty;
        private int userID = -2147483648;

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
    }
}

