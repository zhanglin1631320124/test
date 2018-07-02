<%@ Page Language="C#" AutoEventWireup="true" Inherits="SocoShop.Login.QQ.AddUser" Codebehind="AddUser.aspx.cs" %>
<%@ Import Namespace="SocoShop.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <asp:PlaceHolder ID="PHead" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="PTop" runat="server" />
    <div class="main">
        <% if (errorMessage != string.Empty){%>
        <div class="alertMessage"><%=errorMessage%></div>
        <%} %>
        <div class="loginTitle">填写用户名</div>
        <div class="add register">
            <ul>
                <li class="left">用户名：</li>
                <li class="right"><input type="text" class="input" style="width:120px" name="UserName" id="UserName" onblur="checkUserName(<%=ShopConfig.ReadConfigInfo().UserNameMinLength%>,<%=ShopConfig.ReadConfigInfo().UserNameMaxLength%>)" /> <input id="CheckUserName" value="0" type="hidden" /><span id="UserNameWarningMessage"></span> <%=ShopConfig.ReadConfigInfo().UserNameMinLength%>-<%=ShopConfig.ReadConfigInfo().UserNameMaxLength%>个字符组成（包括字母、数字、下划线、中文）。</li>
            </ul>   
            <ul>
                <li class="left"></li>
                <li class="right"><asp:Button ID="SubmitButton" Text=" 确 定 " runat="server" CssClass="button"  OnClick="SubmitButton_Click" OnClientClick="return checkAddUser()" /></li>
            </ul> 
        </div> 
    </div>    
    <asp:PlaceHolder ID="PFoot" runat="server" />
    </form>
    <script language="javascript" type="text/javascript" src="/Plugins/Template/<%=ShopConfig.ReadConfigInfo().TemplatePath%>/Js/Register.js" ></script>
    <script type="text/javascript">
    function checkAddUser() {
        if(o("CheckUserName").value=="0"){
            alertMessage("用户名有错误");
            return false;
        }
        return true
    }
    </script>
</body>
</html>
