namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class CouponDAL : ICoupon
    {
        public int AddCoupon(CouponInfo coupon)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@money", SqlDbType.Decimal), new SqlParameter("@useMinAmount", SqlDbType.Decimal), new SqlParameter("@useStartDate", SqlDbType.DateTime), new SqlParameter("@useEndDate", SqlDbType.DateTime) };
            pt[0].Value = coupon.Name;
            pt[1].Value = coupon.Money;
            pt[2].Value = coupon.UseMinAmount;
            pt[3].Value = coupon.UseStartDate;
            pt[4].Value = coupon.UseEndDate;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddCoupon", pt));
        }

        public void DeleteCoupon(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteCoupon", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, CouponSearchInfo couponSearch)
        {
            mssqlCondition.Add("[Name]", couponSearch.Name, ConditionType.Like);
            mssqlCondition.Add("[ID]", couponSearch.InCouponID, ConditionType.In);
        }

        public void PrepareCouponModel(SqlDataReader dr, List<CouponInfo> couponList)
        {
            while (dr.Read())
            {
                CouponInfo item = new CouponInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Money = dr.GetDecimal(2);
                item.UseMinAmount = dr.GetDecimal(3);
                item.UseStartDate = dr.GetDateTime(4);
                item.UseEndDate = dr.GetDateTime(5);
                couponList.Add(item);
            }
        }

        public CouponInfo ReadCoupon(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            CouponInfo info = new CouponInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadCoupon", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Money = reader.GetDecimal(2);
                    info.UseMinAmount = reader.GetDecimal(3);
                    info.UseStartDate = reader.GetDateTime(4);
                    info.UseEndDate = reader.GetDateTime(5);
                }
            }
            return info;
        }

        public List<CouponInfo> SearchCouponList(CouponSearchInfo couponSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, couponSearch);
            List<CouponInfo> couponList = new List<CouponInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchCouponList", pt))
            {
                this.PrepareCouponModel(reader, couponList);
            }
            return couponList;
        }

        public List<CouponInfo> SearchCouponList(int currentPage, int pageSize, CouponSearchInfo couponSearch, ref int count)
        {
            List<CouponInfo> couponList = new List<CouponInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Coupon";
            class2.Fields = "[ID],[Name],[Money],[UseMinAmount],[UseStartDate],[UseEndDate]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, couponSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareCouponModel(reader, couponList);
            }
            return couponList;
        }

        public void UpdateCoupon(CouponInfo coupon)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@money", SqlDbType.Decimal), new SqlParameter("@useMinAmount", SqlDbType.Decimal), new SqlParameter("@useStartDate", SqlDbType.DateTime), new SqlParameter("@useEndDate", SqlDbType.DateTime) };
            pt[0].Value = coupon.ID;
            pt[1].Value = coupon.Name;
            pt[2].Value = coupon.Money;
            pt[3].Value = coupon.UseMinAmount;
            pt[4].Value = coupon.UseStartDate;
            pt[5].Value = coupon.UseEndDate;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateCoupon", pt);
        }
    }
}

