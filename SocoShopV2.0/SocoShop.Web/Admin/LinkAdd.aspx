<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="LinkAdd.aspx.cs" Inherits="SocoShop.Web.Admin.LinkAdd" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>链接<%=GetAddUpdate()%></div>
<div class="add">
	<ul>
		<li class="left">类别：</li>
		<li class="right"><asp:DropDownList ID="LinkClass" runat="server" onchange="changeLink(this.value)"><asp:ListItem Value="1">文字链接</asp:ListItem><asp:ListItem Value="2">图片链接</asp:ListItem></asp:DropDownList></li>
	</ul>
	<div id="TextLink">
	<ul>
		<li class="left">文字：</li>
		<li class="right"><SkyCES:TextBox ID="TextDisplay" CssClass="input" runat="server"  Width="400px" /></li>
	</ul>
	</div>
	<div id="PictureLink" style="display:none">
	<ul>
		<li class="left">图片：</li>
		<li class="right"><SkyCES:TextBox ID="PictureDisplay" CssClass="input" runat="server" Width="400px"  /></li>
	</ul>
	<ul>
		<li class="left">上传附件：</li>
		<li class="right"><iframe src="UploadAdd.aspx?Control=PictureDisplay&TableID=<%=LinkBLL.TableID%>&FilePath=LinkUpload/Original" width="400" height="30px" frameborder="0" allowTransparency="true" scrolling="no"></iframe></li>
	</ul> 	
	</div>
	<ul>
		<li class="left">链接地址：</li>
		<li class="right"><SkyCES:TextBox ID="URL" CssClass="input" runat="server"  Width="400px"  RequiredFieldType="网页地址"/></li>
	</ul>	
	<ul>
		<li class="left">备注信息：</li>
		<li class="right"><SkyCES:TextBox ID="Remark" CssClass="input" runat="server" Width="400px" TextMode="MultiLine" Height="50px" /></li>
	</ul>
</div>		
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
</div>
<script language="javascript" type="text/javascript">
    var classID=<%=classID %>;
    changeLink(classID);  
    function changeLink(value)
    {
       var textLink=o("TextLink");
       var pictureLink=o("PictureLink");
       textLink.style.display="none";
       pictureLink.style.display="none";
       if(value=="2"){
            pictureLink.style.display="";
       }
       else{
            textLink.style.display="";
       }
    }
</script>		
</asp:Content>