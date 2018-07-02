<%@ Page Language="C#" AutoEventWireup="true" Inherits="SocoShop.Web.ExchangeAward" Codebehind="ExchangeAward.aspx.cs" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <asp:PlaceHolder ID="PHead" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="PTop" runat="server" />
    <div class="main">
        <div class="giftPackName"><%=exchangeAward.Name %></div>
        <div class="giftPackTime">您当前剩余积分：<%=pointLeft %></div>
        <div class="themeActivityTop" style="font-size:14px; line-height:25px; font-weight:600; text-indent:30px;"><%=exchangeAward.Content %></div>       
        <div class="themeActivityMiddle">
            <div class="productBlock">
            <%int k=0;foreach(ProductInfo product in productList){%> 
                <ul class="productPicture">
                    <li class="photo"><a href="/ProductDetail-I<%=product.ID%>.aspx"><img src="<%=product.Photo.Replace("Original","120-120")%>"  onload="photoLoad(this,120,120)" /></a></li>
                    <li><a href="/ProductDetail-I<%=product.ID%>.aspx" class="productName"><%=product.Name%></a></li>
                    <li class="productPrice">积分： <%=awardDic[product.ID]%></li>
                    <%if(pointLeft>=awardDic[product.ID]){ %>
                    <li><input type="button" class="button" value="立即兑换" onclick="window.location.href='FillUserInfo.aspx?ID=<%=product.ID%>'" /></li>
                    <%} %>
                </ul> 
            <%k++; %>
            <%if(k%6==0){%>
            </div><div class="productBlock">
            <%} %>            
            <%} %>
            </div>
        </div>
    </div>
    <asp:PlaceHolder ID="PFoot" runat="server" />
    </form>
</body>
</html>
