<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="Admin.aspx.cs" Inherits="SocoShop.Web.Admin.Admin" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Business" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<script language="javascript" type="text/javascript" src="/js/Admin.js"></script>
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>管理员列表</div>	
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
        <td style="width:5%">ID</td>
        <td style="width:20%; text-align:left;text-indent:8px;">管理员</td>
        <td style="width:15%">管理员组</td>
        <td style="width:20%">上次登陆时间</td>		 
        <td style="width:10%">上次登陆IP</td>	
        <td style="width:15%">登陆次数</td>	    
        <td style="width:10%">管理</td>
        <td style="width:5%">选择</td>        
    </tr>
<asp:Repeater ID="RecordList" runat="server">
	<ItemTemplate>	     
            <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
                <td style="width:5%"><%# Eval("ID")%></td>
                <td style="width:20%; text-align:left;text-indent:8px;"><a href='AdminAdd.aspx?ID=<%# Eval("ID") %>'><%# Eval("Name")%></a></td>
                <td style="width:15%"><%# AdminGroupBLL.ReadAdminGroupCache(Convert.ToInt32(Eval("GroupID"))).Name%></td>
                <td style="width:20%"><%# Eval("LastLoginDate")%></td>		 
                <td style="width:10%"><%# Eval("LastLoginIP")%></td>	
                <td style="width:15%"><%# Eval("LoginTimes")%></td>	    
                <td style="width:10%"><a href="javascript:pop('AdminAdd.aspx?ID=<%# Eval("ID")%>',600,300,'管理员修改','AdminAdd<%# Eval("ID")%>')"><img src="Style/Images/edit.gif" alt="修改" title="修改" /></a>&nbsp;<%#AdminBLL.NoPasswordAdd(Eval("ID"))%></td>
                <td style="width:5%"><%# AdminBLL.NoDelete(Eval("IsCreate"), Eval("ID"))%></td> 	        
            </tr>
        </ItemTemplate>
</asp:Repeater>
</table>
<div class="listPage"><SkyCES:CommonPager ID="MyPager" runat="server" /></div>
<div class="action">
        <input type="button"  value=" 添 加 " class="button"  onclick="pop('AdminAdd.aspx',600,300,'管理员添加','AdminAdd')"/>&nbsp;<asp:Button CssClass="button" ID="DeleteButton" Text=" 删 除 " OnClientClick="return checkSelect()" runat="server"  OnClick="DeleteButton_Click"/>&nbsp;<input type="checkbox" name="All" onclick="selectAll(this)" />全选/取消
</div>
</asp:Content>
