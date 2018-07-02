<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="FlashPhoto.aspx.cs" Inherits="SocoShop.Web.Admin.FlashPhoto" %>

<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />Flash图片列表</div>
    <table class="listTable" cellpadding="0" cellpadding="0">
        <tr class="listTableHead">
            <td style="width: 5%">
                ID
            </td>
            <td style="width: 30%; text-align: left; text-indent: 8px;">
                显示
            </td>
            <td style="width: 20%">
                标题
            </td>
            <td style="width: 25%">
                URL
            </td>
            <td style="width: 15%">
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
                        <%# Eval("ID")%>
                    </td>
                    <td style="width: 30%; text-align: left; text-indent: 8px; overflow: hidden">
                        <img src="<%# Eval("FileName")%>" height="80" />
                    </td>
                    <td style="width: 20%">
                        <%#Eval("Title")%>
                    </td>
                    <td style="width: 25%">
                        <%#Eval("URL")%>
                    </td>
                    <td style="width: 15%">
                        <a href="?Action=Up&PhotoID=<%# Eval("ID") %>&FlashID=<%# Eval("FlashID") %>">
                            <img src="Style/Images/moveUp.gif" alt="上移" title="上移" /></a> <a href="?Action=Down&PhotoID=<%# Eval("ID") %>&FlashID=<%# Eval("FlashID") %>">
                                <img src="Style/Images/moveDown.gif" alt="下移" title="下移" /></a> <a href="FlashPhoto.aspx?ID=<%#Eval("ID")%>&FlashID=<%# Eval("FlashID") %>">
                                    <img src="Style/Images/edit.gif" alt="修改" title="修改" /></a>
                    </td>
                    <td style="width: 5%">
                        <input type="checkbox" name="SelectID" value="<%# Eval("ID") %>" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="action">
        <input type="button" value=" 添 加 " class="button" onclick="window.location.href='FlashPhoto.aspx?FlashID=<%=flashID %>'" />&nbsp;<asp:Button
            CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()"
            runat="server" OnClick="DeleteButton_Click" />&nbsp;<input type="checkbox" name="All"
                onclick="selectAll(this)" />全选/取消
    </div>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />Flash图片<%=GetAddUpdate()%></div>
    <div class="add">
        <ul>
            <li class="left">名称：</li>
            <li class="right">
                <SkyCES:TextBox ID="txtTitle" CssClass="input" runat="server" Width="400px" /></li>
        </ul>
        <ul>
            <li class="left">链接地址：</li>
            <li class="right">
                <SkyCES:TextBox ID="URL" CssClass="input" runat="server" Width="400px" /></li>
        </ul>
        <ul>
            <li class="left">图片：</li>
            <li class="right">
                <SkyCES:TextBox ID="FileName" CssClass="input" runat="server" Width="400px" /></li>
        </ul>
        <ul>
            <li class="left">上传附件：</li>
            <li class="right">
                <iframe src="UploadAdd.aspx?Control=FileName&TableID=<%=FlashPhotoBLL.TableID%>&FilePath=FlashPhotoUpload"
                    width="400px" height="30px" frameborder="0" allowtransparency="true" scrolling="no">
                </iframe>
            </li>
        </ul>
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
    </div>
</asp:Content>
