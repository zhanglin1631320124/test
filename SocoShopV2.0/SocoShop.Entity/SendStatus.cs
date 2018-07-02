namespace SocoShop.Entity
{
    using SkyCES.EntLib;
    using System;

    public enum SendStatus
    {
        [Enum("发送完成")]
        Finished = 3,
        [Enum("未发送")]
        No = 1,
        [Enum("发送中")]
        Sending = 2
    }
}

