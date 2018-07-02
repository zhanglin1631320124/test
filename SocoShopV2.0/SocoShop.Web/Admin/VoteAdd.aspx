<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="VoteAdd.aspx.cs" Inherits="SocoShop.Web.Admin.VoteAdd" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />投票<%=GetAddUpdate()%></div>
    <div class="add">
        <ul>
            <li class="left">投票类型：</li>
            <li class="right">
                <asp:RadioButtonList ID="VoteType" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="1" Selected="True">单选型</asp:ListItem>
                    <asp:ListItem Value="2">多选型</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">标题：</li>
            <li class="right">
                <SkyCES:TextBox ID="txtTitle" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></li>
        </ul>
        <ul>
            <li class="left">备注：</li>
            <li class="right">
                <SkyCES:TextBox ID="Note" CssClass="input" runat="server" Width="400px" TextMode="MultiLine"
                    Height="50px" /></li>
        </ul>
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
    </div>
</asp:Content>
