namespace SocoShop.Page
{
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserTags : UserBasePage
    {
        protected override void PageLoad()
        {
            base.PageLoad();
            TagsSearchInfo tags = new TagsSearchInfo();
            tags.UserID = base.UserID;
            this.tagsList = TagsBLL.SearchTagsList(tags);
        }
    }
}

