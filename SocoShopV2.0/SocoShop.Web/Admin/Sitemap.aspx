<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True"  CodeBehind="Sitemap.aspx.cs" Inherits="SocoShop.Web.Admin.Sitemap" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES"%>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>网站地图</div>
<ul class="search">
    <li>友情提示：由于网站使用的ajax导致产品网页无法收录，该网站地图只生成产品相关的地图。请保存设置之后再生成网站地图！</li>
</ul>
<div class="add">
    <ul>
		<li class="left">域名：</li>
		<li class="right">http://<SkyCES:TextBox CssClass="input" Width="300px" ID="Domain" runat="server" CanBeNull="必填" /></li>
	</ul>
	<ul>
		<li class="left">更新频率：</li>
		<li class="right">
		    <asp:DropDownList ID="Frequency" runat="server">
		        <asp:ListItem Value="always">经常</asp:ListItem>
		        <asp:ListItem Value="hourly">每小时</asp:ListItem>
		        <asp:ListItem Value="daily">每天</asp:ListItem>
		        <asp:ListItem Value="weekly">每周</asp:ListItem>
		        <asp:ListItem Value="monthly">每月</asp:ListItem>
		        <asp:ListItem Value="yearly">每年</asp:ListItem>
		        <asp:ListItem Value="never">从不</asp:ListItem>
		    </asp:DropDownList>
		</li>
	</ul>
	<ul>
		<li class="left">优先级：</li>
		<li class="right">
		    <asp:DropDownList ID="Priority" runat="server">
		        <asp:ListItem>0.1</asp:ListItem>
		        <asp:ListItem>0.2</asp:ListItem>
		        <asp:ListItem>0.3</asp:ListItem>
		        <asp:ListItem>0.4</asp:ListItem>
		        <asp:ListItem>0.5</asp:ListItem>
		        <asp:ListItem>0.6</asp:ListItem>
		        <asp:ListItem>0.7</asp:ListItem>
		        <asp:ListItem>0.8</asp:ListItem>
		        <asp:ListItem>0.9</asp:ListItem>
		        <asp:ListItem>1.0</asp:ListItem>
		    </asp:DropDownList>
		</li>
	</ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text="保存设置" runat="server" OnClick="SubmitButton_Click" />
    <input type="button" name="ImportProductClassButton" value="生成网站地图" class="button" style="width:100px;" onclick="createSitemap()" />  
</div>
<script language="javascript" type="text/javascript">
//导入淘宝分类数据
function createSitemap(){
    var url="Ajax.aspx?Action=CreateSitemap";
    Ajax.requestURL(url,dealCreateSitemap);
    alertMessage("正在生成网站地图","1");
}
function dealCreateSitemap(data){
    closeAlertDiv();
    if(data=="ok"){
        alertMessage("成功生成网站地图");
    }
    else{
        alertMessage(data);
    }
}
</script>
</asp:Content>
