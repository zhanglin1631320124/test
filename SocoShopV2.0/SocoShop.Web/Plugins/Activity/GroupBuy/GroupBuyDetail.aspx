<%@ Page Language="C#" AutoEventWireup="true" Inherits="SocoShop.Web.GroupBuyDetail" Codebehind="GroupBuyDetail.aspx.cs" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <asp:PlaceHolder ID="PHead" runat="server" />
    <style type="text/css">       
        .groupBuyBox
        {
        	border:3px #A42C2C solid; padding:20px;
        }
        .groupBuyBox .title
        {
        	 font-size:18px; font-weight:bold; line-height:25px;  margin-bottom:20px;
        }
        .groupBuyBox .leftBox
        {
        	  width:300px;float:left; overflow:hidden;
        }
         .groupBuyBox .rightBox
        {
        	   float:left; width:580px;border:3px #CF0019 solid; height:360px; overflow:hidden;
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
        .priceDetail
        {
            line-height:25px; border:solid 1px #CF0019; height:50px;width:270px;  margin-top:20px; overflow:hidden;
         }
         .priceDetail li
        {
             float:left; width:90px;  text-align:center;
        }
         .priceDetail li p
        {
             color:#CF0019; font-weight:bold; font-size:14px;
        }
        .note
        {
        	line-height:28px; margin-top:10px;verflow:hidden;
        }
        .note span
        {
        	color:#CF0019; font-weight:bold; font-size:14px;
        }
         .groupBuyBox .noUser
        {
        	    line-height:25px; font-size:14px; text-align:center;
        }
        .groupBuyUser li
        {
        	line-height:25px; width:85px; overflow:hidden; float:left; height:25px; margin:3px;
        	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="PTop" runat="server" />
    <div class="main">
        <div class="groupBuyBox">
            <div class="title"><%=groupBuy.Name%></div> 
            <div style="width:100%; overflow:hidden;">          
                <div class="leftBox">
                     <div class="price">
                        <span><%=groupBuy.Price %></span>                    
                        <%if(RequestHelper.DateNow<groupBuy.StartDate){ %>
                        <a href="#"><img src="start.png" /></a>
                        <%}else if(RequestHelper.DateNow>groupBuy.EndDate){ %>
                        <a href="#"><img src="finish.png" /></a>
                        <%}else{ %>
                        <a href="GroupBuyOrder?ID=<%=groupBuy.ID%>" target="_blank"><img src="buy.png" /></a>
                        <%} %>
                    </div> 
                    <ul class="priceDetail">
                        <li>
                            <strong>原价</strong>
                            <p>¥<%=product.MarketPrice%></p>
                        </li>
                        <li>
                            <strong>折扣</strong>
                            <p><%if(product.MarketPrice > 0) { ResponseHelper.Write(Math.Round(groupBuy.Price * 10 / product.MarketPrice, 2).ToString()); }%>折</p>
                        </li>
                        <li>
                            <strong>节省</strong>
                            <p><%=product.MarketPrice - groupBuy.Price%></p>
                        </li>
                    </ul>
                    <div class="clear"></div>
                    <ul class="note">
                        <li>1、活动时间：<%=groupBuy.StartDate.ToString("yyyy-MM-dd")%> 到 <%=groupBuy.EndDate.ToString("yyyy-MM-dd")%></li>
                        <li>2、参与活动商品总共 <span><%=groupBuy.MaxCount%></span>件；已经售出<span><%=buyCount%></span>件；还剩<span><%=groupBuy.MaxCount - buyCount%></span>件</li>
                        <li><%if(groupBuy.EachNumber!=-1){ %>3、活动商品每人限购<span><%=groupBuy.EachNumber%></span>件<%} %></li>
                        <%if (groupBuy.StartDate <= DateTime.Now && DateTime.Now <=groupBuy.EndDate){%>
                        <li>4、剩余时间：<span id="leftTime" class="leftTime">加载中...</span></li>
                        <%} %>                    
                     </ul>
                </div>
                <div class="rightBox"><img src="<%=groupBuy.Photo%>"  onload="photoLoad(this,580,360)"/></div>
            </div>
            <div class="clear"></div>
        </div>
        <%if(groupBuy.Description!=string.Empty){ %>
        <div class="height10"></div>
        <div class="groupBuyBox">
            <%=groupBuy.Description%>
        </div>  
        <%} %>      
        <div class="height10"></div>
        <div class="groupBuyBox">
            <%if(buyCount>0){ %>
            <ul class="groupBuyUser">
                <%foreach(UserGroupBuyInfo userGroupBuy in userGroupBuyList){ %>
                <li><%=userGroupBuy.UserName%></li>
                <%} %>
            </ul>
            <%}else{ %>
            <div class="noUser">还没有用户参加该团购活动！！</div>
            <%} %>
            <div class="clear"></div>
        </div>     
    </div>    
    <script language="javascript" type="text/javascript" src="/Plugins/Template/<%=ShopConfig.ReadConfigInfo().TemplatePath %>/Js/LeftTime.js" ></script>
    <script language="javascript" type="text/javascript"> 
        var gmt_end_time = <%=leftTime%>;        
        onload_leftTime();
    </script> 
    <asp:PlaceHolder ID="PFoot" runat="server" />
    </form>
</body>
</html>
