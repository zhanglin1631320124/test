namespace SocoShop.Entity
{
    using System;

    public sealed class ReceiveMessageSearchInfo
    {
        private int isAdmin = -2147483648;
        private int isRead = -2147483648;
        private int userID = -2147483648;

        public int IsAdmin
        {
            get
            {
                return this.isAdmin;
            }
            set
            {
                this.isAdmin = value;
            }
        }

        public int IsRead
        {
            get
            {
                return this.isRead;
            }
            set
            {
                this.isRead = value;
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

