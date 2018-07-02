namespace SocoShop.Web.Admin
{
    using FredCK.FCKeditorV2;
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ArticleAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                foreach (ArticleClassInfo info in ArticleClassBLL.ReadArticleClassNamedList())
                {
                    this.ClassID.Items.Add(new ListItem(info.ClassName, info.ID.ToString()));
                }
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadArticle", PowerCheckType.Single);
                    ArticleInfo info2 = ArticleBLL.ReadArticle(queryString);
                    this.txtTitle.Text = info2.Title;
                    string classID = info2.ClassID;
                    if (classID != string.Empty)
                    {
                        classID = classID.Substring(1, classID.Length - 2);
                        if (classID.IndexOf('|') > -1) classID = classID.Substring(classID.LastIndexOf('|') + 1);
                    }
                    this.ClassID.Text = classID;
                    this.IsTop.Text = info2.IsTop.ToString();
                    this.Author.Text = info2.Author;
                    this.Resource.Text = info2.Resource;
                    this.Keywords.Text = info2.Keywords;
                    this.Url.Text = info2.Url;
                    this.Photo.Text = info2.Photo;
                    this.Summary.Text = info2.Summary;
                    this.Content.Value = info2.Content;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ArticleInfo article = new ArticleInfo();
            article.ID = RequestHelper.GetQueryString<int>("ID");
            article.Title = this.txtTitle.Text;
            article.ClassID = ArticleClassBLL.ReadArticleClassFullFatherID(Convert.ToInt32(this.ClassID.Text));
            article.IsTop = Convert.ToInt32(this.IsTop.Text);
            article.Author = this.Author.Text;
            article.Resource = this.Resource.Text;
            article.Keywords = this.Keywords.Text;
            article.Url = this.Url.Text;
            article.Photo = this.Photo.Text;
            article.Summary = this.Summary.Text;
            article.Content = this.Content.Value;
            article.Date = RequestHelper.DateNow;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (article.ID == -2147483648)
            {
                base.CheckAdminPower("AddArticle", PowerCheckType.Single);
                int id = ArticleBLL.AddArticle(article);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Article"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateArticle", PowerCheckType.Single);
                ArticleBLL.UpdateArticle(article);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Article"), article.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

