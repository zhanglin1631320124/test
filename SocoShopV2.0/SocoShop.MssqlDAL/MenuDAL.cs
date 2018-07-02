namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class MenuDAL : IMenu
    {
        public int AddMenu(MenuInfo Menu)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@menuName", SqlDbType.NVarChar), new SqlParameter("@menuImage", SqlDbType.Int), new SqlParameter("@uRL", SqlDbType.NVarChar), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@iP", SqlDbType.NVarChar) };
            pt[0].Value = Menu.FatherID;
            pt[1].Value = Menu.OrderID;
            pt[2].Value = Menu.MenuName;
            pt[3].Value = Menu.MenuImage;
            pt[4].Value = Menu.URL;
            pt[5].Value = Menu.Date;
            pt[6].Value = Menu.IP;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddMenu", pt));
        }

        public void DeleteMenu(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteMenu", pt);
        }

        public void MoveDownMenu(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveDownMenu", pt);
        }

        public void MoveUpMenu(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveUpMenu", pt);
        }

        public void PrepareMenuModel(SqlDataReader dr, List<MenuInfo> MenuList)
        {
            while (dr.Read())
            {
                MenuInfo item = new MenuInfo();
                item.ID = dr.GetInt32(0);
                item.FatherID = dr.GetInt32(1);
                item.OrderID = dr.GetInt32(2);
                item.MenuName = dr[3].ToString();
                item.MenuImage = dr.GetInt32(4);
                item.URL = dr[5].ToString();
                item.Date = dr.GetDateTime(6);
                item.IP = dr[7].ToString();
                MenuList.Add(item);
            }
        }

        public List<MenuInfo> ReadMenuAllList()
        {
            List<MenuInfo> menuList = new List<MenuInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadMenuAllList"))
            {
                this.PrepareMenuModel(reader, menuList);
            }
            return menuList;
        }

        public void UpdateMenu(MenuInfo Menu)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@menuName", SqlDbType.NVarChar), new SqlParameter("@menuImage", SqlDbType.Int), new SqlParameter("@uRL", SqlDbType.NVarChar) };
            pt[0].Value = Menu.ID;
            pt[1].Value = Menu.FatherID;
            pt[2].Value = Menu.OrderID;
            pt[3].Value = Menu.MenuName;
            pt[4].Value = Menu.MenuImage;
            pt[5].Value = Menu.URL;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateMenu", pt);
        }
    }
}

