using System;
using System.Text;
using System.Web.Mvc;
using CardGame.Web.Models;

namespace CardGame.Web.HtmlHelpers
{
    public static class PagingHelpers
    {
        /// <summary>
        /// The PageLinks extension method generates the HTML for a set of pagelinks usin the information prvided in a PagingInfo Object. The GFunc parameter accepts a delegate that it uses to gerate hte links to view other pages
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pagingInfo"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());

            }


        }
    }