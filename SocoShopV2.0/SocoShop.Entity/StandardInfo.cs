namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class StandardInfo
    {
        private int displayTye;
        private int id;
        private string name = string.Empty;
        private string photoList = string.Empty;
        private string valueList = string.Empty;

        public int DisplayTye
        {
            get
            {
                return this.displayTye;
            }
            set
            {
                this.displayTye = value;
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

        public string PhotoList
        {
            get
            {
                return this.photoList;
            }
            set
            {
                this.photoList = value;
            }
        }

        public string ValueList
        {
            get
            {
                return this.valueList;
            }
            set
            {
                this.valueList = value;
            }
        }
    }
}

