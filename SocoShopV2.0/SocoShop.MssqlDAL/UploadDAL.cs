namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UploadDAL : IUpload
    {
        public void AddUpload(UploadInfo upload)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@tableID", SqlDbType.Int), new SqlParameter("@classID", SqlDbType.Int), new SqlParameter("@recordID", SqlDbType.Int), new SqlParameter("@uploadName", SqlDbType.NVarChar), new SqlParameter("@otherFile", SqlDbType.NVarChar), new SqlParameter("@size", SqlDbType.Int), new SqlParameter("@fileType", SqlDbType.NVarChar), new SqlParameter("@randomNumber", SqlDbType.NVarChar), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@iP", SqlDbType.NVarChar) };
            pt[0].Value = upload.TableID;
            pt[1].Value = upload.ClassID;
            pt[2].Value = upload.RecordID;
            pt[3].Value = upload.UploadName;
            pt[4].Value = upload.OtherFile;
            pt[5].Value = upload.Size;
            pt[6].Value = upload.FileType;
            pt[7].Value = upload.RandomNumber;
            pt[8].Value = upload.Date;
            pt[9].Value = upload.IP;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "AddUpload", pt);
        }

        public void DeleteUploadByClassID(int tableID, string strClassID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@tableID", SqlDbType.Int), new SqlParameter("@strClassID", SqlDbType.NVarChar) };
            pt[0].Value = tableID;
            pt[1].Value = strClassID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUploadByClassID", pt);
        }

        public void DeleteUploadByRecordID(int tableID, string strRecordID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@tableID", SqlDbType.Int), new SqlParameter("@strRecordID", SqlDbType.NVarChar) };
            pt[0].Value = tableID;
            pt[1].Value = strRecordID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUploadByRecordID", pt);
        }

        public void PrepareUploadModel(SqlDataReader dr, List<UploadInfo> uploadList)
        {
            while (dr.Read())
            {
                UploadInfo item = new UploadInfo();
                item.ID = dr.GetInt32(0);
                item.TableID = dr.GetInt32(1);
                item.ClassID = dr.GetInt32(2);
                item.RecordID = dr.GetInt32(3);
                item.UploadName = dr[4].ToString();
                item.OtherFile = dr[5].ToString();
                item.Size = dr.GetInt32(6);
                item.FileType = dr[7].ToString();
                item.RandomNumber = dr[8].ToString();
                item.Date = dr.GetDateTime(9);
                item.IP = dr[10].ToString();
                uploadList.Add(item);
            }
        }

        public List<UploadInfo> ReadUploadByClassID(int tableID, string strClassID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@tableID", SqlDbType.Int), new SqlParameter("@strClassID", SqlDbType.NVarChar) };
            pt[0].Value = tableID;
            pt[1].Value = strClassID;
            List<UploadInfo> uploadList = new List<UploadInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUploadByClassID", pt))
            {
                this.PrepareUploadModel(reader, uploadList);
            }
            return uploadList;
        }

        public List<UploadInfo> ReadUploadByRecordID(int tableID, string strRecordID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@tableID", SqlDbType.Int), new SqlParameter("@strRecordID", SqlDbType.NVarChar) };
            pt[0].Value = tableID;
            pt[1].Value = strRecordID;
            List<UploadInfo> uploadList = new List<UploadInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUploadByRecordID", pt))
            {
                this.PrepareUploadModel(reader, uploadList);
            }
            return uploadList;
        }

        public void UpdateUpload(int tableID, int classID, int recordID, string randomNumber)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@tableID", SqlDbType.Int), new SqlParameter("@classID", SqlDbType.Int), new SqlParameter("@recordID", SqlDbType.Int), new SqlParameter("@randomNumber", SqlDbType.NVarChar) };
            pt[0].Value = tableID;
            pt[1].Value = classID;
            pt[2].Value = recordID;
            pt[3].Value = randomNumber;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUpload", pt);
        }
    }
}

