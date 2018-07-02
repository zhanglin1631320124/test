<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="UserCoupon.aspx.cs" Inherits="SocoShop.Web.Admin.UserCoupon" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />用户优惠券列表</div>
    <ul class="search">
        <li>发放类型：<asp:DropDownList ID="ddlGetType" runat="server">
            <asp:ListItem Value="">所有</asp:ListItem>
            <asp:ListItem Value="1">在线发放</asp:ListItem>
            <asp:ListItem Value="2">线下发放</asp:ListItem>
        </asp:DropDownList>
            是否使用：<asp:DropDownList ID="IsUse" runat="server">
                <asp:ListItem Value="">所有</asp:ListItem>
                <asp:ListItem Value="0">未使用</asp:ListItem>
                <asp:ListItem Value="1">已使用</asp:ListItem>
            </asp:DropDownList>
            编号：<asp:TextBox ID="Number" CssClass="input" runat="server" Height="20px" Width="150px" />
            <asp:Button CssClass="button" ID="SearchButton" Text=" 搜 索 " runat="server" OnClick="SearchButton_Click" /></li>
    </ul>
    <table class="listTable" cellpadding="0" cellpadding="0">
        <tr class="listTableHead">
            <td style="width: 5%">
                ID
            </td>
            <td style="width: 25%; text-align: left; text-indent: 8px;">
                优惠券编号
            </td>
            <td style="width: 15%">
                密码
            </td>
            <td style="width: 15%">
                获取类型
            </td>
            <td style="width: 15%">
                所属用户
            </td>
            <td style="width: 10%">
                是否使用
            </td>
            <td style="width: 10%">
                订单
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
                    <td style="width: 25%; text-align: left; text-indent: 8px;">
                        <%# Eval("Number") %>
                    </td>
                    <td style="width: 15%">
                        <%#Eval("Password")%>
                    </td>
                    <td style="width: 15%">
                        <%# CouponBLL.ReadCouponType(Convert.ToInt32(Eval("GetType")))%>
                    </td>
                    <td style="width: 15%">
                        <%#Eval("UserName")%>
                    </td>
                    <td style="width: 10%">
                        <%#ShopCommon.GetBoolString(Eval("IsUse"))%>
                    </td>
                    <td style="width: 10%">
                        <%#ReadOrderLink(Convert.ToInt32(Eval("OrderID")))%>
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
        <asp:Button CssClass="button" ID="ExportButton" Text="导出Excel" runat="server" OnClick="ExportButton_Click" />&nbsp;<asp:Button
            CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()"
            runat="server" OnClick="DeleteButton_Click" />&nbsp;<input type="checkbox" name="All"
                onclick="selectAll(this)" />全选/取消
    </div>
</asp:Content>
