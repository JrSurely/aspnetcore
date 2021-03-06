﻿using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.Extensions;

namespace Mozlite.Mvc.TagHelpers
{
    /// <summary>
    /// 下拉框标签。
    /// </summary>
    [HtmlTargetElement("moz:pager-select")]
    public class PagerSelectTagHelper : ViewContextableTagHelperBase
    {
        /// <summary>
        /// 当前分页实例对象。
        /// </summary>
        [HtmlAttributeName("data")]
        public IPageEnumerable Data { get; set; }

        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Data == null)
                Data = ViewContext.ViewData.Model as IPageEnumerable;
            if (Data == null || Data.Pages <= 1)
            {
                output.SuppressOutput();
                return;
            }
            if (!ViewContext.RouteData.Values.TryGetValue("page", out var pageValue))
            {
                string qs = ViewContext.HttpContext.Request.Query["page"];
                pageValue = qs;
            }
            var page = Convert.ToInt32(pageValue);
            if (page < 1) page = 1;
            output.TagName = "select";
            for (var i = 1; i <= Data.Pages; i++)
            {
                var builder = new TagBuilder("option");
                builder.MergeAttribute("value", i.ToString());
                builder.InnerHtml.AppendHtml($"第{i}页");
                if (i == page)
                    builder.MergeAttribute("selected", "selected");
                output.Content.AppendHtml(builder);
            }
        }
    }
}