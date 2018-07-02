namespace SocoShop.Entity
{
    using System;

    public sealed class GiftInfo
    {
        private string description = string.Empty;
        private int id;
        private string name = string.Empty;
        private string photo = string.Empty;

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
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

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Photo
        {
            get
            {
                return this.photo;
            }
            set
            {
                this.photo = value;
            }
        }
    }
}

