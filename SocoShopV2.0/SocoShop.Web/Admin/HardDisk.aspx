<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="HardDisk.aspx.cs" Inherits="SocoShop.Web.Admin.HardDisk" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="System.IO" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server"> 
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>共享硬盘</div>	
<div>
    <input type="button" value="上传文件" class="button" onclick="addFile()" />
    <input type="button"  value="增加文件夹"  class="button" style="width:80px" onclick="addDirectory()" />
    <input type="button"  value="剪切"  class="button" onclick="cut()" />
    <input type="button"  value="复制"  class="button" onclick="copy()" />
    <input type="button"  value="粘贴"  class="button" onclick="paste()" />
    <input type="button"  value="全选"  class="button" onclick="selectAll()" />
    <input type="button"  value="不选"  class="button" onclick="selectNo()" />
    <input type="button"  value="返回上级"  class="button" onclick="goBack()" />
    <input type="button"  value="返回" class="button" onclick="history.back(-1)" />
</div>
<div class="filePath"><span>当前路径：</span><%=pathTree %></div>
<div> 
    <%foreach (DirectoryInfo directory in directoryList){%>
    <ul class="fileBlock">
        <li><a href='?Path=<%=encodeCurrentPath+Server.UrlEncode(directory.Name)+"/"%>' title="<%=directory.Name %>"><img  src="/Admin/Style/File/folder.png" /></a></li>
        <li class="name"><span title="<%=directory.Name %>" onclick="edit(this,changeDirectoryName,'')"><%=directory.Name%></span></li>			
        <li><input name="HardDisk" value="<%=encodeCurrentPath+Server.UrlEncode(directory.Name)+"/"%>" type="checkbox"/> <a href="?Action=DeleteDirectory&Path=<%=encodeCurrentPath+Server.UrlEncode(directory.Name)+"/"%>" onclick="return deleteRecord(this);" title="删除"><img src="style/images/delete.gif" /></a></li>
    </ul>		
    <%} %>		
    <%foreach (FileInfo file in fileList){ %>
    <ul class="fileBlock">
        <li><a href='<%=encodeCurrentPath+ Server.UrlEncode(file.Name)%>' title="<%=file.Name %>  文件大小：<%=FileHelper.ReadFileLength(file.Length)%>" target="_blank"><img  src="<%=ShopCommon.ReadFileIcon(file)%>" /></a></li>
        <li class="name"><span title="<%=file.Name %>" onclick="edit(this,changeFileName,'')"><%=file.Name%></span></li>
        <li><input name="HardDisk" value="<%=encodeCurrentPath+ Server.UrlEncode(file.Name)%>" type="checkbox"/> <a  href="?Action=DeleteFile&FileName=<%=encodeCurrentPath+Server.UrlEncode(file.Name)%>" onclick="return deleteRecord(this);" title="删除"><img src="style/images/delete.gif" /></a></li>
    </ul>		
    <%}%>	     
</div>
<script language="javascript" type="text/javascript">var path="<%=encodeCurrentPath%>";</script>
<script language="javascript" type="text/javascript" src="/Admin/Js/HardDisk.js"></script>
<script language="javascript" type="text/javascript" src="/Admin/Js/Edit.js"></script>
</asp:Content>
