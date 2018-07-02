<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True"
    CodeBehind="ShopInfo.aspx.cs" Inherits="SocoShop.Web.Admin.ShopInfo" %>

<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style type="text/css">
        .add ul .right
        {
        }
        .add ul .right span
        {
            margin-left: 22px;
            line-height: 22px;
        }
    </style>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />产品说明</div>
    <div class="add">
        <ul>
            <li class="left">系统名称：</li>
            <li class="right">
                <%=Global.ProductName%></li>
        </ul>
        <ul>
            <li class="left">系统版本：</li>
            <li class="right">
                <%=Global.Version%></li>
        </ul>
        <ul>
            <li class="left">产品介绍：</li>
            <li class="right" style="width: 600px;">
                <%=Global.Description%></li>
        </ul>
        <ul>
            <li class="left">产品版权：</li>
            <li class="right">
                <%=Global.CopyRight%></li>
        </ul>
        <ul>
            <li class="left">第三方组件：</li>
            <li class="right">FCKeditor；lhgcore 框架组件</li>
        </ul>
    </div>
</asp:Content>
