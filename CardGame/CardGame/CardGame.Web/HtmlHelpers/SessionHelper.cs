using System;
using System.Diagnostics;
using System.Web;

namespace CardGame.Web.HtmlHelpers
{
    [DebuggerNonUserCodeAttribute()]
    public static class SessionHelper
    {
        #region SESSIONHELPER- Man hat Nachts ja nichts anderes zu tun
        /// <summary>
        /// this try-catch is done to avoid the issue where the report
        /// session is timing out and breaking the entire  
        /// session on a refresh of the report
        /// </summary>
        public static T Get<T>(string index)
        {
            //            
            if (HttpContext.Current.Session == null)
            {
                var i = HttpContext.Current.Session.Count - 1;

                while (i >= 0)
                {
                    try
                    {
                        var obj = HttpContext.Current.Session[i];
                        if (obj != null && obj.GetType().ToString() == "Microsoft.Reporting.WebForms.ReportHierarchy")
                            HttpContext.Current.Session.RemoveAt(i);
                    }
                    catch (Exception)
                    {
                        HttpContext.Current.Session.RemoveAt(i);
                    }

                    i--;
                }
                if (!HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Equals("~/Home/Default"))
                {
                    HttpContext.Current.Response.Redirect("~/Home/Default");
                }
                throw new System.ComponentModel.DataAnnotations.ValidationException(string.Format("You session has expired or you are currently logged out.", index));
            }

            try
            {
                if (HttpContext.Current.Session.Keys.Count > 0 && !HttpContext.Current.Session.Keys.Equals(index))
                {

                    return (T)HttpContext.Current.Session[index];
                }
                else
                {
                    var i = HttpContext.Current.Session.Count - 1;

                    while (i >= 0)
                    {
                        try
                        {
                            var obj = HttpContext.Current.Session[i];
                            if (obj != null && obj.GetType().ToString() == "Microsoft.Reporting.WebForms.ReportHierarchy")
                                HttpContext.Current.Session.RemoveAt(i);
                        }
                        catch (Exception)
                        {
                            HttpContext.Current.Session.RemoveAt(i);
                        }

                        i--;
                    }
                    if (!HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Equals("~/Home/Default"))
                    {
                        HttpContext.Current.Response.Redirect("~/Home/Default");
                    }
                    throw new System.ComponentModel.DataAnnotations.ValidationException(string.Format("You session does not contain {0} or has expired or you are currently logged out.", index));
                }
            }
            catch (Exception e)
            {
                var i = HttpContext.Current.Session.Count - 1;

                while (i >= 0)
                {
                    try
                    {
                        var obj = HttpContext.Current.Session[i];
                        if (obj != null && obj.GetType().ToString() == "Microsoft.Reporting.WebForms.ReportHierarchy")
                            HttpContext.Current.Session.RemoveAt(i);
                    }
                    catch (Exception)
                    {
                        HttpContext.Current.Session.RemoveAt(i);
                    }

                    i--;
                }
                if (!HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Equals("~/Home/Default"))
                {
                    HttpContext.Current.Response.Redirect("~/Home/Default");
                }
                return default(T);
            }
        }
        #endregion

        #region SET (SESSION)
        /// <summary>
        /// Takes the string index and the T value
        /// to return a Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public static void Set<T>(string index, T value)
        {
            HttpContext.Current.Session[index] = (T)value;
        } 
        #endregion
    }
}


