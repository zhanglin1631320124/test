namespace SocoShop.Entity
{
    using System;

    public sealed class ThemeActivityInfo
    {
        private string css = string.Empty;
        private string description = string.Empty;
        private int id;
        private string name = string.Empty;
        private string photo = string.Empty;
        private string productGroup = string.Empty;
        private string style = string.Empty;

        public string Css
        {
            get
            {
                return this.css;
            }
            set
            {
                this.css = value;
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

        public string ProductGroup
        {
            get
            {
                return this.productGroup;
            }
            set
            {
                this.productGroup = value;
            }
        }

        public string Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }
    }
}

