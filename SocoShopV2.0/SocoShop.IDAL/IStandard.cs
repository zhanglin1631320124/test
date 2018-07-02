namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IStandard
    {
        int AddStandard(StandardInfo standard);
        void DeleteStandard(string strID);
        List<StandardInfo> ReadStandardAllList();
        void UpdateStandard(StandardInfo standard);
    }
}

