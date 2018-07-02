<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="Article.aspx.cs" Inherits="SocoShop.Web.Admin.Article" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Business" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script language="javascript" src="/Admin/js/calendar.js" type="text/javascript"></script>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />资讯列表</div>
    <ul class="search">
        <li>类别：<asp:DropDownList ID="ArticleClassID" runat="server" />
            标题：<SkyCES:TextBox CssClass="input" ID="txtTitle" runat="server" Width="200px" />
            是否推荐：<asp:DropDownList ID="IsTop" runat="server">
                <asp:ListItem Value="">全部</asp:ListItem>
                <asp:ListItem Value="0">否</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
            </asp:DropDownList>
            <asp:Button CssClass="button" ID="SearchButton" Text=" 搜 索 " runat="server" OnClick="SearchButton_Click" />
        </li>
    </ul>
    <div class="listBlock">
        <ul>
            <li <%if(classID==string.Empty){%>class="listOn" <%} %> onclick="window.location='Article.aspx'">
                所有资讯</li>
            <%foreach (ArticleClassInfo articleClass in ArticleClassBLL.ReadArticleClassRootList())
              { %>
            <li <%if(classID==articleClass.ID.ToString()){%>class="listOn" <%} %> onclick="window.location='Article.aspx?Action=search&ClassID=|<%=articleClass.ID %>|'">
                <%=articleClass.ClassName%></li>
            <%} %>
        </ul>
    </div>
    <table class="listTable" cellpadding="0" cellpadding="0">
        <tr class="listTableHead">
            <td style="width: 5%">
                ID
            </td>
            <td style="width: 50%; text-align: left; text-indent: 8px;">
                标题
            </td>
            <td style="width: 25%">
                类别
            </td>
            <td style="width: 10%">
                是否推荐
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
                    <td style="width: 50%; text-align: left; text-indent: 8px;">
                        <%# Eval("Title") %>
                    </td>
                    <td style="width: 25%">
                        <%#ArticleClassBLL.ArticleClassNameList(Eval("ClassID").ToString())%>
                    </td>
                    <td style="width: 10%">
                        <%#ShopCommon.GetBoolString(Eval("IsTop")) %>
                    </td>
                    <td style="width: 5%;">
                        <a href="javascript:pop('ArticleAdd.aspx?ID=<%# Eval("ID") %>',800,600,'资讯修改','ActivityAdd<%# Eval("ID") %>')">
                            <img src="Style/Images/edit.gif" alt="修改" title="修改" /></a>
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
        <input type="button" value=" 添 加 " class="button" onclick="pop('ArticleAdd.aspx',800,600,'资讯添加','ActivityAdd')" />&nbsp;<asp:Button
            CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()"
            runat="server" OnClick="DeleteButton_Click" />&nbsp;<input type="checkbox" name="All"
                onclick="selectAll(this)" />全选/取消
    </div>
</asp:Content>
