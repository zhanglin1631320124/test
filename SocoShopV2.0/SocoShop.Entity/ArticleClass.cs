namespace SocoShop.Entity
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=1)]
    public struct ArticleClass
    {
        public const int News = 1;
        public const int Bottom = 2;
        public const int Product = 3;
        public const int Help = 4;
    }
}

