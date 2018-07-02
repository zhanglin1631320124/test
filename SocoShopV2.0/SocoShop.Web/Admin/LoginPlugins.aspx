<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="LoginPlugins.aspx.cs" Inherits="SocoShop.Web.Admin.LoginPlugins" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>登录列表</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
        <td style="width:5%">图片</td>
        <td  style="width:10%;">名称</td>
        <td style="width:55%">描述</td>
        <td style="width:5%">启用</td>
        <td style="width:5%">管理</td>
    </tr>
    <asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>	
    <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
        <td><img src="<%# Eval("Photo") %>" /></td>
		<td><%# Eval("Name") %></td>
		<td style="text-align:left;text-indent:8px;"><%# Eval("Description")%></td>
        <td><%# ShopCommon.GetBoolString(Eval("IsEnabled"))%></td> 
		<td>
		    <a href="javascript:pop('LoginPluginsAdd.aspx?Key=<%# Eval("Key") %>',600,560,'登录插件修改','LoginPluginsAdd<%# Eval("Key") %>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a>  
		</td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
</table>
</asp:Content>
