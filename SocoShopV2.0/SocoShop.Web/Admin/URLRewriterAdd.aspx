<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True" CodeBehind="URLRewriterAdd.aspx.cs" Inherits="SocoShop.Web.Admin.URLRewriterAdd"  %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>地址重写<%=GetAddUpdate()%></div>
<div class="add">		
	<ul>
		<li class="left">实际地址：</li>
		<li class="right"><SkyCES:TextBox ID="RealPath" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></li>
	</ul>
	<ul>
		<li class="left">重写地址：</li>
		<li class="right"><SkyCES:TextBox ID="VitualPath" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></li>
	</ul>
	<ul>
		<li class="left">是否启用：</li>
		<li class="right"><asp:RadioButtonList ID="IsEffect" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="1" Selected="True">是</asp:ListItem><asp:ListItem Value="0">否</asp:ListItem></asp:RadioButtonList></li>
	</ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
</div>
</asp:Content>
