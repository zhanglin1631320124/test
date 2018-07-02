namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface ITags
    {
        int AddTags(TagsInfo tags);
        void DeleteTags(string strID, int userID);
        void DeleteTagsByProductID(string strProductID);
        TagsInfo ReadTags(int id, int userID);
        string ReadTagsIDList(string strID, int userID);
        List<TagsInfo> SearchTagsList(TagsSearchInfo tags);
        List<TagsInfo> SearchTagsList(int currentPage, int pageSize, TagsSearchInfo tags, ref int count);
        void UpdateTags(TagsInfo tags);
        void UpdateTagsIsTop(int id, int status);
    }
}

