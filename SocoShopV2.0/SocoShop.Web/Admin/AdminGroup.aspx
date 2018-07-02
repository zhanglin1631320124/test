<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="AdminGroup.aspx.cs" Inherits="SocoShop.Web.Admin.AdminGroup" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>管理组列表</div>		   	 
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
        <td style="width:5%">ID</td>
        <td style="width:60%; text-align:left;text-indent:8px;">用户组名</td>
        <td style="width:20%">下属管理员</td>
        <td style="width:10%">管理</td>
        <td style="width:5%">选择</td>
    </tr>		     
<asp:Repeater ID="RecordList" runat="server">
    <ItemTemplate>
        <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
            <td style="width:5%"><%# Eval("ID") %></td>
            <td style="width:60%; text-align:left;text-indent:8px;"><%# Eval("Name") %></td>
            <td style="width:20%"><a href="Admin.aspx?GroupID=<%# Eval("ID") %>"><b style="color:Red"><%# Eval("AdminCount") %></b> 个</a></td>
            <td style="width:10%"><a href="javascript:pop('AdminGroupAdd.aspx?ID=<%# Eval("ID") %>',800,600,'管理组修改','AdminGroupAdd<%# Eval("ID") %>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a></td>
            <td style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></td>	
        </tr>
    </ItemTemplate>
</asp:Repeater>
</table>
<div class="action">
    <input type="button" class="button" value=" 增 加 " onclick="pop('AdminGroupAdd.aspx',800,600,'管理组添加','AdminGroupAdd')" />&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server" OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>
