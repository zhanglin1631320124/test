<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="AdAdd.aspx.cs" Inherits="SocoShop.Web.Admin.AdAdd" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<script language="javascript" src="/Admin/Js/calendar.js" type="text/javascript"></script>
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>广告<%=GetAddUpdate()%></div>
<div class="add">
	<ul>
		<li class="left">标题：</li>
		<li class="right"><SkyCES:TextBox ID="txtTitle" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></li>
	</ul>
	<ul>
		<li class="left">说明：</li>
		<li class="right"><SkyCES:TextBox ID="Introduction" CssClass="input" runat="server" Width="400px"  TextMode="MultiLine" Height="50px" HintInfo="描述该广告的位置，便于自己识别"/></li>
	</ul>
	<ul>
		<li class="left">类别：</li>
		<li class="right"><asp:DropDownList ID="AdClass" runat="server" onchange="changeAd(this.value)"><asp:ListItem Value="1">文字广告</asp:ListItem><asp:ListItem Value="2">图片广告</asp:ListItem><asp:ListItem Value="3">Flash广告</asp:ListItem><asp:ListItem Value="4">代码广告</asp:ListItem></asp:DropDownList></li>
	</ul>
	<div id="TextAd">
	<ul>
		<li class="left">文字：</li>
		<li class="right"><SkyCES:TextBox ID="TextDisplay" CssClass="input" runat="server" TextMode="MultiLine" Height="100px" Width="400px" /></li>
	</ul>
	<ul>
		<li class="left">链接地址：</li>
		<li class="right"><SkyCES:TextBox ID="TextURL" CssClass="input" runat="server" Width="400px" /></li>
	</ul>
	</div>
	<div id="PictureAd" style="display:none">
	<ul>
		<li class="left">图片：</li>
		<li class="right"><SkyCES:TextBox ID="PictureDisplay" CssClass="input" runat="server" Width="400px" /></li>
	</ul>
	<ul>
		<li class="left">上传图片：</li>
		<li class="right"><iframe src="UploadAdd.aspx?Control=PictureDisplay&TableID=<%=AdBLL.TableID%>&FilePath=AdUpload" width="400" height="30px" frameborder="0" allowTransparency="true" scrolling="no"></iframe></li>
	</ul> 
	<ul>
		<li class="left">链接地址：</li>
		<li class="right"><SkyCES:TextBox ID="PictureURL" CssClass="input" runat="server"  Width="400px"/></li>
	</ul>
	</div>
	<div id="FlashAd" style="display:none">
	<ul>
		<li class="left">Flash：</li>
		<li class="right"><SkyCES:TextBox ID="FlashDisplay" CssClass="input" runat="server" Width="400px" /></li>
	</ul>
	<ul>
		<li class="left">上传Flash：</li>
		<li class="right"><iframe src="UploadAdd.aspx?Control=FlashDisplay&TableID=<%=AdBLL.TableID%>&FilePath=AdUpload" width="400" height="30px" frameborder="0" allowTransparency="true" scrolling="no"></iframe></li>
	</ul> 
	</div>
	<div id="CodeAd" style="display:none">
	<ul>
		<li class="left">代码：</li>
		<li class="right"><SkyCES:TextBox ID="CodeDisplay" CssClass="input" runat="server" TextMode="MultiLine" Height="100px" Width="400px" HintInfo="支持html的代码"/></li>
	</ul>
	</div>
	<ul>
		<li class="left">大小：</li>
		<li class="right">宽 <SkyCES:TextBox ID="Width" CssClass="input" runat="server" Width="80px" CanBeNull="必填" RequiredFieldType="数据校验"/>px X 高 <SkyCES:TextBox ID="Height" CssClass="input" runat="server" Width="100px" CanBeNull="必填" RequiredFieldType="数据校验"/>px</li>
	</ul>
	<ul>
		<li class="left">有效时间：</li>
		<li class="right">从 <SkyCES:TextBox ID="StartDate" CssClass="input" runat="server" Width="150px" CanBeNull="必填" RequiredFieldType="日期时间"  onfocus="cdr.show(this);"/>  到 
		              <SkyCES:TextBox ID="EndDate" CssClass="input" runat="server" Width="150px" CanBeNull="必填" RequiredFieldType="日期时间"  onfocus="cdr.show(this);"/></li>
	</ul>
	<ul>
		<li class="left">备注信息：</li>
		<li class="right"><SkyCES:TextBox ID="Remark" CssClass="input" runat="server" Width="400px"  TextMode="MultiLine" Height="50px" HintInfo="描述该广告的其他信息，比如广告的联系人信息，便于自己以后查找"/></li>
	</ul>
	<ul>
		<li class="left">是否启用：</li>
		<li class="right"><asp:RadioButtonList ID="IsEnabled" runat="server" RepeatDirection="Horizontal"><asp:ListItem Value="0" Selected="True">否</asp:ListItem><asp:ListItem Value="1">是</asp:ListItem></asp:RadioButtonList></li>
	</ul>
	<ul><SkyCES:Hint ID="Hint" runat="server"/></ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server"  OnClick="SubmitButton_Click" />
</div>
<script language="javascript" type="text/javascript">
    var classID=<%=classID %>;
    changeAd(classID);
    function changeAd(value){
       var textAd=o("TextAd");
       var pictureAd=o("PictureAd");
       var flashAd=o("FlashAd");
       var codeAd=o("CodeAd");
       textAd.style.display="none";
       pictureAd.style.display="none";
       flashAd.style.display="none";
       codeAd.style.display="none";
       if(value=="2"){
            pictureAd.style.display="";
       }
       else if(value=="3"){
            flashAd.style.display="";
       }
       else if(value=="4"){
            codeAd.style.display="";
       }
       else{
            textAd.style.display="";
       }
    }
</script>
</asp:Content>
