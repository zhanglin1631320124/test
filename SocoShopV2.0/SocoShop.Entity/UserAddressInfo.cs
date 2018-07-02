namespace SocoShop.Entity
{
    using System;

    public sealed class UserAddressInfo
    {
        private string address = string.Empty;
        private string consignee = string.Empty;
        private int id;
        private int isDefault;
        private string mobile = string.Empty;
        private string regionID = string.Empty;
        private string tel = string.Empty;
        private int userID;
        private string userName = string.Empty;
        private string zipCode = string.Empty;

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public string Consignee
        {
            get
            {
                return this.consignee;
            }
            set
            {
                this.consignee = value;
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

        public int IsDefault
        {
            get
            {
                return this.isDefault;
            }
            set
            {
                this.isDefault = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
            }
        }

        public string RegionID
        {
            get
            {
                return this.regionID;
            }
            set
            {
                this.regionID = value;
            }
        }

        public string Tel
        {
            get
            {
                return this.tel;
            }
            set
            {
                this.tel = value;
            }
        }

        public int UserID
        {
            get
            {
                return this.userID;
            }
            set
            {
                this.userID = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                this.zipCode = value;
            }
        }
    }
}

