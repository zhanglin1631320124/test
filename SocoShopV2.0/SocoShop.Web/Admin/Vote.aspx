<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="Vote.aspx.cs" Inherits="SocoShop.Web.Admin.Vote" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>投票列表</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
	<td style="width:5%">ID</td>
    <td style="width:40%; text-align:left;text-indent:8px;">标题</td>
    <td style="width:20%">类型</td>
    <td style="width:10%">选项数</td>   
    <td style="width:20%">管理</td>
    <td style="width:5%">选择</td> 
</tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>
		<tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">		
			<td style="width:5%"><%# Eval("ID")%></td>
            <td style="width:40%; text-align:left;text-indent:8px;"><%# Eval("Title") %></td>
            <td style="width:20%"><%#ShopCommon.GetVoteString(Eval("VoteType")) %></td>
            <td style="width:10%"><%#Eval("ItemCount")%></td>    
            <td style="width:20%">
                <a href="javascript:pop('VoteItem.aspx?VoteID=<%# Eval("ID")%>',650,400,'选项管理','VoteItem<%# Eval("ID") %>')"><img src="Style/Images/list.gif" alt="选项管理" title="选项管理" /></a>  
                <a href="javascript:pop('VoteRecord.aspx?VoteID=<%# Eval("ID")%>',800,600,'投票记录','VoteRecord<%# Eval("ID") %>')"><img src="Style/Images/passport.gif" alt="投票记录" title="投票记录" /></a>
                <a href="javascript:pop('VoteAdd.aspx?ID=<%# Eval("ID") %>',650,300,'投票修改','VoteAdd<%# Eval("ID") %>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a> 
            </td>
            <td style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></td> 
		</tr>
	</ItemTemplate>
</asp:Repeater>
</table>
<div class="listPage">
    <SkyCES:CommonPager ID="MyPager" runat="server" />
</div>
<div class="action">
    <input type="button"  value=" 添 加 " class="button" onclick="pop('VoteAdd.aspx',650,300,'投票添加','VoteAdd')"/>&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>  
</asp:Content>
