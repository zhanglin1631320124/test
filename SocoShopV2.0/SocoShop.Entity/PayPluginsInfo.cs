namespace SocoShop.Entity
{
    using System;

    public class PayPluginsInfo
    {
        private string description = string.Empty;
        private int isCod;
        private int isEnabled = 0;
        private int isOnline;
        private string key = string.Empty;
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

        public int IsCod
        {
            get
            {
                return this.isCod;
            }
            set
            {
                this.isCod = value;
            }
        }

        public int IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;
            }
        }

        public int IsOnline
        {
            get
            {
                return this.isOnline;
            }
            set
            {
                this.isOnline = value;
            }
        }

        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
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

