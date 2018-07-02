<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" Inherits="SocoShop.Web.ExchangeAwardAdd" Codebehind="ExchangeAwardAdd.aspx.cs" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Entity"%>
<%@ Import Namespace="SocoShop.Common"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<script language="javascript" src="/Admin/js/calendar.js" type="text/javascript"></script>
<script language="javascript" src="ExchangeAwardAdd.js" type="text/javascript"></script>
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>兑换奖品</div>
<div class="add">
	<ul>
		<li class="left">标题：</li>
		<li class="right"><SkyCES:TextBox CssClass="input" Width="400px" ID="Name" runat="server" CanBeNull="必填"/></li>
	</ul>
	<ul>
		<li class="left">介绍：</li>
		<li class="right"><SkyCES:TextBox CssClass="input" Width="400px" ID="Content" runat="server" TextMode="MultiLine"  Height="100px"/></li>
	</ul>	
	<ul>
		<li class="left">奖品列表：</li>
		<li class="right" style="width:600px;">
		商品分类：<asp:DropDownList ID="ClassID" runat="server" /> 名称：<input ID="ProductName" class="input" /> <input type="button" class="button" value=" 搜 索 " onclick="ExchangeAwardAdd.searchProduct()"/><br />
		<div id="SearchProduct" class="searchProduct"></div>
		<div class="clear"></div>
		<div id="AwardProduct">
		<% foreach(ProductInfo product in productList){ %>
		    <div class="exchangeAwardPhoto" id="Product<%=product.ID%>">
		        <div><img  src="<%=product.Photo.Replace("Original","120-120")%>" alt=""  title="<%=product.Name%>" onload="photoLoad(this,90,90)"/></div>
		        <%=StringHelper.Substring(product.Name,6)%><br />
		        <span onclick="ExchangeAwardAdd.deleteProduct(<%=product.ID %>)" style="cursor:pointer"><img src="/Admin/style/images/delete.gif" /></span><br />
		        积分数：<input class="input" name="PointList" style="width:50px;" type="text" value="<%=awardDic[product.ID] %>"/>
		        <input class="input" name="ProductList" type="hidden" value="<%=product.ID%>"/>
		    </div>
		<%} %>
		</div>
		</li>
	</ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" /> &nbsp; 
    <input type="button" value="查看兑换记录" onclick="javascript:window.open('<%=downFile %>')" class="button" style="width:100px;" />
</div>
</asp:Content>
