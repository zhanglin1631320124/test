namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using System;
    using System.Reflection;

    public sealed class ConfigHelper
    {
        public static T ReadPropertyFromXml<T>(string fileName) where T: new()
        {
            T local = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();
            using (XmlHelper helper = new XmlHelper(fileName))
            {
                foreach (PropertyInfo info in properties)
                {
                    object obj2 = helper.ReadAttribute("Config/" + info.Name, "Value");
                    if (info.PropertyType == typeof(int))
                        info.SetValue(local, Convert.ToInt32(obj2), null);
                    else if (info.PropertyType == typeof(DateTime))
                        info.SetValue(local, Convert.ToDateTime(obj2), null);
                    else if (info.PropertyType == typeof(decimal))
                        info.SetValue(local, Convert.ToDecimal(obj2), null);
                    else if (info.PropertyType == typeof(double))
                        info.SetValue(local, Convert.ToDouble(obj2), null);
                    else
                        info.SetValue(local, obj2, null);
                }
            }
            return local;
        }

        public static void UpdatePropertyToXml<T>(string fileName, T t)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            using (XmlHelper helper = new XmlHelper(fileName))
            {
                foreach (PropertyInfo info in properties)
                {
                    object obj2 = info.GetValue(t, null);
                    if (obj2 != null) helper.UpdateAttribute("Config/" + info.Name, "Value", obj2.ToString());
                }
                helper.Save();
            }
        }
    }
}

