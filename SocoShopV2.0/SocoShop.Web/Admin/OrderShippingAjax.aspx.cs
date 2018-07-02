namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;

    public partial class OrderShippingAjax : AdminBasePage
    {
        protected int orderShippingID = 0;
        protected List<ShippingInfo> shippingList = new List<ShippingInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ClearCache();
            this.orderShippingID = RequestHelper.GetQueryString<int>("ShippingID");
            string queryString = RequestHelper.GetQueryString<string>("RegionID");
            if (queryString != string.Empty)
            {
                List<ShippingInfo> list = ShippingBLL.ReadShippingIsEnabledCacheList();
                string strShippingID = string.Empty;
                foreach (ShippingInfo info in list)
                {
                    if (strShippingID == string.Empty)
                        strShippingID = info.ID.ToString();
                    else
                        strShippingID = strShippingID + "," + info.ID.ToString();
                }
                List<ShippingRegionInfo> list2 = ShippingRegionBLL.ReadShippingRegionByShipping(strShippingID);
                foreach (ShippingInfo info in list)
                {
                    for (string str3 = queryString; str3.Length >= 1; str3 = str3.Substring(0, str3.LastIndexOf('|') + 1))
                    {
                        bool flag = false;
                        foreach (ShippingRegionInfo info2 in list2)
                        {
                            if (("|" + info2.RegionID + "|").IndexOf("|" + str3 + "|") > -1 && info2.ShippingID == info.ID)
                            {
                                flag = true;
                                this.shippingList.Add(info);
                                break;
                            }
                        }
                        if (flag) break;
                        str3 = str3.Substring(0, str3.Length - 1);
                    }
                }
            }
        }
    }
}

