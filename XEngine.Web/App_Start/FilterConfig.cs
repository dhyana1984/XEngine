using System.Web;
using System.Web.Mvc;
using XEngine.Web.Utility;

namespace XEngine.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //增加角色过滤
            //filters.Add(new CustomAuthorizeAttribute());

        }
    }
}
