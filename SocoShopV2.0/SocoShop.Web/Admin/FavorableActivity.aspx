<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="FavorableActivity.aspx.cs" Inherits="SocoShop.Web.Admin.FavorableActivity" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>优惠活动列表</div>	
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
	    <td style="width:5%">ID</td>
	    <td style="width:40%; text-align:left;text-indent:8px;">优惠活动</td>
	    <td style="width:30%">时间</td>
	    <td style="width:15%">最低产品金额</td>    
	    <td style="width:5%">管理</td>   
	    <td style="width:5%">选择</td>        
    </tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>	     
        <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
			<td style="width:5%"><%# Eval("ID") %></td>
			<td style="width:40%; text-align:left;text-indent:8px;"><%# Eval("Name") %></td>
			<td style="width:30%"><%# Eval("StartDate","{0:yyyy-MM-dd}") %> 到 <%# Eval("EndDate","{0:yyyy-MM-dd}") %></td>
	        <td style="width:15%"><%# Eval("OrderProductMoney")%></td>    
			<td style="width:5%;">
			    <a href="javascript:pop('FavorableActivityAdd.aspx?ID=<%# Eval("ID") %>',800,600,'优惠活动修改','FavorableActivityAdd<%# Eval("ID") %>')"><img src="Style/Images/edit.gif" alt="优惠活动修改" title="优惠活动修改" /></a>
			</td>
			<td style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></td> 	        
		</tr>
        </ItemTemplate>
</asp:Repeater>
</table>
<div class="listPage"><SkyCES:CommonPager ID="MyPager" runat="server" /></div>
<div class="action">
        <input type="button"  value=" 添 加 " class="button" onclick="pop('FavorableActivityAdd.aspx',800,600,'优惠活动添加','FavorableActivityAdd')"/>&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>
