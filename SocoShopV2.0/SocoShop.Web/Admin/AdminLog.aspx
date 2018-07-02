<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True" CodeBehind="AdminLog.aspx.cs" Inherits="SocoShop.Web.Admin.AdminLog" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>管理员日志</div>		   	 
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
    <td style="width:5%">ID</td>
    <td style="width:40%; text-align:left;text-indent:8px;">操作记录</td>
    <td style="width:15%">管理员</td>
    <td style="width:20%">时间</td>
    <td style="width:15%">IP</td>	
    <td style="width:5%">选择</td>
</tr>
<asp:Repeater ID="RecordList" runat="server">
    <ItemTemplate>
    <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
            <td style="width:5%"><%# Eval("ID") %></td>
            <td style="width:40%; text-align:left;text-indent:8px;"><%# Eval("Action") %></td>
            <td style="width:15%"><%# Eval("AdminName")%></td>
            <td style="width:20%"><%# Eval("AddDate")%></td>
            <td style="width:15%"><%# Eval("IP") %></td>	
            <td style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></td>	
        </tr>
    </ItemTemplate>
</asp:Repeater>
</table>
<div class="listPage">
    <SkyCES:CommonPager ID="MyPager" runat="server" />
</div>
<div class="action">
    <asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server" OnClick="DeleteButton_Click" />&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>
