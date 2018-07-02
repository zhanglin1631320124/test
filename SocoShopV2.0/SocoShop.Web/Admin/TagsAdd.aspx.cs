namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class TagsAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadTags", PowerCheckType.Single);
                    TagsInfo info = TagsBLL.ReadTags(queryString, 0);
                    this.PrdouctID.Items.Add(new ListItem(ProductBLL.ReadProduct(info.ProductID).Name, info.ProductID.ToString()));
                    this.Word.Text = info.Word;
                    this.Word.Attributes.Add("style", string.Concat(new object[] { "color:", info.Color, ";font-size:", info.Size, "px;" }));
                    this.Color.Text = info.Color;
                    this.Size.Text = info.Size.ToString();
                    this.IsTop.Text = info.IsTop.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            TagsInfo tags = new TagsInfo();
            tags.ID = RequestHelper.GetQueryString<int>("ID");
            tags.ProductID = RequestHelper.GetForm<int>(base.NamePrefix + "PrdouctID");
            tags.Word = this.Word.Text;
            tags.Color = this.Color.Text;
            tags.Size = Convert.ToInt32(this.Size.Text);
            tags.IsTop = Convert.ToInt32(this.IsTop.Text);
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (tags.ID == -2147483648)
            {
                base.CheckAdminPower("AddTags", PowerCheckType.Single);
                int id = TagsBLL.AddTags(tags);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Tags"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateTags", PowerCheckType.Single);
                TagsBLL.UpdateTags(tags);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Tags"), tags.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

