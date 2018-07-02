namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public partial class HardDisk : AdminBasePage
    {
        protected List<DirectoryInfo> directoryList = new List<DirectoryInfo>();
        protected string encodeCurrentPath = string.Empty;
        protected List<FileInfo> fileList = new List<FileInfo>();
        protected string pathTree = string.Empty;

        protected void ChangeDirectoryName()
        {
            try
            {
                string queryString = RequestHelper.GetQueryString<string>("NewName");
                string directoryName = RequestHelper.GetQueryString<string>("OldName");
                string str3 = RequestHelper.GetQueryString<string>("Path");
                if (FileHelper.SafeDirectoryName(queryString) && FileHelper.SafeDirectoryName(directoryName) && FileHelper.SafeFullDirectoryName(str3) && str3.ToLower().StartsWith("/upload/harddisk/"))
                {
                    str3 = ServerHelper.MapPath(str3);
                    Directory.Move(str3 + directoryName + "/", str3 + queryString + "/");
                    ResponseHelper.Write("ok");
                }
                else
                    ResponseHelper.Write("error");
            }
            catch
            {
                ResponseHelper.Write("error");
            }
            ResponseHelper.End();
        }

        protected void ChangeFileName()
        {
            try
            {
                string queryString = RequestHelper.GetQueryString<string>("NewName");
                string str2 = RequestHelper.GetQueryString<string>("OldName");
                string directoryName = RequestHelper.GetQueryString<string>("Path");
                if (ShopConfig.ReadConfigInfo().UploadFile.IndexOf(FileHelper.GetFileExtension(queryString)) > -1 && FileHelper.SafeFullDirectoryName(directoryName) && directoryName.ToLower().StartsWith("/upload/harddisk/"))
                {
                    directoryName = ServerHelper.MapPath(directoryName);
                    File.Move(directoryName + str2, directoryName + queryString);
                    ResponseHelper.Write("ok");
                }
                else
                    ResponseHelper.Write("error");
            }
            catch
            {
                ResponseHelper.Write("error");
            }
            ResponseHelper.End();
        }

        protected void DeleteDirectory()
        {
            string queryString = RequestHelper.GetQueryString<string>("Path");
            if (queryString.ToLower().StartsWith("/upload/harddisk/"))
            {
                try
                {
                    Directory.Delete(base.Server.MapPath(queryString), true);
                    ResponseHelper.Write("ok");
                }
                catch
                {
                    ResponseHelper.Write("error");
                }
            }
            else
                ResponseHelper.Write("error");
            ResponseHelper.End();
        }

        protected void DeleteFile()
        {
            string queryString = RequestHelper.GetQueryString<string>("FileName");
            if (queryString.ToLower().StartsWith("/upload/harddisk/"))
            {
                try
                {
                    File.Delete(ServerHelper.MapPath(queryString));
                    ResponseHelper.Write("ok");
                }
                catch
                {
                    ResponseHelper.Write("error");
                }
            }
            else
                ResponseHelper.Write("error");
            ResponseHelper.End();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ClearCache();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != null)
            {
                if (!(queryString == "DeleteDirectory"))
                {
                    if (queryString == "DeleteFile")
                        this.DeleteFile();
                    else if (queryString == "Paste")
                        this.Paste();
                    else if (queryString == "ChangeDirectoryName")
                        this.ChangeDirectoryName();
                    else if (queryString == "ChangeFileName") this.ChangeFileName();
                }
                else
                    this.DeleteDirectory();
            }
            string filePath = RequestHelper.GetQueryString<string>("Path");
            if (!(filePath != string.Empty && filePath.ToLower().StartsWith("/upload/harddisk/"))) filePath = "/Upload/HardDisk/";
            this.fileList = FileHelper.ListFile(ServerHelper.MapPath(filePath));
            this.directoryList = FileHelper.ListDirectory(ServerHelper.MapPath(filePath));
            int num = 1;
            foreach (string str3 in filePath.Split(new char[] { '/' }))
            {
                this.encodeCurrentPath = this.encodeCurrentPath + base.Server.UrlEncode(str3) + "/";
                if (str3 != string.Empty && num > 2)
                {
                    if (this.pathTree == string.Empty)
                        this.pathTree = "<a href=\"?Path=" + this.encodeCurrentPath + "\">根目录</a>";
                    else
                    {
                        string pathTree = this.pathTree;
                        this.pathTree = pathTree + " ›› <a href=\"?Path=" + this.encodeCurrentPath + "\">" + str3 + "</a>";
                    }
                }
                num++;
            }
            this.encodeCurrentPath = this.encodeCurrentPath.Replace("//", "/");
        }

        protected void Paste()
        {
            string queryString = RequestHelper.GetQueryString<string>("FileList");
            string str2 = RequestHelper.GetQueryString<string>("SourceAction");
            string str3 = ServerHelper.MapPath(RequestHelper.GetQueryString<string>("Path"));
            if (RequestHelper.GetQueryString<string>("Path").ToLower().StartsWith("/upload/harddisk/"))
            {
                try
                {
                    if (queryString != string.Empty)
                    {
                        foreach (string str4 in queryString.Split(new char[] { '|' }))
                        {
                            if (str4.ToLower().StartsWith("/upload/harddisk/"))
                            {
                                string path = ServerHelper.MapPath(str4);
                                if (File.Exists(path))
                                {
                                    string str6 = path.Substring(path.LastIndexOf(@"\"));
                                    if (str2 == "Cut")
                                        File.Move(path, str3 + str6);
                                    else
                                        File.Copy(path, str3 + str6);
                                }
                                else if (Directory.Exists(path))
                                {
                                    string str7 = path.Substring(0, path.Length - 1);
                                    str7 = str7.Substring(str7.LastIndexOf(@"\"));
                                    if (str2 == "Cut")
                                        Directory.Move(path, str3 + str7 + @"\");
                                    else
                                        FileHelper.CopyDirectory(path, str3 + str7 + @"\");
                                }
                            }
                        }
                    }
                    ResponseHelper.Write("ok");
                }
                catch
                {
                    ResponseHelper.Write("error");
                }
            }
            else
                ResponseHelper.Write("error");
            ResponseHelper.End();
        }
    }
}

