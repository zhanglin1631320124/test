<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="Template.aspx.cs" Inherits="SocoShop.Web.Admin.Template" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>模板列表</div>
<%foreach (TemplatePluginsInfo templatePlugins in templatePluginsList){ %>
<div style="width:180px; float:left; margin:8px; height:310px; text-align:center;border:1px solid #CADBE7;">
<table cellpadding="0" cellpadding="0" align="center">
    <tr>
        <td><img src="<%=templatePlugins.Photo %>" width="160" height="200" style="border:#EFF2F5 1px solid" /></td>
    </tr>
    <tr>
	    <td><%=templatePlugins.Name%></td>
	</tr>
	<tr>
	    <td><%=templatePlugins.CopyRight%>（<%=templatePlugins.PublishDate%>）</td> 
	</tr>
	<tr>
	    <td style="line-height:40px;">	       
	        <input class="button" value="编辑" type="button" onclick="javascript:popPageOnly('TemplateFile.aspx?Path=/Plugins/Template/<%=templatePlugins.Path%>/',800,600,'模板文件','TemplateFile')" />	        
	         <%if (ShopConfig.ReadConfigInfo().TemplatePath == templatePlugins.Path){ %>
	        <img src="Style/Images/success.gif" title="已启用" height="18"  />
	        <%}else{ %>
	        <input class="button" value="启用" type="button" onclick="window.location.href='Template.aspx?Action=Active&Path=<%=templatePlugins.Path %>'" />
	        <%} %>
	    </td>
    </tr>
</table>
</div>
<%} %>
</asp:Content>
