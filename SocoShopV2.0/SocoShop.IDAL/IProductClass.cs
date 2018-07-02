namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IProductClass
    {
        int AddProductClass(ProductClassInfo productClass);
        void DeleteProductClass(int id);
        void DeleteTaobaoProductClass();
        void MoveDownProductClass(int id);
        void MoveUpProductClass(int id);
        List<ProductClassInfo> ReadProductClassAllList();
        void UpdateProductClass(ProductClassInfo productClass);
        void UpdateProductFatherID(Dictionary<long, int> fatherIDDic);
    }
}

