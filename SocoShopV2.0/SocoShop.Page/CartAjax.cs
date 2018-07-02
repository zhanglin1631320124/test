namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class CartAjax : AjaxBasePage
    {
        protected List<CartCommonProductVirtualInfo> cartCommonProductVirtualList = new List<CartCommonProductVirtualInfo>();
        protected List<CartGiftPackVirtualInfo> cartGiftPackVirtualList = new List<CartGiftPackVirtualInfo>();
        protected List<CartInfo> cartList = new List<CartInfo>();
        protected List<ProductInfo> productList = new List<ProductInfo>();

        private void ChangeBuyCount()
        {
            string strID = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("StrCartID"));
            int queryString = RequestHelper.GetQueryString<int>("BuyCount");
            int num2 = RequestHelper.GetQueryString<int>("OldCount");
            decimal num3 = RequestHelper.GetQueryString<decimal>("Price");
            int num4 = RequestHelper.GetQueryString<int>("ProductCount");
            decimal num5 = RequestHelper.GetQueryString<decimal>("ProductWeight");
            CartBLL.UpdateCart(strID, queryString, base.UserID);
            Sessions.ProductBuyCount += (queryString - num2) * num4;
            Sessions.ProductTotalPrice += (queryString - num2) * num3;
            Sessions.ProductTotalWeight += (queryString - num2) * num5;
            string[] strArray = new string[] { strID, "|", Sessions.ProductBuyCount.ToString(), "|", Sessions.ProductTotalPrice.ToString(), "|", (queryString * num3).ToString(), "|", queryString.ToString() };
            ResponseHelper.Write(string.Concat(strArray));
            ResponseHelper.End();
        }

        private void ClearCart()
        {
            CartBLL.ClearCart(base.UserID);
            Sessions.ProductBuyCount = 0;
            Sessions.ProductTotalPrice = 0M;
            Sessions.ProductTotalWeight = 0M;
            ResponseHelper.End();
        }

        private void Delete()
        {
            string strID = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("StrCartID"));
            int queryString = RequestHelper.GetQueryString<int>("OldCount");
            decimal num2 = RequestHelper.GetQueryString<decimal>("Price");
            int num3 = RequestHelper.GetQueryString<int>("ProductCount");
            decimal num4 = RequestHelper.GetQueryString<decimal>("ProductWeight");
            CartBLL.DeleteCart(strID, base.UserID);
            Sessions.ProductBuyCount -= queryString * num3;
            Sessions.ProductTotalPrice -= queryString * num2;
            Sessions.ProductTotalWeight -= queryString * num4;
            ResponseHelper.Write(strID + "|" + Sessions.ProductBuyCount.ToString() + "|" + Sessions.ProductTotalPrice.ToString());
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != null)
            {
                if (!(queryString == "Read"))
                {
                    if (queryString == "ClearCart")
                        this.ClearCart();
                    else if (queryString == "ChangeBuyCount")
                        this.ChangeBuyCount();
                    else if (queryString == "Delete") this.Delete();
                }
                else
                    this.ReadCart();
            }
        }

        private void ReadCart()
        {
            this.cartList = CartBLL.ReadCartList(base.UserID);
            string strProductID = string.Empty;
            foreach (CartInfo info in this.cartList)
            {
                if (strProductID == string.Empty)
                    strProductID = info.ProductID.ToString();
                else
                    strProductID = strProductID + "," + info.ProductID.ToString();
            }
            if (strProductID != string.Empty)
            {
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = strProductID;
                this.productList = ProductBLL.SearchProductList(productSearch);
            }
            List<MemberPriceInfo> memberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
            foreach (CartInfo info in this.cartList)
            {
                ProductInfo product = ProductBLL.ReadProductByProductList(this.productList, info.ProductID);
                info.ProductWeight = product.Weight;
                info.SendPoint = product.SendPoint;
                if (ShopConfig.ReadConfigInfo().ProductStorageType == 1)
                    info.LeftStorageCount = product.TotalStorageCount - product.OrderCount;
                else
                    info.LeftStorageCount = product.ImportVirtualStorageCount;
                info.ProductPrice = MemberPriceBLL.ReadCurrentMemberPrice(memberPriceList, base.GradeID, product);
            }
            CartBLL.HandlerCartList(this.cartList, ref this.cartGiftPackVirtualList, ref this.cartCommonProductVirtualList);
            int num = 0;
            decimal num2 = 0M;
            decimal num3 = 0M;
            foreach (CartGiftPackVirtualInfo info4 in this.cartGiftPackVirtualList)
            {
                num3 += info4.TotalProductWeight * info4.GiftPackBuyCount;
                num2 += info4.TotalPrice * info4.GiftPackBuyCount;
            }
            foreach (CartCommonProductVirtualInfo info5 in this.cartCommonProductVirtualList)
            {
                num3 += info5.FatherCart.ProductWeight * info5.FatherCart.BuyCount;
                num2 += info5.FatherCart.ProductPrice * info5.FatherCart.BuyCount;
            }
            foreach (CartInfo info in this.cartList)
            {
                if (info.FatherID == 0) num += info.BuyCount;
            }
            Sessions.ProductBuyCount = num;
            Sessions.ProductTotalPrice = num2;
            Sessions.ProductTotalWeight = num3;
        }
    }
}

