namespace SocoShop.Entity
{
    using System;

    public class TemplatePluginsInfo
    {
        private string copyRight = string.Empty;
        private string disCreateFile = string.Empty;
        private string name = string.Empty;
        private string path = string.Empty;
        private string photo = string.Empty;
        private string publishDate = string.Empty;

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

        public string DisCreateFile
        {
            get
            {
                return this.disCreateFile;
            }
            set
            {
                this.disCreateFile = value;
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

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
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

        public string PublishDate
        {
            get
            {
                return this.publishDate;
            }
            set
            {
                this.publishDate = value;
            }
        }
    }
}

