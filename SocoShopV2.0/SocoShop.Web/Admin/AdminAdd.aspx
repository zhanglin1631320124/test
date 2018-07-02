<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="AdminAdd.aspx.cs" Inherits="SocoShop.Web.Admin.AdminAdd" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />管理员<%=GetAddUpdate()%></div>
    <div class="add">
        <ul>
            <li class="left">管理组：</li>
            <li class="right">
                <asp:DropDownList Width="300px" ID="GroupID" runat="server" />
            </li>
        </ul>
        <ul>
            <li class="left">管理员名：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="300px" ID="Name" runat="server" CanBeNull="必填" /></li>
        </ul>
        <ul>
            <li class="left">电子邮箱：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="300px" ID="Email" runat="server" CanBeNull="必填"
                    RequiredFieldType="电子邮箱" /></li>
        </ul>
        <asp:PlaceHolder ID="Add" runat="server">
            <ul>
                <li class="left">密码：</li>
                <li class="right">
                    <SkyCES:TextBox CssClass="input" Width="300px" ID="Password" runat="server" CanBeNull="必填"
                        RequiredFieldType="自定义验证表达式" ValidationExpression="^[\W\w]{6,16}$" CustomErr="密码长度大于6位少于16位"
                        TextMode="Password" /></li>
            </ul>
            <ul>
                <li class="left">重复密码：</li>
                <li class="right">
                    <SkyCES:TextBox CssClass="input" Width="300px" ID="Password2" runat="server" CanBeNull="必填"
                        RequiredFieldType="自定义验证表达式" ValidationExpression="^[\W\w]{6,16}$" CustomErr="密码长度大于6位少于16位"
                        TextMode="Password" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码不一致"
                        ControlToCompare="Password" ControlToValidate="Password2" Display="Dynamic"></asp:CompareValidator>
                </li>
            </ul>
        </asp:PlaceHolder>
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
    </div>
</asp:Content>
