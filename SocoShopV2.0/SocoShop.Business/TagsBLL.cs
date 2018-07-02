namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class TagsBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("HoTags");
        private static readonly ITags dal = FactoryHelper.Instance<ITags>(Global.DataProvider, "TagsDAL");

        public static int AddTags(TagsInfo tags)
        {
            tags.ID = dal.AddTags(tags);
            CacheHelper.Remove(cacheKey);
            return tags.ID;
        }

        public static void DeleteTags(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadTagsIDList(strID, userID);
            dal.DeleteTags(strID, userID);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteTagsByProductID(string strProductID)
        {
            dal.DeleteTagsByProductID(strProductID);
            CacheHelper.Remove(cacheKey);
        }

        public static List<TagsInfo> ReadHotTagsList()
        {
            if (CacheHelper.Read(cacheKey) == null)
            {
                TagsSearchInfo tags = new TagsSearchInfo();
                tags.IsTop = 1;
                CacheHelper.Write(cacheKey, SearchTagsList(tags));
            }
            return (List<TagsInfo>) CacheHelper.Read(cacheKey);
        }

        public static TagsInfo ReadTags(int id, int userID)
        {
            return dal.ReadTags(id, userID);
        }

        public static List<TagsInfo> SearchTagsList(TagsSearchInfo tags)
        {
            return dal.SearchTagsList(tags);
        }

        public static List<TagsInfo> SearchTagsList(int currentPage, int pageSize, TagsSearchInfo tags, ref int count)
        {
            return dal.SearchTagsList(currentPage, pageSize, tags, ref count);
        }

        public static void UpdateTags(TagsInfo tags)
        {
            dal.UpdateTags(tags);
            CacheHelper.Remove(cacheKey);
        }

        public static void UpdateTagsIsTop(int id, int status)
        {
            dal.UpdateTagsIsTop(id, status);
            CacheHelper.Remove(cacheKey);
        }
    }
}

