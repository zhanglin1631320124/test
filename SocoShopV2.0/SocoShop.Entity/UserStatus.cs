namespace SocoShop.Entity
{
    using SkyCES.EntLib;
    using System;

    public enum UserStatus
    {
        [Enum("冻结")]
        Frozen = 3,
        [Enum("未验证")]
        NoCheck = 1,
        [Enum("正常")]
        Normal = 2
    }
}

