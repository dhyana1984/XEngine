/**************************************************************************
*
* NAME        : MvcSiteMap.cs
*
* VERSION     : 1.0.0
*
* DATE        : 29-July-2016
*
* DESCRIPTION : mvc站点地图(面包屑)
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        28-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XEngine.Web.Models
{
    /// <summary>
    /// mvc站点地图(面包屑)
    /// </summary>
    public class MvcSiteMap
    {
        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Url")]
        public string Url { get; set; }
        [XmlAttribute(AttributeName = "ParnetID")]
        public int ParnetID { get; set; }

        public MvcSiteMap Parent { get; set; }
    }


}