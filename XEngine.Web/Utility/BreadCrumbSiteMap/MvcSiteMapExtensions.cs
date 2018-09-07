using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XEngine.Web.Utility.BreadCrumbSiteMap
{
    /// <summary>
    /// 站点地图扩展
    /// </summary>
    public static class MvcSiteMapExtensions
    {
        public static MvcHtmlString PopulateBreadcrumb(this HtmlHelper helper, string url)
        {
            //url 为绝对路径例如  "/XEngine/home/about"
            return MvcSiteMapHelper.PopulateBreadcrumb(url);
        }
    }
}