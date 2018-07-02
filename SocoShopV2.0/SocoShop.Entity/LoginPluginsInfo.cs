namespace SocoShop.Entity
{
    using System;

    public class LoginPluginsInfo
    {
        private string description = string.Empty;
        private int isEnabled = 0;
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

