namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class AdminGroupAdd : AdminBasePage
    {
        protected List<PowerInfo> channelPowerList = new List<PowerInfo>();
        protected string power = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadAdminGroup", PowerCheckType.Single);
                    AdminGroupInfo info = AdminGroupBLL.ReadAdminGroupCache(queryString);
                    this.Name.Text = info.Name;
                    this.Note.Text = info.Note;
                    this.power = info.Power;
                }
            }
            XmlNode node = new XmlHelper(ServerHelper.MapPath("~/Config/AdminPower.Config")).ReadNode("Config");
            foreach (XmlNode node2 in node.ChildNodes)
            {
                PowerInfo item = new PowerInfo();
                item.Text = node2.Attributes["Text"].Value;
                item.Key = node2.Attributes["Key"].Value;
                item.XML = node2.InnerXml;
                this.channelPowerList.Add(item);
            }
        }

        protected List<PowerInfo> ReadPowerBlock(string xml)
        {
            string str = "<root>" + xml + "</root>";
            XmlDocument document = new XmlDocument();
            document.LoadXml(str);
            List<PowerInfo> list = new List<PowerInfo>();
            foreach (XmlNode node in document.SelectNodes("root/Block"))
            {
                PowerInfo item = new PowerInfo();
                item.Text = node.Attributes["Text"].Value;
                item.XML = node.InnerXml;
                list.Add(item);
            }
            return list;
        }

        protected List<PowerInfo> ReadPowerItem(string xml)
        {
            string str = "<root>" + xml + "</root>";
            XmlDocument document = new XmlDocument();
            document.LoadXml(str);
            List<PowerInfo> list = new List<PowerInfo>();
            foreach (XmlNode node in document.SelectNodes("root/Item"))
            {
                PowerInfo item = new PowerInfo();
                item.Text = node.Attributes["Text"].Value;
                item.Value = node.Attributes["Value"].Value;
                list.Add(item);
            }
            return list;
        }

        protected void SubmitButton_Click(object sender, EventArgs E)
        {
            AdminGroupInfo adminGroup = new AdminGroupInfo();
            adminGroup.ID = RequestHelper.GetQueryString<int>("ID");
            adminGroup.Name = this.Name.Text;
            adminGroup.Power = RequestHelper.GetForm<string>("Rights").Replace(",", "|");
            if (adminGroup.Power != string.Empty) adminGroup.Power = "|" + adminGroup.Power + "|";
            string alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            if (adminGroup.ID == -2147483648)
            {
                base.CheckAdminPower("AddAdminGroup", PowerCheckType.Single);
                int id = AdminGroupBLL.AddAdminGroup(adminGroup);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("AdminGroup"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateAdminGroup", PowerCheckType.Single);
                AdminGroupBLL.UpdateAdminGroup(adminGroup);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("AdminGroup"), adminGroup.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

