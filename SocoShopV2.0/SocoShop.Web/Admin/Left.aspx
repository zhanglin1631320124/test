<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Left.aspx.cs" Inherits="SocoShop.Web.Admin.Left" %>

<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Business" %>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>左侧菜单</title>
    <link href="/Admin/Style/style.css" type="text/css" rel="stylesheet" media="all" />
</head>
<body>
    <div id="middleLeft">
        <div id="menu">
            <div class="menuBody" id="MenuBody">
                <%int i = 1; foreach (MenuInfo menu in menuList)
                  { %>
                <div onclick="show('Default<%=i%>')" id="Default<%=i%>Div">
                    <img src="/Admin/Style/Icon/<%=menu.MenuImage %>-icon.gif" alt="" /><%=menu.MenuName %></div>
                <ul id="Default<%=i%>Menu" <%if(i>1){ %> style="display: none" <%} %>>
                    <%int j = 1; foreach (MenuInfo tempMenu in MenuBLL.ReadMenuChildList(menu.ID))
                      { %>
                    <li id="Default<%=i%>Menu-<%=j %>" onclick="shwoSmall('Default<%=i%>Menu-<%=j %>')">
                        <img src="/Admin/Style/Icon/<%=tempMenu.MenuImage %>-icon.gif" alt="" /><a href="javascript:goUrl('<%=tempMenu.URL %>')"><%=tempMenu.MenuName%></a></li>
                    <%j += 1;
                      } %>
                    <%if (i < menuList.Count)
                      { %><li class="foot"></li>
                    <%} %>
                </ul>
                <%i += 1;
                  } %>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript" src="/Admin/Js/Common.js"></script>
    <script language="javascript" type="text/javascript" src="/Admin/Js/Left.js"></script>
</body>
</html>
