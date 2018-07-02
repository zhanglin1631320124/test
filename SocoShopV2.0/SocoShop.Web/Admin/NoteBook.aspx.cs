using SkyCES.EntLib;
using SocoShop.Business;
using SocoShop.Common;
using SocoShop.Entity;
using SocoShop.Page;
using System;
using System.Web.UI.WebControls;

namespace SocoShop.Web.Admin
{
    public partial class NoteBook : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                AdminInfo info = AdminBLL.ReadAdmin(Cookies.Admin.GetAdminID(false));
                this.NoteBookContent.Text = info.NoteBook;
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            AdminInfo admin = AdminBLL.ReadAdmin(Cookies.Admin.GetAdminID(false));
            admin.NoteBook = this.NoteBookContent.Text;
            AdminBLL.UpdateAdmin(admin);
            AdminBasePage.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
        }
    }
}

