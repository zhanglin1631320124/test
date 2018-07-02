namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Web;

    public sealed class EmailSendRecordBLL
    {
        private static readonly IEmailSendRecord dal = FactoryHelper.Instance<IEmailSendRecord>(Global.DataProvider, "EmailSendRecordDAL");

        public static int AddEmailSendRecord(EmailSendRecordInfo emailSendRecord)
        {
            emailSendRecord.ID = dal.AddEmailSendRecord(emailSendRecord);
            return emailSendRecord.ID;
        }

        public static void DeleteEmailSendRecord(string strID)
        {
            dal.DeleteEmailSendRecord(strID);
        }

        public static EmailSendRecordInfo ReadEmailSendRecord(int id)
        {
            return dal.ReadEmailSendRecord(id);
        }

        public static void RecordOpenedEmailRecord(string email, int id)
        {
            dal.RecordOpenedEmailRecord(email, id);
        }

        public static void SaveEmailSendRecordStatus(EmailSendRecordInfo emailSendRecord)
        {
            dal.SaveEmailSendRecordStatus(emailSendRecord);
        }

        public static List<EmailSendRecordInfo> SearchEmailSendRecordList(EmailSendRecordSearchInfo emailSendRecord)
        {
            return dal.SearchEmailSendRecordList(emailSendRecord);
        }

        public static List<EmailSendRecordInfo> SearchEmailSendRecordList(int currentPage, int pageSize, EmailSendRecordSearchInfo emailSendRecord, ref int count)
        {
            return dal.SearchEmailSendRecordList(currentPage, pageSize, emailSendRecord, ref count);
        }

        public static EmailSendRecordInfo SendEmail(EmailSendRecordInfo emailSendRecord)
        {
            foreach (string str in emailSendRecord.EmailList.Split(new char[] { ',' }))
            {
                if (str != string.Empty)
                {
                    MailInfo mail = new MailInfo();
                    mail.ToEmail = str;
                    mail.Title = emailSendRecord.Title;
                    mail.Content = emailSendRecord.Content;
                    if (emailSendRecord.IsStatisticsOpendEmail == 1)
                    {
                        object content = mail.Content;
                        mail.Content = string.Concat(new object[] { content, "<img style=\"display:none\" src=\"http://", HttpContext.Current.Request.ServerVariables["Http_Host"], "/Admin/EmailCheckOpen.aspx?Email=", str, "&ID=", emailSendRecord.ID, "\" >" });
                    }
                    mail.UserName = ShopConfig.ReadConfigInfo().EmailUserName;
                    mail.Password = ShopConfig.ReadConfigInfo().EmailPassword;
                    mail.Server = ShopConfig.ReadConfigInfo().EmailServer;
                    mail.ServerPort = ShopConfig.ReadConfigInfo().EmailServerPort;
                    try
                    {
                        MailClass.SendEmail(mail);
                    }
                    catch (Exception exception)
                    {
                        ExceptionHelper.ProcessException(exception, true);
                    }
                }
            }
            emailSendRecord.SendDate = RequestHelper.DateNow;
            emailSendRecord.SendStatus = 3;
            SaveEmailSendRecordStatus(emailSendRecord);
            return emailSendRecord;
        }
    }
}

