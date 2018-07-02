<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ProductPhotoAdd.aspx.cs" Inherits="SocoShop.Web.Admin.ProductPhotoAdd" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>产品图片上传</title>
    <link href="/Admin/Style/style.css" type="text/css" rel="stylesheet" media="all" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        名称：<SkyCES:TextBox ID="Name" CssClass="input" runat="server" Width="200px" /> &nbsp;<asp:FileUpload ID="UploadFile" CssClass="uploadFile" runat="server" />&nbsp;<asp:Button CssClass="button" ID="SubmitButton" Text=" 上 传 " runat="server"  OnClick="SubmitButton_Click" />
    </div>
    </form>
</body>
</html>
