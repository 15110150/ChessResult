using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOM.Web.Utilities.App
{
    public static class ThemeUtils
    {
        const string CurrentThemeCookieKey = "theme", DefaultTheme = "Glass";

        static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        static HttpRequest Request
        {
            get { return Context.Request; }
        }

        public static string CurrentTheme
        {
            get
            {
                if (Request.Cookies[CurrentThemeCookieKey] != null)
                    return HttpUtility.UrlDecode(Request.Cookies[CurrentThemeCookieKey].Value);
                return DefaultTheme;
            }
        }
    }
}