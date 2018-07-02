namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class AttributeInfo
    {
        private int attributeClassID;
        private AttributeRecordInfo attributeRecord = new AttributeRecordInfo();
        private int id;
        private int inputType;
        private string inputValue = string.Empty;
        private string name = string.Empty;
        private int orderID;

        public int AttributeClassID
        {
            get
            {
                return this.attributeClassID;
            }
            set
            {
                this.attributeClassID = value;
            }
        }

        public AttributeRecordInfo AttributeRecord
        {
            get
            {
                return this.attributeRecord;
            }
            set
            {
                this.attributeRecord = value;
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

        public int InputType
        {
            get
            {
                return this.inputType;
            }
            set
            {
                this.inputType = value;
            }
        }

        public string InputValue
        {
            get
            {
                return this.inputValue;
            }
            set
            {
                this.inputValue = value;
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

        public int OrderID
        {
            get
            {
                return this.orderID;
            }
            set
            {
                this.orderID = value;
            }
        }
    }
}

