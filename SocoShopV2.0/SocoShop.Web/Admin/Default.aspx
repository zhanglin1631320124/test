<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="SocoShop.Web.Admin.Default" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=Global.ProductName%>后台管理</title>
<script language="javascript" type="text/javascript" src="/Admin/Js/Common.js"></script>
<script language="javascript" type="text/javascript" src="/Admin/Js/Admin.js"></script>
<script language="javascript" type="text/javascript" src="/Admin/Js/ScrollText.js"></script>
<script type="text/javascript" src="/Admin/Pop/lhgcore.min.js"></script>
<script type="text/javascript" src="/Admin/Pop/lhgdialog.min.js?s=chrome"></script>	 
<link href="/Admin/Style/style.css" type="text/css" rel="stylesheet" media="all" />
</head>
<body>
	<div class="head">
	    <div class="top">
	        <div class="logo"><img src="/Admin/Style/Images/logo2.png" /></div>
            <div class="welcome">
                <%=Cookies.Admin.GetAdminName(false)%>：您好，
                <script language="javascript" type="text/javascript">
                var today=new Date();
                document.write("今天："+today.getFullYear()+ "年"+(today.getMonth()+1)+"月"+today.getDate()+"日，");
                </script>
                权限组：<%=AdminGroupBLL.ReadAdminGroupCache(Cookies.Admin.GetGroupID(false)).Name%>
               <a href="javascript:goUrl('ChangePassword.aspx')" >修改密码</a>
               <a href="Logout.aspx" target="_top">安全退出</a>
           </div>                             
	    </div>	
        <ul class="channel">
            <li class="on" id="aCommon"><a href="javascript:switchBlock('Common','Left.aspx?ID=1','Right.aspx')">基础设置</a></li>
            <li ><img src="/Admin/Style/Images/channelPadding.gif" alt="" /></li>
            <li id="aProduct"><a href="javascript:switchBlock('Product','Left.aspx?ID=2','Right.aspx')">商品管理</a></li>
            <li><img src="/Admin/Style/Images/channelPadding.gif"  alt=""/></li>
            <li id="aUser"><a href="javascript:switchBlock('User','Left.aspx?ID=3','Right.aspx')">用户中心</a></li>
            <li><img src="/Admin/Style/Images/channelPadding.gif"  alt=""/></li>
            <li id="aMarket"><a href="javascript:switchBlock('Market','Left.aspx?ID=4','Right.aspx')">市场营销</a></li>
            <li><img src="/Admin/Style/Images/channelPadding.gif"  alt=""/></li>
            <li id="aOrder"><a href="javascript:switchBlock('Order','Left.aspx?ID=5','Right.aspx')">订单与统计</a></li>	         
        </ul>    
	    <div class="menu">
	        <div class="text">官方公告：</div><script language="javascript" src="http://www.skyces.com/Plugins/000002.js" type="text/javascript"></script>
	        <ul>
	            <li><a href="http://www.socoshop.com/help.aspx" target="_blank">帮助中心</a></li>	   
	            <li><img src="/Admin/Style/Images/menuPadding.gif"  alt=""/></li>
	            <li><a href="/" target="_blank" >网站主页</a></li>
	            <li><img src="/Admin/Style/Images/menuPadding.gif"  alt=""/></li>
	            <li><a href="javascript:goUrl('Menu.aspx')" >菜单设置</a></li>
	            <li><img src="/Admin/Style/Images/menuPadding.gif"  alt=""/></li>
	            <li><a href="javascript:goUrl('HardDisk.aspx')" >共享硬盘</a></li>
	            <li><img src="/Admin/Style/Images/menuPadding.gif"  alt=""/></li>
	            <li><a href="javascript:popPageOnly('NoteBook.aspx',500,280,'记事本','NoteBook')"/>记事本</a></li>
	            <li><img src="/Admin/Style/Images/menuPadding.gif"  alt=""/></li>
	            <li><a href="javascript:popPageOnly('SendEmail.aspx',600,450,'发邮件','SendEmail')"/>发邮件</a></li>
	        </ul>
	    </div>
	</div>
	<ul class="body" id="Body">
	    <li class="leftFrame"><iframe src="Left.aspx" height="100%" frameborder="0" id="LeftFrame"></iframe></li>
	    <li class="rightFrame"><iframe src="Right.aspx" height="100%" width="100%" frameborder="0" id="RightFrame"></iframe></li>
	</ul>
</body>
<script language="javascript" type="text/javascript" src="/Admin/Js/Default.js"></script>
</html>