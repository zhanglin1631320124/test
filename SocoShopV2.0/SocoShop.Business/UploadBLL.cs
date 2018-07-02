namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class UploadBLL
    {
        private static readonly IUpload dal = FactoryHelper.Instance<IUpload>(Global.DataProvider, "UploadDAL");

        public static void AddUpload(UploadInfo upload)
        {
            dal.AddUpload(upload);
        }

        public static void DeleteUploadByClassID(int tableID, string strClassID)
        {
            FileHelper.DeleteFile(ReadUploadByClassID(tableID, strClassID));
            dal.DeleteUploadByClassID(tableID, strClassID);
        }

        public static void DeleteUploadByRecordID(int tableID, string strRecordID)
        {
            FileHelper.DeleteFile(ReadUploadByRecordID(tableID, strRecordID));
            dal.DeleteUploadByRecordID(tableID, strRecordID);
        }

        public static List<string> ReadUploadByClassID(int tableID, string strClassID)
        {
            List<string> list = new List<string>();
            List<UploadInfo> list2 = dal.ReadUploadByClassID(tableID, strClassID);
            foreach (UploadInfo info in list2)
            {
                list.Add(info.UploadName);
                if (info.OtherFile != string.Empty)
                {
                    foreach (string str in info.OtherFile.Split(new char[] { '|' }))
                    {
                        list.Add(str);
                    }
                }
            }
            return list;
        }

        public static List<string> ReadUploadByRecordID(int tableID, string strRecordID)
        {
            List<string> list = new List<string>();
            List<UploadInfo> list2 = dal.ReadUploadByRecordID(tableID, strRecordID);
            foreach (UploadInfo info in list2)
            {
                list.Add(info.UploadName);
                if (info.OtherFile != string.Empty)
                {
                    foreach (string str in info.OtherFile.Split(new char[] { '|' }))
                    {
                        list.Add(str);
                    }
                }
            }
            return list;
        }

        public static void UpdateUpload(int tableID, int classID, int recordID, string randomNumber)
        {
            dal.UpdateUpload(tableID, classID, recordID, randomNumber);
        }
    }
}

