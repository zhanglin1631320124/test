namespace SocoShop.Entity
{
    using System;
    using System.Collections.Generic;

    public sealed class OrderCommonProductVirtualInfo
    {
        private List<OrderDetailInfo> childOrderDetailList = new List<OrderDetailInfo>();
        private OrderDetailInfo fatherOrderDetail = new OrderDetailInfo();
        private string strOrderDetailID = string.Empty;
        private string strProductID = string.Empty;

        public List<OrderDetailInfo> ChildOrderDetailList
        {
            get
            {
                return this.childOrderDetailList;
            }
            set
            {
                this.childOrderDetailList = value;
            }
        }

        public OrderDetailInfo FatherOrderDetail
        {
            get
            {
                return this.fatherOrderDetail;
            }
            set
            {
                this.fatherOrderDetail = value;
            }
        }

        public string StrOrderDetailID
        {
            get
            {
                return this.strOrderDetailID;
            }
            set
            {
                this.strOrderDetailID = value;
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

