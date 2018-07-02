namespace SocoShop.Entity
{
    using System;

    public sealed class FlashInfo
    {
        private int height;
        private int id;
        private string introduce = string.Empty;
        private int photoCount;
        private string title = string.Empty;
        private int width;

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

        public string Introduce
        {
            get
            {
                return this.introduce;
            }
            set
            {
                this.introduce = value;
            }
        }

        public int PhotoCount
        {
            get
            {
                return this.photoCount;
            }
            set
            {
                this.photoCount = value;
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

