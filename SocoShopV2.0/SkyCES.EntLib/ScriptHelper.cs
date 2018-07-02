namespace SkyCES.EntLib
{
    using System;

    public sealed class ScriptHelper
    {
        public static void Alert(string message)
        {
            ResponseHelper.Write("<script language='javascript'>alert('" + message + "');history.back(-1);</script>");
            ResponseHelper.End();
        }

        public static void Alert(string message, string url)
        {
            ResponseHelper.Write("<script language='javascript'>alert('" + message + "');window.location.href='" + url + "';</script>");
            ResponseHelper.End();
        }
    }
}

