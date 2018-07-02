<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="UploadAdd.aspx.cs" Inherits="SocoShop.Web.Admin.UploadAdd" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>附件上传</title>
    <link href="/Admin/Style/style.css" type="text/css" rel="stylesheet" media="all" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="UploadFile" CssClass="uploadFile" runat="server" />&nbsp;<asp:Button CssClass="button" ID="UploadButton" Text=" 上 传 "  runat="server"  OnClick="UploadImage"/>
    </div>
    </form>
</body>
</html>
