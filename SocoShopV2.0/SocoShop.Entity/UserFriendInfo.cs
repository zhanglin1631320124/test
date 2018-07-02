namespace SocoShop.Entity
{
    using System;

    public sealed class UserFriendInfo
    {
        private int friendID;
        private string friendName = string.Empty;
        private int id;
        private int userID;
        private string userName = string.Empty;

        public int FriendID
        {
            get
            {
                return this.friendID;
            }
            set
            {
                this.friendID = value;
            }
        }

        public string FriendName
        {
            get
            {
                return this.friendName;
            }
            set
            {
                this.friendName = value;
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
    }
}

