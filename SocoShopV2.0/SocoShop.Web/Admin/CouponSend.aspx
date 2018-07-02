<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True"
    CodeBehind="CouponSend.aspx.cs" Inherits="SocoShop.Web.Admin.CouponSend" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="/Admin/Js/ProductAdd.js"></script>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />发放优惠券</div>
    <div class="add">
        <ul>
            <li class="left">名称：</li>
            <li class="right">
                <%=coupon.Name%></li>
        </ul>
        <ul>
            <li class="left">优惠券金额：</li>
            <li class="right">
                <%=coupon.Money%>
                元 </li>
        </ul>
        <ul>
            <li class="left">最小订单产品金额：</li>
            <li class="right">
                <%=coupon.UseMinAmount%>
                元 </li>
        </ul>
        <ul>
            <li class="left">线下发放数量：</li>
            <li class="right">
                <SkyCES:TextBox ID="SendCount" CssClass="input" runat="server" Width="140px" RequiredFieldType="数据校验"
                    Text="0" /></li>
        </ul>
        <ul>
            <li class="left">在线发放：</li>
            <li class="right">
                <ul class="search">
                    <li>用户名：<SkyCES:TextBox ID="UserName" CssClass="input" runat="server" Height="20px"
                        Width="150px" />
                        <input id="Button1" type="button" class="button" value=" 搜 索 " onclick="searchUser()" /></li>
                </ul>
                <ul style="text-align: center; overflow: hidden; height: 310px">
                    <li id="CandidateUserBox">
                        <select id="<%=IDPrefix%>CandidateUser" name="<%=NamePrefix %>CandidateUser" style="width: 260px;
                            height: 300px;" multiple="multiple">
                        </select>
                    </li>
                    <li style="margin: 80px 10px 10px 10px">
                        <input id="Button2" type="button" class="button" value=">>" onclick="addAll('<%=IDPrefix%>CandidateUser','<%=IDPrefix%>User')" /><br />
                        <br />
                        <input id="Button3" type="button" class="button" value=">" onclick="addSingle('<%=IDPrefix%>CandidateUser','<%=IDPrefix%>User')" /><br />
                        <br />
                        <input id="Button4" type="button" class="button" value="<" onclick="dropSingle('<%=IDPrefix%>User')" /><br />
                        <br />
                        <input id="Button5" type="button" class="button" value="<<" onclick="dropAll('<%=IDPrefix%>User')" /><br />
                    </li>
                    <li>
                        <asp:ListBox ID="lbxUser" runat="server" SelectionMode="Multiple" Width="260px" Height="300px">
                        </asp:ListBox>
                        <input type="hidden" name="RelationUser" id="RelationUser" />
                    </li>
                </ul>
            </li>
        </ul>
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click"
            OnClientClick="return checkSubmit()" />
    </div>
    <script language="javascript" type="text/javascript">        //搜索用户
        function searchUser() {
            var userName = o(globalIDPrefix + "UserName").value;
            var url = "ProductAjax.aspx?ControlName=CandidateUser&Action=SearchUser&UserName=" + encodeURI(userName);
            Ajax.requestURL(url, dealSearchUser);
            alertMessage("正在搜索...", 1);
        }
        function dealSearchUser(data) {
            closeAlertDiv();
            var obj = o("CandidateUserBox");
            obj.removeChild(o(globalIDPrefix + "CandidateUser"));
            obj.innerHTML = data;
        }
        //提交检查
        function checkSubmit() {
            checkProductHandler("<%=IDPrefix%>User", "RelationUser");
            Page_ClientValidate();
        }</script>
</asp:Content>
