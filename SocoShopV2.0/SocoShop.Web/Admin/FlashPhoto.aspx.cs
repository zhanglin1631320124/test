namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class FlashPhoto : AdminBasePage
    {
        protected int flashID = 0;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteFlashPhoto", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                this.flashID = RequestHelper.GetQueryString<int>("FlashID");
                FlashPhotoBLL.DeleteFlashPhoto(intsForm, this.flashID);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("FlashPhoto"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadFlashPhoto", PowerCheckType.Single);
                this.flashID = RequestHelper.GetQueryString<int>("FlashID");
                string queryString = RequestHelper.GetQueryString<string>("Action");
                int id = RequestHelper.GetQueryString<int>("PhotoID");
                if (queryString != string.Empty && id != -2147483648)
                {
                    base.CheckAdminPower("UpdateFlashPhoto", PowerCheckType.Single);
                    string str2 = queryString;
                    if (str2 != null)
                    {
                        if (!(str2 == "Up"))
                        {
                            if (str2 == "Down") FlashPhotoBLL.ChangeFlashPhotoOrder(ChangeAction.Down, id, this.flashID);
                        }
                        else
                            FlashPhotoBLL.ChangeFlashPhotoOrder(ChangeAction.Up, id, this.flashID);
                    }
                    AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("MoveRecord"), ShopLanguage.ReadLanguage("FlashPhoto"), id);
                }
                int num2 = RequestHelper.GetQueryString<int>("ID");
                if (num2 != -2147483648)
                {
                    FlashPhotoInfo info = FlashPhotoBLL.ReadFlashPhoto(num2);
                    this.txtTitle.Text = info.Title;
                    this.URL.Text = info.URL;
                    this.FileName.Text = info.FileName;
                }
                base.BindControl(FlashPhotoBLL.ReadFlashPhotoByFlash(this.flashID), this.RecordList);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            FlashPhotoInfo flashPhoto = new FlashPhotoInfo();
            flashPhoto.ID = RequestHelper.GetQueryString<int>("ID");
            flashPhoto.FlashID = RequestHelper.GetQueryString<int>("FlashID");
            flashPhoto.Title = this.txtTitle.Text;
            flashPhoto.URL = this.URL.Text;
            flashPhoto.FileName = this.FileName.Text;
            flashPhoto.Date = RequestHelper.DateNow;
            string message = ShopLanguage.ReadLanguage("AddOK");
            if (flashPhoto.ID == -2147483648)
            {
                base.CheckAdminPower("AddFlashPhoto", PowerCheckType.Single);
                int id = FlashPhotoBLL.AddFlashPhoto(flashPhoto);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("FlashPhoto"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateFlashPhoto", PowerCheckType.Single);
                FlashPhotoBLL.UpdateFlashPhoto(flashPhoto);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("FlashPhoto"), flashPhoto.ID);
                message = ShopLanguage.ReadLanguage("UpdateOK");
            }
            ScriptHelper.Alert(message, RequestHelper.RawUrl);
        }
    }
}

