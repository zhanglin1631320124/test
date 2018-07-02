<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True" CodeBehind="TemplateFile.aspx.cs" Inherits="SocoShop.Web.Admin.TemplateFile"  %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="System.IO" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>文件列表</div>
<div class="filePath"><span>当前路径：</span><%=pathTree %></div>
<div> 
    <%foreach (DirectoryInfo directory in directoryList){%>
    <ul class="fileBlock" style="height:65px; width:65px">
        <li><a href='?Path=<%=encodeCurrentPath+Server.UrlEncode(directory.Name)+"/"%>' title="<%=directory.Name %>"><img  src="/Admin/Style/File/folder.png" /></a></li>
        <li class="name"><%=directory.Name%></li>			
    </ul>		
    <%} %>		
    <%foreach (FileInfo file in fileList){ %>
    <ul class="fileBlock" style="height:65px; width:65px">
         <%if("|.css|.htm|.html|.js|".IndexOf( file.Extension)>0){ %>
        <li><a href='TemplateFileAdd.aspx?Path=<%=encodeCurrentPath%>&FileName=<%=Server.UrlEncode(file.Name)%>' title="<%=file.Name %>  文件大小：<%=FileHelper.ReadFileLength(file.Length)%>"><img  src="<%=ShopCommon.ReadFileIcon(file)%>" /></a></li>
        <%}else{ %>
        <li><a href='<%=encodeCurrentPath%><%=Server.UrlEncode(file.Name)%>' title="<%=file.Name %>  文件大小：<%=FileHelper.ReadFileLength(file.Length)%>" target="_blank"><img  src="<%=ShopCommon.ReadFileIcon(file)%>" /></a></li>
        <%} %>
        <li class="name"><%=file.Name%></li>
    </ul>		
    <%}%>	     
</div>
</asp:Content>
