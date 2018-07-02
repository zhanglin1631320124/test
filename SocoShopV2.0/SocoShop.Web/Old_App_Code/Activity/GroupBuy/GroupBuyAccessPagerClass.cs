using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using SkyCES.EntLib;

namespace SocoShop.Web
{
    /// <summary>
    /// 读取数据列表类
    /// </summary>
    public partial class GroupBuyAccessPagerClass : AccessPagerClass
    {
        private int count = 0;   
        /// <summary>
        /// 返回记录总数
        /// </summary>
        public int Count
        {
            get
            {
                int result = 0;
                if (this.count != int.MinValue)
                {
                    object count = GroupBuyAccessHelper.ExecuteScalar(this.PrepareCountSQL());
                    if (count != null && count != DBNull.Value)
                    {
                        if (count.ToString() != "0")
                        {
                            result = Convert.ToInt32(count);
                        }
                    }
                }
                return result;
            }
            set
            {
                this.count = value;
            }
        }

        /// <summary>
        /// 返回DataReader对像
        /// </summary>
        /// <returns></returns>
        public override OleDbDataReader ExecuteReader()
        {
            return GroupBuyAccessHelper.ExecuteReader(this.PrepareSQL());
        }
        /// <summary>
        /// 返回DataTable对像
        /// </summary>
        /// <returns></returns>
        public override DataTable ExecuteDataTable()
        {
            return GroupBuyAccessHelper.ExecuteDataTable(this.PrepareSQL());
        }     
    }
}
