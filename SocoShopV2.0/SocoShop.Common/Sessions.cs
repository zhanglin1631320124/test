namespace SocoShop.Common
{
    using System;
    using System.Web;

    public sealed class Sessions
    {
        public static int ProductBuyCount
        {
            get
            {
                int num = 0;
                if (HttpContext.Current.Session["ProductBuyCount"] != null) num = Convert.ToInt32(HttpContext.Current.Session["ProductBuyCount"]);
                return num;
            }
            set
            {
                HttpContext.Current.Session["ProductBuyCount"] = value.ToString();
            }
        }

        public static decimal ProductTotalPrice
        {
            get
            {
                decimal num = 0M;
                if (HttpContext.Current.Session["ProductTotalPrice"] != null) num = Convert.ToDecimal(HttpContext.Current.Session["ProductTotalPrice"]);
                return num;
            }
            set
            {
                HttpContext.Current.Session["ProductTotalPrice"] = value.ToString();
            }
        }

        public static decimal ProductTotalWeight
        {
            get
            {
                decimal num = 0M;
                if (HttpContext.Current.Session["ProductTotalWeight"] != null) num = Convert.ToDecimal(HttpContext.Current.Session["ProductTotalWeight"]);
                return num;
            }
            set
            {
                HttpContext.Current.Session["ProductTotalWeight"] = value.ToString();
            }
        }
    }
}

