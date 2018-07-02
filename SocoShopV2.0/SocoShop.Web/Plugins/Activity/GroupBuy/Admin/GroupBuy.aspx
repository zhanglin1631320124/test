<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" Inherits="SocoShop.Web.GroupBuy" Codebehind="GroupBuy.aspx.cs" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>团购活动列表</div>	
<ul class="listHead">
	<li style="width:5%">ID</li>
	<li style="width:45%; text-align:left;text-indent:8px;">团购</li>
	<li style="width:10%">团购价</li> 
	<li style="width:25%">活动时间</li>   
	<li style="width:10%">管理</li>       
	<li style="width:5%">选择</li>        
</ul>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>	     
        	<ul class="listMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
			<li style="width:5%"><%# Eval("ID") %></li>
			<li style="width:45%; text-align:left;text-indent:8px;"><%# Eval("Name") %></li>
            <li style="width:10%"><%# Eval("Price")%></li> 
            <li style="width:25%"><%#Eval("StartDate","{0:yyyy-MM-dd}")%> 到 <%#Eval("EndDate","{0:yyyy-MM-dd}")%></li>
			<li style="width:10%;">
				<a href="javascript:pop('/Plugins/Activity/GroupBuy/Admin/GroupBuyAdd.aspx?ID=<%# Eval("ID") %>',800,600,'团购活动修改','GroupBuyAdd<%# Eval("ID") %>')"><img src="/admin/Style/Images/edit.gif" alt="修改" title="修改" /></a> 
			    <a href="javascript:popPageOnly('/Plugins/Activity/GroupBuy/Admin/UserGroupBuy.aspx?GroupBuyID=<%# Eval("ID") %>',800,600,'用户团购列表','UserGroupBuy<%# Eval("ID") %>')"><img src="/admin/Style/Images/list.gif" alt="用户团购列表" title="用户团购列表" /></a>
			</li>
			<li style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></li> 	        
		</ul>
        </ItemTemplate>
</asp:Repeater>
<div class="listPage"><SkyCES:CommonPager ID="MyPager" runat="server" /></div>
<div class="action">
        <input type="button"  value=" 添 加 " class="button"  onclick="javascript:pop('/Plugins/Activity/GroupBuy/Admin/GroupBuyAdd.aspx',800,600,'团购活动添加','GroupBuyAdd')"/>&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>
