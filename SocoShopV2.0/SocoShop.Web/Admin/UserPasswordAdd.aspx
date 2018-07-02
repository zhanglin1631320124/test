<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" Inherits="SocoShop.Web.Admin.UserPasswordAdd" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>修改密码</div>
<div class="add">
	<ul>
		<li class="left">登陆名：</li>
		<li class="right"><SkyCES:TextBox CssClass="input" Width="300px" ID="Name" runat="server" Enabled="false" /></li>
	</ul>
	<ul>
		<li class="left">新密码：</li>
		<li class="right"><SkyCES:TextBox CssClass="input" Width="300px" ID="NewPassword" runat="server" CanBeNull="必填" RequiredFieldType="自定义验证表达式" ValidationExpression="^[\W\w]{6,16}$" CustomErr="密码长度大于6位少于16位"  TextMode="Password"/></li>
	</ul>
	<ul>
		<li class="left">重复密码：</li>
		<li class="right"><SkyCES:TextBox CssClass="input" Width="300px" ID="NewPassword2" runat="server" CanBeNull="必填" RequiredFieldType="自定义验证表达式" ValidationExpression="^[\W\w]{6,16}$" CustomErr="密码长度大于6位少于16位" TextMode="Password"/>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码不一致" ControlToCompare="NewPassword" ControlToValidate="NewPassword2" Display="Dynamic"></asp:CompareValidator>
        </li>
	</ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server"  OnClick="SubmitButton_Click" />
</div>
</asp:Content>
