namespace SocoShop.Entity
{
    using System;

    public sealed class UploadInfo
    {
        private int classID;
        private DateTime date = DateTime.Now;
        private string fileType = string.Empty;
        private int id;
        private string iP = string.Empty;
        private string otherFile = string.Empty;
        private string randomNumber = string.Empty;
        private int recordID;
        private int size;
        private int tableID;
        private string uploadName = string.Empty;

        public int ClassID
        {
            get
            {
                return this.classID;
            }
            set
            {
                this.classID = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
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

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string IP
        {
            get
            {
                return this.iP;
            }
            set
            {
                this.iP = value;
            }
        }

        public string OtherFile
        {
            get
            {
                return this.otherFile;
            }
            set
            {
                this.otherFile = value;
            }
        }

        public string RandomNumber
        {
            get
            {
                return this.randomNumber;
            }
            set
            {
                this.randomNumber = value;
            }
        }

        public int RecordID
        {
            get
            {
                return this.recordID;
            }
            set
            {
                this.recordID = value;
            }
        }

        public int Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public int TableID
        {
            get
            {
                return this.tableID;
            }
            set
            {
                this.tableID = value;
            }
        }

        public string UploadName
        {
            get
            {
                return this.uploadName;
            }
            set
            {
                this.uploadName = value;
            }
        }
    }
}

