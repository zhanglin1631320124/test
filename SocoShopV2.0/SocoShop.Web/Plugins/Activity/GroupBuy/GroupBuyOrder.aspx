<%@ Page Language="C#" AutoEventWireup="true" Inherits="SocoShop.Web.GroupBuyOrder"
    CodeBehind="GroupBuyOrder.aspx.cs" %>

<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="SocoShop.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:PlaceHolder ID="PHead" runat="server" />
    <script language="javascript" type="text/javascript" src="/Admin/js/UnlimitClass.js"></script>
    <style type="text/css">
        .height15
        {
            height: 15px;
            clear: both;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="PTop" runat="server" />
    <div class="main">
        <table cellpadding="0" cellspacing="0" border="0" width="960px">
            <tr class="cartHead">
                <td style="width: 100px">
                    商品
                </td>
                <td>
                    名称
                </td>
                <td style="width: 100px">
                    团购价
                </td>
                <td style="width: 100px">
                    数量
                </td>
                <td style="width: 100px">
                    小计
                </td>
            </tr>
            <tr class="cartMain" valign="middle">
                <td style="width: 100px" class="photo">
                    <a href="/ProductDetail-I<%=product.ID%>.aspx" target="_blank">
                        <img src="<%=product.Photo%>" onload="photoLoad(this,60,60)" /></a>
                </td>
                <td style="text-align: left; text-indent: 8px;">
                    <a href="/ProductDetail-I<%=product.ID%>.aspx" target="_blank">
                        <%=product.Name%></a>
                </td>
                <td style="width: 100px">
                    <%=groupBuy.Price%>
                </td>
                <td style="width: 100px">
                    <input type="text" class="text" value="1" id="BuyCount" name="BuyCount" onblur="changeBuyCount(this.value,<%=groupBuy.EachNumber%>,<%=groupBuy.MaxCount - buyCount%>,<%=groupBuy.Price%>)" />
                </td>
                <td style="width: 100px" id="Statics">
                    <%=groupBuy.Price%>
                </td>
            </tr>
        </table>
        <div class="height15">
        </div>
        <div class="add">
            <ul>
                <li class="left">收货人姓名：</li>
                <li class="right">
                    <SkyCES:TextBox ID="Consignee" CssClass="input" Style="width: 200px" runat="server"
                        CanBeNull="必填" /></li>
            </ul>
            <ul>
                <li class="left">固定电话：</li>
                <li class="right">
                    <SkyCES:TextBox ID="Tel" CssClass="input" Style="width: 200px" runat="server" /></li>
            </ul>
            <ul>
                <li class="left">手机：</li>
                <li class="right">
                    <SkyCES:TextBox ID="Mobile" CssClass="input" Style="width: 200px" runat="server" /></li>
            </ul>
            <ul>
                <li class="left">地址：</li>
                <li class="right">
                    <SkyCES:SingleUnlimitControl ID="RegionID" runat="server" />
                    邮编：<SkyCES:TextBox ID="ZipCode" CssClass="input" Style="width: 80px" runat="server"
                        CanBeNull="必填" />
                    <br />
                    <SkyCES:TextBox ID="Address" CssClass="input" Style="width: 360px" runat="server"
                        CanBeNull="必填" /></li>
            </ul>
            <ul>
                <li class="left">&nbsp;</li>
                <li class="right">
                    <asp:Button ID="bigbutton" runat="server" OnClick="SubmitButton_Click" CssClass="bigbutton"
                        Text="确认提交" /></li>
            </ul>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function changeBuyCount(buyCount, eachNumber, leftCount, price) {
            if (Validate.isInt(buyCount)) {
                if (eachNumber != -1) {
                    if (buyCount > eachNumber) {
                        alertMessage("您购买的数量不能超出单人购买的数量", 500);
                        o("BuyCount").value = "";
                        o("BuyCount").focus();
                        return;
                    }
                }
                if (buyCount > leftCount) {
                    alertMessage("您购买的数量不能超出商品的最大销售数量", 500);
                    o("BuyCount").value = "";
                    o("BuyCount").focus();
                    return;
                }
                o("Statics").innerHTML = buyCount * price;
            }
            else {
                alertMessage("您购买的数量必须是整数", 500);
                o("BuyCount").value = "";
                o("BuyCount").focus();
            }
        }
    </script>
    <asp:PlaceHolder ID="PFoot" runat="server" />
    </form>
</body>
</html>
