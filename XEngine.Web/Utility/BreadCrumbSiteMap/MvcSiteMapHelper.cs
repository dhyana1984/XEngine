using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using XEngine.Web.Models;

namespace XEngine.Web.Utility.BreadCrumbSiteMap
{

    /// <summary>
    /// 站点地图帮助类
    /// </summary>
    public static class MvcSiteMapHelper
    {        
        #region 获取sitemap配置信息
        private static string SiteMapString = System.Configuration.ConfigurationManager.AppSettings["SiteMapString"] ?? string.Empty;
  
        //获取sitemap的配置信息
        public static IList<MvcSiteMap> GetSiteMapList()
        {
            using (TextReader reader = new StreamReader(HttpContext.Current.Server.MapPath(SiteMapString)))
            {
                var serializer = new XmlSerializer(typeof(MvcSiteMaps));
                var items = (MvcSiteMaps)serializer.Deserialize(reader);
                if (items != null)
                {
                    return items.Items;
                }
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 填充面包屑
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static MvcHtmlString PopulateBreadcrumb(string url)
        {
            StringBuilder str = new StringBuilder();
            List<string> pathList = new List<string>();
            MvcSiteMap current = GetSiteMapList().FirstOrDefault(m=>m.Url==url);

            GetParent(current, pathList);
            pathList.Reverse();

            for (int i = 0; i < pathList.Count; i++)
            {
                if (i == pathList.Count - 1)
                {
                    string s = pathList[i];
                    s = s.Substring(s.IndexOf(">") + 1, s.LastIndexOf("<") - s.IndexOf(">") - 1);
                    str.AppendFormat("<li class='active'>{0}</li>", s);
                }
                else
                {
                    str.AppendFormat("<li>{0}</li>", pathList[i]);
                }
            }

            string result = str.ToString();
            return MvcHtmlString.Create(result);
        }

  
        /// <summary>
        /// 递归找到上一级
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="pathList"></param>
        static void GetParent(MvcSiteMap parent, List<string> pathList)
        {
            if (parent != null)
            {
                pathList.Add(string.Format("<a href={0}>{1}</a>", parent.Url, parent.Name));
                parent.Parent = GetSiteMapList().FirstOrDefault(i => i.ID == parent.ParnetID);
                GetParent(parent.Parent, pathList);
            }
        }
    }
}