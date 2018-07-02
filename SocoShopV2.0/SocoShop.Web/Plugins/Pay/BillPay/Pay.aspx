<%@ Page Language="C#" AutoEventWireup="true" Inherits="SocoShop.Pay.BillPay.Pay" Codebehind="Pay.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head">
    <title></title>
</head>
<body>
	<form name="frm" method="post" action="https://www.99bill.com/webapp/receiveMerchantInfoAction.do" id="billForm">
		<input id="merchant_id" type="hidden" runat="server">
		<input id="orderid"  type="hidden"  runat="server">
		<input id="amount"  type="hidden"  runat="server">
		<input id="currency"  type="hidden"  runat="server">
		<input id="isSupportDES"  type="hidden"  runat="server">
		<input  id="mac"  type="hidden"  runat="server">
		
		<input id="merchant_url"  type="hidden"  runat="server">
		<input id="pname"  type="hidden"  runat="server">
		<input id="commodity_info"  type="hidden"  runat="server">
		<input id="merchant_param" type="hidden"   runat="server">

		<input id="pemail" type="hidden"   runat="server">
		<input id="pid" type="hidden"   runat="server">
		
	</form>
	<script language="javascript" type="text/javascript">
	    window.onload = function() {
	        var obj = document.getElementById("billForm");
	        obj.submit();
	    }
	</script>
</body>
</html>
