namespace SocoShop.Entity
{
    using System;

    public sealed class PowerInfo
    {
        private string key = string.Empty;
        private string text = string.Empty;
        private string value = string.Empty;
        private string xml = string.Empty;

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

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public string XML
        {
            get
            {
                return this.xml;
            }
            set
            {
                this.xml = value;
            }
        }
    }
}

