namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class UserGradeDAL : IUserGrade
    {
        public int AddUserGrade(UserGradeInfo userGrade)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@minMoney", SqlDbType.Decimal), new SqlParameter("@maxMoney", SqlDbType.Decimal), new SqlParameter("@discount", SqlDbType.Decimal) };
            pt[0].Value = userGrade.Name;
            pt[1].Value = userGrade.MinMoney;
            pt[2].Value = userGrade.MaxMoney;
            pt[3].Value = userGrade.Discount;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddUserGrade", pt));
        }

        public void DeleteUserGrade(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteUserGrade", pt);
        }

        public void PrepareUserGradeModel(SqlDataReader dr, List<UserGradeInfo> userGradeList)
        {
            while (dr.Read())
            {
                UserGradeInfo item = new UserGradeInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.MinMoney = dr.GetDecimal(2);
                item.MaxMoney = dr.GetDecimal(3);
                item.Discount = dr.GetDecimal(4);
                userGradeList.Add(item);
            }
        }

        public List<UserGradeInfo> ReadUserGradeAllList()
        {
            List<UserGradeInfo> userGradeList = new List<UserGradeInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadUserGradeAllList"))
            {
                this.PrepareUserGradeModel(reader, userGradeList);
            }
            return userGradeList;
        }

        public void UpdateUserGrade(UserGradeInfo userGrade)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@minMoney", SqlDbType.Decimal), new SqlParameter("@maxMoney", SqlDbType.Decimal), new SqlParameter("@discount", SqlDbType.Decimal) };
            pt[0].Value = userGrade.ID;
            pt[1].Value = userGrade.Name;
            pt[2].Value = userGrade.MinMoney;
            pt[3].Value = userGrade.MaxMoney;
            pt[4].Value = userGrade.Discount;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateUserGrade", pt);
        }
    }
}

