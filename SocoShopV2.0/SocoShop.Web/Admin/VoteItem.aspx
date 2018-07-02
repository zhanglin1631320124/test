<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="VoteItem.aspx.cs" Inherits="SocoShop.Web.Admin.VoteItem" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>选项列表</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
	<td style="width:5%">ID</td>
    <td style="width:60%; text-align:left;text-indent:8px;">投票选项</td>
    <td style="width:10%">投票数量</td>    	   
    <td style="width:20%">管理</td>
    <td style="width:5%">选择</td> 
</tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>
		<tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">		
			<td style="width:5%"><%# Eval("ID")%></td>
            <td style="width:60%; text-align:left;text-indent:8px;"><%# Eval("ItemName") %></td>
            <td style="width:10%"><%#Eval("VoteCount") %></td>            	   
            <td style="width:20%">            
                <a href="?Action=Up&VoteID=<%# Eval("VoteID")%>&ItemID=<%# Eval("ID") %>"><img src="Style/Images/moveUp.gif" alt="上移" title="上移" /></a> 
                <a href="?Action=Down&VoteID=<%# Eval("VoteID")%>&ItemID=<%# Eval("ID") %>"><img src="Style/Images/moveDown.gif" alt="下移" title="下移" /></a>
                <a href='VoteItem.aspx?ID=<%# Eval("ID") %>&VoteID=<%# Eval("VoteID")%>'><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a> 
            </td>
            <td style="width:5%"><%#ShowCheckBox(Convert.ToInt32(Eval("ID")),Convert.ToInt32(Eval("VoteCount")))%></td> 
		</tr>
	</ItemTemplate>
</asp:Repeater>
</table>
<div class="action">
    <input type="button"  value=" 添 加 " class="button"  onclick="window.location.href='VoteItem.aspx?VoteID=<%=voteID %>'"/>&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>选项<%=GetAddUpdate()%></div>
<div class="add">
	<ul>
		<li class="left">选项：</td>
		<li class="right"><SkyCES:TextBox ID="ItemName" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></td>
	</tr>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server"  OnClick="SubmitButton_Click" />
</div>
</asp:Content>
