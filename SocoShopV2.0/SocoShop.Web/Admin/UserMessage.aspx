<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="UserMessage.aspx.cs" Inherits="SocoShop.Web.Admin.UserMessage" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script language="javascript" src="/Admin/js/calendar.js" type="text/javascript"></script>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />用户留言列表</div>
    <ul class="search">
        <li>
            <asp:DropDownList ID="MessageClass" runat="server">
                <asp:ListItem Value="">全部</asp:ListItem>
                <asp:ListItem Value="1">留言</asp:ListItem>
                <asp:ListItem Value="2">投诉</asp:ListItem>
                <asp:ListItem Value="3">询问</asp:ListItem>
                <asp:ListItem Value="4">售后</asp:ListItem>
                <asp:ListItem Value="5">求购</asp:ListItem>
            </asp:DropDownList>
            标题：<asp:TextBox ID="txtTitle" CssClass="input" runat="server" Height="20px" Width="60px" />
            用户：<asp:TextBox ID="UserName" CssClass="input" runat="server" Height="20px" Width="100px" />
            是否处理：<asp:DropDownList ID="IsHandler" runat="server">
                <asp:ListItem Value="">所有</asp:ListItem>
                <asp:ListItem Value="0">否</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
            </asp:DropDownList>
            留言时间：<SkyCES:TextBox CssClass="input" ID="StartPostDate" runat="server" RequiredFieldType="日期时间"
                onfocus="cdr.show(this);" />
            到
            <SkyCES:TextBox CssClass="input" ID="EndPostDate" runat="server" RequiredFieldType="日期时间"
                onfocus="cdr.show(this);" />
            <asp:Button CssClass="button" ID="SearchButton" Text=" 搜 索 " runat="server" OnClick="SearchButton_Click" /></li>
    </ul>
    <div class="listBlock">
        <ul>
            <li <%if(classID==int.MinValue){%>class="listOn" <%} %> onclick="window.location='UserMessage.aspx'">
                所有留言</li>
            <li <%if(classID==1){%>class="listOn" <%} %> onclick="window.location='UserMessage.aspx?Action=search&MessageClass=1'">
                留言</li>
            <li <%if(classID==2){%>class="listOn" <%} %> onclick="window.location='UserMessage.aspx?Action=search&MessageClass=2'">
                投诉</li>
            <li <%if(classID==3){%>class="listOn" <%} %> onclick="window.location='UserMessage.aspx?Action=search&MessageClass=3'">
                询问</li>
            <li <%if(classID==4){%>class="listOn" <%} %> onclick="window.location='UserMessage.aspx?Action=search&MessageClass=4'">
                售后</li>
            <li <%if(classID==5){%>class="listOn" <%} %> onclick="window.location='UserMessage.aspx?Action=search&MessageClass=5'">
                求购</li>
        </ul>
    </div>
    <table class="listTable" cellpadding="0" cellspacing="0">
        <tr class="listTableHead">
            <td style="width: 5%">
                ID
            </td>
            <td style="width: 45%; text-align: left; text-indent: 8px;">
                标题
            </td>
            <td style="width: 10%">
                类型
            </td>
            <td style="width: 15%">
                时间
            </td>
            <td style="width: 10%">
                姓名
            </td>
            <td style="width: 5%">
                处理
            </td>
            <td style="width: 5%">
                管理
            </td>
            <td style="width: 5%">
                选择
            </td>
        </tr>
        <asp:Repeater ID="RecordList" runat="server">
            <ItemTemplate>
                <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
                    <td style="width: 5%">
                        <%# Eval("ID") %>
                    </td>
                    <td style="width: 45%; text-align: left; text-indent: 8px;">
                        <%# Eval("Title") %>
                    </td>
                    <td style="width: 10%">
                        <%# UserMessageBLL.ReadMessageType(Convert.ToInt32(Eval("MessageClass"))) %>
                    </td>
                    <td style="width: 15%">
                        <%# Eval("PostDate") %>
                    </td>
                    <td style="width: 10%">
                        <%#Eval("UserName") %>
                    </td>
                    <td style="width: 5%">
                        <%# ShopCommon.GetBoolString(Eval("IsHandler"))%>
                    </td>
                    <td style="width: 5%;">
                        <a href="javascript:pop('UserMessageAdd.aspx?ID=<%# Eval("ID") %>',600,440,'留言回复','UserMessageAdd<%# Eval("ID") %>')">
                            <img src="Style/Images/reply.gif" alt="留言回复" title="留言回复" /></a>
                    </td>
                    <td style="width: 5%">
                        <input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="listPage">
        <SkyCES:CommonPager ID="MyPager" runat="server" />
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()"
            runat="server" OnClick="DeleteButton_Click" />&nbsp;<input type="checkbox" name="All"
                onclick="selectAll(this)" />全选/取消
    </div>
</asp:Content>
