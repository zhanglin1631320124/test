namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class EmailSendRecordDAL : IEmailSendRecord
    {
        public int AddEmailSendRecord(EmailSendRecordInfo emailSendRecord)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@isSystem", SqlDbType.Int), new SqlParameter("@emailList", SqlDbType.NText), new SqlParameter("@openEmailList", SqlDbType.NText), new SqlParameter("@isStatisticsOpendEmail", SqlDbType.Int), new SqlParameter("@sendStatus", SqlDbType.Int), new SqlParameter("@note", SqlDbType.NVarChar), new SqlParameter("@addDate", SqlDbType.DateTime), new SqlParameter("@sendDate", SqlDbType.DateTime) };
            pt[0].Value = emailSendRecord.Title;
            pt[1].Value = emailSendRecord.Content;
            pt[2].Value = emailSendRecord.IsSystem;
            pt[3].Value = emailSendRecord.EmailList;
            pt[4].Value = emailSendRecord.OpenEmailList;
            pt[5].Value = emailSendRecord.IsStatisticsOpendEmail;
            pt[6].Value = emailSendRecord.SendStatus;
            pt[7].Value = emailSendRecord.Note;
            pt[8].Value = emailSendRecord.AddDate;
            pt[9].Value = emailSendRecord.SendDate;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddEmailSendRecord", pt));
        }

        public void DeleteEmailSendRecord(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteEmailSendRecord", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, EmailSendRecordSearchInfo emailSendRecordSearch)
        {
            mssqlCondition.Add("[IsSystem]", emailSendRecordSearch.IsSystem, ConditionType.Equal);
        }

        public void PrepareEmailSendRecordModel(SqlDataReader dr, List<EmailSendRecordInfo> emailSendRecordList)
        {
            while (dr.Read())
            {
                EmailSendRecordInfo item = new EmailSendRecordInfo();
                item.ID = dr.GetInt32(0);
                item.Title = dr[1].ToString();
                item.Content = dr[2].ToString();
                item.IsSystem = dr.GetInt32(3);
                item.EmailList = dr[4].ToString();
                item.OpenEmailList = dr[5].ToString();
                item.IsStatisticsOpendEmail = dr.GetInt32(6);
                item.SendStatus = dr.GetInt32(7);
                item.Note = dr[8].ToString();
                item.AddDate = dr.GetDateTime(9);
                item.SendDate = dr.GetDateTime(10);
                emailSendRecordList.Add(item);
            }
        }

        public EmailSendRecordInfo ReadEmailSendRecord(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            EmailSendRecordInfo info = new EmailSendRecordInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadEmailSendRecord", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Title = reader[1].ToString();
                    info.Content = reader[2].ToString();
                    info.IsSystem = reader.GetInt32(3);
                    info.EmailList = reader[4].ToString();
                    info.OpenEmailList = reader[5].ToString();
                    info.IsStatisticsOpendEmail = reader.GetInt32(6);
                    info.SendStatus = reader.GetInt32(7);
                    info.Note = reader[8].ToString();
                    info.AddDate = reader.GetDateTime(9);
                    info.SendDate = reader.GetDateTime(10);
                }
            }
            return info;
        }

        public void RecordOpenedEmailRecord(string email, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@email", SqlDbType.NVarChar), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = email;
            pt[1].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "RecordOpenedEmailRecord", pt);
        }

        public void SaveEmailSendRecordStatus(EmailSendRecordInfo emailSendRecord)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@sendStatus", SqlDbType.Int), new SqlParameter("@sendDate", SqlDbType.DateTime) };
            pt[0].Value = emailSendRecord.ID;
            pt[1].Value = emailSendRecord.SendStatus;
            pt[2].Value = emailSendRecord.SendDate;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "SaveEmailSendRecordStatus", pt);
        }

        public List<EmailSendRecordInfo> SearchEmailSendRecordList(EmailSendRecordSearchInfo emailSendRecordSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, emailSendRecordSearch);
            List<EmailSendRecordInfo> emailSendRecordList = new List<EmailSendRecordInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchEmailSendRecordList", pt))
            {
                this.PrepareEmailSendRecordModel(reader, emailSendRecordList);
            }
            return emailSendRecordList;
        }

        public List<EmailSendRecordInfo> SearchEmailSendRecordList(int currentPage, int pageSize, EmailSendRecordSearchInfo emailSendRecordSearch, ref int count)
        {
            List<EmailSendRecordInfo> emailSendRecordList = new List<EmailSendRecordInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "EmailSendRecord";
            class2.Fields = "[ID],[Title],[Content],[IsSystem],[EmailList],[OpenEmailList],[IsStatisticsOpendEmail],[SendStatus],[Note],[AddDate],[SendDate]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, emailSendRecordSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareEmailSendRecordModel(reader, emailSendRecordList);
            }
            return emailSendRecordList;
        }
    }
}

