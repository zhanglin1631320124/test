namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class VoteItemDAL : IVoteItem
    {
        public int AddVoteItem(VoteItemInfo voteItem)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@voteID", SqlDbType.Int), new SqlParameter("@itemName", SqlDbType.NVarChar), new SqlParameter("@voteCount", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int) };
            pt[0].Value = voteItem.VoteID;
            pt[1].Value = voteItem.ItemName;
            pt[2].Value = voteItem.VoteCount;
            pt[3].Value = voteItem.OrderID;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddVoteItem", pt));
        }

        public void ChangeVoteItemCount(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeVoteItemCount", pt);
        }

        public void ChangeVoteItemCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeVoteItemCountByGeneral", pt);
        }

        public void ChangeVoteItemOrder(ChangeAction action, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@action", SqlDbType.NVarChar), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = action.ToString();
            pt[1].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeVoteItemOrder", pt);
        }

        public void DeleteVoteItem(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteVoteItem", pt);
        }

        public void DeleteVoteItemByVoteID(string strVoteID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strVoteID", SqlDbType.NVarChar) };
            pt[0].Value = strVoteID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteVoteItemByVoteID", pt);
        }

        public void PrepareVoteItemModel(SqlDataReader dr, List<VoteItemInfo> voteItemList)
        {
            while (dr.Read())
            {
                VoteItemInfo item = new VoteItemInfo();
                item.ID = dr.GetInt32(0);
                item.VoteID = dr.GetInt32(1);
                item.ItemName = dr[2].ToString();
                item.VoteCount = dr.GetInt32(3);
                item.OrderID = dr.GetInt32(4);
                voteItemList.Add(item);
            }
        }

        public VoteItemInfo ReadVoteItem(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar) };
            pt[0].Value = id;
            VoteItemInfo info = new VoteItemInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadVoteItem", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.VoteID = reader.GetInt32(1);
                    info.ItemName = reader[2].ToString();
                    info.VoteCount = reader.GetInt32(3);
                    info.OrderID = reader.GetInt32(4);
                }
            }
            return info;
        }

        public List<VoteItemInfo> ReadVoteItemAllList()
        {
            List<VoteItemInfo> voteItemList = new List<VoteItemInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadVoteItemAllList"))
            {
                this.PrepareVoteItemModel(reader, voteItemList);
            }
            return voteItemList;
        }

        public List<VoteItemInfo> ReadVoteItemByVote(int voteID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@voteID", SqlDbType.Int) };
            pt[0].Value = voteID;
            List<VoteItemInfo> voteItemList = new List<VoteItemInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadVoteItemByVote", pt))
            {
                this.PrepareVoteItemModel(reader, voteItemList);
            }
            return voteItemList;
        }

        public void UpdateVoteItem(VoteItemInfo voteItem)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@itemName", SqlDbType.NVarChar) };
            pt[0].Value = voteItem.ID;
            pt[1].Value = voteItem.ItemName;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateVoteItem", pt);
        }
    }
}

