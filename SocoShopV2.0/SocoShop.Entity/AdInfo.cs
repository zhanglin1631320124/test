namespace SocoShop.Entity
{
    using System;

    public sealed class AdInfo
    {
        private int adClass;
        private int clickCount;
        private string display = string.Empty;
        private DateTime endDate = DateTime.Now;
        private int height;
        private int id;
        private string introduction = string.Empty;
        private int isEnabled;
        private string remark = string.Empty;
        private DateTime startDate = DateTime.Now;
        private string title = string.Empty;
        private string url = string.Empty;
        private int width;

        public int AdClass
        {
            get
            {
                return this.adClass;
            }
            set
            {
                this.adClass = value;
            }
        }

        public int ClickCount
        {
            get
            {
                return this.clickCount;
            }
            set
            {
                this.clickCount = value;
            }
        }

        public string Display
        {
            get
            {
                return this.display;
            }
            set
            {
                this.display = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
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

        public string Introduction
        {
            get
            {
                return this.introduction;
            }
            set
            {
                this.introduction = value;
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

        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
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

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }
    }
}

