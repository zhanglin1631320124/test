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
    public partial class GroupBuyList : PluginsBasePage
    {
        protected List<GroupBuyInfo> groupBuyList = new List<GroupBuyInfo>();
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected Dictionary<int, int> dicCount = new Dictionary<int, int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int currentPage = RequestHelper.GetQueryString<int>("Page");
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            int pageSize = 10;
            int count = 0;
            groupBuyList = GroupBuyBLL.ReadGroupBuyList(currentPage, pageSize, ref count);
            MyPager.CurrentPage = currentPage;
            MyPager.PageSize = pageSize;
            MyPager.Count = count;
           
            string productIDList = string.Empty;
            string idList = string.Empty;
            foreach (GroupBuyInfo groupBuy in groupBuyList)
            {
                if (productIDList == string.Empty)
                {
                    productIDList = groupBuy.ProductID.ToString();
                    idList = groupBuy.ID.ToString();
                }
                else
                {
                    productIDList += "," + groupBuy.ProductID.ToString();
                    idList += "," + groupBuy.ID.ToString();
                }
            }
            //读取商品
            if (productIDList != string.Empty)
            {
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = productIDList;
                productList = ProductBLL.SearchProductList(productSearch);
            }
            //读取购买人数
            if (idList != string.Empty)
            {
                dicCount = UserGroupBuyBLL.ReadUserGroupBuyCount(idList);
            }            
        }
        /// <summary>
        /// 读取一条产品
        /// </summary>
        /// <param name="productList"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        protected ProductInfo ReadProduct(List<ProductInfo> productList, int productID)
        {
            ProductInfo product = new ProductInfo();
            foreach (ProductInfo tempProduct in productList)
            {
                if (tempProduct.ID == productID)
                {
                    product = tempProduct;
                    break;
                }
            }
            return product;
        }
    }
}
