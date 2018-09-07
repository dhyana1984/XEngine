using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.Models;
using XEngine.Web.Utility.MenuHelper.Base;

namespace XEngine.Web.Utility.MenuHelper
{
    public class HtmlBuilder : BaseHtmlTagEngine<SysMenu>
    {

        public HtmlBuilder(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        protected override void BuildTagContainer(SysMenu item, TagContainer parent)
        {
            TagContainer tc = FillTag(item, parent);

            foreach (SysMenu mmi in item.GetChildren())
            {
                BuildTagContainer(mmi, tc);
            }
        }

        /// <summary>
        /// item有子成员返回ul container, 没有子成员返回li container
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tc_parent"></param>
        /// <returns></returns>
        TagContainer FillTag(SysMenu item, TagContainer tc_parent)
        {
            //先把本身的菜单项加上
            #region 先把本身的菜单项加上
            TagContainer li_tc = new TagContainer(ref _CntNumber, tc_parent);
            li_tc.Name = item.Name;
            li_tc.Tb = AddItem(item); //li tag
            #endregion

            if (HasChildren(item))
            {
                TagContainer ui_container = new TagContainer(ref _CntNumber, li_tc);
                ui_container.Name = "**";
                ui_container.Tb = Add_UL_Tag();
                return ui_container;
            }

            return li_tc;
        }

        /// <summary>
        /// 增加一个 li （即一个菜单项）
        /// </summary>
        /// <param name="mi"></param>
        /// <returns>li</returns>
        TagBuilder AddItem(SysMenu mi)
        {
            var li_tag = new TagBuilder("li");
            var a_tag = new TagBuilder("a");
            var b_tag = new TagBuilder("b");
            var image_tag = new TagBuilder("img");

            if (!string.IsNullOrEmpty(mi.IconImage))
            {
                //[todo] 路径需改成可配置
                string path = "/XEngine/Images/" + mi.IconImage;
                image_tag.MergeAttribute("src", path);
                image_tag.AddCssClass("imgMenu");
            }

            b_tag.AddCssClass("caret");

            var contentUrl = GenerateContentUrlFromHttpContext(_htmlHelper);
            string a_href = GenerateUrlForMenuItem(mi, contentUrl);

            a_tag.Attributes.Add("href", a_href);


            a_tag.InnerHtml += image_tag.ToString();
            a_tag.InnerHtml += mi.Name;

            if (mi.MenuType == MenuTypeOption.Top)
            {
                li_tag.AddCssClass("dropdown");
                a_tag.MergeAttribute("data-toggle", "dropdown");
                a_tag.AddCssClass("dropdown-toggle");

                a_tag.InnerHtml +=" "+ b_tag.ToString();
            }
            else
            {
                if (HasChildren(mi))
                {
                    li_tag.AddCssClass("dropdown-submenu");
                }
            }
            li_tag.InnerHtml = a_tag.ToString();
            return li_tag;
        }

        string GenerateContentUrlFromHttpContext(HtmlHelper htmlHelper)
        {
            string contentUrl = UrlHelper.GenerateContentUrl("~/", htmlHelper.ViewContext.HttpContext);
            return contentUrl;
        }

        /// <summary>
        /// 生成菜单地址
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="contentUrl"></param>
        /// <returns></returns>
        string GenerateUrlForMenuItem(SysMenu menuItem, string contentUrl)
        {
            var url = contentUrl + menuItem.Controller;
            if (!string.IsNullOrEmpty(menuItem.Action))
            {
                url += "/" + menuItem.Action;
            }
            return url;
        }

        /// <summary>
        /// 添加一个li的子菜单容器ul
        /// </summary>
        /// <returns></returns>
        TagBuilder Add_UL_Tag()
        {
            TagBuilder ul_tag = new TagBuilder("ul");
            //ul_tag.MergeAttribute("id", "menu1");
            ul_tag.AddCssClass("dropdown-menu");
            //ul_tag.AddCssClass("MenuProps");
            return ul_tag;
        }
    }
}