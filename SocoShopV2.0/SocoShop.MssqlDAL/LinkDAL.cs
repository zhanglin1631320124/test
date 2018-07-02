namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class LinkDAL : ILink
    {
        public int AddLink(LinkInfo link)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@linkClass", SqlDbType.Int), new SqlParameter("@display", SqlDbType.NVarChar), new SqlParameter("@uRL", SqlDbType.NVarChar), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@remark", SqlDbType.NVarChar) };
            pt[0].Value = link.LinkClass;
            pt[1].Value = link.Display;
            pt[2].Value = link.URL;
            pt[3].Value = link.OrderID;
            pt[4].Value = link.Remark;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddLink", pt));
        }

        public void ChangeLinkOrder(ChangeAction action, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@action", SqlDbType.NVarChar), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = action.ToString();
            pt[1].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeLinkOrder", pt);
        }

        public void DeleteLink(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteLink", pt);
        }

        public void PrepareLinkModel(SqlDataReader dr, List<LinkInfo> linkList)
        {
            while (dr.Read())
            {
                LinkInfo item = new LinkInfo();
                item.ID = dr.GetInt32(0);
                item.LinkClass = dr.GetInt32(1);
                item.Display = dr[2].ToString();
                item.URL = dr[3].ToString();
                item.OrderID = dr.GetInt32(4);
                item.Remark = dr[5].ToString();
                linkList.Add(item);
            }
        }

        public List<LinkInfo> ReadLinkAllList()
        {
            List<LinkInfo> linkList = new List<LinkInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadLinkAllList"))
            {
                this.PrepareLinkModel(reader, linkList);
            }
            return linkList;
        }

        public void UpdateLink(LinkInfo link)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@linkClass", SqlDbType.Int), new SqlParameter("@display", SqlDbType.NVarChar), new SqlParameter("@uRL", SqlDbType.NVarChar), new SqlParameter("@remark", SqlDbType.NVarChar) };
            pt[0].Value = link.ID;
            pt[1].Value = link.LinkClass;
            pt[2].Value = link.Display;
            pt[3].Value = link.URL;
            pt[4].Value = link.Remark;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateLink", pt);
        }
    }
}

