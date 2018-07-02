namespace SocoShop.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.UI.WebControls;

    public sealed class ShopCommon
    {
        public static void BindYearMonth(DropDownList Year, DropDownList Month)
        {
            int num;
            Year.Items.Insert(0, new ListItem("请选择", ""));
            for (num = ShopConfig.ReadConfigInfo().StartYear; num <= ShopConfig.ReadConfigInfo().EndYear; num++)
            {
                Year.Items.Add(new ListItem(num.ToString(), num.ToString()));
            }
            Month.Items.Insert(0, new ListItem("请选择", ""));
            for (num = 1; num <= 12; num++)
            {
                Month.Items.Add(new ListItem(num.ToString(), num.ToString()));
            }
        }

        public static int CountMonthDays(int year, int month)
        {
            return Convert.ToDateTime(string.Concat(new object[] { year, "-", month, "-01" })).AddMonths(1).AddDays(-1.0).Day;
        }

        public static string CreateCouponNo(int couponID, int i)
        {
            string str = string.Empty;
            str = "000" + couponID.ToString();
            str = str.Substring(str.Length - 3);
            string str2 = string.Empty;
            str2 = "00000" + i.ToString();
            str2 = str2.Substring(str2.Length - 5);
            Random random = new Random(i);
            return (str + str2 + random.Next(0x3e8, 0x270f).ToString());
        }

        public static string CreateCouponPassword(int i)
        {
            Random random = new Random(i);
            return random.Next(0x186a0, 0xf423f).ToString();
        }

        public static string CreateOrderNumber()
        {
            Random random = new Random();
            return (DateTime.Now.ToString("yyMMddhhmmss") + random.Next(10, 0x63).ToString());
        }

        public static string GetAdFile(string strID)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (strID != string.Empty)
            {
                foreach (string str3 in strID.Split(new char[] { ',' }))
                {
                    str = "000000" + str3;
                    str2 = str2 + "/Upload/AdUpload/" + str.Substring(str.Length - 6) + ".js,";
                }
                if (str2 != string.Empty) str2 = str2.Substring(0, str2.Length - 1);
            }
            return str2;
        }

        public static string GetBoolString(object oj)
        {
            string str = string.Empty;
            if (oj == null) return str;
            if (Convert.ToInt32(oj) == 1) return "√";
            return "X";
        }

        public static string GetBoolText(int i)
        {
            if (i == 1) return "是";
            return "否";
        }

        public static string GetFlashFile(string strID)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (strID != string.Empty)
            {
                foreach (string str3 in strID.Split(new char[] { ',' }))
                {
                    str = "000000" + str3;
                    str2 = str2 + "/Upload/FlashPhotoUpload/" + str.Substring(str.Length - 6) + ".js,";
                }
                if (str2 != string.Empty) str2 = str2.Substring(0, str2.Length - 1);
            }
            return str2;
        }

        public static string GetVoteString(object oj)
        {
            string str = string.Empty;
            if (oj == null) return str;
            int num = 1;
            if (oj.ToString() == num.ToString()) return "单选";
            return "多选";
        }

        public static string ReadDirectory(string fullName, string name, string path)
        {
            string str = string.Empty;
            if (fullName != string.Empty && fullName.IndexOf(path) > -1) str = fullName.Substring(fullName.IndexOf(path) - 1).Replace(name, string.Empty).Replace(@"\", "/").Replace(path + "/", string.Empty);
            return str;
        }

        public static string ReadFileIcon(FileInfo file)
        {
            return ("/Admin/Style/File/" + file.Extension.Substring(1) + ".gif");
        }

        public static string ReadFileName(string fullName)
        {
            string str = string.Empty;
            if (fullName != string.Empty && fullName.IndexOf("Plugins") > -1) str = fullName.Substring(fullName.IndexOf("Plugins") - 1).Replace(@"\", "/");
            return str;
        }

        public static F ReadValue<T, F>(Dictionary<T, F> dic, T key) where F: new()
        {
            F local = new F();
            if (dic.ContainsKey(key)) local = dic[key];
            return local;
        }

        public static DateTime SearchEndDate(DateTime dt)
        {
            if (dt != DateTime.MinValue) dt = dt.AddDays(1.0);
            return dt;
        }
    }
}

