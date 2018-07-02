namespace SocoShop.Entity
{
    using System;

    public sealed class UserFriendSearchInfo
    {
        private string friendName = string.Empty;
        private int userID = -2147483648;

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

