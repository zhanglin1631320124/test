<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="UserStatus.aspx.cs" Inherits="SocoShop.Web.Admin.UserStatus" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">	
<script language="javascript" src="/Admin/js/calendar.js" type="text/javascript"></script>
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>会员分析</div>	
<div class="listBlock">
    <ul>
        <li><a href="UserCount.aspx">数量分析</a></li>
        <li class="listOn"><a href="UserStatus.aspx">状态分析</a></li>
        <li><a href="UserActive.aspx">活跃度分析</a></li>
        <li><a href="UserConsume.aspx">消费分析</a></li>   
    </ul>	
</div>
<div class="pageMark">	
    <div class="statisticsSearch">
		    注册时间：<SkyCES:TextBox CssClass="input" ID="StartRegisterDate" runat="server" RequiredFieldType="日期时间"  onfocus="cdr.show(this);"/> 到 <SkyCES:TextBox CssClass="input" ID="EndRegisterDate" runat="server" RequiredFieldType="日期时间"  onfocus="cdr.show(this);"/> 
	        <asp:Button CssClass="button" ID="SearchButton" Text=" 搜 索 " runat="server"  OnClick="SearchButton_Click" />
    </div>
    <OBJECT classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"  codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" WIDTH="600" HEIGHT="300" id="OrderGeneral" ALIGN="middle">
        <PARAM NAME="FlashVars" value="&dataXML=<graph caption='用户状态分析' decimalPrecision='2' showPercentageValues='0' showNames='1' showValues='1' showPercentageInLabel='0' pieYScale='45' pieBorderAlpha='40' pieFillAlpha='70' pieSliceDepth='15' pieRadius='100' outCnvBaseFontSize='13' baseFontSize='12'><%=result %></graph>">
        <PARAM NAME="movie" VALUE="flash/pie3d.swf?chartWidth=600&chartHeight=300">
        <PARAM NAME="quality" VALUE="high">
        <PARAM NAME=bgcolor VALUE="#FFFFFF">
        <param name="wmode" value="opaque" />
        <EMBED src="flash/pie3d.swf?chartWidth=600&chartHeight=300" FlashVars="&dataXML=<graph caption='用户状态分析' decimalPrecision='2' showPercentageValues='0' showNames='1' showValues='1' showPercentageInLabel='0' pieYScale='45' pieBorderAlpha='40' pieFillAlpha='70' pieSliceDepth='15' pieRadius='100' outCnvBaseFontSize='13' baseFontSize='12'><%=result %></graph>" quality="high" bgcolor="#FFFFFF" WIDTH="600" HEIGHT="300" wmode="opaque" NAME="OrderGeneral" ALIGN="middle" TYPE="application/x-shockwave-flash" PLUGINSPAGE="http://www.macromedia.com/go/getflashplayer"></EMBED>
    </OBJECT>
</div>
</asp:Content>
