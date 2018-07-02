namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public sealed class FlashBLL
    {
        private static readonly IFlash dal = FactoryHelper.Instance<IFlash>(Global.DataProvider, "FlashDAL");

        public static int AddFlash(FlashInfo flash)
        {
            flash.ID = dal.AddFlash(flash);
            RebuildFile(flash.ID);
            return flash.ID;
        }

        public static void ChangeFlashCount(int id, ChangeAction action)
        {
            dal.ChangeFlashCount(id, action);
        }

        public static void ChangeFlashCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeFlashCountByGeneral(strID, action);
        }

        public static void DeleteFlash(string strID)
        {
            FlashPhotoBLL.DeleteFlashPhotoByFlashID(strID);
            dal.DeleteFlash(strID);
        }

        public static FlashInfo ReadFlash(int id)
        {
            return dal.ReadFlash(id);
        }

        public static List<FlashInfo> ReadFlashList(int currentPage, int pageSize, ref int count)
        {
            return dal.ReadFlashList(currentPage, pageSize, ref count);
        }

        public static void RebuildFile(int flashID)
        {
            StringBuilder builder = new StringBuilder();
            FlashInfo info = ReadFlash(flashID);
            if (info.ID > 0)
            {
                string str = info.Width.ToString();
                string str2 = info.Height.ToString();
                string flashFile = ShopCommon.GetFlashFile(flashID.ToString());
                string title = string.Empty;
                string uRL = string.Empty;
                string fileName = string.Empty;
                bool flag = true;
                builder.Append("var swf_width=" + str + ";\r\n");
                builder.Append("var swf_height=" + str2 + ";\r\n");
                List<FlashPhotoInfo> list = FlashPhotoBLL.ReadFlashPhotoByFlash(flashID);
                foreach (FlashPhotoInfo info2 in list)
                {
                    if (flag)
                    {
                        flag = false;
                        title = info2.Title;
                        uRL = info2.URL;
                        fileName = info2.FileName;
                    }
                    else
                    {
                        title = title + "|" + info2.Title;
                        uRL = uRL + "|" + info2.URL;
                        fileName = fileName + "|" + info2.FileName;
                    }
                }
                builder.Append("var files='" + fileName + "';\r\n");
                builder.Append("var links='" + uRL + "';\r\n");
                builder.Append("var texts='" + title.Replace("'", "'").Replace("\"", "\\\"") + "';\r\n");
                builder.Append("document.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" width=\"'+ swf_width +'\" height=\"'+ swf_height +'\">');\r\n");
                builder.Append("document.write('<param name=\"movie\" value=\"/Upload/FlashPhotoUpload/picturePlayer.swf\"><param name=\"quality\" value=\"high\">');\r\n");
                builder.Append("document.write('<param name=\"menu\" value=\"false\"><param name=\"wmode\" value=\"opaque\">');\r\n");
                builder.Append("document.write('<param name=\"FlashVars\" value=\"bcastr_file='+files+'&bcastr_link='+links+'&bcastr_title='+texts+'\">');\r\n");
                builder.Append("document.write('<embed src=\"/Upload/FlashPhotoUpload/picturePlayer.swf\" wmode=\"opaque\" FlashVars=\"bcastr_file='+files+'&bcastr_link='+links+'&bcastr_title='+texts+' menu=\"false\" quality=\"high\" width=\"'+ swf_width +'\" height=\"'+ swf_height +'\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />');\r\n");
                builder.Append("document.write('</object>'); \r\n");
                using (StreamWriter writer = new StreamWriter(ServerHelper.MapPath(flashFile), false, Encoding.UTF8))
                {
                    writer.Write(builder.ToString());
                }
            }
        }

        public static void UpdateFlash(FlashInfo flash)
        {
            dal.UpdateFlash(flash);
            RebuildFile(flash.ID);
        }
    }
}

