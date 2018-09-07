/**************************************************************************
*
* NAME        : GetXMLRoles.cs
*
* VERSION     : 1.0.0
*
* DATE        : 28-Apr-2016
*
* DESCRIPTION : 自定义过滤器,配合 ActionRoles.xml使用
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        28-Apr-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace XEngine.Web.Utility
{
    public class GetXMLRoles
    {
        public static string GetActionRoles(string action, string controller)
        {
            XElement rootElement = XElement.Load(HttpContext.Current.Server
                .MapPath("~/Config/") + "ActionRoles.xml");
            XElement controllerElement = FindElementByAttribute(rootElement, "Controller", controller);
            if (controllerElement != null)
            {
                XElement actionElement = FindElementByAttribute(controllerElement, "Action", action);
                if (actionElement != null)
                {
                    return actionElement.Value;
                }
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xElement"></param>
        /// <param name="tagName"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// <?xml version="1.0" encoding="utf-8" ?>
        /// <Roles>
        ///   <Controller name="Home">
        ///     <Action name="Index"></Action>
        ///     <Action name="About">Manager,Admin</Action>
        ///     <Action name="Contact">Admin</Action>  
        ///     </Controller>
        ///  </Roles>
        /// </code>
        /// </example>

        public static XElement FindElementByAttribute(XElement xElement, string tagName, string attribute)
        {
            return xElement.Elements(tagName).FirstOrDefault
                (x => x.Attribute("name").Value.Equals(attribute, StringComparison.OrdinalIgnoreCase));
        }
    }
}