<%@ Control Language="C#" AutoEventWireup="true" Inherits="SocoShop.Page.Controls.Top" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Business" %>
<div class="top">
    <div class="tips"><p><%if(Cookies.User.GetUserID(true)==0){%>您好，欢迎您回来！[ <a href="/User/Login.aspx">请登录</a> ]，新用户？[ <a href="/User/Register.aspx">免费注册</a> ]<%}else{%><%=Cookies.User.GetUserName(false)%>：您好，欢迎您回来！[ <a href="/User/Index.aspx">会员中心</a> ] ， [ <a href="/User/Logout.aspx">安全退出</a> ]<%} %></p></div>
    <div class="logo">
        <h1><img src="/Plugins/Template/Red/style/images/logo.gif"  alt="SocoShop"  /></h1>
        <p>
            <strong>订购热线</strong><br /> <span><%=ShopConfig.ReadConfigInfo().Tel%></span>
        </p>
    </div>
    <div class="wrap">
        <ul class="topmenu">
            <li id="Default"><a href="/Default.aspx">首页</a> </li>
            <li  id="News"><a href="/Product.aspx">商品展示</a> </li>
            <li id="Product"><a href="/Brand.aspx">品牌专区</a> </li>
            <li id="Down"><a href="/Activity.aspx">商家活动</a> </li>
            <li id="GuestBook"><a href="/User/Index.aspx">会员中心</a> </li>
            <li id="Case"><a href="/Help.aspx">帮助中心</a> </li>
            <span><a href="/Cart.aspx"><img src="/Plugins/Template/Red/style/images/cart.gif"/>购物车有 <strong id="ProductBuyCount"><%=Sessions.ProductBuyCount%></strong> 件商品 合计 <strong id="ProductTotalPrice"><%=Sessions.ProductTotalPrice%></strong> 元</a> <a href="/CheckOut.aspx"><img src="/Plugins/Template/Red/style/images/checkout.gif"/></a></span>
        </ul>
        <div class="search">
            <select id="classID"><option value="">所有分类</option><%foreach(ProductClassInfo productClass in allProductClassList){%><option value="<%=productClass.ID%>"><%=productClass.ClassName%></option><%}%></select>   
            <input type="text" value="" id="keyWord"  onfocus="clearKeyWord()" onblur="fillKeyWord()" />  
            <input type="button" value="搜 索" class="button" onclick="return topSearchProduct()" />  
            <span>热门关键词：</span><%foreach(string temp in hotKeyword.Split(',')){%><a href="/Product/Keyword/<%=Server.UrlEncode(temp)%>.aspx"><%=temp%></a><%} %>
        </div>
    </div>
</div>
