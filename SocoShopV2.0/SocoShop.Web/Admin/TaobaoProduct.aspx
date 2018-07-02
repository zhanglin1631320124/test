<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="TaobaoProduct.aspx.cs" Inherits="SocoShop.Web.Admin.TaobaoProduct" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SkyCES.EntLib" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>淘宝商品</div>
<ul class="search">
    <li>友情提示：1、请保存设置之后再导入淘宝数据；2、如果要导多个淘宝店的数据，建议选择“不删除旧淘宝商品分类”，否则建议选择“删除旧淘宝商品分类”。3、由于导数据会耗费比较长的时间，请点击按钮之后耐心等待！</li>
</ul>
<div class="add">
	<ul>
		<li class="left">AppKey：</li>
		<li class="right"><SkyCES:TextBox ID="AppKey" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></li>
	</ul>
	<ul>
		<li class="left">AppSecret：</li>
		<li class="right"><SkyCES:TextBox ID="AppSecret" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></li>
	</ul>
	<ul>
		<li class="left">账户名：</li>
		<li class="right"><SkyCES:TextBox ID="NickName" CssClass="input" runat="server" Width="400px" CanBeNull="必填" /></li>
	</ul>
	<ul>
		<li class="left">删除淘宝商品分类：</li>
		<li class="right"><asp:RadioButtonList ID="DeleteProductClass" runat="server" RepeatDirection="Horizontal"><asp:ListItem Value="0" Selected="True">不删除旧淘宝商品分类</asp:ListItem><asp:ListItem Value="1">删除旧淘宝商品分类</asp:ListItem></asp:RadioButtonList></li>
	</ul>
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text="保存设置" runat="server"  OnClick="SubmitButton_Click" />    
    <input type="button" name="ImportProductClassButton" value="导入淘宝分类" class="button" style="width:100px;" onclick="importProductClass()" />    
    <input type="button" name="ImportProducButton" value="导入淘宝商品" class="button" style="width:100px;" onclick="window.open('TaobaoProductAdd.aspx')" />
    <input type="button" name="ImportProductClassButton" value="下载淘宝图片" class="button" style="width:100px;" onclick="downTaobaoPhoto()" />  
</div>
<script language="javascript" type="text/javascript">
//导入淘宝分类数据
function importProductClass(){
    var url="Ajax.aspx?Action=ImportProductClass";
    Ajax.requestURL(url,dealImportProductClass);
    alertMessage("正在导入淘宝商品分类","1");
}
function dealImportProductClass(data){
    closeAlertDiv();
    if(data=="ok"){
        alertMessage("成功导入淘宝分类数据");
    }
    else{
        alertMessage(data);
    }
}
//下载淘宝图片
function downTaobaoPhoto(){
    var url="Ajax.aspx?Action=DownTaobaoPhoto";
    Ajax.requestURL(url,dealDownTaobaoPhoto);
    alertMessage("正在下载淘宝图片","1");
}
function dealDownTaobaoPhoto(data){
    closeAlertDiv();
    if(data=="ok"){
        alertMessage("成功下载淘宝图片");
    }
    else{
        alertMessage(data);
    }
}
</script>
</asp:Content>
