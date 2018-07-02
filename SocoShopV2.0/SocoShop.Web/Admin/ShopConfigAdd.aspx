<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="True"
    CodeBehind="ShopConfigAdd.aspx.cs" Inherits="SocoShop.Web.Admin.ShopConfigAdd" %>

<%@ Register Assembly="SkyCES.EntLib" Namespace="SkyCES.EntLib" TagPrefix="SkyCES" %>
<asp:Content ID="MasterContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="/Admin/Js/Color.js"></script>
    <div class="position">
        <img src="/Admin/Style/Images/PositionIcon.png" alt="" />基础配置</div>
    <div class="listBlock">
        <ul>
            <li class="listOn" id="TitleDefault" onclick="switchBlock('Default')">基本设置</li>
            <li id="TitleProduct" onclick="switchBlock('Product')">产品设置</li>
            <li id="TitleUser" onclick="switchBlock('User')">用户设置</li>
            <li id="TitleEmail" onclick="switchBlock('Email')">Email设置</li>
            <li id="TitleOrder" onclick="switchBlock('Order')">订单设置</li>
            <li id="TitlePhoto" onclick="switchBlock('Photo')">图片水印</li>
        </ul>
    </div>
    <div class="line">
    </div>
    <div class="add" id="ContentDefault">
        <ul>
            <li class="left">标题：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="txtTitle" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">版权：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="Copyright" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">作者：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="Author" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">关键字：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="Keywords" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">描述：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="Description" TextMode="MultiLine"
                    Height="50px" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">验证码类型：</li>
            <li class="right">
                <asp:RadioButtonList ID="CodeType" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="1" Selected="True">纯数字</asp:ListItem>
                    <asp:ListItem Value="2">纯字母</asp:ListItem>
                    <asp:ListItem Value="3">数字字母混合</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">验证码长度：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="CodeLength" runat="server" CanBeNull="必填"
                    RequiredFieldType="数据校验" /></li>
            <li class="left">验证码杂点数：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="CodeDot" runat="server" CanBeNull="必填"
                    RequiredFieldType="数据校验" /></li>
        </ul>
        <ul>
            <li class="left">开始年份：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="StartYear" runat="server" CanBeNull="必填"
                    RequiredFieldType="数据校验" /></li>
            <li class="left">结束年份：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="EndYear" runat="server" CanBeNull="必填"
                    RequiredFieldType="数据校验" /></li>
        </ul>
        <ul>
            <li class="left">服务电话：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="Tel" runat="server" /></li>
            <li class="left">备案号：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="RecordCode" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">统计代码：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="StaticCode" runat="server" TextMode="MultiLine"
                    Height="50px" /></li>
        </ul>
        <ul>
            <li class="left">首页投票ID：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="VoteID" runat="server" CanBeNull="必填"
                    RequiredFieldType="数据校验" /></li>
            <li class="left">容许匿名投票：</li>
            <li class="right">
                <asp:RadioButtonList ID="AllowAnonymousVote" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不容许</asp:ListItem>
                    <asp:ListItem Value="1">容许</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">投票时间限制：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="200px" ID="VoteRestrictTime" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />（秒）（少于等于0表示不限制）</li>
        </ul>
        <ul>
            <li class="left">完成操作关闭弹窗：</li>
            <li class="right">
                <asp:RadioButtonList ID="SaveAutoClosePop" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不关闭</asp:ListItem>
                    <asp:ListItem Value="1">自动关闭</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">弹窗关闭后刷新：</li>
            <li class="right">
                <asp:RadioButtonList ID="PopCloseRefresh" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不刷新</asp:ListItem>
                    <asp:ListItem Value="1">自动刷新</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">上传文件类型：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="UploadFile" runat="server" /></li>
        </ul>
    </div>
    <div class="add" id="ContentProduct" style="display: none">
        <ul>
            <li class="left">产品库存系统：</li>
            <li class="right">
                <asp:RadioButtonList ID="ProductStorageType" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="1" Selected="True">自身的库存系统</asp:ListItem>
                    <asp:ListItem Value="2">导入第三方的库存系统</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">热门关键词：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="HotKeyword" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">容许匿名评论：</li>
            <li class="right">
                <asp:RadioButtonList ID="AllowAnonymousComment" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不容许</asp:ListItem>
                    <asp:ListItem Value="1">容许</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">评论默认状态：</li>
            <li class="right">
                <asp:RadioButtonList ID="CommentDefaultStatus" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="1" Selected="True">未处理</asp:ListItem>
                    <asp:ListItem Value="2">显示</asp:ListItem>
                    <asp:ListItem Value="3">不显示</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">评论时间限制：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="200px" ID="CommentRestrictTime" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />（秒）（少于等于0表示不限制）</li>
        </ul>
        <ul>
            <li class="left">容许匿名支持/反对：</li>
            <li class="right">
                <asp:RadioButtonList ID="AllowAnonymousCommentOperate" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Value="0" Selected="True">不容许</asp:ListItem>
                    <asp:ListItem Value="1">容许</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">支持/反对时间限制：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="200px" ID="CommentOperateRestrictTime" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />（秒）（少于等于0表示不限制）</li>
        </ul>
        <ul>
            <li class="left">容许匿名回复：</li>
            <li class="right">
                <asp:RadioButtonList ID="AllowAnonymousReply" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不容许</asp:ListItem>
                    <asp:ListItem Value="1">容许</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">回复时间限制：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="200px" ID="ReplyRestrictTime" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />（秒）（少于等于0表示不限制）</li>
        </ul>
        <ul>
            <li class="left">容许匿名提交标签：</li>
            <li class="right">
                <asp:RadioButtonList ID="AllowAnonymousAddTags" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不容许</asp:ListItem>
                    <asp:ListItem Value="1">容许</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">提交标签时间限制：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="200px" ID="AddTagsRestrictTime" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />（秒）（少于等于0表示不限制）</li>
        </ul>
        <ul>
            <li class="left">容许匿名购物：</li>
            <li class="right">
                <asp:RadioButtonList ID="AllowAnonymousAddCart" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不容许</asp:ListItem>
                    <asp:ListItem Value="1">容许</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
    </div>
    <div class="add" id="ContentUser" style="display: none">
        <ul>
            <li class="left">禁止注册用户名：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="ForbiddenName" runat="server"
                    TextMode="MultiLine" Height="80px" />
                多个名字之间用"|"隔开</li>
        </ul>
        <ul>
            <li class="left">密码长度：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="PasswordMinLength" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />
                到
                <SkyCES:TextBox CssClass="input" Width="100px" ID="PasswordMaxLength" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" /></li>
        </ul>
        <ul>
            <li class="left">用户名长度：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="UserNameMinLength" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />
                到
                <SkyCES:TextBox CssClass="input" Width="100px" ID="UserNameMaxLength" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" /></li>
        </ul>
        <ul>
            <li class="left">注册审核：</li>
            <li class="right">
                <asp:RadioButtonList ID="RegisterCheck" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">无需审核</asp:ListItem>
                    <asp:ListItem Value="2">邮件验证</asp:ListItem>
                    <asp:ListItem Value="3">人工审核</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">注册协议：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="Agreement" runat="server" TextMode="MultiLine"
                    Height="400px" />
            </li>
        </ul>
        <ul>
            <li class="left">找回密码时间限制：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="100px" ID="FindPasswordTimeRestrict" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" />
                （小时） （-1表示不限制）</li>
        </ul>
    </div>
    <div class="add" id="ContentEmail" style="display: none">
        <ul>
            <li class="left">用户名：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="EmailUserName" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">密码：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="EmailPassword" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">服务器地址：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="EmailServer" runat="server" /></li>
        </ul>
        <ul>
            <li class="left">端口号：</li>
            <li class="right">
                <SkyCES:TextBox CssClass="input" Width="400px" ID="EmailServerPort" runat="server"
                    CanBeNull="必填" RequiredFieldType="数据校验" /></li>
        </ul>
        <ul>
            <li class="left">Email测试：</li>
            <li class="right">收件箱：<input id="ToEmail" type="text" class="input" style="width: 200px" />
                <input class="button" type="button" value="测试" onclick="testSendEmail()" /></li>
        </ul>
    </div>
    <div class="add" id="ContentOrder" style="display: none">
        <ul>
            <li class="left">支付订单：</li>
            <li class="right">
                <asp:RadioButtonList ID="PayOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">取消订单：</li>
            <li class="right">
                <asp:RadioButtonList ID="CancleOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">审核订单：</li>
            <li class="right">
                <asp:RadioButtonList ID="CheckOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">订单发货：</li>
            <li class="right">
                <asp:RadioButtonList ID="SendOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">确认收货：</li>
            <li class="right">
                <asp:RadioButtonList ID="ReceivedOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">订单换货：</li>
            <li class="right">
                <asp:RadioButtonList ID="ChangeOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">订单退货：</li>
            <li class="right">
                <asp:RadioButtonList ID="ReturnOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">撤销订单操作：</li>
            <li class="right">
                <asp:RadioButtonList ID="BackOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul>
            <li class="left">订单退款：</li>
            <li class="right">
                <asp:RadioButtonList ID="RefundOrder" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="0" Selected="True">不发邮件</asp:ListItem>
                    <asp:ListItem Value="1">发邮件</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
    </div>
    <div class="add" id="ContentPhoto" style="display: none">
        <ul>
            <li class="left">水印方式：</li>
            <li class="right">
                <asp:RadioButtonList ID="WaterType" runat="server" RepeatDirection="Horizontal" onclick="selectWaterType()">
                    <asp:ListItem Value="1" Selected="True">无水印</asp:ListItem>
                    <asp:ListItem Value="2">文字水印</asp:ListItem>
                    <asp:ListItem Value="3">图片水印</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <ul id="WaterPossition">
            <li class="left">水印位置：</li>
            <li class="right">
                <asp:RadioButtonList ID="WaterPossition" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">左上</asp:ListItem>
                    <asp:ListItem Value="2">左中</asp:ListItem>
                    <asp:ListItem Value="3">左下</asp:ListItem>
                    <asp:ListItem Value="4">中上</asp:ListItem>
                    <asp:ListItem Value="5">正中</asp:ListItem>
                    <asp:ListItem Value="6">中下</asp:ListItem>
                    <asp:ListItem Value="7">右上</asp:ListItem>
                    <asp:ListItem Value="8">中下</asp:ListItem>
                    <asp:ListItem Value="9">右下</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
        <div id="TextWaterDiv">
            <ul>
                <li class="left">水印文字：</li>
                <li class="right">
                    <SkyCES:TextBox CssClass="input" Width="400px" ID="Text" runat="server" CanBeNull="必填" /></li>
            </ul>
            <ul>
                <li class="left">文字样式：</li>
                <li class="right">
                    <asp:DropDownList ID="TextFont" runat="server">
                        <asp:ListItem Value="Algerian">Algerian</asp:ListItem>
                        <asp:ListItem Value="Arial">Arial</asp:ListItem>
                        <asp:ListItem Value="Arial Black">Arial Black</asp:ListItem>
                        <asp:ListItem Value="Arial Narrow">Arial Narrow</asp:ListItem>
                        <asp:ListItem Value="Arial Unicode MS">Arial Unicode MS</asp:ListItem>
                        <asp:ListItem Value="Baskerville Old Face">Baskerville Old Face</asp:ListItem>
                        <asp:ListItem Value="Batang">Batang</asp:ListItem>
                        <asp:ListItem Value="BatangChe">BatangChe</asp:ListItem>
                        <asp:ListItem Value="Bauhaus 93">Bauhaus 93</asp:ListItem>
                        <asp:ListItem Value="Bell MT">Bell MT</asp:ListItem>
                        <asp:ListItem Value="Berlin Sans FB">Berlin Sans FB</asp:ListItem>
                        <asp:ListItem Value="Berlin Sans FB Demi">Berlin Sans FB Demi</asp:ListItem>
                        <asp:ListItem Value="Bernard MT Condensed">Bernard MT Condensed</asp:ListItem>
                        <asp:ListItem Value="Bodoni MT Poster Compressed">Bodoni MT Poster Compressed</asp:ListItem>
                        <asp:ListItem Value="Book Antiqua">Book Antiqua</asp:ListItem>
                        <asp:ListItem Value="Bookman Old Style">Bookman Old Style</asp:ListItem>
                        <asp:ListItem Value="Bookshelf Symbol 7">Bookshelf Symbol 7</asp:ListItem>
                        <asp:ListItem Value="Britannic Bold">Britannic Bold</asp:ListItem>
                        <asp:ListItem Value="Broadway">Broadway</asp:ListItem>
                        <asp:ListItem Value="Brush Script MT">Brush Script MT</asp:ListItem>
                        <asp:ListItem Value="Calibri">Calibri</asp:ListItem>
                        <asp:ListItem Value="Californian FB">Californian FB</asp:ListItem>
                        <asp:ListItem Value="Cambria">Cambria</asp:ListItem>
                        <asp:ListItem Value="Cambria Math">Cambria Math</asp:ListItem>
                        <asp:ListItem Value="Candara">Candara</asp:ListItem>
                        <asp:ListItem Value="Centaur">Centaur</asp:ListItem>
                        <asp:ListItem Value="Century">Century</asp:ListItem>
                        <asp:ListItem Value="Century Gothic">Century Gothic</asp:ListItem>
                        <asp:ListItem Value="Chiller">Chiller</asp:ListItem>
                        <asp:ListItem Value="Colonna MT">Colonna MT</asp:ListItem>
                        <asp:ListItem Value="Comic Sans MS">Comic Sans MS</asp:ListItem>
                        <asp:ListItem Value="Consolas">Consolas</asp:ListItem>
                        <asp:ListItem Value="Constantia">Constantia</asp:ListItem>
                        <asp:ListItem Value="Cooper Black">Cooper Black</asp:ListItem>
                        <asp:ListItem Value="Corbel">Corbel</asp:ListItem>
                        <asp:ListItem Value="Courier New">Courier New</asp:ListItem>
                        <asp:ListItem Value="Dotum">Dotum</asp:ListItem>
                        <asp:ListItem Value="DotumChe">DotumChe</asp:ListItem>
                        <asp:ListItem Value="Estrangelo Edessa">Estrangelo Edessa</asp:ListItem>
                        <asp:ListItem Value="Footlight MT Light">Footlight MT Light</asp:ListItem>
                        <asp:ListItem Value="Franklin Gothic Medium">Franklin Gothic Medium</asp:ListItem>
                        <asp:ListItem Value="Freestyle Script">Freestyle Script</asp:ListItem>
                        <asp:ListItem Value="Garamond">Garamond</asp:ListItem>
                        <asp:ListItem Value="Gautami">Gautami</asp:ListItem>
                        <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                        <asp:ListItem Value="Gulim">Gulim</asp:ListItem>
                        <asp:ListItem Value="GulimChe">GulimChe</asp:ListItem>
                        <asp:ListItem Value="Gungsuh">Gungsuh</asp:ListItem>
                        <asp:ListItem Value="GungsuhChe">GungsuhChe</asp:ListItem>
                        <asp:ListItem Value="Harlow Solid Italic">Harlow Solid Italic</asp:ListItem>
                        <asp:ListItem Value="Harrington">Harrington</asp:ListItem>
                        <asp:ListItem Value="High Tower Text">High Tower Text</asp:ListItem>
                        <asp:ListItem Value="Impact">Impact</asp:ListItem>
                        <asp:ListItem Value="Informal Roman">Informal Roman</asp:ListItem>
                        <asp:ListItem Value="Jokerman">Jokerman</asp:ListItem>
                        <asp:ListItem Value="Juice ITC">Juice ITC</asp:ListItem>
                        <asp:ListItem Value="Kristen ITC">Kristen ITC</asp:ListItem>
                        <asp:ListItem Value="Kunstler Script">Kunstler Script</asp:ListItem>
                        <asp:ListItem Value="Latha">Latha</asp:ListItem>
                        <asp:ListItem Value="Lucida Bright">Lucida Bright</asp:ListItem>
                        <asp:ListItem Value="Lucida Calligraphy">Lucida Calligraphy</asp:ListItem>
                        <asp:ListItem Value="Lucida Console">Lucida Console</asp:ListItem>
                        <asp:ListItem Value="Lucida Fax">Lucida Fax</asp:ListItem>
                        <asp:ListItem Value="Lucida Handwriting">Lucida Handwriting</asp:ListItem>
                        <asp:ListItem Value="Lucida Sans Unicode">Lucida Sans Unicode</asp:ListItem>
                        <asp:ListItem Value="Magneto">Magneto</asp:ListItem>
                        <asp:ListItem Value="Mangal">Mangal</asp:ListItem>
                        <asp:ListItem Value="Marlett">Marlett</asp:ListItem>
                        <asp:ListItem Value="Matura MT Script Capitals">Matura MT Script Capitals</asp:ListItem>
                        <asp:ListItem Value="Microsoft Sans Serif">Microsoft Sans Serif</asp:ListItem>
                        <asp:ListItem Value="MingLiU">MingLiU</asp:ListItem>
                        <asp:ListItem Value="Mistral">Mistral</asp:ListItem>
                        <asp:ListItem Value="Modern No. 20">Modern No. 20</asp:ListItem>
                        <asp:ListItem Value="Monotype Corsiva">Monotype Corsiva</asp:ListItem>
                        <asp:ListItem Value="MS Gothic">MS Gothic</asp:ListItem>
                        <asp:ListItem Value="MS Mincho">MS Mincho</asp:ListItem>
                        <asp:ListItem Value="MS PGothic">MS PGothic</asp:ListItem>
                        <asp:ListItem Value="MS PMincho">MS PMincho</asp:ListItem>
                        <asp:ListItem Value="MS Reference Sans Serif">MS Reference Sans Serif</asp:ListItem>
                        <asp:ListItem Value="MS Reference Specialty">MS Reference Specialty</asp:ListItem>
                        <asp:ListItem Value="MS UI Gothic">MS UI Gothic</asp:ListItem>
                        <asp:ListItem Value="MT Extra">MT Extra</asp:ListItem>
                        <asp:ListItem Value="MV Boli">MV Boli</asp:ListItem>
                        <asp:ListItem Value="Niagara Engraved">Niagara Engraved</asp:ListItem>
                        <asp:ListItem Value="Niagara Solid">Niagara Solid</asp:ListItem>
                        <asp:ListItem Value="Nina">Nina</asp:ListItem>
                        <asp:ListItem Value="Old English Text MT">Old English Text MT</asp:ListItem>
                        <asp:ListItem Value="Onyx">Onyx</asp:ListItem>
                        <asp:ListItem Value="Palatino Linotype">Palatino Linotype</asp:ListItem>
                        <asp:ListItem Value="Parchment">Parchment</asp:ListItem>
                        <asp:ListItem Value="Playbill">Playbill</asp:ListItem>
                        <asp:ListItem Value="PMingLiU">PMingLiU</asp:ListItem>
                        <asp:ListItem Value="Poor Richard">Poor Richard</asp:ListItem>
                        <asp:ListItem Value="Raavi">Raavi</asp:ListItem>
                        <asp:ListItem Value="Ravie">Ravie</asp:ListItem>
                        <asp:ListItem Value="Segoe Condensed">Segoe Condensed</asp:ListItem>
                        <asp:ListItem Value="Segoe UI">Segoe UI</asp:ListItem>
                        <asp:ListItem Value="Showcard Gothic">Showcard Gothic</asp:ListItem>
                        <asp:ListItem Value="Shruti">Shruti</asp:ListItem>
                        <asp:ListItem Value="Snap ITC">Snap ITC</asp:ListItem>
                        <asp:ListItem Value="Stencil">Stencil</asp:ListItem>
                        <asp:ListItem Value="Sylfaen">Sylfaen</asp:ListItem>
                        <asp:ListItem Value="Symbol">Symbol</asp:ListItem>
                        <asp:ListItem Value="Tahoma">Tahoma</asp:ListItem>
                        <asp:ListItem Value="Tempus Sans ITC">Tempus Sans ITC</asp:ListItem>
                        <asp:ListItem Value="Times New Roman">Times New Roman</asp:ListItem>
                        <asp:ListItem Value="Trebuchet MS">Trebuchet MS</asp:ListItem>
                        <asp:ListItem Value="Tunga">Tunga</asp:ListItem>
                        <asp:ListItem Value="Verdana">Verdana</asp:ListItem>
                        <asp:ListItem Value="Viner Hand ITC">Viner Hand ITC</asp:ListItem>
                        <asp:ListItem Value="Vivaldi">Vivaldi</asp:ListItem>
                        <asp:ListItem Value="Vladimir Script">Vladimir Script</asp:ListItem>
                        <asp:ListItem Value="Webdings">Webdings</asp:ListItem>
                        <asp:ListItem Value="Wide Latin">Wide Latin</asp:ListItem>
                        <asp:ListItem Value="Wingdings">Wingdings</asp:ListItem>
                        <asp:ListItem Value="Wingdings 2">Wingdings 2</asp:ListItem>
                        <asp:ListItem Value="Wingdings 3">Wingdings 3</asp:ListItem>
                        <asp:ListItem Value="仿宋_GB2312">仿宋_GB2312</asp:ListItem>
                        <asp:ListItem Value="华文中宋">华文中宋</asp:ListItem>
                        <asp:ListItem Value="华文仿宋">华文仿宋</asp:ListItem>
                        <asp:ListItem Value="华文宋体">华文宋体</asp:ListItem>
                        <asp:ListItem Value="华文彩云">华文彩云</asp:ListItem>
                        <asp:ListItem Value="华文新魏">华文新魏</asp:ListItem>
                        <asp:ListItem Value="华文楷体">华文楷体</asp:ListItem>
                        <asp:ListItem Value="华文琥珀">华文琥珀</asp:ListItem>
                        <asp:ListItem Value="华文细黑">华文细黑</asp:ListItem>
                        <asp:ListItem Value="华文行楷">华文行楷</asp:ListItem>
                        <asp:ListItem Value="华文隶书">华文隶书</asp:ListItem>
                        <asp:ListItem Value="宋体">宋体</asp:ListItem>
                        <asp:ListItem Value="幼圆">幼圆</asp:ListItem>
                        <asp:ListItem Value="微软雅黑">微软雅黑</asp:ListItem>
                        <asp:ListItem Value="新宋体">新宋体</asp:ListItem>
                        <asp:ListItem Value="方正姚体">方正姚体</asp:ListItem>
                        <asp:ListItem Value="方正舒体">方正舒体</asp:ListItem>
                        <asp:ListItem Value="楷体_GB2312">楷体_GB2312</asp:ListItem>
                        <asp:ListItem Value="隶书">隶书</asp:ListItem>
                        <asp:ListItem Value="黑体">黑体</asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
            <ul>
                <li class="left">文字大小：</li>
                <li class="right">
                    <SkyCES:TextBox CssClass="input" Width="100px" ID="TextSize" runat="server" CanBeNull="必填"
                        RequiredFieldType="数据校验" />
                    px</li>
                <li class="left">文字颜色：</li>
                <li class="right">
                    <SkyCES:TextBox CssClass="input" Width="100px" ID="TextColor" runat="server" CanBeNull="必填" /><input
                        type="button" value=" 拾色器 " class="button" onclick="colorOpen(this,'','<%=IDPrefix%>TextColor','<%=IDPrefix%>TextColor')" /></li>
            </ul>
        </div>
        <div id="PhotoWaterDiv">
            <ul>
                <li class="left">水印图片：</li>
                <li class="right">
                    <SkyCES:TextBox CssClass="input" Width="400px" ID="WaterPhoto" runat="server" CanBeNull="必填" /></li>
            </ul>
            <ul>
                <li class="left">上传图片：</li>
                <li class="right">
                    <iframe src="UploadAdd.aspx?Control=WaterPhoto&FilePath=WaterPhoto" width="400" height="30px"
                        frameborder="0" allowtransparency="true" scrolling="no"></iframe>
                </li>
            </ul>
        </div>
    </div>
    <div class="action">
        <asp:Button CssClass="button" ID="SubmitButton" Text=" 确 定 " runat="server" OnClick="SubmitButton_Click" />
    </div>
    <script language="javascript" type="text/javascript">
        //发送email
        function testSendEmail() {
            var emailUserName = o(globalIDPrefix + "EmailUserName").value;
            var emailPassword = o(globalIDPrefix + "EmailPassword").value;
            var emailServer = o(globalIDPrefix + "EmailServer").value;
            var emailServerPort = o(globalIDPrefix + "EmailServerPort").value;
            var toEmail = o("ToEmail").value;
            var url = "Ajax.aspx?Action=TestSendEmail&EmailUserName=" + emailUserName + "&EmailPassword=" + emailPassword + "&EmailServer=" + emailServer + "&EmailServerPort=" + emailServerPort + "&ToEmail=" + toEmail;
            Ajax.requestURL(url, dealTestSendEmail);
        }
        function dealTestSendEmail(data) {
            if (data == "ok") {
                alertMessage("发送成功");
            }
            else {
                alertMessage("发送失败");
            }
        }
        //选择水印方式
        function selectWaterType() {
            var objs = os("name", globalNamePrefix + "WaterType");
            var waterType = getRadioValue(objs);
            switch (waterType) {
                case "1":
                    o("WaterPossition").style.display = "none";
                    o("TextWaterDiv").style.display = "none";
                    o("PhotoWaterDiv").style.display = "none";
                    break;
                case "2":
                    o("WaterPossition").style.display = "";
                    o("TextWaterDiv").style.display = "";
                    o("PhotoWaterDiv").style.display = "none";
                    break;
                case "3":
                    o("WaterPossition").style.display = "";
                    o("TextWaterDiv").style.display = "none";
                    o("PhotoWaterDiv").style.display = "";
                    break;
                default:
                    break;
            }
        }
        selectWaterType();
    </script>
</asp:Content>
