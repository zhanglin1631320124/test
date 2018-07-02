namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UploadAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void UploadImage(object sender, EventArgs e)
        {
            string queryString = RequestHelper.GetQueryString<string>("Control");
            int num = RequestHelper.GetQueryString<int>("TableID");
            string directoryName = RequestHelper.GetQueryString<string>("FilePath");
            string uploadFile = ShopConfig.ReadConfigInfo().UploadFile;
            if (FileHelper.SafeFullDirectoryName(directoryName))
            {
                try
                {
                    UploadHelper helper = new UploadHelper();
                    helper.Path = "/Upload/" + directoryName + "/" + RequestHelper.DateNow.ToString("yyyyMM") + "/";
                    helper.FileType = uploadFile;
                    helper.FileNameType = FileNameType.Guid;
                    FileInfo info = helper.SaveAs();
                    string filePath = helper.Path + info.Name;
                    string str5 = string.Empty;
                    string str6 = string.Empty;
                    Dictionary<int, int> dictionary = new Dictionary<int, int>();
                    if (num == ProductBLL.TableID)
                    {
                        dictionary.Add(60, 60);
                        dictionary.Add(120, 120);
                        dictionary.Add(340, 340);
                    }
                    else if (num == ProductBrandBLL.TableID)
                        dictionary.Add(0x58, 0x1f);
                    else if (num == LinkBLL.TableID)
                        dictionary.Add(0x58, 0x1f);
                    else if (num == StandardBLL.TableID)
                        dictionary.Add(0x19, 0x19);
                    else if (num == ThemeActivityBLL.TableID)
                        dictionary.Add(300, 150);
                    else if (num == GiftPackBLL.TableID)
                        dictionary.Add(150, 60);
                    else if (num == FavorableActivityBLL.TableID)
                        dictionary.Add(300, 120);
                    else if (num == GiftBLL.TableID) dictionary.Add(100, 100);
                    if (dictionary.Count > 0)
                    {
                        foreach (KeyValuePair<int, int> pair in dictionary)
                        {
                            str6 = filePath.Replace("Original", pair.Key.ToString() + "-" + pair.Value.ToString());
                            str5 = str5 + str6 + "|";
                            ImageHelper.MakeThumbnailImage(ServerHelper.MapPath(filePath), ServerHelper.MapPath(str6), pair.Key, pair.Value, ThumbnailType.InBox);
                        }
                        str5 = str5.Substring(0, str5.Length - 1);
                    }
                    ResponseHelper.Write("<script> window.parent.o('" + base.IDPrefix + queryString + "').value='" + filePath + "';</script>");
                    UploadInfo upload = new UploadInfo();
                    upload.TableID = num;
                    upload.ClassID = 0;
                    upload.RecordID = 0;
                    upload.UploadName = filePath;
                    upload.OtherFile = str5;
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
            else
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("ErrorPathName"));
        }
    }
}

