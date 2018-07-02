<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True" CodeBehind="FavorableActivityAdd.aspx.cs" Inherits="SocoShop.Web.Admin.FavorableActivityAdd" %>
<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES"%>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Business" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<script language="javascript" src="/Admin/js/calendar.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="/Admin/js/UnlimitClass.js"></script>
<div class="position"><img src="/Admin/Style/Images/PositionIcon.png"  alt=""/>优惠活动<%=GetAddUpdate()%></div>
<div class="add">
	<ul>
		<li class="left">标题：</li>
		<li class="right"><SkyCES:TextBox CssClass="input" Width="400px" ID="Name" runat="server" CanBeNull="必填"/></li>
	</ul>
	<ul>
		<li class="left">图片：</li>
		<li class="right"><SkyCES:TextBox ID="Photo" CssClass="input" runat="server" Width="400px" /></li>
	</ul>
	<ul>
		<li class="left">上传图片：</li>
		<li class="right"><iframe src="UploadAdd.aspx?Control=Photo&TableID=<%=FavorableActivityBLL.TableID %>&FilePath=GiftPackPhoto/Original" width="400" height="30px" frameborder="0" allowTransparency="true" scrolling="no"></iframe></li>
	</ul> 
	<ul>
		<li class="left">介绍：</li>
		<li class="right"><SkyCES:TextBox CssClass="input" Width="400px" ID="Content" runat="server" TextMode="MultiLine"  Height="100px"/></li>
	</ul>
	<ul>
		<li class="left">条件：</li>
		<li class="right">
		    <ul>
		        <li class="left">活动时间：</li>
		        <li class="right"><SkyCES:TextBox ID="StartDate" CssClass="input" runat="server" Width="140px"  RequiredFieldType="日期时间"  CanBeNull="必填" onfocus="cdr.show(this);"/> 到 <SkyCES:TextBox ID="EndDate" CssClass="input" runat="server" Width="140px" RequiredFieldType="日期时间"  CanBeNull="必填"  onfocus="cdr.show(this);" /></li>
	        </ul>	
	        <ul>
		        <li class="left">用户等级：</li>
		        <li class="right"><asp:CheckBoxList ID="UserGrade" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList></li>
	        </ul>
	        <ul>
		        <li class="left">订单产品最低金额：</li>
		        <li class="right"><SkyCES:TextBox ID="OrderProductMoney" CssClass="input" runat="server" Width="60px"  CanBeNull="必填" RequiredFieldType="金额"  Text="0"/> 元</li>
	        </ul>
		</li>
	</ul>
	<ul>
		<li class="left">优惠：</li>
		<li class="right">
	        <ul>
		        <li class="left">运费优惠：</li>
		        <li class="right">
		            <input name="ShippingWay" onclick="changeShippingWay()" value="0" type="radio" /> 无运费优惠 
		            <input name="ShippingWay" onclick="changeShippingWay()" value="1" type="radio" /> 免运费 
		        </li>
	        </ul>
	        <ul id="ShippingRegionDiv">
		        <li class="left">运费优惠区域：</li>
		        <li class="right"><SkyCES:MultiUnlimitControl ID="RegionID" runat="server"/> </li>
	        </ul>
	        <ul>
		        <li class="left">价格优惠：</li>
		        <li class="right">
		            <input name="ReduceWay" onclick="changeReduceWay()" value="0" type="radio" /> 无价格优惠 
		            <input name="ReduceWay" onclick="changeReduceWay()" value="1" type="radio" /> 现金减免 <span id="ReduceMoneyDiv"><SkyCES:TextBox CssClass="input" Width="30px" ID="ReduceMoney" runat="server" CanBeNull="必填" RequiredFieldType="金额"  Text="0"/> 元 </span> 
		            <input name="ReduceWay" onclick="changeReduceWay()" value="2" type="radio" /> 价格折扣 <span id="ReduceDiscountDiv"><SkyCES:TextBox CssClass="input" Width="30px" ID="ReduceDiscount" runat="server" CanBeNull="必填" RequiredFieldType="金额"  Text="0"/> 折 </span></li>
	        </ul>
	        <ul>
		        <li class="left">赠送礼品：</li>
		        <li class="right">
		            <input name="GiftWay" onclick="changeGiftWay()" value="0" type="radio" /> 无礼品赠送
		            <input name="GiftWay" onclick="changeGiftWay()" value="1" type="radio" /> 赠送以下任一种礼品 
		        </li>
	        </ul>
	        <div id="GiftDiv">
	        <ul>
		        <li class="left">选择礼品：</li>
		        <li class="right"><input id="GiftName" class="input" /> <input type="button" value="搜索"  onclick="searchGift()" class="button" />   
		        <div id="SearchGiftList"></div></li>
	        </ul>
	        <ul>
		        <li class="left">优惠礼品：</li>
		        <li class="right">
		        <div id="SelectGiftList">
		        <%foreach (GiftInfo gift in giftList){  %>
		             <span id="Gift<%=gift.ID %>"><%=StringHelper.Substring(gift.Name,10)%><span onclick="deleteGift(<%=gift.ID %>)" style="cursor:pointer"><img src="style/images/delete.gif" /></span><input class="input" name="GiftList" type="hidden" value="<%=gift.ID %>"/></span>
                <%} %>
		        </div></li>
	        </ul>
	        </div>
	    </li>
	</ul>
	
</div>
<div class="action">
    <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
</div>
<script language="javascript" type="text/javascript" src="Js/FavorableActivityAdd.js"></script>
<script language="javascript" type="text/javascript">
function init(){
    var shippingWayObjs=os("name","ShippingWay");
    setRadioValue(shippingWayObjs,"<%=favorableActivity.ShippingWay.ToString()%>");
    var reduceWayObjs=os("name","ReduceWay");
    setRadioValue(reduceWayObjs,"<%=favorableActivity.ReduceWay.ToString()%>");
    var giftObjs=os("name","GiftWay");
    var giftValue="<%=favorableActivity.GiftID.ToString()%>";
    if(giftValue!=""){
        giftValue="1";
    }
    else{
        giftValue="0";
    }    
    setRadioValue(giftObjs,giftValue);
    changeShippingWay();
    changeReduceWay();
    changeGiftWay();
}
init();
</script>
</asp:Content>
