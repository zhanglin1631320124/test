namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface ILink
    {
        int AddLink(LinkInfo link);
        void ChangeLinkOrder(ChangeAction action, int id);
        void DeleteLink(string strID);
        List<LinkInfo> ReadLinkAllList();
        void UpdateLink(LinkInfo link);
    }
}

