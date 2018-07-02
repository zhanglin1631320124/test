namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class AttributeClassInfo
    {
        private int attributeCount;
        private int id;
        private string name = string.Empty;

        public int AttributeCount
        {
            get
            {
                return this.attributeCount;
            }
            set
            {
                this.attributeCount = value;
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
    }
}

