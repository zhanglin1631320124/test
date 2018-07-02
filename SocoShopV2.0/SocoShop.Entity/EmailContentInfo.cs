namespace SocoShop.Entity
{
    using System;

    public class EmailContentInfo
    {
        private string emailContent = string.Empty;
        private string emailTitle = string.Empty;
        private int isSystem;
        private string key = string.Empty;
        private string note = string.Empty;

        public string EmailContent
        {
            get
            {
                return this.emailContent;
            }
            set
            {
                this.emailContent = value;
            }
        }

        public string EmailTitle
        {
            get
            {
                return this.emailTitle;
            }
            set
            {
                this.emailTitle = value;
            }
        }

        public int IsSystem
        {
            get
            {
                return this.isSystem;
            }
            set
            {
                this.isSystem = value;
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
    }
}

