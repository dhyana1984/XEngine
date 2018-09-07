using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XEngine.Web.ViewModels
{
    public class MenuViewModel<T>
    {
        public IList<T> MenuItems = new List<T>();
    }
}