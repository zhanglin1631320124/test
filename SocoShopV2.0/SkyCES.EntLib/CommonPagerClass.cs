namespace SkyCES.EntLib
{
    using System;
    using System.Text;

    public class CommonPagerClass : BasePagerClass
    {
        public override string ShowPage()
        {
            StringBuilder builder = new StringBuilder("");
            if (base.Count > 0)
            {
                int num;
                string[] strArray;
                int num2;
                builder.Append("<div class=\"pageCss\">");
                if (base.DisCount) builder.Append(string.Concat(new object[] { "<ul class=\"disCount\"><li>共有", base.Count, "条</li><li>当前", base.CurrentPage, "/", base.PageCount, "页</li></ul>" }));
                if (base.PrenextType)
                {
                    builder.Append("<ul class=\"prenextType\">");
                    if (base.CurrentPage > 1)
                        builder.Append("<li><a href=" + base.URL.Replace("$Page", "1") + ">" + base.FirstPage + "</a></li>");
                    else
                        builder.Append("<li>" + base.FirstPage + "</li>");
                    if (base.CurrentPage - 1 > 0)
                    {
                        strArray = new string[5];
                        strArray[0] = "<li><a href=";
                        num2 = base.CurrentPage - 1;
                        strArray[1] = base.URL.Replace("$Page", num2.ToString());
                        strArray[2] = ">";
                        strArray[3] = base.PreviewPage;
                        strArray[4] = "</a></li>";
                        builder.Append(string.Concat(strArray));
                    }
                    else
                        builder.Append("<li>" + base.PreviewPage + "</li>");
                    builder.Append("</ul>");
                }
                if (base.NumType)
                {
                    base.CountStartEndPage();
                    builder.Append("<ul class=\"numType\">");
                    for (num = base.StartPage; num <= base.EndPage; num++)
                    {
                        if (base.CurrentPage != num)
                            builder.Append(string.Concat(new object[] { "<li><a href=", base.URL.Replace("$Page", num.ToString()), ">", num, "</a></li>" }));
                        else
                            builder.Append("<li id=\"currentPage\">" + num + "</li>");
                    }
                    builder.Append("</ul>");
                }
                if (base.PrenextType)
                {
                    builder.Append("<ul class=\"prenextType\">");
                    if (base.CurrentPage + 1 <= base.PageCount)
                    {
                        strArray = new string[5];
                        strArray[0] = "<li><a href=";
                        num2 = base.CurrentPage + 1;
                        strArray[1] = base.URL.Replace("$Page", num2.ToString());
                        strArray[2] = ">";
                        strArray[3] = base.NextPage;
                        strArray[4] = "</a></li>";
                        builder.Append(string.Concat(strArray));
                    }
                    else
                        builder.Append("<li>" + base.NextPage + "</li>");
                    if (base.CurrentPage < base.PageCount)
                        builder.Append("<li><a href=" + base.URL.Replace("$Page", base.PageCount.ToString()) + ">" + base.LastPage + "</a></li>");
                    else
                        builder.Append("<li>" + base.LastPage + "</li>");
                    builder.Append("</ul>");
                }
                if (base.ListType)
                {
                    builder.Append("<ul class=\"listType\">");
                    builder.Append("<li>跳转 ");
                    builder.Append("<select name=select onchange=\"window.location.href=this.value\">");
                    for (num = 1; num <= base.PageCount; num++)
                    {
                        if (num == base.CurrentPage)
                            builder.Append(string.Concat(new object[] { "<option value=", base.URL.Replace("$Page", num.ToString()), " selected=selected>", num, "</option>" }));
                        else
                            builder.Append(string.Concat(new object[] { "<option value=", base.URL.Replace("$Page", num.ToString()), ">", num, "</option>" }));
                    }
                    builder.Append("</select></li>");
                    builder.Append("</ul>");
                }
                builder.Append("</div>");
            }
            return builder.ToString();
        }
    }
}

