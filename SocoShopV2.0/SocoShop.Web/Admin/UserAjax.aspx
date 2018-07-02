<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="UserAjax.aspx.cs" Inherits="SocoShop.Web.Admin.UserAjax" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Entity" %>
<div style="border:1px solid #EEEEEE;">
<%foreach(UserInfo user in userList){ %>
<span><input type="checkbox" name="user" value="<%=user.ID%>|<%=user.UserName %>"  onclick="SendMessageAdd.selectUser(this)"/><%=user.UserName %></span>
<%} %>
<div class="clear"></div>
<div class="listPage"><SkyCES:AjaxPager ID="MyPager" runat="server" /></div>
</div>