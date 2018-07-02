using SkyCES.EntLib;
using SocoShop.Entity;
using SocoShop.Page;
using System;
using System.Collections.Generic;
using System.IO;

namespace SocoShop.Web.Admin
{
    public partial class TemplateFile : AdminBasePage
    {
        protected List<DirectoryInfo> directoryList = new List<DirectoryInfo>();
        protected string encodeCurrentPath = string.Empty;
        protected List<FileInfo> fileList = new List<FileInfo>();
        protected string pathTree = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAdminPower("Template", PowerCheckType.Single);
            string queryString = RequestHelper.GetQueryString<string>("Path");
            this.fileList = FileHelper.ListFile(ServerHelper.MapPath(queryString));
            this.directoryList = FileHelper.ListDirectory(ServerHelper.MapPath(queryString));
            int num = 1;
            foreach (string str2 in queryString.Split(new char[] { '/' }))
            {
                this.encodeCurrentPath = this.encodeCurrentPath + base.Server.UrlEncode(str2) + "/";
                if (str2 != string.Empty && num > 3)
                {
                    if (this.pathTree == string.Empty)
                        this.pathTree = "<a href=\"?Path=" + this.encodeCurrentPath + "\">根目录</a>";
                    else
                    {
                        string pathTree = this.pathTree;
                        this.pathTree = pathTree + " ›› <a href=\"?Path=" + this.encodeCurrentPath + "\">" + str2 + "</a>";
                    }
                }
                num++;
            }
            this.encodeCurrentPath = this.encodeCurrentPath.Replace("//", "/");
        }
    }
}

