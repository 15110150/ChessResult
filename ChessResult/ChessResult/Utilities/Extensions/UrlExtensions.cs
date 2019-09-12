using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECOM.Web.Utilities.Extensions
{
    public static class UrlExtensions
    {
        public static string Content(this UrlHelper urlHelper, string contentPath, bool toAbsolute = false)
        {
            var path = urlHelper.Content(contentPath);
            var url = new Uri(HttpContext.Current.Request.Url, path);

            return toAbsolute ? url.AbsoluteUri : path;
        }

        public static string GetUserPhotoUrl(string url)
        {
            string result = "~/Content/Images/anonymous.png";
            if (string.IsNullOrEmpty(url) == false)
            {
                var path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/"), url);
                if (File.Exists(path))
                {
                    result = "~/" + url;
                }
            }
            return result;
       
        }

    }
}