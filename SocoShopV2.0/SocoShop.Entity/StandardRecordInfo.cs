namespace SocoShop.Entity
{
    using System;

    public sealed class StandardRecordInfo
    {
        private string groupTag = string.Empty;
        private int productID;
        private string standardIDList = string.Empty;
        private string valueList = string.Empty;

        public string GroupTag
        {
            get
            {
                return this.groupTag;
            }
            set
            {
                this.groupTag = value;
            }
        }

        public int ProductID
        {
            get
            {
                return this.productID;
            }
            set
            {
                this.productID = value;
            }
        }

        public string StandardIDList
        {
            get
            {
                return this.standardIDList;
            }
            set
            {
                this.standardIDList = value;
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

