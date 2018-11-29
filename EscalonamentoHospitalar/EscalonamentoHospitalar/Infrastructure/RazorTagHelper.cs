using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscalonamentoHospitalar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EscalonamentoHospitalar.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private readonly int MaxPageLinks = 10;

        public PagingViewModel PageModel { get; set; }

        public string PageAction { get; set; }

        private IUrlHelperFactory urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            var resultDiv = new TagBuilder("div");

            int initialPage = PageModel.CurrentPage - MaxPageLinks / 2;
            if (initialPage < 1) initialPage = 1;

            int finalPage = initialPage + MaxPageLinks - 1;
            if (finalPage > PageModel.NumberPages) finalPage = PageModel.NumberPages;

            for (int p = initialPage; p <= finalPage; p++)
            {
                var link = new TagBuilder("a");

                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = p });
                link.AddCssClass("btn");

                if (p == PageModel.CurrentPage)
                {
                    link.AddCssClass("btn-info");
                }
                else
                {
                    link.AddCssClass("btn-default");
                }

                link.InnerHtml.Append(p.ToString());

                resultDiv.InnerHtml.AppendHtml(link);
            }

            output.Content.AppendHtml(resultDiv.InnerHtml);
        }
    }
}
