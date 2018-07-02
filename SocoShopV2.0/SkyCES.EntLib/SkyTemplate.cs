﻿namespace SkyCES.EntLib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class SkyTemplate
    {
        private string ashxFolder = string.Empty;
        private Regex execRg = new Regex(@"<%([^=][\s\S]*?)%>", RegexOptions.None);
        private Regex includeRg = new Regex("<html:include file=\"([\\s\\S]+?)\" />", RegexOptions.None);
        private string inheritsNameSpace = string.Empty;
        private string lineWord = "<html:br>";
        private string nameSpace = string.Empty;
        private Regex namespaceRg = new Regex("<html:namespace name=\"(\\S+)\" />", RegexOptions.None);
        public PreWriteHandler OnPreWrite;
        public ProcessFinishedHandler OnProcessFinished;
        public ProcessingHandler OnProcessing;
        private Regex setRg = new Regex(@"<%=([\s\S]+?)%>", RegexOptions.None);
        private List<string> templateFileList;
        private string templateFodler = string.Empty;

        public SkyTemplate(string templateFodler, string ashxFolder, List<string> templateFileList)
        {
            this.templateFodler = templateFodler;
            this.ashxFolder = ashxFolder;
            this.templateFileList = templateFileList;
        }

        public string IncludeFile(string fileName)
        {
            string content = string.Empty;
            string path = HttpContext.Current.Server.MapPath(this.templateFodler + fileName);
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                    reader.Close();
                }
                this.IncludeHandler(ref content);
            }
            return content;
        }

        private void IncludeHandler(ref string content)
        {
            foreach (Match match in this.includeRg.Matches(content))
            {
                content = content.Replace(match.Groups[0].ToString(), this.IncludeFile(match.Groups[1].ToString()));
            }
        }

        private string LineHandler(string content)
        {
            foreach (Match match in this.execRg.Matches(content))
            {
                content = content.Replace(match.Groups[0].ToString(), "\r\n<%" + match.Groups[1].ToString().Replace("\r\n", this.lineWord) + "%>\r\n");
            }
            return content;
        }

        private string PrepareOut(string content)
        {
            string str = "\r\n\tStringBuilder document = new StringBuilder();";
            string input = string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach (string str3 in Regex.Split(content, "\r\n"))
            {
                if (str3.Trim() != string.Empty && str3.Trim() != "\r\n")
                {
                    if (this.setRg.IsMatch(str3))
                    {
                        input = str3.Replace("\"", "\\\"");
                        foreach (Match match in this.setRg.Matches(input))
                        {
                            input = input.Replace(match.Groups[0].ToString(), "\"+" + match.Groups[1].ToString().Replace("\\\"", "\"") + "+\"");
                        }
                        str = str + "\r\n\tdocument.Append(\"" + input + "\\r\\n\");";
                    }
                    else if (this.execRg.IsMatch(str3))
                    {
                        foreach (Match match in this.execRg.Matches(str3))
                        {
                            str = str + "\r\n\t" + str3.Replace(match.Groups[0].ToString(), match.Groups[1].ToString()).Replace(this.lineWord, "\r\n");
                        }
                    }
                    else
                        str = str + "\r\n\tdocument.Append(\"" + str3.Replace("\"", "\\\"") + "\\r\\n\");";
                }
            }
            if (this.OnPreWrite != null)
            {
                PreWriteEventArgs e = new PreWriteEventArgs(str);
                this.OnPreWrite(this, e);
                str = e.Content;
            }
            str = str + "\r\n\tResponse.Write(document.ToString());";
            if (str.IndexOf("</script>") > -1) str = str.Replace("</script>", "</\"+\"script>");
            return str.Replace("<html:templatePath>/", this.templateFodler).Replace("<html:templatePath>", this.templateFodler);
        }

        public void Process()
        {
            foreach (string str in this.templateFileList)
            {
                string content = string.Empty;
                string path = HttpContext.Current.Server.MapPath(this.templateFodler + str);
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                        reader.Close();
                    }
                    this.IncludeHandler(ref content);
                    this.TagHandler(ref content);
                    string str4 = string.Empty;
                    foreach (Match match in this.namespaceRg.Matches(content))
                    {
                        str4 = str4 + "\r\nusing " + match.Groups[1].ToString() + " ;";
                        content = content.Replace(match.Groups[0].ToString(), string.Empty);
                    }
                    string folderName = FileHelper.GetFolderName(str);
                    string fileName = FileHelper.GetFileName(str);
                    string str10 = ((("<%@ WebHandler Language=\"C#\" Class=\"" + this.nameSpace + "." + fileName + "\" %>") + "\r\nusing System;\r\nusing System.Text;\r\nusing System.Web;\r\nusing System.Collections;\r\nusing System.Collections.Generic;" + str4) + "\r\nnamespace " + this.nameSpace) + "\r\n{";
                    string s = ((((((str10 + "\r\npublic class " + fileName + " : " + this.inheritsNameSpace + "." + fileName) + "\r\n{" + "\r\nprotected override void ShowPage()") + "\r\n{" + "\r\n\t/* ") + "\r\n\t\t本页面代码由天易模板生成于 " + DateTime.Now.ToString() + ".") + "\r\n\t*/") + this.PrepareOut(this.LineHandler(content)) + "\r\n}") + "\r\n}" + "\r\n}";
                    string str8 = HttpContext.Current.Server.MapPath(this.ashxFolder + folderName);
                    if (!Directory.Exists(str8)) Directory.CreateDirectory(str8);
                    using (FileStream stream = new FileStream(str8 + fileName + ".ashx", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(s);
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Close();
                    }
                }
                if (this.OnProcessing != null)
                {
                    int currentPage = this.templateFileList.IndexOf(str) + 1;
                    int count = this.templateFileList.Count;
                    this.OnProcessing(this, new ProcessingEventArgs(count, currentPage));
                }
            }
            if (this.OnProcessFinished != null) this.OnProcessFinished(this, new EventArgs());
        }

        private void TagHandler(ref string content)
        {
            TagComposite composite = new TagComposite();
            composite.AddTag(new CsharpTag());
            composite.AddTag(new SetTag());
            composite.AddTag(new ForeachTag());
            composite.AddTag(new IfTag());
            composite.AddTag(new SwitchTag());
            composite.AddTag(new BreakTag());
            composite.AddTag(new WhileTag());
            composite.AddTag(new ForTag());
            composite.TagHandler(ref content);
        }

        public string InheritsNameSpace
        {
            get
            {
                return this.inheritsNameSpace;
            }
            set
            {
                this.inheritsNameSpace = value;
            }
        }

        public string NameSpace
        {
            get
            {
                return this.nameSpace;
            }
            set
            {
                this.nameSpace = value;
            }
        }
    }
}

