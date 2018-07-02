namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class VoteRecordDAL : IVoteRecord
    {
        public int AddVoteRecord(VoteRecordInfo voteRecord)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@voteID", SqlDbType.Int), new SqlParameter("@itemID", SqlDbType.NVarChar), new SqlParameter("@userIP", SqlDbType.NVarChar), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = voteRecord.VoteID;
            pt[1].Value = voteRecord.ItemID;
            pt[2].Value = voteRecord.UserIP;
            pt[3].Value = voteRecord.AddDate;
            pt[4].Value = voteRecord.UserID;
            pt[5].Value = voteRecord.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddVoteRecord", pt));
        }

        public void DeleteVoteRecord(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteVoteRecord", pt);
        }

        public void DeleteVoteRecordByItemID(string strItemID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strItemID", SqlDbType.NVarChar) };
            pt[0].Value = strItemID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteVoteRecordByItemID", pt);
        }

        public void DeleteVoteRecordByVoteID(string strVoteID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strVoteID", SqlDbType.NVarChar) };
            pt[0].Value = strVoteID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteVoteRecordByVoteID", pt);
        }

        public void PrepareVoteRecordModel(SqlDataReader dr, List<VoteRecordInfo> voteRecordList)
        {
            while (dr.Read())
            {
                VoteRecordInfo item = new VoteRecordInfo();
                item.ID = dr.GetInt32(0);
                item.VoteID = dr.GetInt32(1);
                item.ItemID = dr[2].ToString();
                item.UserIP = dr[3].ToString();
                item.AddDate = dr.GetDateTime(4);
                item.UserID = dr.GetInt32(5);
                item.UserName = dr[6].ToString();
                voteRecordList.Add(item);
            }
        }

        public VoteRecordInfo ReadVoteRecord(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar) };
            pt[0].Value = id;
            VoteRecordInfo info = new VoteRecordInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadVoteRecord", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.VoteID = reader.GetInt32(1);
                    info.ItemID = reader[2].ToString();
                    info.UserIP = reader[3].ToString();
                    info.AddDate = reader.GetDateTime(4);
                    info.UserID = reader.GetInt32(5);
                    info.UserName = reader[6].ToString();
                }
            }
            return info;
        }

        public List<VoteRecordInfo> ReadVoteRecordList(int voteID, int currentPage, int pageSize, ref int count)
        {
            List<VoteRecordInfo> voteRecordList = new List<VoteRecordInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "VoteRecord";
            class2.Fields = "[ID],[VoteID],[ItemID],[UserIP],[AddDate],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[VoteID]", voteID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareVoteRecordModel(reader, voteRecordList);
            }
            return voteRecordList;
        }
    }
}

