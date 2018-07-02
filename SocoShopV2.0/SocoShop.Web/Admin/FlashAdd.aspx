<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="FlashAdd.aspx.cs" Inherits="SocoShop.Web.Admin.FlashAdd" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>Flash<%=GetAddUpdate()%></div>
<div class="add">
	<ul>
		<li class="left">标题：</li>
		<li class="right"><SkyCES:TextBox ID="txtTitle" CssClass="input" runat="server"  Width="400px" CanBeNull="必填"/></li>
	</ul>
	<ul>
		<li class="left">说明：</li>
		<li class="right"><SkyCES:TextBox ID="Introduce" CssClass="input" runat="server"  Width="400px" TextMode="MultiLine" Height="50px" /></li>
	</ul>
	<ul>
		<li class="left">大小：</li>
		<li class="right">宽 <SkyCES:TextBox ID="Width" CssClass="input" runat="server" Width="80px" CanBeNull="必填" RequiredFieldType="数据校验"/>px X 高 <SkyCES:TextBox ID="Height" CssClass="input" runat="server" Width="100px" CanBeNull="必填" RequiredFieldType="数据校验"/>px</li>
	</ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server"  OnClick="SubmitButton_Click" />
</div>
</asp:Content>
