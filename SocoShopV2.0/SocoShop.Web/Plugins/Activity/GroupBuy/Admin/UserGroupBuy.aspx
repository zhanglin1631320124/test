<%@ Page Language="C#"  MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" Inherits="SocoShop.Web.UserGroupBuy" Codebehind="UserGroupBuy.aspx.cs" %><%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%><asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<script type="text/javascript" src="/Admin/Pop/lhgdialog_ex.js"></script>	
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>用户团购列表</div>	
<ul class="listHead">
	<li style="width:5%">ID</li>
	<li style="width:40%; text-align:left;text-indent:8px;">用户团购</li>
	<li style="width:10%">购买数量</li>  
	<li style="width:10%">IP</li>  
	<li style="width:20%">时间</li>      
	<li style="width:10%">状态</li>      
	<li style="width:5%">选择</li>        
</ul>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>	     
        	<ul class="listMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
			<li style="width:5%"><%# Eval("ID") %></li>
			<li style="width:40%; text-align:left;text-indent:8px;"><%# Eval("UserName") %></li>
            <li style="width:10%"><%# Eval("BuyCount") %></li>  
	        <li style="width:10%"><%#Eval("IP")%></li>  
	        <li style="width:20%"><%#Eval("Date")%></li>       
            <li style="width:10%"><%# ReadStatus(Convert.ToInt32(Eval("OrderID")))%></li>  
			<li style="width:5%"><input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" /></li> 	        
		</ul>
        </ItemTemplate>
</asp:Repeater>
<div class="listPage"><SkyCES:CommonPager ID="MyPager" runat="server" /></div>
<div class="action">
    <%if(groupBuy.EndDate<=DateTime.Now){ %>
    <%if(isCreateOrder){ %>
    <asp:Button CssClass="button" ID="CreateOrderButton" Text="生成订单"  runat="server"  OnClick="CreateOrderButton_Click" />&nbsp;
    <%}else{ %>
    <asp:Button CssClass="button" ID="CancleButton" Text="取消活动"  runat="server"  OnClick="CancleButton_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
    <%}} %>
    <asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
<div  class="note">
    <ul>
        <li style="title">说明</li>
        <li>1、生成订单操作是把未处理状态的用户团购记录生成一个已经付款的订单，其他的状态不处理</li>
        <li>2、取消活动操作只是把未处理状态的用户团购记录的余额退还给用户，其他的状态也不处理。</li>
    </ul>
</div>
</asp:Content>
