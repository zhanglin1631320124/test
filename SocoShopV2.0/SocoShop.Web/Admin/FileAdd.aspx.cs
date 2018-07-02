namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Page;
    using System;
    using System.IO;
    using System.Web.UI.WebControls;

    public partial class FileAdd : AdminBasePage
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
                if (FileHelper.SafeFullDirectoryName(queryString))
                {
                    if (File.Exists(ServerHelper.MapPath(queryString + this.UploadFile.FileName)))
                        alertMessage = ShopLanguage.ReadLanguage("ExsitsThisFile");
                    else
                    {
                        try
                        {
                            UploadHelper helper = new UploadHelper();
                            helper.Path = queryString;
                            helper.FileType = ShopConfig.ReadConfigInfo().UploadFile;
                            helper.FileNameType = FileNameType.OriginalFileName;
                            helper.SaveAs();
                            alertMessage = ShopLanguage.ReadLanguage("AddOK");
                        }
                        catch (Exception exception)
                        {
                            ExceptionHelper.ProcessException(exception, false);
                        }
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

