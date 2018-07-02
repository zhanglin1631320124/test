namespace SocoShop.Entity
{
    using System;

    public sealed class ProductPhotoSearchInfo
    {
        private string name = string.Empty;
        private ProductInfo product = new ProductInfo();

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

        public ProductInfo Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
            }
        }
    }
}

