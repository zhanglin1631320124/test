namespace SocoShop.Entity
{
    using System;
    using System.Collections.Generic;

    public sealed class CartCommonProductVirtualInfo
    {
        private List<CartInfo> childCartList = new List<CartInfo>();
        private CartInfo fatherCart = new CartInfo();
        private string strCartID = string.Empty;
        private string strProductID = string.Empty;

        public List<CartInfo> ChildCartList
        {
            get
            {
                return this.childCartList;
            }
            set
            {
                this.childCartList = value;
            }
        }

        public CartInfo FatherCart
        {
            get
            {
                return this.fatherCart;
            }
            set
            {
                this.fatherCart = value;
            }
        }

        public string StrCartID
        {
            get
            {
                return this.strCartID;
            }
            set
            {
                this.strCartID = value;
            }
        }

        public string StrProductID
        {
            get
            {
                return this.strProductID;
            }
            set
            {
                this.strProductID = value;
            }
        }
    }
}

