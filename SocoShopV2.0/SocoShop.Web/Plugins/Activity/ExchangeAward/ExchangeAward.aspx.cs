using System;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SocoShop.Common;
using SocoShop.Business;
using SocoShop.Entity;
using SkyCES.EntLib;
using SocoShop.Page;

namespace SocoShop.Web
{
    public partial class ExchangeAward : PluginsBasePage
    {
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected SocoShop.Web.ExchangeAwardInfo exchangeAward = new ExchangeAwardInfo();
        protected Dictionary<int, int> awardDic = new Dictionary<int, int>();
        protected int pointLeft = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            exchangeAward = ExchangeAwardBLL.ReadConfigInfo();
            if (exchangeAward.PorudctIDList != string.Empty)
            {
                string[] productArray = exchangeAward.PorudctIDList.Split(',');
                string[] pointArray = exchangeAward.PointList.Split(',');
                for (int i = 0; i < productArray.Length; i++)
                {
                    awardDic.Add(Convert.ToInt32(productArray[i]), Convert.ToInt32(pointArray[i]));
                }
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = exchangeAward.PorudctIDList;
                productList = ProductBLL.SearchProductList(productSearch);
            }
            pointLeft = UserBLL.ReadUserMore(Cookies.User.GetUserID(true)).PointLeft;
            Head.Title = "兑换奖品";
        }
    }
}
