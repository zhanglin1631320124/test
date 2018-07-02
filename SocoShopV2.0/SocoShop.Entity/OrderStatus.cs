namespace SocoShop.Entity
{
    using SkyCES.EntLib;
    using System;

    public enum OrderStatus
    {
        [Enum("已退货")]
        HasReturn = 7,
        [Enum("已发货")]
        HasShipping = 5,
        [Enum("无效")]
        NoEffect = 3,
        [Enum("已收货")]
        ReceiveShipping = 6,
        [Enum("配货中")]
        Shipping = 4,
        [Enum("待审核")]
        WaitCheck = 2,
        [Enum("待付款")]
        WaitPay = 1
    }
}

