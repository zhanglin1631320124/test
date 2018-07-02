namespace SocoShop.Entity
{
    using System;

    public sealed class EmailSendRecordSearchInfo
    {
        private int isSystem = -2147483648;

        public int IsSystem
        {
            get
            {
                return this.isSystem;
            }
            set
            {
                this.isSystem = value;
            }
        }
    }
}

