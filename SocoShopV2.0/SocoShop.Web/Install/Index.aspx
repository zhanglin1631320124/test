<%@ Page Language="C#" AutoEventWireup="true"%>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="SkyCES.EntLib" %>
<%@ Import Namespace="SocoShop.Common" %>
<%@ Register Namespace="SkyCES.EntLib" Assembly="SkyCES.EntLib" TagPrefix="SkyCES"%>
<html>
<head>
    <title>SocoShop安装向导</title>
    <style type="text/css">
        body,form,input,button,ul,ol,dl,dt,dd,div,textarea { font-size:12px; margin:0px; padding:0px;}
        ul,ol,li {list-style-type:none;}
        .mainBody {	margin: 100px auto;  overflow: hidden; text-align:center;}
        .logo{ text-align:left;width:500px;}
        .add{margin-bottom:10px; margin-top:20px;width:500px;}
        .add ul {background: #fff;border: 1px solid #eee;padding: 0;list-style: none;overflow: hidden;zoom: 1;}
        .add ul li {float: left;padding-top: 5px;padding-left: 0;padding-right: 0;padding-bottom: 1005px;margin-bottom: -1000px;zoom: 1;overflow: hidden;}
        .add ul .left {width: 130px;text-align: right;padding-right: 10px;padding-top: 8px;margin-top: 1px;margin-right: 1px;font-weight: 800;border-right: 1px solid #fff;border-left: 1px solid #fff;}
        .add ul .right {width: auto;border-left: 1px solid #eee;padding-left: 10px;margin-left: -1px;overflow:hidden;}
        .note{ margin-top:20px;width:500px; text-align:left}
        .note span{ color:Red; font-weight:bold; font-size:14px;}
        .action{margin-top:5px; border-top:#A5D5F5 2px solid;  padding:3px;  text-align:center; width:500px;}
        .error{ margin-top:50px; text-align:center;  font-size:14px; color:red; font-weight:bold}
        .error img{  vertical-align:middle}
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <script language="C#" runat="server">
        protected string error = string.Empty;
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //检查.net frameworks 版本
                if (System.Environment.Version.Major < 2)
                {
                    error = "运行SocoShop需要.NET Frameworks 2.0 版本，当前服务器.NET Frameworks版本号为：" + Environment.Version.ToString() + "！";
                }
                //检查Bin文件夹
                string pah = Request.PhysicalApplicationPath;
                if (!File.Exists(pah + "bin\\SkyCES.EntLib.dll"))
                {
                    error = "DLL文件放置错误,应该将bin文件夹里的DLL文件移动到目录" + pah + "bin 里面！";
                }
            }
        }
        /// <summary>
        /// 安装按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string connectionString = "server=" + ServerName.Text + ";uid=" + UserName.Text.Trim() + ";pwd=" + Password.Text.Trim() + ";database=" + DataBaseName.Text.Trim();
            string tablePrefix=TablePrefix.Text;
            using( SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        //安装数据表
                        cmd.CommandText = File.ReadAllText(Server.MapPath("Table.sql"), Encoding.Default).Replace("SocoShop_", tablePrefix);
                        cmd.ExecuteNonQuery();
                        //安装存储过程
                        cmd.CommandText = File.ReadAllText(Server.MapPath("Procedure.sql"), Encoding.Default).Replace("SocoShop_", tablePrefix);
                        cmd.ExecuteNonQuery();
                        //安装视图
                        cmd.CommandText = File.ReadAllText(Server.MapPath("View.sql"), Encoding.Default).Replace("SocoShop_", tablePrefix);
                        cmd.ExecuteNonQuery();
                        //安装基础数据
                        string qq = File.ReadAllText(Server.MapPath("Data.sql"), Encoding.Default).Replace("SocoShop_", tablePrefix);
                        cmd.CommandText = File.ReadAllText(Server.MapPath("Data.sql"), Encoding.Default).Replace("SocoShop_", tablePrefix);
                        cmd.ExecuteNonQuery();  
                        //添加后台管理员
                        string adminName = AdminName.Text;
                        string adminPassword = StringHelper.Password(AdminPassword.Text, (PasswordType)ShopConfig.ReadConfigInfo().PasswordType);
                        cmd.CommandText = "INSERT INTO " + tablePrefix + "Admin([Name],[Email],[GroupID],[Password],[LastLoginIP],[LastLoginDate],[LoginTimes],[NoteBook],[IsCreate])VALUES('" + adminName + "','',1,'" + adminPassword + "','" + ClientHelper.IP + "','" + RequestHelper.DateNow + "',0,'',1)";
                        cmd.ExecuteNonQuery();             
                    }
                    //更改ShopConfig.config的值
                    using (XmlHelper xh = new XmlHelper(Server.MapPath("/Config/ShopConfig.config")))
                    {
                        xh.UpdateAttribute("Config/SecureKey" , "Value",DateTime.Now.Ticks.ToString());
                        xh.UpdateAttribute("Config/PowerKey", "Value", tablePrefix);
                        xh.UpdateAttribute("Config/AdminCookies", "Value", tablePrefix+"Admin");
                        xh.UpdateAttribute("Config/UserCookies", "Value",tablePrefix+"User");                       
                        xh.Save();
                    }
                    //更改AdminPower.config
                    string fileName=ServerHelper.MapPath("/Config/AdminPower.config");
                    string content=File.ReadAllText(fileName).Replace("SocoShop_",tablePrefix);
                    File.WriteAllText(fileName, content);
                    //更改Web.Config的值
                    XmlDocument xd=new XmlDocument();
	                xd.Load(Server.MapPath("/Web.config"));
                    XmlNodeList xnList = xd.SelectNodes("configuration/appSettings/add");
                    foreach (XmlNode xn in xnList)
                    {
                        if (xn.Attributes["key"].Value == "ConnectionString")
                        {
                            xn.Attributes["value"].Value = connectionString;
                        }
                        else if (xn.Attributes["key"].Value == "TablePrefix")
                        {
                            xn.Attributes["value"].Value = tablePrefix;
                        }
                    }
                    xd.Save(Server.MapPath("/Web.config"));
                    ResponseHelper.Write("<script>alert('恭喜你，安装成功！');window.location='/Default.aspx'<" + "/script>");
                    ResponseHelper.End();
                }
                catch(Exception ex)
                {
                    ResponseHelper.Write(ex.Message.ToString());
                    ResponseHelper.End();
                }
            }
        }
    </script>
    <div class="mainBody">
        <div class="logo"><img src="logo.jpg" /></div>
        <% if (error==string.Empty){%>
        <div class="add">
            <ul>
                <li class="left">服务器名称/IP：</li>
                <li class="right"><SkyCES:TextBox ID="ServerName" Text="(local)" runat="server" CanBeNull="必填" Width="200px"/></li>
            </ul>
            <ul>
                <li class="left">登录用户名：</li>
                <li class="right"><SkyCES:TextBox ID="UserName" Text="sa" runat="server" CanBeNull="必填"  Width="200px"/></li>
            </ul>
            <ul>
                <li class="left">登录密码：</li>
                <li class="right"><SkyCES:TextBox ID="Password" TextMode="password" runat="server" CanBeNull="必填"  Width="200px"/></li>
            </ul>
            <ul>
                <li class="left">数据库名称：</li>
                <li class="right"><SkyCES:TextBox ID="DataBaseName" runat="server"  CanBeNull="必填"  Width="200px"/></li>
            </ul>
            <ul>
                <li class="left">数据表前缀：</li>
                <li class="right"><SkyCES:TextBox ID="TablePrefix" Text="SocoShop_" runat="server"  CanBeNull="必填"  Width="200px"/></li>
            </ul>
            <ul>
                <li class="left">后台管理登录名：</li>
                <li class="right"><SkyCES:TextBox ID="AdminName" runat="server" Text="admin"  CanBeNull="必填"  Width="200px"/></li>
            </ul>
            <ul>
                <li class="left">后台管理密码：</li>
                <li class="right"><SkyCES:TextBox ID="AdminPassword" runat="server"  CanBeNull="必填"  Width="200px" TextMode="Password" RequiredFieldType="自定义验证表达式" ValidationExpression="^[\W\w]{6,16}$" CustomErr="密码长度大于6位少于16位" /></li>
            </ul>
            <ul>
                <li class="left">后台管理确认密码：</li>
                <li class="right"><SkyCES:TextBox ID="AdminPassword2" runat="server"  CanBeNull="必填"  Width="200px" TextMode="Password" RequiredFieldType="自定义验证表达式" ValidationExpression="^[\W\w]{6,16}$" CustomErr="密码长度大于6位少于16位"/><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码不一致" ControlToCompare="AdminPassword" ControlToValidate="AdminPassword2" Display="Dynamic"></asp:CompareValidator></li>
            </ul>
        </div>
        <div class="action"><asp:Button ID="SubmitButton" Text="一键安装" runat="server" OnClick="SubmitButton_Click" /></div>
        <div  class="note"><span>注意：</span>安装完成之后，请手动把install目录删除掉！！</div>
        <% }else{ %>
        <div class="error"><img src="error.jpg" /> <%=error %></div>
        <%} %>
    </div>
    </form>
</body>
</html>