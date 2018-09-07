using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XEngine.Web.Models
{
    [XmlRoot("MvcSiteMaps")]
    public class MvcSiteMaps
    {
        [XmlElement("MvcSiteMap")]
        public MvcSiteMap[] Items { get; set; }

    }
}