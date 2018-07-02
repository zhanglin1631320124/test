namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public class UserAdd : UserBasePage
    {
        protected SingleUnlimitClass singleUnlimitClass = new SingleUnlimitClass();
        protected UserInfo user = new UserInfo();

        protected override void PageLoad()
        {
            base.PageLoad();
            this.user = UserBLL.ReadUser(base.UserID);
            this.singleUnlimitClass.DataSource = RegionBLL.ReadRegionUnlimitClass();
            this.singleUnlimitClass.ClassID = this.user.RegionID;
        }

        protected override void PostBack()
        {
            UserInfo user = UserBLL.ReadUser(base.UserID);
            user.Email = StringHelper.AddSafe(RequestHelper.GetForm<string>("Email"));
            user.Sex = RequestHelper.GetForm<int>("Sex");
            user.Birthday = StringHelper.AddSafe(RequestHelper.GetForm<string>("Birthday"));
            user.MSN = StringHelper.AddSafe(RequestHelper.GetForm<string>("MSN"));
            user.QQ = StringHelper.AddSafe(RequestHelper.GetForm<string>("QQ"));
            user.Tel = StringHelper.AddSafe(RequestHelper.GetForm<string>("Tel"));
            user.Mobile = StringHelper.AddSafe(RequestHelper.GetForm<string>("Mobile"));
            user.RegionID = this.singleUnlimitClass.ClassID;
            user.Address = StringHelper.AddSafe(RequestHelper.GetForm<string>("Address"));
            user.Introduce = StringHelper.AddSafe(RequestHelper.GetForm<string>("Introduce"));
            string str = this.UploadUserPhoto();
            if (str != string.Empty)
            {
                user.Photo = str;
                CookiesHelper.AddCookie("UserPhoto", str);
            }
            CookiesHelper.AddCookie("UserEmail", user.Email);
            UserBLL.UpdateUser(user);
            ScriptHelper.Alert("修改成功", RequestHelper.RawUrl);
        }

        protected string UploadUserPhoto()
        {
            string filePath = string.Empty;
            if (HttpContext.Current.Request.Files[0].FileName != string.Empty)
            {
                try
                {
                    UploadHelper helper = new UploadHelper();
                    helper.Path = "/Upload/UserPhoto/Original/";
                    helper.FileType = ShopConfig.ReadConfigInfo().UploadFile;
                    FileInfo info = helper.SaveAs();
                    filePath = helper.Path + info.Name;
                    string str2 = string.Empty;
                    string str3 = string.Empty;
                    Dictionary<int, int> dictionary = new Dictionary<int, int>();
                    dictionary.Add(70, 70);
                    dictionary.Add(190, 190);
                    foreach (KeyValuePair<int, int> pair in dictionary)
                    {
                        str3 = filePath.Replace("Original", pair.Key.ToString() + "-" + pair.Value.ToString());
                        str2 = str2 + str3 + "|";
                        ImageHelper.MakeThumbnailImage(ServerHelper.MapPath(filePath), ServerHelper.MapPath(str3), pair.Key, pair.Value, ThumbnailType.InBox);
                    }
                    str2 = str2.Substring(0, str2.Length - 1);
                    UploadInfo upload = new UploadInfo();
                    upload.TableID = UserBLL.TableID;
                    upload.ClassID = 0;
                    upload.RecordID = 0;
                    upload.UploadName = filePath;
                    upload.OtherFile = str2;
                    upload.Size = Convert.ToInt32(info.Length);
                    upload.FileType = info.Extension;
                    upload.Date = RequestHelper.DateNow;
                    upload.IP = ClientHelper.IP;
                    UploadBLL.AddUpload(upload);
                }
                catch (Exception exception)
                {
                    ExceptionHelper.ProcessException(exception, false);
                }
            }
            return filePath;
        }
    }
}

