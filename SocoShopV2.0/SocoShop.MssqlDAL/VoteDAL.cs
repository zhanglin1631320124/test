namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class VoteDAL : IVote
    {
        public int AddVote(VoteInfo vote)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@itemCount", SqlDbType.Int), new SqlParameter("@voteType", SqlDbType.Int), new SqlParameter("@note", SqlDbType.NVarChar) };
            pt[0].Value = vote.Title;
            pt[1].Value = vote.ItemCount;
            pt[2].Value = vote.VoteType;
            pt[3].Value = vote.Note;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddVote", pt));
        }

        public void ChangeVoteCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeVoteCount", pt);
        }

        public void ChangeVoteCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeVoteCountByGeneral", pt);
        }

        public void DeleteVote(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteVote", pt);
        }

        public void PrepareVoteModel(SqlDataReader dr, List<VoteInfo> voteList)
        {
            while (dr.Read())
            {
                VoteInfo item = new VoteInfo();
                item.ID = dr.GetInt32(0);
                item.Title = dr[1].ToString();
                item.ItemCount = dr.GetInt32(2);
                item.VoteType = dr.GetInt32(3);
                item.Note = dr[4].ToString();
                voteList.Add(item);
            }
        }

        public VoteInfo ReadVote(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            VoteInfo info = new VoteInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadVote", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Title = reader[1].ToString();
                    info.ItemCount = reader.GetInt32(2);
                    info.VoteType = reader.GetInt32(3);
                    info.Note = reader[4].ToString();
                }
            }
            return info;
        }

        public List<VoteInfo> ReadVoteList(int currentPage, int pageSize, ref int count)
        {
            List<VoteInfo> voteList = new List<VoteInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Vote";
            class2.Fields = "[ID],[Title],[ItemCount],[VoteType],[Note]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareVoteModel(reader, voteList);
            }
            return voteList;
        }

        public void UpdateVote(VoteInfo vote)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@voteType", SqlDbType.Int), new SqlParameter("@note", SqlDbType.NVarChar) };
            pt[0].Value = vote.ID;
            pt[1].Value = vote.Title;
            pt[2].Value = vote.VoteType;
            pt[3].Value = vote.Note;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateVote", pt);
        }
    }
}

