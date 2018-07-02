namespace SocoShop.Entity
{
    using System;

    public sealed class GiftSearchInfo
    {
        private string inGiftID = string.Empty;
        private string name = string.Empty;

        public string InGiftID
        {
            get
            {
                return this.inGiftID;
            }
            set
            {
                this.inGiftID = value;
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

