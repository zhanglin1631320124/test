namespace SocoShop.Entity
{
    using System;

    public sealed class ArticleSearchInfo
    {
        private int allowComment = -2147483648;
        private string author = string.Empty;
        private string classID = string.Empty;
        private DateTime endDate = DateTime.MinValue;
        private string inArticleID = string.Empty;
        private int isTop = -2147483648;
        private string keywords = string.Empty;
        private string resource = string.Empty;
        private DateTime startDate = DateTime.MinValue;
        private string title = string.Empty;

        public int AllowComment
        {
            get
            {
                return this.allowComment;
            }
            set
            {
                this.allowComment = value;
            }
        }

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

        public string InArticleID
        {
            get
            {
                return this.inArticleID;
            }
            set
            {
                this.inArticleID = value;
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
    }
}

