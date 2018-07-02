namespace SkyCES.EntLib
{
    using System;
    using System.Web;

    public sealed class RequestHelper
    {
        public static T GetForm<T>(string key)
        {
            object typeDefaultValue = new object();
            Type conversionType = typeof(T);
            try
            {
                string str = HttpContext.Current.Request.Form[key];
                if (conversionType.FullName == "System.String" && str == null) str = string.Empty;
                typeDefaultValue = Convert.ChangeType(str, conversionType);
            }
            catch
            {
                typeDefaultValue = GetTypeDefaultValue(conversionType);
            }
            return (T) typeDefaultValue;
        }

        public static string GetIntsForm(string key)
        {
            string str = string.Empty;
            try
            {
                string str2 = HttpContext.Current.Request.Form[key];
                foreach (string str3 in str2.Split(new char[] { ',' }))
                {
                    if (str == string.Empty)
                        str = Convert.ToInt32(str3).ToString();
                    else
                        str = str + "," + Convert.ToInt32(str3).ToString();
                }
            }
            catch
            {
            }
            return str;
        }

        public static string GetIntsQueryString(string key)
        {
            string str = string.Empty;
            try
            {
                string str2 = HttpContext.Current.Request.QueryString[key];
                foreach (string str3 in str2.Split(new char[] { ',' }))
                {
                    if (str == string.Empty)
                        str = Convert.ToInt32(str3).ToString();
                    else
                        str = str + "," + Convert.ToInt32(str3).ToString();
                }
            }
            catch
            {
            }
            return str;
        }

        public static T GetQueryString<T>(string key)
        {
            object typeDefaultValue = new object();
            Type conversionType = typeof(T);
            try
            {
                string str = HttpContext.Current.Request.QueryString[key];
                if (conversionType.FullName == "System.String" && str == null) str = string.Empty;
                typeDefaultValue = Convert.ChangeType(str, conversionType);
            }
            catch
            {
                typeDefaultValue = GetTypeDefaultValue(conversionType);
            }
            return (T) typeDefaultValue;
        }

        private static object GetTypeDefaultValue(Type type)
        {
            object obj2 = new object();
            string fullName = type.FullName;
            if (fullName == null) return obj2;
            if (!(fullName == "System.String"))
            {
                if (fullName != "System.Int32")
                {
                    if (fullName == "System.Decimal") return -79228162514264337593543950335M;
                    if (fullName == "System.Double") return -1.7976931348623157E+308;
                    if (fullName != "System.DateTime") return obj2;
                    return DateTime.MinValue;
                }
            }
            else
                return string.Empty;
            return -2147483648;
        }

        public static DateTime DateNow
        {
            get
            {
                return DateTime.Now;
            }
        }

        public static string RawUrl
        {
            get
            {
                return HttpContext.Current.Request.RawUrl;
            }
        }
    }
}

