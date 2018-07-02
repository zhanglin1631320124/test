namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class FlashPhotoDAL : IFlashPhoto
    {
        public int AddFlashPhoto(FlashPhotoInfo flashPhoto)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@flashID", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@fileName", SqlDbType.NVarChar), new SqlParameter("@uRL", SqlDbType.NVarChar), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@date", SqlDbType.DateTime) };
            pt[0].Value = flashPhoto.FlashID;
            pt[1].Value = flashPhoto.Title;
            pt[2].Value = flashPhoto.FileName;
            pt[3].Value = flashPhoto.URL;
            pt[4].Value = flashPhoto.OrderID;
            pt[5].Value = flashPhoto.Date;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddFlashPhoto", pt));
        }

        public void ChangeFlashPhotoOrder(ChangeAction action, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@action", SqlDbType.NVarChar), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = action.ToString();
            pt[1].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeFlashPhotoOrder", pt);
        }

        public void DeleteFlashPhoto(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteFlashPhoto", pt);
        }

        public void DeleteFlashPhotoByFlashID(string strFlashID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strFlashID", SqlDbType.NVarChar) };
            pt[0].Value = strFlashID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteFlashPhotoByFlashID", pt);
        }

        public void PrepareFlashPhotoModel(SqlDataReader dr, List<FlashPhotoInfo> flashPhotoList)
        {
            while (dr.Read())
            {
                FlashPhotoInfo item = new FlashPhotoInfo();
                item.ID = dr.GetInt32(0);
                item.FlashID = dr.GetInt32(1);
                item.Title = dr[2].ToString();
                item.FileName = dr[3].ToString();
                item.URL = dr[4].ToString();
                item.OrderID = dr.GetInt32(5);
                item.Date = dr.GetDateTime(6);
                flashPhotoList.Add(item);
            }
        }

        public FlashPhotoInfo ReadFlashPhoto(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            FlashPhotoInfo info = new FlashPhotoInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadFlashPhoto", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.FlashID = reader.GetInt32(1);
                    info.Title = reader[2].ToString();
                    info.FileName = reader[3].ToString();
                    info.URL = reader[4].ToString();
                    info.OrderID = reader.GetInt32(5);
                    info.Date = reader.GetDateTime(6);
                }
            }
            return info;
        }

        public List<FlashPhotoInfo> ReadFlashPhotoByFlash(int flashID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@flashID", SqlDbType.Int) };
            pt[0].Value = flashID;
            List<FlashPhotoInfo> flashPhotoList = new List<FlashPhotoInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadFlashPhotoByFlash", pt))
            {
                this.PrepareFlashPhotoModel(reader, flashPhotoList);
            }
            return flashPhotoList;
        }

        public void UpdateFlashPhoto(FlashPhotoInfo flashPhoto)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@flashID", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@fileName", SqlDbType.NVarChar), new SqlParameter("@uRL", SqlDbType.NVarChar) };
            pt[0].Value = flashPhoto.ID;
            pt[1].Value = flashPhoto.FlashID;
            pt[2].Value = flashPhoto.Title;
            pt[3].Value = flashPhoto.FileName;
            pt[4].Value = flashPhoto.URL;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateFlashPhoto", pt);
        }
    }
}

