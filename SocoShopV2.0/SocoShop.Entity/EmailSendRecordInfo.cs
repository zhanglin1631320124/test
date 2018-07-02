namespace SocoShop.Entity
{
    using System;

    public sealed class EmailSendRecordInfo
    {
        private DateTime addDate = DateTime.Now;
        private string content = string.Empty;
        private string emailList = string.Empty;
        private int id;
        private int isStatisticsOpendEmail;
        private int isSystem;
        private string note = string.Empty;
        private string openEmailList = string.Empty;
        private DateTime sendDate = DateTime.Now;
        private int sendStatus;
        private string title = string.Empty;

        public DateTime AddDate
        {
            get
            {
                return this.addDate;
            }
            set
            {
                this.addDate = value;
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public string EmailList
        {
            get
            {
                return this.emailList;
            }
            set
            {
                this.emailList = value;
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

        public int IsStatisticsOpendEmail
        {
            get
            {
                return this.isStatisticsOpendEmail;
            }
            set
            {
                this.isStatisticsOpendEmail = value;
            }
        }

        public int IsSystem
        {
            get
            {
                return this.isSystem;
            }
            set
            {
                this.isSystem = value;
            }
        }

        public string Note
        {
            get
            {
                return this.note;
            }
            set
            {
                this.note = value;
            }
        }

        public string OpenEmailList
        {
            get
            {
                return this.openEmailList;
            }
            set
            {
                this.openEmailList = value;
            }
        }

        public DateTime SendDate
        {
            get
            {
                return this.sendDate;
            }
            set
            {
                this.sendDate = value;
            }
        }

        public int SendStatus
        {
            get
            {
                return this.sendStatus;
            }
            set
            {
                this.sendStatus = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }
    }
}

