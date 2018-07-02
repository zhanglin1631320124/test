namespace SocoShop.Entity
{
    using System;

    public class ActivityPluginsInfo
    {
        private string adminUrl = string.Empty;
        private string applyVersion = string.Empty;
        private string copyRight = string.Empty;
        private string description = string.Empty;
        private int isEnabled;
        private string key = string.Empty;
        private string name = string.Empty;
        private string photo = string.Empty;
        private string showUrl = string.Empty;

        public string AdminUrl
        {
            get
            {
                return this.adminUrl;
            }
            set
            {
                this.adminUrl = value;
            }
        }

        public string ApplyVersion
        {
            get
            {
                return this.applyVersion;
            }
            set
            {
                this.applyVersion = value;
            }
        }

        public string CopyRight
        {
            get
            {
                return this.copyRight;
            }
            set
            {
                this.copyRight = value;
            }
        }

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

        public string ShowUrl
        {
            get
            {
                return this.showUrl;
            }
            set
            {
                this.showUrl = value;
            }
        }
    }
}

