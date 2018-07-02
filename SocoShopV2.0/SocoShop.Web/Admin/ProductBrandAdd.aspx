<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="ProductBrandAdd.aspx.cs" Inherits="SocoShop.Web.Admin.ProductBrandAdd" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>商品品牌<%=GetAddUpdate()%></div>
<div class="add">	
	<ul>
		<li class="left">名称：</li>
		<li class="right"><SkyCES:TextBox ID="Name" CssClass="input" runat="server" Width="400px"  CanBeNull="必填"/></li>
	</ul>
	<ul>
		<li class="left">Logo：</li>
		<li class="right"><SkyCES:TextBox ID="Logo" CssClass="input" runat="server" Width="400px" /></li>
	</ul>
	<ul>
		<li class="left">上传附件：</li>
		<li class="right"><iframe src="UploadAdd.aspx?Control=Logo&TableID=<%=ProductBrandBLL.TableID %>&FilePath=BrandLogo/Original" width="400" height="30px" frameborder="0" allowTransparency="true" scrolling="no"></iframe></li>
	</ul> 
	<ul>
		<li class="left">链接地址：</li>
		<li class="right"><SkyCES:TextBox ID="Url" CssClass="input" runat="server" Width="400px" HintInfo="填写该项就表示品牌详细页直接链接到该地址，如果是外部地址，请在地址前带上Http://" /></li>
	</ul>
	<ul>
		<li class="left">介绍：</li>
		<li class="right"><SkyCES:TextBox ID="Description" CssClass="input" runat="server" Width="400px" Height="100px" TextMode="MultiLine" /></li>
	</ul>
	<ul>
		<li class="left">是否推荐：</li>
		<li class="right"><asp:RadioButtonList ID="IsTop" runat="server" RepeatDirection="Horizontal"><asp:ListItem Value="1" Selected="True">是</asp:ListItem><asp:ListItem Value="0">否</asp:ListItem></asp:RadioButtonList></li>
	</ul>
	<ul><SkyCES:Hint ID="Hint" runat="server"/></ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server"  OnClick="SubmitButton_Click" />
</div>
</asp:Content>
