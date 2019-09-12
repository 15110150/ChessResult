using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOM.Web.Utilities.App
{
    public static class HttpSession
    {
        public const string UserLoginKey = "UserLoginKey";

        public static void SetSession(object value)
        {
            HttpContext.Current.Session[UserLoginKey] = value;
        }

        public static T GetFromSession<T>()
        {
            return (T)HttpContext.Current.Session[UserLoginKey];
        }
    }
}