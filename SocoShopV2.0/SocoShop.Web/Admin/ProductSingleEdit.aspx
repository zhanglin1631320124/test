<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="True" CodeBehind="ProductSingleEdit.aspx.cs" Inherits="SocoShop.Web.Admin.ProductSingleEdit" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<script language="javascript" src="/Admin/js/ProductBatchEdit.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="/Admin/js/calendar.js"></script>
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>逐个编辑</div>
<ul class="search">
    <li>友情提示：1、会员价格为-1表示会员价格将根据会员等级折扣比例计算 2、统一编辑中文本框不写表示不修改此项</li>
</ul>
<ul class="search">
    <li>
        分类：<asp:DropDownList ID="ClassID" runat="server" /> 
        品牌：<asp:DropDownList ID="BrandID" runat="server" /> 
        名称：<SkyCES:TextBox CssClass="input" ID="Name" runat="server" Width="100px"/>
		添加时间：从<SkyCES:TextBox CssClass="input" ID="StartAddDate" runat="server" RequiredFieldType="日期时间"  onfocus="cdr.show(this);"/> 到 <SkyCES:TextBox CssClass="input" ID="EndAddDate" runat="server" RequiredFieldType="日期时间"  onfocus="cdr.show(this);"/>
		<asp:Button CssClass="button" ID="SearchButton" Text=" 搜 索 " runat="server"  OnClick="SearchButton_Click" />
    </li>
</ul>
<div class="listBlock">
    <ul>
        <li class="listOn" onclick="window.location='ProductSingleEdit.aspx'">逐个编辑</li>
        <li onclick="window.location='ProductUnionEdit.aspx'">统一编辑</li>
    </ul>	
</div>
<table class="listTable" cellpadding="0" cellpadding="0">
    <tr class="listTableHead">
	    <td style="width:5%">ID</td>
	    <td style="width:15%; text-align:left;text-indent:8px;">商品</td>
	    <td style="width:10%">编码</td>
	    <td style="width:5%">重量</td>
	    <td style="width:8%">市场价</td>
	    <%foreach(UserGradeInfo userGrade in userGradeList){ %>
	    <td style="width:8%"><%=userGrade.Name %></td>
	    <%} %>
	    <td style="width:8%">赠送积分</td>	    
	    <td style="width:8%<%if (ShopConfig.ReadConfigInfo().ProductStorageType == (int)ProductStorageType.SelfStorageSystem){ %>;display:none<%} %>"> 总库存数量</td>
	    <td style="width:8%">库存下限</td>
	    <td style="width:8%">库存上限</td> 	
	    <td style="width:5%">管理</td>           
    </tr>
    <%foreach(ProductInfo product in productList){ %>		
    <tr class="listTableMain" onmousemove="changeColor(this,'#FFFDD7')" onmouseout="changeColor(this,'#FFF')">
	    <td><%=product.ID%></td>
	    <td style="text-align:left;text-indent:8px;" title="<%=product.Name%>"><%=StringHelper.Substring(product.Name,7)%></td>
        <td><input id="ProductNumber<%=product.ID%>" value='<%=product.ProductNumber%>' style="width:60px" class="input" title="编码"/></td>           
        <td><input id="Weight<%=product.ID%>" value='<%=product.Weight%>' style="width:40px" class="input" title="重量"/></td>
        <td><input id="MarketPrice<%=product.ID%>" value='<%=product.MarketPrice%>' style="width:40px" class="input" title="市场价"/></td>
        <%foreach(UserGradeInfo userGrade in userGradeList){ %>
        <td><input id="MemberPrice<%=userGrade.ID %>_<%=product.ID%>" value='<%= ReadMemberPrice(product.ID,userGrade.ID,memberPriceList)%>' style="width:40px" class="input" title="<%=userGrade.Name %>"/></td>
        <%} %>
        <td><input id="SendPoint<%=product.ID%>" value='<%=product.SendPoint%>' style="width:40px" class="input" title="赠送积分"/></td>
        <td <%if (ShopConfig.ReadConfigInfo().ProductStorageType == (int)ProductStorageType.SelfStorageSystem){ %>style="display:none"<%} %>><input id="TotalStorageCount<%=product.ID%>" value='<%=product.TotalStorageCount%>' style="width:40px" class="input" title="总库存数量"/></td>
        <td><input id="LowerCount<%=product.ID%>" value='<%=product.LowerCount%>' style="width:40px" class="input" title="库存下限"/></td>
        <td><input id="UpperCount<%=product.ID%>" value='<%=product.UpperCount%>' style="width:40px" class="input" title="库存上限"/></td>
        <td><input type="button" class="button" value="保存" onclick="saveSingleEdit(<%=product.ID%>)" /></td>
    </tr>
    <%} %>
</table>
<script language="javascript" type="text/javascript">
var userGradeIDList="<%=userGradeIDList %>";
var userGradeNameList="<%=userGradeNameList %>";
</script>
<div class="listPage"><SkyCES:CommonPager ID="MyPager" runat="server" /></div>
<div class="action"></div>
</asp:Content>
