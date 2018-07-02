namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.IO;
    using System.Web.UI.WebControls;

    public partial class TemplateFileAdd : AdminBasePage
    {
        protected string fileName = string.Empty;
        protected string path = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("Template", PowerCheckType.Single);
                this.path = RequestHelper.GetQueryString<string>("Path");
                this.fileName = RequestHelper.GetQueryString<string>("FileName");
                using (StreamReader reader = File.OpenText(ServerHelper.MapPath(this.path + this.fileName)))
                {
                    this.Content.Text = reader.ReadToEnd();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("Template", PowerCheckType.Single);
            this.path = RequestHelper.GetQueryString<string>("Path");
            this.fileName = RequestHelper.GetQueryString<string>("FileName");
            using (StreamWriter writer = File.CreateText(ServerHelper.MapPath(this.path + this.fileName)))
            {
                writer.Write(this.Content.Text);
                writer.Close();
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
            }
        }
    }
}

