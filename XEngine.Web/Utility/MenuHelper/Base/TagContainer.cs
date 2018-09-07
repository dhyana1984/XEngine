using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XEngine.Web.Utility.MenuHelper.Base
{
    public class TagContainer
    {
        public int OrdinalNum;
        public string Name;
        public TagBuilder Tb;
        public TagContainer ParentContainer;
        public List<TagContainer> ChildrenContainers = new List<TagContainer>();

        public TagContainer(ref int Num, TagContainer parent)
        {
            OrdinalNum = Num++;

            ParentContainer = parent;

            if (parent!=null)
            {
                parent.ChildrenContainers.Add(this);
            }
        }

        public override string ToString()
        {
            string str = OrdinalNum.ToString() + "," + Name + "," + ChildrenContainers.Count.ToString() + "#";

            if (Tb != null)
            {
                str += Tb.ToString();
            }
            return str;
        }

    }
}