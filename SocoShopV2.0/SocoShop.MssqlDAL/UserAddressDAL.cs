namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserAddressDAL : IUserAddress
    {
        public int AddUserAddress(UserAddressInfo userAddress)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@consignee", SqlDbType.NVarChar), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@address", SqlDbType.NVarChar), new SqlParameter("@zipCode", SqlDbType.NVarChar), new SqlParameter("@tel", SqlDbType.NVarChar), new SqlParameter("@mobile", SqlDbType.NVarChar), new SqlParameter("@isDefault", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = userAddress.Consignee;
            pt[1].Value = userAddress.RegionID;
            pt[2].Value = userAddress.Address;
            pt[3].Value = userAddress.ZipCode;
            pt[4].Value = userAddress.Tel;
            pt[5].Value = userAddress.Mobile;
            pt[6].Value = userAddress.IsDefault;
            pt[7].Value = userAddress.UserID;
            pt[8].Value = userAddress.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserAddress", pt));
        }

        public void DeleteUserAddress(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserAddress", pt);
        }

        public void DeleteUserAddressByUserID(string strUserID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strUserID", SqlDbType.NVarChar) };
            pt[0].Value = strUserID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserAddressByUserID", pt);
        }

        public void PrepareUserAddressModel(SqlDataReader dr, List<UserAddressInfo> userAddressList)
        {
            while (dr.Read())
            {
                UserAddressInfo item = new UserAddressInfo();
                item.ID = dr.GetInt32(0);
                item.Consignee = dr[1].ToString();
                item.RegionID = dr[2].ToString();
                item.Address = dr[3].ToString();
                item.ZipCode = dr[4].ToString();
                item.Tel = dr[5].ToString();
                item.Mobile = dr[6].ToString();
                item.IsDefault = dr.GetInt32(7);
                item.UserID = dr.GetInt32(8);
                item.UserName = dr[9].ToString();
                userAddressList.Add(item);
            }
        }

        public UserAddressInfo ReadUserAddress(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            UserAddressInfo info = new UserAddressInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserAddress", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Consignee = reader[1].ToString();
                    info.RegionID = reader[2].ToString();
                    info.Address = reader[3].ToString();
                    info.ZipCode = reader[4].ToString();
                    info.Tel = reader[5].ToString();
                    info.Mobile = reader[6].ToString();
                    info.IsDefault = reader.GetInt32(7);
                    info.UserID = reader.GetInt32(8);
                    info.UserName = reader[9].ToString();
                }
            }
            return info;
        }

        public List<UserAddressInfo> ReadUserAddressByUser(int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = userID;
            List<UserAddressInfo> userAddressList = new List<UserAddressInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserAddressByUser", pt))
            {
                this.PrepareUserAddressModel(reader, userAddressList);
            }
            return userAddressList;
        }

        public void UpdateUserAddress(UserAddressInfo userAddress)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@consignee", SqlDbType.NVarChar), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@address", SqlDbType.NVarChar), new SqlParameter("@zipCode", SqlDbType.NVarChar), new SqlParameter("@tel", SqlDbType.NVarChar), new SqlParameter("@mobile", SqlDbType.NVarChar), new SqlParameter("@isDefault", SqlDbType.Int) };
            pt[0].Value = userAddress.ID;
            pt[1].Value = userAddress.Consignee;
            pt[2].Value = userAddress.RegionID;
            pt[3].Value = userAddress.Address;
            pt[4].Value = userAddress.ZipCode;
            pt[5].Value = userAddress.Tel;
            pt[6].Value = userAddress.Mobile;
            pt[7].Value = userAddress.IsDefault;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserAddress", pt);
        }

        public void UpdateUserAddressIsDefault(int isDefault, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@isDefault", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = isDefault;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserAddressIsDefault", pt);
        }
    }
}

