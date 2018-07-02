<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True" CodeBehind="MenuAdd.aspx.cs" Inherits="SocoShop.Web.Admin.MenuAdd"  %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>菜单<%=GetAddUpdate()%></div>
<div class="add">
	<ul>
		<li class="left">所属分类：</li>
		<li class="right"><asp:DropDownList ID="FatherID" runat="server" Width="380px"/></li>
	</ul>
	<ul>
		<li class="left">菜单名称：</li>
		<li class="right"><SkyCES:TextBox  ID="MenuName" CssClass="input" Width="380px" CanBeNull="必填" runat="server" /></li>
	</ul>
	<ul>
		<li class="left">菜单图标：</li>
		<li class="right"><asp:RadioButtonList ID="MenuImage" runat="server" RepeatDirection="Horizontal" RepeatColumns="10"></asp:RadioButtonList></li>
	</ul>
	<ul>
		<li class="left">链接地址：</li>
		<li class="right"><SkyCES:TextBox ID="URL"  CssClass="input" Width="380px" HintInfo="如果是外部地址，请在地址前带上Http://" CanBeNull="必填" runat="server" /></li>
	</ul>
	<ul>
		<li class="left">排序ID：</li>
		<li class="right"><SkyCES:TextBox ID="OrderID" CssClass="input" runat="server" Width="380px" CanBeNull="必填" RequiredFieldType="数据校验"   HintInfo="数字越小越排前"/></li>
	</ul>
	<ul><SkyCES:Hint ID="Hint" runat="server"/></ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
</div>
</asp:Content>
