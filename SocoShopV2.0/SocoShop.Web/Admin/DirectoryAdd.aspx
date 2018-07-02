<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="DirectoryAdd.aspx.cs" Inherits="SocoShop.Web.Admin.DirectoryAdd" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="add">
    <ul>
        <li class="left">文件夹名：</li>
        <li class="right"><SkyCES:TextBox ID="DirectoryName" Width="160px" CssClass="input" runat="server" CanBeNull="必填" /></li>
    </ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server"  OnClick="SubmitButton_Click" />
</div>
</asp:Content>
