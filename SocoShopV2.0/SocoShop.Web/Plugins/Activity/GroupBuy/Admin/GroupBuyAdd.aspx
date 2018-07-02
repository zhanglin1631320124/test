<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true"
    Inherits="SocoShop.Web.GroupBuyAdd" CodeBehind="GroupBuyAdd.aspx.cs" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Web" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="/Admin/Js/SearchProduct.js"></script>
    <script language="javascript" src="/Admin/js/calendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Admin/Pop/lhgdialog_ex.js"></script>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />团购活动<%=GetAddUpdate()%></div>
    <div class="add">
        <ul>
            <li class="left">活动名称：</li>
            <li class="right">
                <SkyCES:TextBox ID="Name" CssClass="input" runat="server" Width="500px" CanBeNull="必填"
                    TextMode="MultiLine" Height="40px" /></li>
        </ul>
        <ul>
            <li class="left">图片：</li>
            <li class="right">
                <SkyCES:TextBox ID="Photo" CssClass="input" runat="server" Width="500px" /></li>
        </ul>
        <ul>
            <li class="left">上传图片：</li>
            <li class="right">
                <iframe src="/Admin/UploadAdd.aspx?Control=Photo&TableID=<%=GroupBuyBLL.TableID %>&FilePath=GroupBuyPhoto/Original"
                    width="400" height="30px" frameborder="0" allowtransparency="true" scrolling="no">
                </iframe>
            </li>
        </ul>
        <ul>
            <li class="left">描述：</li>
            <li class="right">
                <FCKeditorV2:FCKeditor ToolbarSet="Basic" id="Description" runat="server" Width="600px"
                    Height="330px" /></li>
        </ul>
        <ul>
            <li class="left">产品：</li>
            <li class="right">
                <input type="text" id="ProductName" name="ProductName" class="input" style="width: 100px;" />
                <input type="button" id="Search" name="Search" onclick="searchProduct()" class="button"
                    value=" 搜 索 " />
                <span id="PrdouctIDBox">
                    <asp:DropDownList ID="PrdouctID" runat="server" Width="240px" />
                </span></li>
        </ul>
        <ul>
            <li class="left">开始时间：</li>
            <li class="right">
                <SkyCES:TextBox ID="StartDate" CssClass="input" runat="server" Width="140px" RequiredFieldType="日期时间"
                    onfocus="cdr.show(this);" />
                到
                <SkyCES:TextBox ID="EndDate" CssClass="input" runat="server" Width="140px" RequiredFieldType="日期时间"
                    onfocus="cdr.show(this);" /></li>
        </ul>
        <ul>
            <li class="left">限购数量：</li>
            <li class="right">
                <SkyCES:TextBox ID="MinCount" CssClass="input" runat="server" Width="60px" CanBeNull="必填"
                    RequiredFieldType="数据校验" Text="0" />
                到
                <SkyCES:TextBox ID="MaxCount" CssClass="input" runat="server" Width="60px" CanBeNull="必填"
                    RequiredFieldType="数据校验" Text="0" /></li>
            <li class="left">单人限购数量：</li>
            <li class="right">
                <SkyCES:TextBox ID="EachNumber" CssClass="input" runat="server" Width="100px" HintInfo="-1表示不限制"
                    CanBeNull="必填" RequiredFieldType="数据校验" Text="0" /></li>
        </ul>
        <ul>
            <li class="left">价格：</li>
            <li class="right">
                <SkyCES:TextBox ID="Price" CssClass="input" runat="server" Width="100px" CanBeNull="必填"
                    RequiredFieldType="金额" Text="0" /></li>
        </ul>
        <ul>
            <SkyCES:Hint ID="Hint" runat="server" />
        </ul>
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
    </div>
</asp:Content>
