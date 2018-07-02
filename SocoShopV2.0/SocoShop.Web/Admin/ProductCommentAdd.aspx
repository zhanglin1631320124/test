<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="ProductCommentAdd.aspx.cs" Inherits="SocoShop.Web.Admin.ProductCommentAdd" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />商品评论审核</div>
    <div class="add">
        <ul>
            <li class="left">商品名称：</li>
            <li class="right">
                <asp:Label ID="Name" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">评论标题：</li>
            <li class="right">
                <asp:Label ID="txtTitle" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">评论内容：</li>
            <li class="right">
                <asp:Label ID="Content" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">评论IP：</li>
            <li class="right">
                <asp:Label ID="UserIP" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">评论时间：</li>
            <li class="right">
                <asp:Label ID="PostDate" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">有用数：</li>
            <li class="right">
                <asp:Label ID="Support" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">没用数：</li>
            <li class="right">
                <asp:Label ID="Against" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">分数：</li>
            <li class="right">
                <asp:Label ID="Rank" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">回复数：</li>
            <li class="right">
                <asp:Label ID="ReplyCount" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">用户：</li>
            <li class="right">
                <asp:Label ID="UserName" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">状态：</li>
            <li class="right">
                <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">未处理</asp:ListItem>
                    <asp:ListItem Value="2">显示</asp:ListItem>
                    <asp:ListItem Value="3">不显示</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">管理员回复：</li>
            <li class="right">
                <SkyCES:TextBox ID="AdminReplyContent" CssClass="input" runat="server" Width="400px"
                    TextMode="MultiLine" Height="60px" /></li>
        </ul>
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
    </div>
</asp:Content>
