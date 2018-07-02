<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True" CodeBehind="ShopLanguageAdd.aspx.cs" Inherits="SocoShop.Web.Admin.ShopLanguageAdd" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>语言包设置</div>
<div class="add">
    <%foreach (KeyValuePair<string, string> keyValue in language){  %>
	<ul>
		<li class="left"><%=keyValue.Key%>：</li>
		<li class="right"><input name="<%=keyValue.Key %>" value="<%=keyValue.Value%>" class="input" style="width:300px" /></li>
	</ul>
	<%} %>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
</div>
<style type="text/css">
       .add ul .left{width:200px;}
</style>
</asp:Content>
