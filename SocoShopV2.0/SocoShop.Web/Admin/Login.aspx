<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="SocoShop.Web.Admin.Login" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><%=Global.ProductName%>后台管理</title>
    <link href="/Admin/Style/style.css" type="text/css" rel="stylesheet" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainBody" style="width:800px;margin: auto">
        <div class="loginbody">
            <div class="logo"><img src="/admin/Style/images/logo.png" /></div>
            <div class="loginTable">
                <div class="title">欢迎登录<%=Global.ProductName%></div>             
                <div class="lable">用户名：<SkyCES:TextBox ID="AdminName" CssClass="input" Width="150px" runat="server" /></div>
                <div class="lable">密　码：<SkyCES:TextBox ID="Password" TextMode="password" CssClass="input" Width="150px" runat="server" /></div>
                <div class="lable">验证码：<SkyCES:TextBox ID="SafeCode" CssClass="input" Width="100px" runat="server" />&nbsp;<img src="/CheckCode.ashx" onclick="this.src='/CheckCode.ashx?t=' + new Date();" style="cursor:pointer" alt="点击刷新验证码" align="absmiddle" /></div>   
                <div class="lable blank"><asp:CheckBox ID="Remember" runat="server"/> <label for="Remember" >记住登录状态</label></div>
                <div class="lable blank"><asp:Button CssClass="button" ID="SubmitButton" Text=" 登  录 " runat="server"  OnClick="SubmitButton_Click"/></div>                       
                <div class="lable2"><a href="/" >网站主页</a> | <a href="http://www.skyces.com/" >官方网站</a> | <a href="http://bbs.skyces.com/">在线帮助</a></div>         
            </div>
            <div style="clear:both"></div>
        </div>    
        <div class="footer"> 
            <span id="SkyVersion"><%=Global.ProductName%> <%=Global.Version%></span>
	        <span><a href="http://www.skyces.com" target="_blank"><%=Global.CopyRight%></a></span>
	    </div>    
    </form>
</body>
</html>
