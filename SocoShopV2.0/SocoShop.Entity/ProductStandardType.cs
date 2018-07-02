namespace SocoShop.Entity
{
    using SkyCES.EntLib;
    using System;

    public enum ProductStandardType
    {
        [Enum("产品组规格")]
        Group = 2,
        [Enum("无规格")]
        No = 0,
        [Enum("单产品规格")]
        Single = 1
    }
}

