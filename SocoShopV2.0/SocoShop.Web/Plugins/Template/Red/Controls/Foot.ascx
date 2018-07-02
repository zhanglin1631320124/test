<%@ Control Language="C#" AutoEventWireup="true" Inherits="SocoShop.Page.Controls.Foot" %>
<%@ Import Namespace="SkyCES.EntLib" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Business" %>
 <div class="foot">
    <div class="block">
        <div class="help">
            <%foreach(ArticleClassInfo articleClass in helpClassList){%>
            <ul>
                <li class="title"><%=articleClass.ClassName%></li>
                <%foreach(ArticleClassInfo childArticleClass in ArticleClassBLL.ReadArticleClassChildList(articleClass.ID)){%>
                <li> <a href="/Help-I<%=childArticleClass.ID%>.aspx"><%=childArticleClass.ClassName%></a></li>
                <%} %> 
            </ul>
            <%} %>         
        </div>
        <div class="clear redLine"></div>
        <p class="message">
        <%int bo=0;%>
        <%foreach(ArticleInfo bottom in bottomList){%>
        <%if(bo>0){%> | <%} %>
        <a href="<%if(bottom.Url==string.Empty){%>/ArticleDetail-I<%=bottom.ID%>.aspx<%}else{%><%=bottom.Url%><%} %>"><%=StringHelper.Substring(bottom.Title,11)%></a>
         <%bo++;%>
        <%} %>  
        <span style="display:none"><%=ShopConfig.ReadConfigInfo().StaticCode%></span>
        </p>
    </div>
</div>
<script language="javascript" type="text/javascript" src="/Plugins/Template/Red/Js/Global.js" ></script>
<div style="text-align:center; font-size:11px; margin-bottom:10px"><%=SocoShop.Common.Global.ProductName%> <%=SocoShop.Common.Global.Version%><a href="http://www.skyces.com" target="_blank" style="text-decoration:none; color:#4C5A62"><%= SocoShop.Common.Global.CopyRight%></a></div>
