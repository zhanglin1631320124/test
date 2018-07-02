namespace SocoShop.Entity
{
    using System;

    [Serializable]
    public sealed class MemberPriceInfo
    {
        private int gradeID;
        private decimal price;
        private int productID;

        public int GradeID
        {
            get
            {
                return this.gradeID;
            }
            set
            {
                this.gradeID = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
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
    }
}

