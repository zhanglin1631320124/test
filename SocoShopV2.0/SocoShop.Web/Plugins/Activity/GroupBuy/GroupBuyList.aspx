<%@ Page Language="C#" AutoEventWireup="true" Inherits="SocoShop.Web.GroupBuyList" Codebehind="GroupBuyList.aspx.cs" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <asp:PlaceHolder ID="PHead" runat="server" />
    <style type="text/css">
     /**团购***/	
    .groupList li
    {
	    border: 1px solid #C0C0C0;width:300px;  float:left; margin:7px;  overflow:hidden;
    }
     .groupInfo {
        overflow: hidden;
        text-align:center; 
    }
     .groupInfo  h2{
        text-align:left;  font-size:14px;    padding:4px; line-height:23px; height:70px; margin:10px 0 10px 0;
    }
      .groupInfo  h3{
         height:180px; text-align:left; width:290px ; padding-left:4px;
    }
     .price 
     {
        height: 50px;
        background-image:url("price.png");    
        margin-top:10px;
        font-size:28px;
        font-weight:bold;
        color:#fff;
        padding-left:120px;
        text-align:left;
        background-color:#fff;
    }
        .price  span
     {
          line-height:40px; display:block; float:left;
    }
    .price  a
     {
          line-height:40px; padding-top:5px; display:block; text-align:right; margin-right:5px; height:40px; overflow:hidden;
    }
    .groupBottom
    { 
    	 margin-top:10px;
	     height:50px;  background-color:#f1f1f1;
    }
    .groupBottom ul li
    { 
        float:left; width:98px; border:none; font-size:13px; text-align:center; line-height:25px;  margin:0px;
    }   
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="PTop" runat="server" />
    <div class="main">
        <div class="position">首页 > 团购活动</div>
        <ul class="groupList">
        <%foreach(GroupBuyInfo groupBuy in groupBuyList){
              ProductInfo product = ReadProduct(productList, groupBuy.ProductID);
         %>        
            <li>	
                <div class="groupInfo">
                    <h2><a href="GroupBuyDetail.aspx?ID=<%=groupBuy.ID%>" target="_blank"><%=StringHelper.Substring(groupBuy.Name,50)%></a></h2>
                    <h3><a href="GroupBuyDetail.aspx?ID=<%=groupBuy.ID%>" target="_blank"><img src="<%=groupBuy.Photo%>" onload="photoLoad(this,290,180)" /></a></h3>  
                    <div class="price">
                        <span><%=groupBuy.Price %></span>
                        <a href="GroupBuyDetail.aspx?ID=<%=groupBuy.ID%>" target="_blank">
                        <%if(RequestHelper.DateNow<groupBuy.StartDate){ %>
                        <img src="start.png" />
                        <%}else if(RequestHelper.DateNow>groupBuy.EndDate){ %>
                        <img src="finish.png" />
                        <%}else{ %>
                        <img src="see.png" />
                        <%} %>
                        </a>
                    </div>    
                </div>
                <div class="groupBottom">
                    <ul>
                        <li>
                            <strong>原价</strong>
                            <p>¥<%=product.MarketPrice%></p>
                        </li>
                        <li>
                            <strong>折扣</strong>
                            <p><%if(product.MarketPrice > 0) { ResponseHelper.Write(Math.Round(groupBuy.Price * 10 / product.MarketPrice, 2).ToString()); }%>折</p>
                        </li>
                        <li>
                            <strong>购买人数</strong>
                            <p><%=ShopCommon.ReadValue<int,int>(dicCount,groupBuy.ID)%>人购买</p>
                        </li>
                    </ul>
                </div>
            </li>        
        <%} %>
        </ul>
        <div class="clear"></div>
        <SkyCES:CommonPager ID="MyPager" runat="server" />
    </div>
    <asp:PlaceHolder ID="PFoot" runat="server" />
    </form>
</body>
</html>
