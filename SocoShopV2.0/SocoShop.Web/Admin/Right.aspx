<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True"
    CodeBehind="Right.aspx.cs" Inherits="SocoShop.Web.Admin.Right" %>

<%@ Import Namespace="SocoShop.Common" %>
<%@ Import Namespace="SocoShop.Business" %>
<%@ Import Namespace="SkyCES.EntLib" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="SocoShop.Entity" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Net" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />系统桌面</div>
    <div class="rightBlock">
        <div class="rightLeft">
            <div class="boldTitle">
                待处理事务</div>
            <ul class="add">
                <li><span class="left">产品</span> 未处理商品评论 <a href="ProductComment.aspx?Action=search&Status=1"
                    class="red">
                    <%=dt.Rows[0][0]%>
                </a>条；未处理的缺货登记 <a href="BookingProduct.aspx?Action=search&IsHandler=0" class="red">
                    <%=dt.Rows[0][1]%>
                </a>条</li>
                <li><span class="left">用户</span> 未激活用户<a href="User.aspx?Action=search&Status=1"
                    class="red">
                    <%=dt.Rows[0][2]%>
                </a>位； 冻结用户 <a href="User.aspx?Action=search&Status=3" class="red">
                    <%=dt.Rows[0][3]%>
                </a>位； 未处理的用户留言 <a href="UserMessage.aspx?Action=search&IsHandler=0" class="red">
                    <%=dt.Rows[0][4]%></a> 条； 未处理的提现申请<a href="UserApply.aspx?Action=search&Status=1"
                        class="red">
                        <%=dt.Rows[0][5]%>
                    </a>条</li>
                <li><span class="left">订单</span> 待付款订单 <a href="Order.aspx?OrderStatus=1" class="red">
                    <%=dt.Rows[0][6]%>
                </a>个；待审核订单 <a href="Order.aspx?OrderStatus=2" class="red">
                    <%=dt.Rows[0][7]%>
                </a>个；待发货订单 <a href="Order.aspx?OrderStatus=4" class="red">
                    <%=dt.Rows[0][8]%>
                </a>个；待收货确认订单 <a href="Order.aspx?OrderStatus=5" class="red">
                    <%=dt.Rows[0][9]%>
                </a>个</li>
            </ul>
            <div class="boldTitle">
                快捷操作：
                <input type="button" value="商品管理" onclick="javascript:window.location.href='Product.aspx'"
                    class="button" />&nbsp;&nbsp;<input type="button" value="商品评论" onclick="javascript:window.location.href='ProductComment.aspx'"
                        class="button" />&nbsp;&nbsp;<input type="button" value="用户管理" onclick="javascript:window.location.href='User.aspx'"
                            class="button" />&nbsp;&nbsp;<input type="button" value="用户留言" onclick="javascript:window.location.href='UserMessage.aspx'"
                                class="button" />&nbsp;&nbsp;<input type="button" value="提现申请" onclick="javascript:window.location.href='UserApply.aspx'"
                                    class="button" />
                &nbsp;&nbsp;<input type="button" value="充值查询" onclick="javascript:window.location.href='UserRecharge.aspx'"
                    class="button" />&nbsp;&nbsp;<input type="button" value="订单管理" onclick="javascript:window.location.href='Order.aspx'"
                        class="button" />
            </div>
            <ul style="background-color: #F5FAFE">
                <li>
                    <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"
                        width="600" height="200" id="MSLine">
                        <param name="movie" value="Flash/MSLine.swf?ChartNoDataText=%E6%B2%A1%E6%9C%89%E5%8F%AF%E6%98%BE%E7%A4%BA%E7%9A%84%E6%95%B0%E6%8D%AE&PBarLoadingText=%E6%AD%A3%E5%9C%A8%E8%BD%BD%E5%85%A5%E5%9B%BE%E8%A1%A8%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&XMLLoadingText=%E6%AD%A3%E5%9C%A8%E8%8E%B7%E5%8F%96%E6%95%B0%E6%8D%AE%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&ParsingDataText=%E6%AD%A3%E5%9C%A8%E8%AF%BB%E5%8F%96%E6%95%B0%E6%8D%AE%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&RenderingChartText=%E6%AD%A3%E5%9C%A8%E6%B8%B2%E6%9F%93%E5%9B%BE%E8%A1%A8%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&LoadDataErrorText=%E8%BD%BD%E5%85%A5%E6%95%B0%E6%8D%AE%E6%97%B6%E5%8F%91%E7%94%9F%E9%94%99%E8%AF%AF&InvalidXMLText=无效的XML数据" />
                        <param name="FlashVars" value="&dataURL=OrderCountData.aspx<%=queryString %>">
                        <param name="quality" value="high" />
                        <param name="wmode" value="transparent">
                        <embed src="Flash/MSLine.swf?ChartNoDataText=%E6%B2%A1%E6%9C%89%E5%8F%AF%E6%98%BE%E7%A4%BA%E7%9A%84%E6%95%B0%E6%8D%AE&PBarLoadingText=%E6%AD%A3%E5%9C%A8%E8%BD%BD%E5%85%A5%E5%9B%BE%E8%A1%A8%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&XMLLoadingText=%E6%AD%A3%E5%9C%A8%E8%8E%B7%E5%8F%96%E6%95%B0%E6%8D%AE%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&ParsingDataText=%E6%AD%A3%E5%9C%A8%E8%AF%BB%E5%8F%96%E6%95%B0%E6%8D%AE%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&RenderingChartText=%E6%AD%A3%E5%9C%A8%E6%B8%B2%E6%9F%93%E5%9B%BE%E8%A1%A8%EF%BC%8C%E8%AF%B7%E7%A8%8D%E5%80%99&LoadDataErrorText=%E8%BD%BD%E5%85%A5%E6%95%B0%E6%8D%AE%E6%97%B6%E5%8F%91%E7%94%9F%E9%94%99%E8%AF%AF&InvalidXMLText=无效的XML数据"
                            flashvars="&dataURL=OrderCountData.aspx<%=queryString %>" quality="high" wmode="transparent"
                            width="600" height="200" name="MSLine" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                    </object>
                </li>
                <li class="boldTitle" style="text-align: center">本月订单统计</li>
            </ul>
        </div>
        <div class="rightRight">
            <div class="boldTitle">
                系统版本</div>
            <div class="version">
                感谢使用<a href="http://www.skyces.com" target="_blank">天易CES</a>旗下<%=Global.ProductName%>！<br />
                当前版本：
                <%=Global.Version%><br />
                <script language="javascript" src="http://www.skyces.com/Upload/AdUpload/000009.js"
                    type="text/javascript"></script>
            </div>
            <div>
                <script language="javascript" src="http://www.skyces.com/Upload/AdUpload/000001.js"
                    type="text/javascript"></script>
            </div>
            <div class="boldTitle">
                在线帮助</div>
            <ul class="help">
                <script language="javascript" src="http://www.skyces.com/Upload/AdUpload/000010.js"
                    type="text/javascript"></script>
            </ul>
        </div>
        <%
        
                    //int result = 1;
                    //string organizationName = string.Empty;
                    //    string domain = ShopConfig.ReadConfigInfo().Domain.ToLower();
                    //    string host = Request.Url.Host.ToLower();
                    //    if (host.IndexOf(domain) > -1)
                    //    {
                    //        string fileName = StringHelper.Password(StringHelper.Password(domain, PasswordType.MD516), PasswordType.MD516);
                    //        if (File.Exists(ServerHelper.MapPath("/" + fileName + ".key")))
                    //        {
                    //                Assembly assembly = Assembly.LoadFile(ServerHelper.MapPath("/" + fileName + ".key"));
                    //                object oj = assembly.CreateInstance("SocoShop.AuthorizationCode.Key");
                    //                Type type = oj.GetType();
                    //                string authorizationCode = type.GetField("AuthorizationCode").GetValue(oj).ToString();
                    //                organizationName = type.GetField("OrganizationName").GetValue(oj).ToString();
                    //                int status = Convert.ToInt32(type.GetField("Status").GetValue(oj));
                    //                string apiURL = type.GetField("APIURL").GetValue(oj).ToString();
                    //                string tempDomain = type.GetField("Domain").GetValue(oj).ToString();
                    //                if (tempDomain == domain && authorizationCode != string.Empty && apiURL != string.Empty)
                    //                {
                    //                    //远程验证
                    //                    string requestResult = string.Empty;
                    //                    try
                    //                    {
                    //                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + "?Action=CheckSocoShop&Domain=" + domain + "&AuthorizationCode=" + authorizationCode);
                    //                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    //                        {
                    //                            using (StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312")))
                    //                            {
                    //                                requestResult = sr.ReadToEnd();
                    //                            }
                    //                        }
                    //                    }
                    //                    catch
                    //                    {
                    //                        requestResult = "ok";
                    //                    }
                    //                    if (requestResult == "ok")
                    //                    {
                    //                        result = status;
                    //                    }
                    //                }
                    //        }
                    //    }
                    //    ResponseHelper.Write(result.ToString());
        %>
    </div>
    <div class="bottom">
        <div class="foot">
            <span id="SkyVersion">
                <%=Global.ProductName%>
                <%=Global.Version%></span> <span><a href="http://www.skyces.com" target="_blank">
                    <%=Global.CopyRight%></a></span>
        </div>
    </div>
</asp:Content>
