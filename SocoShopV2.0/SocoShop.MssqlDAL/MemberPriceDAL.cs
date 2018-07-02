namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class MemberPriceDAL : IMemberPrice
    {
        public void AddMemberPrice(MemberPriceInfo memberPrice)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@gradeID", SqlDbType.Int), new SqlParameter("@price", SqlDbType.Decimal) };
            pt[0].Value = memberPrice.ProductID;
            pt[1].Value = memberPrice.GradeID;
            pt[2].Value = memberPrice.Price;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "AddMemberPrice", pt);
        }

        public void DeleteMemberPriceByGradeID(string strGradeID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strGradeID", SqlDbType.NVarChar) };
            pt[0].Value = strGradeID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteMemberPriceByGradeID", pt);
        }

        public void DeleteMemberPriceByProductID(string strProductID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NVarChar) };
            pt[0].Value = strProductID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteMemberPriceByProductID", pt);
        }

        public void PrepareMemberPriceModel(SqlDataReader dr, List<MemberPriceInfo> memberPriceList)
        {
            while (dr.Read())
            {
                MemberPriceInfo item = new MemberPriceInfo();
                item.ProductID = dr.GetInt32(0);
                item.GradeID = dr.GetInt32(1);
                item.Price = dr.GetDecimal(2);
                memberPriceList.Add(item);
            }
        }

        public List<MemberPriceInfo> ReadMemberPriceByProduct(int productID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int) };
            pt[0].Value = productID;
            List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadMemberPriceByProduct", pt))
            {
                this.PrepareMemberPriceModel(reader, memberPriceList);
            }
            return memberPriceList;
        }

        public List<MemberPriceInfo> ReadMemberPriceByProduct(string strProductID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NChar) };
            pt[0].Value = strProductID;
            List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadMemberPriceByStrProduct", pt))
            {
                this.PrepareMemberPriceModel(reader, memberPriceList);
            }
            return memberPriceList;
        }

        public List<MemberPriceInfo> ReadMemberPriceByProductGrade(string strProductID, int gradeID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NChar), new SqlParameter("@gradeID", SqlDbType.Int) };
            pt[0].Value = strProductID;
            pt[1].Value = gradeID;
            List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadMemberPriceByProductGrade", pt))
            {
                this.PrepareMemberPriceModel(reader, memberPriceList);
            }
            return memberPriceList;
        }
    }
}

