namespace SocoShop.Entity
{
    using System;

    public sealed class ArticleInfo
    {
        private string author = string.Empty;
        private string classID = string.Empty;
        private string content = string.Empty;
        private DateTime date = DateTime.Now;
        private int id;
        private int isTop;
        private string keywords = string.Empty;
        private string photo = string.Empty;
        private string resource = string.Empty;
        private string summary = string.Empty;
        private string title = string.Empty;
        private string url = string.Empty;

        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }

        public string ClassID
        {
            get
            {
                return this.classID;
            }
            set
            {
                this.classID = value;
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
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

        public int IsTop
        {
            get
            {
                return this.isTop;
            }
            set
            {
                this.isTop = value;
            }
        }

        public string Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
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

        public string Resource
        {
            get
            {
                return this.resource;
            }
            set
            {
                this.resource = value;
            }
        }

        public string Summary
        {
            get
            {
                return this.summary;
            }
            set
            {
                this.summary = value;
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
    }
}

