<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="ProductComment.aspx.cs" Inherits="SocoShop.Web.Admin.ProductComment" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script language="javascript" src="/Admin/js/calendar.js" type="text/javascript"></script>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />商品评论列表</div>
    <ul class="search">
        <li>状态：<asp:DropDownList ID="Status" runat="server">
            <asp:ListItem Value="">所有</asp:ListItem>
            <asp:ListItem Value="1">未处理</asp:ListItem>
            <asp:ListItem Value="2">显示</asp:ListItem>
            <asp:ListItem Value="3">不显示</asp:ListItem>
        </asp:DropDownList>
            商品名称：<asp:TextBox ID="Name" CssClass="input" runat="server" Height="20px" Width="100px" />
            评论标题：<asp:TextBox ID="txtTitle" CssClass="input" runat="server" Height="20px" Width="100px" />
            评论时间：<SkyCES:TextBox CssClass="input" ID="StartPostDate" runat="server" RequiredFieldType="日期时间"
                onfocus="cdr.show(this);" />
            到
            <SkyCES:TextBox CssClass="input" ID="EndPostDate" runat="server" RequiredFieldType="日期时间"
                onfocus="cdr.show(this);" />
            <asp:Button CssClass="button" ID="SearchButton" Text=" 搜 索 " runat="server" OnClick="SearchButton_Click" /></li>
    </ul>
    <table class="listTable" cellpadding="0" cellpadding="0">
        <tr class="listTableHead">
            <td style="width: 5%">
                ID
            </td>
            <td style="width: 30%; text-align: left; text-indent: 8px;">
                评论标题
            </td>
            <td style="width: 25%; text-align: left; text-indent: 8px;">
                商品
            </td>
            <td style="width: 15%">
                时间
            </td>
            <td style="width: 10%">
                状态
            </td>
            <td style="width: 10%">
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
                    <td style="width: 30%; text-align: left; text-indent: 8px;">
                        <%# Eval("Title") %>
                    </td>
                    <td style="width: 25%; text-align: left; text-indent: 8px;">
                        <%#Eval("Product.Name") %>
                    </td>
                    <td style="width: 15%">
                        <%#Eval("PostDate") %>
                    </td>
                    <td style="width: 10%">
                        <%# ProductCommentBLL.ReadCommentStatus(Convert.ToInt32(Eval("Status")))%>
                    </td>
                    <td style="width: 10%;">
                        <a href="javascript:pop('ProductCommentAdd.aspx?ID=<%# Eval("ID") %>',800,600,'商品评论审核','ProductCommentAdd<%# Eval("ID") %>')">
                            <img src="Style/Images/edit.gif" alt="商品评论审核" title="商品评论审核" /></a> <a href="javascript:popPageOnly('ProductReply.aspx?CommentID=<%#Eval("ID")%>',800,600,'评论回复列表','ProductReply<%# Eval("ID") %>')">
                                <img src="Style/Images/list.gif" alt="评论回复列表" title="评论回复列表" /></a>
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
        <asp:Button CssClass="button" ID="NoHandlerButton" Text=" 未处理 " OnClientClick="return checkSelect()"
            runat="server" OnClick="NoHandlerButton_Click" />&nbsp;
        <asp:Button CssClass="button" ID="NoShowButton" Text=" 不显示 " OnClientClick="return checkSelect()"
            runat="server" OnClick="NoShowButton_Click" />&nbsp;
        <asp:Button CssClass="button" ID="ShowButton" Text=" 显 示 " OnClientClick="return checkSelect()"
            runat="server" OnClick="ShowButton_Click" />&nbsp;
        <asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()"
            runat="server" OnClick="DeleteButton_Click" />&nbsp;
        <input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
    </div>
</asp:Content>
