<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="OrderShippingAjax.aspx.cs" Inherits="SocoShop.Web.Admin.OrderShippingAjax" %>
<%@ Import Namespace="SocoShop.Entity" %>
<select name="ShippingID">
    <%foreach(ShippingInfo shipping in shippingList){%>    
    <option value="<%=shipping.ID%>" <%if(orderShippingID==shipping.ID){ %>selected="selected"<%} %>><%=shipping.Name%></option>
    <%} %>
</select>