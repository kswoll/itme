using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace ItMe.Utils
{
    public class RazorViewToStringRenderer : IRazorViewToStringRenderer
    {
        private readonly IRazorViewEngine viewEngine;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly IRazorPageActivator activator;
        private readonly IHttpContextAccessor httpContext;
        private readonly IActionContextAccessor actionContext;

        public RazorViewToStringRenderer(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IRazorPageActivator activator, IHttpContextAccessor httpContext, IActionContextAccessor actionContext)
        {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
            this.activator = activator;
            this.httpContext = httpContext;
            this.actionContext = actionContext;
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var httpContext = this.httpContext.HttpContext;
            var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(), this.actionContext.ActionContext.ActionDescriptor);

            var page = (Page)FindPage(actionContext, viewName);

            var view = new RazorView(viewEngine,
                activator,
                new List<IRazorPage>(),
                page,
                HtmlEncoder.Default,
                new DiagnosticListener("ViewRenderService"));

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(metadataProvider: new EmptyModelMetadataProvider(), modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                page.PageContext = new PageContext
                {
                    ViewData = viewContext.ViewData
                };

                page.ViewContext = viewContext;

                activator.Activate(page, viewContext);

                await page.ExecuteAsync();

                return output.ToString();
            }
        }

        private IRazorPage FindPage(ActionContext actionContext, string pageName)
        {
            var findPageResult = viewEngine.FindPage(actionContext, pageName);
            if (findPageResult.Page != null)
            {
                return findPageResult.Page;
            }

            var errorMessage = $"Unable to find page '{pageName}'.";
            throw new InvalidOperationException(errorMessage);
        }
    }
}
