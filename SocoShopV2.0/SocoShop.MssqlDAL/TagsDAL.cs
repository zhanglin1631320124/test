namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class TagsDAL : ITags
    {
        public int AddTags(TagsInfo tags)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@word", SqlDbType.NVarChar), new SqlParameter("@color", SqlDbType.NVarChar), new SqlParameter("@size", SqlDbType.Int), new SqlParameter("@isTop", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = tags.ProductID;
            pt[1].Value = tags.Word;
            pt[2].Value = tags.Color;
            pt[3].Value = tags.Size;
            pt[4].Value = tags.IsTop;
            pt[5].Value = tags.UserID;
            pt[6].Value = tags.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddTags", pt));
        }

        public void DeleteTags(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteTags", pt);
        }

        public void DeleteTagsByProductID(string strProductID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NVarChar) };
            pt[0].Value = strProductID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteTagsByProductID", pt);
        }

        public void PrepareTagsModel(SqlDataReader dr, List<TagsInfo> tagsList)
        {
            while (dr.Read())
            {
                TagsInfo item = new TagsInfo();
                item.ID = dr.GetInt32(0);
                item.ProductID = dr.GetInt32(1);
                item.Word = dr[2].ToString();
                item.Color = dr[3].ToString();
                item.Size = dr.GetInt32(4);
                item.IsTop = dr.GetInt32(5);
                item.UserID = dr.GetInt32(6);
                item.UserName = dr[7].ToString();
                tagsList.Add(item);
            }
        }

        public TagsInfo ReadTags(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            TagsInfo info = new TagsInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadTags", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.ProductID = reader.GetInt32(1);
                    info.Word = reader[2].ToString();
                    info.Color = reader[3].ToString();
                    info.Size = reader.GetInt32(4);
                    info.IsTop = reader.GetInt32(5);
                    info.UserID = reader.GetInt32(6);
                    info.UserName = reader[7].ToString();
                }
            }
            return info;
        }

        public string ReadTagsIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadTagsIDList", pt))
            {
                while (reader.Read())
                {
                    if (str == string.Empty)
                        str = reader.GetInt32(0).ToString();
                    else
                        str = str + "," + reader.GetInt32(0).ToString();
                }
            }
            return str;
        }

        public List<TagsInfo> SearchTagsList(TagsSearchInfo tagsSearch)
        {
            MssqlCondition condition = new MssqlCondition();
            condition.Add("[Word]", tagsSearch.Word, ConditionType.Like);
            condition.Add("[IsTop]", tagsSearch.IsTop, ConditionType.Equal);
            condition.Add("[ProductID]", tagsSearch.ProductID, ConditionType.Equal);
            condition.Add("[UserID]", tagsSearch.UserID, ConditionType.Equal);
            List<TagsInfo> tagsList = new List<TagsInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = condition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchTagsList", pt))
            {
                this.PrepareTagsModel(reader, tagsList);
            }
            return tagsList;
        }

        public List<TagsInfo> SearchTagsList(int currentPage, int pageSize, TagsSearchInfo tagsSearch, ref int count)
        {
            List<TagsInfo> list = new List<TagsInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = " " + ShopMssqlHelper.TablePrefix + "Tags INNER JOIN " + ShopMssqlHelper.TablePrefix + "Product ON " + ShopMssqlHelper.TablePrefix + "Tags.[ProductID]=" + ShopMssqlHelper.TablePrefix + "Product.[ID] ";
            class2.Fields = ShopMssqlHelper.TablePrefix + "Tags.[ID],[ProductID],[Word]," + ShopMssqlHelper.TablePrefix + "Tags.[Color],[Size]," + ShopMssqlHelper.TablePrefix + "Tags.[IsTop],[UserID],[UserName]," + ShopMssqlHelper.TablePrefix + "Product.[Name]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = ShopMssqlHelper.TablePrefix + "Tags.[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add(ShopMssqlHelper.TablePrefix + "Tags.[Word]", tagsSearch.Word, ConditionType.Like);
            class2.MssqlCondition.Add(ShopMssqlHelper.TablePrefix + "Tags.[IsTop]", tagsSearch.IsTop, ConditionType.Equal);
            class2.MssqlCondition.Add(ShopMssqlHelper.TablePrefix + "Product.[Name]", tagsSearch.ProductName, ConditionType.Like);
            class2.MssqlCondition.Add("[UserID]", tagsSearch.UserID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                while (reader.Read())
                {
                    TagsInfo item = new TagsInfo();
                    item.ID = reader.GetInt32(0);
                    item.ProductID = reader.GetInt32(1);
                    item.Word = reader[2].ToString();
                    item.Color = reader[3].ToString();
                    item.Size = reader.GetInt32(4);
                    item.IsTop = reader.GetInt32(5);
                    item.UserID = reader.GetInt32(6);
                    item.UserName = reader[7].ToString();
                    item.Product.Name = reader[8].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public void UpdateTags(TagsInfo tags)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@word", SqlDbType.NVarChar), new SqlParameter("@color", SqlDbType.NVarChar), new SqlParameter("@size", SqlDbType.Int), new SqlParameter("@isTop", SqlDbType.Int) };
            pt[0].Value = tags.ID;
            pt[1].Value = tags.ProductID;
            pt[2].Value = tags.Word;
            pt[3].Value = tags.Color;
            pt[4].Value = tags.Size;
            pt[5].Value = tags.IsTop;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateTags", pt);
        }

        public void UpdateTagsIsTop(int id, int status)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@isTop", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = status;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateTagsIsTop", pt);
        }
    }
}

