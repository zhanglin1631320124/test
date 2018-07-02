namespace SocoShop.Entity
{
    using System;

    public sealed class VoteItemInfo
    {
        private int id;
        private string itemName = string.Empty;
        private int orderID;
        private int voteCount;
        private int voteID;

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

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
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

        public int VoteCount
        {
            get
            {
                return this.voteCount;
            }
            set
            {
                this.voteCount = value;
            }
        }

        public int VoteID
        {
            get
            {
                return this.voteID;
            }
            set
            {
                this.voteID = value;
            }
        }
    }
}

