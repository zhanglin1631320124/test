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

namespace SocoShop.Web
{
    public partial class ExchangeAwardAdd :SocoShop.Page.AdminBasePage
    {
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected Dictionary<int, int> awardDic = new Dictionary<int, int>();
        protected string downFile = string.Empty;
        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearCache();

            if (!Page.IsPostBack)
            {
                string action = RequestHelper.GetQueryString<string>("Action");
                if (action == "SearchProduct")
                {
                    SearchProduct();
                }

                CheckAdminPower("ReadExchangeAward", PowerCheckType.Single);
                Name.Text = ExchangeAwardBLL.ReadConfigInfo().Name;
                Content.Text = ExchangeAwardBLL.ReadConfigInfo().Content;
                string strProductID = ExchangeAwardBLL.ReadConfigInfo().PorudctIDList;
                string pointList = ExchangeAwardBLL.ReadConfigInfo().PointList;
                if (strProductID != string.Empty && pointList != string.Empty)
                {
                    string[] productArray = strProductID.Split(',');
                    string[] pointArray = pointList.Split(',');
                    for (int i = 0; i < productArray.Length; i++)
                    {
                        awardDic.Add(Convert.ToInt32(productArray[i]), Convert.ToInt32(pointArray[i]));
                    }
                    ProductSearchInfo productSearch = new ProductSearchInfo();
                    productSearch.InProductID = strProductID;
                    productList = ProductBLL.SearchProductList(productSearch);
                }
                foreach (ProductClassInfo productClass in ProductClassBLL.ReadProductClassNamedList())
                {
                    ClassID.Items.Add(new ListItem(productClass.ClassName, "|" + productClass.ID + "|"));
                }
                ClassID.Items.Insert(0, new ListItem("所有分类", string.Empty));
                downFile = StringHelper.Encode(ShopConfig.ReadConfigInfo().SecureKey, ShopConfig.ReadConfigInfo().SecureKey) + ".txt";
            }
        }
        /// <summary>
        /// 提交按钮点击方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            CheckAdminPower("UpdateExchangeAward", PowerCheckType.Single);
            ExchangeAwardInfo exchangeAward = ExchangeAwardBLL.ReadConfigInfo();
            exchangeAward.Name = Name.Text;
            exchangeAward.Content = Content.Text;
            exchangeAward.PorudctIDList = RequestHelper.GetForm<string>("ProductList");
            exchangeAward.PointList = RequestHelper.GetForm<string>("PointList");
            ExchangeAwardBLL.UpdateConfigInfo(exchangeAward);
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateExchangeAward"));
            ScriptHelper.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
        }
        /// <summary>
        /// 搜索产品
        /// </summary>
        protected void SearchProduct()
        {
            string result = string.Empty;
            ProductSearchInfo productSearch = new ProductSearchInfo();
            productSearch.Name = RequestHelper.GetQueryString<string>("ProductName");
            productSearch.ClassID = RequestHelper.GetQueryString<string>("ClassID");
            List<ProductInfo> productList = ProductBLL.SearchProductList(productSearch);
            foreach (ProductInfo product in productList)
            {
                if (result == string.Empty)
                {
                    result = product.ID + "|" + product.Name + "|" + product.Photo;
                }
                else
                {
                    result += "#" + product.ID + "|" + product.Name + "|" + product.Photo;
                }
            }
            ResponseHelper.Write(result);
            ResponseHelper.End();
        }
    }
}
