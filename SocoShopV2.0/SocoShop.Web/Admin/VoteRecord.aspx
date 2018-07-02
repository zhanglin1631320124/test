<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="VoteRecord.aspx.cs" Inherits="SocoShop.Web.Admin.VoteRecord" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>投票记录</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
	<td style="width:5%">ID</td>
    <td style="width:50%; text-align:left;text-indent:8px;">投票选项</td>
    <td style="width:10%">用户</td>
    <td style="width:20%">时间</td>	 
    <td style="width:10%">IP</td>	
    <td style="width:5%">选择</td> 
</tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>
		<tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">				
			<td style="width:5%"><%# Eval("ID")%></td>
            <td style="width:50%; text-align:left;text-indent:8px;"><%#VoteItemBLL.ReadItemName(Eval("ItemID").ToString(), voteItemList)%></td>
            <td style="width:10%"><%#Eval("UserName")%></td>
            <td style="width:20%"><%# Eval("AddDate") %></td>	 
            <td style="width:10%"><%# Eval("UserIP")%></td>	
            <td style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></td> 
		</tr>
	</ItemTemplate>
</asp:Repeater> 
</table>
<div class="listPage">
    <SkyCES:CommonPager ID="MyPager" runat="server" />
</div>
<div class="action">
    <asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>
