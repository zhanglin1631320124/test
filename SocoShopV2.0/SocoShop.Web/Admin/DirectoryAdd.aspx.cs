namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Page;
    using System;
    using System.IO;
    using System.Web.UI.WebControls;

    public partial class DirectoryAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string queryString = RequestHelper.GetQueryString<string>("Path");
            string alertMessage = string.Empty;
            if (queryString.ToLower().StartsWith("/upload/harddisk/"))
            {
                string directoryName = queryString + this.DirectoryName.Text + "/";
                if (FileHelper.SafeDirectoryName(this.DirectoryName.Text) && FileHelper.SafeFullDirectoryName(directoryName))
                {
                    if (Directory.Exists(directoryName))
                        alertMessage = ShopLanguage.ReadLanguage("ExsitsThisDirectory");
                    else
                    {
                        Directory.CreateDirectory(ServerHelper.MapPath(directoryName));
                        alertMessage = ShopLanguage.ReadLanguage("AddOK");
                    }
                }
                else
                    alertMessage = ShopLanguage.ReadLanguage("ErrorPathName");
            }
            else
                alertMessage = ShopLanguage.ReadLanguage("DirectoryStartWith");
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

