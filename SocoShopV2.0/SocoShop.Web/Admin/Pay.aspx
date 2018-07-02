<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="Pay.aspx.cs" Inherits="SocoShop.Web.Admin.Pay" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>支付列表</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
        <td style="width:20%">图片</td>
        <td  style="width:10%;">名称</td>
        <td style="width:40%">描述</td>
        <td style="width:10%">货到付款</td>
        <td style="width:10%">在线支付</td>
        <td style="width:5%">启用</td>
        <td style="width:5%">管理</td>
    </tr>
    <asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>	
    <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
        <td style="width:20%"><img src="<%# Eval("Photo") %>" /></td>
		<td style="width:10%;"><%# Eval("Name") %></td>
		<td style="width:40%; text-align:left;text-indent:8px;"><%# Eval("Description")%></td>
		<td style="width:10%"><%# ShopCommon.GetBoolString(Eval("IsCod"))%></td> 
        <td style="width:10%"><%# ShopCommon.GetBoolString(Eval("IsOnline"))%></td>
        <td style="width:5%"><%# ShopCommon.GetBoolString(Eval("IsEnabled"))%></td> 
		<td style="width:5%;">
		    <a href="javascript:pop('PayAdd.aspx?Key=<%# Eval("Key") %>',600,560,'支付修改','PayAdd<%# Eval("Key") %>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a>  
		</td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
</table>
</asp:Content>
