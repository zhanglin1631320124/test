<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="Link.aspx.cs" Inherits="SocoShop.Web.Admin.Link" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>链接列表</div>
<div class="listBlock">
    <ul>
        <li id="TextLink" <%if(classID==1){%>class="listOn"<%} %>><a href="Link.aspx?ClassID=1">文字链接</a></li>
        <li id="PictureLink" <%if(classID==2){%>class="listOn"<%} %>><a href="Link.aspx?ClassID=2">图片链接</a></li>
    </ul>	
</div>	
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
    <td style="width:5%">ID</td>
    <td style="width:40%; text-align:left;text-indent:8px;">显示</td>
    <td style="width:40%">URL</td>	 
    <td style="width:10%">管理</td>
    <td style="width:5%">选择</td> 
    </tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>
		<tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
			<td style="width:5%"><%# Eval("ID")%></td>
            <td style="width:40%; text-align:left;text-indent:8px;"><%# LinkBLL.ReadLinkDisplay(Eval("Display"), Eval("LinkClass"))%></td>
            <td style="width:40%"><%#Eval("URL")%></td>	 
            <td style="width:10%">
                <a href="?Action=Up&ID=<%# Eval("ID") %>&ClassID=<%=classID%>"><img src="Style/Images/moveUp.gif" alt="上移" title="上移" /></a> 
                <a href="?Action=Down&ID=<%# Eval("ID") %>&ClassID=<%=classID%>"><img src="Style/Images/moveDown.gif" alt="下移" title="下移" /></a>  
                <a href="javascript:pop('LinkAdd.aspx?ID=<%#Eval("ID")%>&ClassID=<%=classID %>',600,350,'链接修改','LinkAdd<%# Eval("ID") %>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a>  
            </td>
            <td style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></td> 
		</tr>
	</ItemTemplate>
</asp:Repeater>
</table>
<div class="action">
    <input type="button"  value=" 添 加 " class="button"  onclick="pop('LinkAdd.aspx?ClassID=<%=classID %>',600,350,'链接添加','LinkAdd')" />&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>