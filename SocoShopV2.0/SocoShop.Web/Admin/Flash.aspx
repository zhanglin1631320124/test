<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="Flash.aspx.cs" Inherits="SocoShop.Web.Admin.Flash" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>Flash列表</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
	    <td style="width:5%">ID</td>
        <td style="width:25%; text-align:left;text-indent:8px;">标题</td>    
        <td style="width:40%;">说明</td>    
        <td style="width:10%">宽 X 高</td>		
        <td style="width:10%">调用代码</td>	  
        <td style="width:10%">管理</td>
    </tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>
		<tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">				
			<td style="width:5%"><%# Eval("ID")%></td>
            <td style="width:25%; text-align:left;text-indent:8px;"><%# Eval("Title")%></td>  
            <td style="width:40%"><%# Eval("Introduce")%></td>          
            <td style="width:10%"><%# Eval("Width")%> X <%# Eval("Height")%></td>
            <td style="width:10%"><a href="javascript:copyText('<%# ShopCommon.GetFlashFile(Eval("ID").ToString()) %>')"><strong><font color="blue">点击获取</font></strong></a></td>	    
            <td style="width:10%">
                <a href="javascript:popPageOnly('FlashPhoto.aspx?FlashID=<%# Eval("ID")%>',800,600,'Flash图片管理','FlashPhoto<%# Eval("ID") %>')"><img src="Style/Images/list.gif" alt="Flash图片管理" title="Flash图片管理" /></a>  
                <a href="javascript:pop('FlashAdd.aspx?ID=<%# Eval("ID") %>',600,300,'Flash修改','FlashAdd<%# Eval("ID") %>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a> 
            </td>	
		</tr>
	</ItemTemplate>
</asp:Repeater>
</table>
<div class="listPage">
    <SkyCES:CommonPager ID="MyPager" runat="server" />
</div>
<div class="action">
    <input type="button"  value=" 添 加 " class="button"  onclick="pop('FlashAdd.aspx',600,300,' Flash添加','FlashAdd')"/>
</div>
</asp:Content>