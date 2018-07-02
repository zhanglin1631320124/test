namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class AttributeRecordInfo
    {
        private int attributeID;
        private int productID;
        private string value = string.Empty;

        public int AttributeID
        {
            get
            {
                return this.attributeID;
            }
            set
            {
                this.attributeID = value;
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
    }
}

