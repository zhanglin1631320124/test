<%@ Page Language="C#" AutoEventWireup="true" Inherits="SocoShop.Web.FillUserInfo" Codebehind="FillUserInfo.aspx.cs" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Register Namespace="SkyCES.EntLib"  Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <asp:PlaceHolder ID="PHead" runat="server" />
    <script language="javascript" type="text/javascript" src="/Admin/js/UnlimitClass.js"></script>
    <style type="text/css">
        .main .left
        {
        	float:left; width:550px; padding-top:60px; padding-left:10px;
        }
        .main .right
        {
        	float:left; width:400px; overflow:hidden;
        }
        .productPicture .photo
        {
        	width:340px; height:340px;
        }
        .productPicture .productName
        {
        	width:340px; height:30px;  text-align:center;display:block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="PTop" runat="server" />
    <div class="main">
        <div class="add left">
            <ul>
                <li class="left"> 收货人姓名：</li>
                <li class="right"><SkyCES:TextBox ID="Consignee" CssClass="input"  style="width:200px" runat="server" CanBeNull="必填"/></li>
            </ul>  
            <ul>
                <li class="left"> 固定电话：</li>
                <li class="right"><SkyCES:TextBox ID="Tel" CssClass="input"  style="width:200px" runat="server" /></li>
            </ul>
            <ul>
                <li class="left"> 手机：</li>
                <li class="right"><SkyCES:TextBox ID="Mobile" CssClass="input"  style="width:200px" runat="server" /></li>
            </ul>
             <ul>
                <li class="left"> 地址：</li>
                <li class="right"><SkyCES:SingleUnlimitControl ID="RegionID" runat="server" /> 邮编：<SkyCES:TextBox ID="ZipCode" CssClass="input"  style="width:80px" runat="server" CanBeNull="必填"/>
                <br /><SkyCES:TextBox ID="Address" CssClass="input"  style="width:300px" runat="server" CanBeNull="必填"/></li>
            </ul> 
             <ul>
                <li class="left">&nbsp;</li>
                <li class="right"><asp:Button ID="bigbutton" runat="server"  OnClick="SubmitButton_Click"  CssClass="bigbutton" Text="确认提交" /></li>
            </ul>
         </div>
         <div class="right">
            <ul class="productPicture">
                <li class="photo"><a href="/ProductDetail-I<%=product.ID%>.aspx"><img src="<%=product.Photo.Replace("Original","340-340")%>"  onload="photoLoad(this,340,340)" /></a></li>
                <li><a href="/ProductDetail-I<%=product.ID%>.aspx" class="productName"><%=product.Name%></a></li>              
            </ul> 
         </div>  
    </div>
    <asp:PlaceHolder ID="PFoot" runat="server" />
    </form>
</body>
</html>
