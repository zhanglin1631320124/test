namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;

    public class CheckOutAddressAjax : AjaxBasePage
    {
        protected SingleUnlimitClass singleUnlimitClass = new SingleUnlimitClass();
        protected UserAddressInfo userAddress = new UserAddressInfo();

        protected override void PageLoad()
        {
            base.PageLoad();
            this.singleUnlimitClass.DataSource = RegionBLL.ReadRegionUnlimitClass();
            this.singleUnlimitClass.FunctionName = "readShippingList()";
            int queryString = RequestHelper.GetQueryString<int>("ID");
            if (queryString > 0)
            {
                this.userAddress = UserAddressBLL.ReadUserAddress(queryString, base.UserID);
                this.singleUnlimitClass.ClassID = this.userAddress.RegionID;
            }
        }
    }
}

