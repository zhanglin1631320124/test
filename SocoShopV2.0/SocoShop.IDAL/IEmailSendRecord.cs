namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IEmailSendRecord
    {
        int AddEmailSendRecord(EmailSendRecordInfo emailSendRecord);
        void DeleteEmailSendRecord(string strID);
        EmailSendRecordInfo ReadEmailSendRecord(int id);
        void RecordOpenedEmailRecord(string email, int id);
        void SaveEmailSendRecordStatus(EmailSendRecordInfo emailSendRecord);
        List<EmailSendRecordInfo> SearchEmailSendRecordList(EmailSendRecordSearchInfo emailSendRecord);
        List<EmailSendRecordInfo> SearchEmailSendRecordList(int currentPage, int pageSize, EmailSendRecordSearchInfo emailSendRecord, ref int count);
    }
}

