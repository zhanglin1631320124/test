namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ProductPhotoAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                UploadHelper helper = new UploadHelper();
                helper.Path = "/Upload/ProductPhoto/Original/" + RequestHelper.DateNow.ToString("yyyyMM") + "/";
                helper.FileNameType = FileNameType.Guid;
                helper.FileType = ShopConfig.ReadConfigInfo().UploadFile;
                FileInfo info = helper.SaveAs();
                string filePath = helper.Path + info.Name;
                string str2 = string.Empty;
                string str3 = string.Empty;
                Hashtable hashtable = new Hashtable();
                hashtable.Add("60", "60");
                hashtable.Add("340", "340");
                foreach (DictionaryEntry entry in hashtable)
                {
                    str3 = filePath.Replace("Original", entry.Key + "-" + entry.Value);
                    str2 = str2 + str3 + "|";
                    ImageHelper.MakeThumbnailImage(ServerHelper.MapPath(filePath), ServerHelper.MapPath(str3), Convert.ToInt32(entry.Key), Convert.ToInt32(entry.Value), ThumbnailType.InBox);
                }
                str2 = str2.Substring(0, str2.Length - 1);
                ResponseHelper.Write("<script>window.parent.addProductPhoto('" + filePath.Replace("Original", "340-340") + "','" + this.Name.Text + "');</script>");
                UploadInfo upload = new UploadInfo();
                upload.TableID = ProductPhotoBLL.TableID;
                upload.ClassID = 0;
                upload.RecordID = 0;
                upload.UploadName = filePath;
                upload.OtherFile = str2;
                upload.Size = Convert.ToInt32(info.Length);
                upload.FileType = info.Extension;
                upload.RandomNumber = Cookies.Admin.GetRandomNumber(false);
                upload.Date = RequestHelper.DateNow;
                upload.IP = ClientHelper.IP;
                UploadBLL.AddUpload(upload);
            }
            catch (Exception exception)
            {
                ExceptionHelper.ProcessException(exception, false);
            }
        }
    }
}

