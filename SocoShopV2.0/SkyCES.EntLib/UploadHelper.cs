namespace SkyCES.EntLib
{
    using System;
    using System.IO;
    using System.Web;

    public class UploadHelper
    {
        private string fileExtension;
        private SkyCES.EntLib.FileNameType fileNameType = SkyCES.EntLib.FileNameType.Date;
        private string fileType = string.Empty;
        private long localFileLength;
        private string localFileName;
        private string localFilePath;
        private string path = string.Empty;
        private HttpPostedFile postedFile;
        private string saveFileFolderPath;
        private string saveFileFullPath;
        private string saveFileName;
        private int sizes = 0x7d0;

        private string GetSaveFileFolderPath()
        {
            string path = string.Empty;
            path = ServerHelper.MapPath(this.path);
            DirectoryInfo info = new DirectoryInfo(path);
            if (!info.Exists) info.Create();
            return path;
        }

        public FileInfo SaveAs()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            FileInfo info = null;
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    this.postedFile = files[i];
                    this.localFilePath = this.postedFile.FileName;
                    if (this.localFilePath == null || this.localFilePath == "") throw new Exception("不能上传空文件");
                    this.localFileLength = this.postedFile.ContentLength;
                    if (this.localFileLength >= this.sizes * 0x400) throw new Exception("上传的图片不能大于:" + this.sizes + "KB");
                    this.saveFileFolderPath = this.GetSaveFileFolderPath();
                    this.localFileName = System.IO.Path.GetFileName(this.postedFile.FileName);
                    this.fileExtension = FileHelper.GetFileExtension(this.localFileName);
                    if (this.fileType.ToLower().IndexOf(this.fileExtension) == -1) throw new Exception("目前本系统支持的格式为:" + this.fileType);
                    this.saveFileName = FileHelper.CreateFileName(this.fileNameType, this.localFileName, this.fileExtension);
                    this.saveFileFullPath = this.saveFileFolderPath + this.saveFileName;
                    this.postedFile.SaveAs(this.saveFileFullPath);
                    info = new FileInfo(this.saveFileFolderPath + this.saveFileName);
                }
            }
            catch
            {
                throw;
            }
            return info;
        }

        public SkyCES.EntLib.FileNameType FileNameType
        {
            get
            {
                return this.fileNameType;
            }
            set
            {
                this.fileNameType = value;
            }
        }

        public string FileType
        {
            get
            {
                return this.fileType;
            }
            set
            {
                this.fileType = value;
            }
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        public int Sizes
        {
            get
            {
                return this.sizes;
            }
            set
            {
                this.sizes = value;
            }
        }
    }
}

