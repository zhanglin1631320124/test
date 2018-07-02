namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Article : AdminBasePage
    {
        protected string classID = string.Empty;

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteArticle", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ArticleBLL.DeleteArticle(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Article"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadArticle", PowerCheckType.Single);
                foreach (ArticleClassInfo info in ArticleClassBLL.ReadArticleClassNamedList())
                {
                    this.ArticleClassID.Items.Add(new ListItem(info.ClassName, "|" + info.ID + "|"));
                }
                this.ArticleClassID.Items.Insert(0, new ListItem("所有分类", string.Empty));
                this.txtTitle.Text = RequestHelper.GetQueryString<string>("Title");
                this.ArticleClassID.Text = RequestHelper.GetQueryString<string>("ClassID");
                this.IsTop.Text = RequestHelper.GetQueryString<string>("IsTop");
                this.classID = RequestHelper.GetQueryString<string>("ClassID");
                if (this.classID != string.Empty)
                {
                    this.classID = ArticleClassBLL.ReadArticleClassFullFatherID(Convert.ToInt32(this.classID.Replace("|", string.Empty)));
                    this.classID = this.classID.Substring(1, this.classID.Length - 2);
                    if (this.classID.IndexOf('|') > -1) this.classID = this.classID.Substring(0, this.classID.IndexOf('|'));
                }
                ArticleSearchInfo article = new ArticleSearchInfo();
                article.Title = RequestHelper.GetQueryString<string>("Title");
                article.ClassID = RequestHelper.GetQueryString<string>("ClassID");
                article.IsTop = RequestHelper.GetQueryString<int>("IsTop");
                base.BindControl(ArticleBLL.SearchArticleList(base.CurrentPage, base.PageSize, article, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((("Article.aspx?Action=search&" + "Title=" + this.txtTitle.Text + "&") + "ClassID=" + this.ArticleClassID.Text + "&") + "IsTop=" + this.IsTop.Text);
        }
    }
}

