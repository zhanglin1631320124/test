<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="Ad.aspx.cs" Inherits="SocoShop.Web.Admin.Ad" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />广告列表</div>
    <div class="listBlock">
        <ul>
            <li id="TextAd" <%if(classID==1){%>class="listOn" <%} %>><a href="Ad.aspx?ClassID=1">
                文字广告</a></li>
            <li id="PictureAd" <%if(classID==2){%>class="listOn" <%} %>><a href="Ad.aspx?ClassID=2">
                图片广告</a></li>
            <li id="FlashAd" <%if(classID==3){%>class="listOn" <%} %>><a href="Ad.aspx?ClassID=3">
                Flash广告</a></li>
            <li id="CodeAd" <%if(classID==4){%>class="listOn" <%} %>><a href="Ad.aspx?ClassID=4">
                代码广告</a></li>
        </ul>
    </div>
    <table class="listTable" cellpadding="0" cellpadding="0">
        <tr class="listTableHead">
            <td style="width: 5%">
                ID
            </td>
            <td style="width: 35%; text-align: left; text-indent: 8px;">
                标题
            </td>
            <td style="width: 10%">
                宽 X 高
            </td>
            <td style="width: 20%">
                有效时间
            </td>
            <td style="width: 10%">
                调用代码
            </td>
            <td style="width: 10%">
                点击数
            </td>
            <td style="width: 10%">
                管理
            </td>
        </tr>
        <asp:Repeater ID="RecordList" runat="server">
            <ItemTemplate>
                <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
                    <td style="width: 5%">
                        <%# Eval("ID")%>
                    </td>
                    <td style="width: 35%; text-align: left; text-indent: 8px; height: 30px; overflow: hidden">
                        <%# Eval("Title")%>
                    </td>
                    <td style="width: 10%">
                        <%# Eval("Width")%>
                        X
                        <%# Eval("Height")%>
                    </td>
                    <td style="width: 20%">
                        <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString()%>
                        到
                        <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString()%>
                    </td>
                    <td style="width: 10%">
                        <a href="javascript:copyText('<%# ShopCommon.GetAdFile(Eval("ID").ToString()) %>')">
                            <strong><font color="blue">点击获取</font></strong></a>
                    </td>
                    <td style="width: 10%">
                        <%#Eval("ClickCount")%>
                    </td>
                    <td style="width: 10%">
                        <a href="javascript:pop('AdRecord.aspx?AdID=<%# Eval("ID") %>',800,600,'广告记录','AdRecord<%# Eval("ID") %>')">
                            <img src="Style/Images/list.gif" alt="广告记录" title="广告记录" /></a> <a href="javascript:pop('AdAdd.aspx?ClassID=<%=RequestHelper.GetQueryString<int>("ClassID")%>&ID=<%# Eval("ID") %>',800,600,'广告修改','AdAdd<%# Eval("ID") %>')">
                                <img src="Style/Images/edit.gif" alt="修改" title="修改" /></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="listPage">
        <SkyCES:CommonPager ID="MyPager" runat="server" />
    </div>
    <div class="action">
        <input type="button" value=" 添 加 " class="button" onclick="pop('AdAdd.aspx',800,600,'广告添加','AdAdd')" />
    </div>
</asp:Content>
