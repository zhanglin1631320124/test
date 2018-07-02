<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True" CodeBehind="URLRewriter.aspx.cs" Inherits="SocoShop.Web.Admin.URLRewriter"  %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>地址重写列表</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
	    <td style="width:5%">ID</td>
        <td style="width:35%; text-align:left;text-indent:8px;">实际地址</td>
        <td style="width:35%; text-align:left;text-indent:8px;">重写地址</td>
        <td style="width:10%">是否启用</td>		 
        <td style="width:10%">管理</td>	   
        <td style="width:5%">选择</td> 
    </tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>
		<tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">	
			<td style="width:5%"><%# Eval("ID") %></td>
            <td style="width:35%; text-align:left;text-indent:8px;"><%# Eval("RealPath")%></td>
            <td style="width:35%; text-align:left;text-indent:8px;"><%# Eval("VitualPath")%></td>
            <td style="width:10%"><%#ShopCommon.GetBoolString(Eval("IsEffect"))%></td>		 
            <td style="width:10%"><a href="javascript:pop('URLRewriterAdd.aspx?ID=<%# Eval("ID") %>',650,260,'地址重写修改','URLRewriterAdd<%# Eval("ID") %>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a></td>	   
            <td style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></td> 
		</tr>
	</ItemTemplate>
</asp:Repeater>
</table>
<div class="action">
    <input type="button"  value=" 添 加 " class="button" onclick="pop('URLRewriterAdd.aspx',650,260,'地址重写添加','URLRewriterAdd')"/>&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>
