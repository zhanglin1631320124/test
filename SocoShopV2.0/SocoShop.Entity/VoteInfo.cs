namespace SocoShop.Entity
{
    using System;

    public sealed class VoteInfo
    {
        private int id;
        private int itemCount;
        private string note = string.Empty;
        private string title = string.Empty;
        private int voteType;

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

        public int ItemCount
        {
            get
            {
                return this.itemCount;
            }
            set
            {
                this.itemCount = value;
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

        public int VoteType
        {
            get
            {
                return this.voteType;
            }
            set
            {
                this.voteType = value;
            }
        }
    }
}

