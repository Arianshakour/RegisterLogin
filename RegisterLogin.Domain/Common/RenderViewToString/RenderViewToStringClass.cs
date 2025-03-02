using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterLogin.Domain.Common.RenderViewToString
{
    public static class RenderViewToStringClass
    {
        public static async Task<string> RenderViewToStringAsync(this Controller controller, string viewName, object model)
        {
            var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var tempDataProvider = controller.HttpContext.RequestServices.GetService(typeof(ITempDataProvider)) as ITempDataProvider;
            var viewData = new ViewDataDictionary(controller.ViewData)
            {
                Model = model
            };

            using (var sw = new StringWriter())
            {
                var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                if (!viewResult.Success)
                {
                    return null;
                }

                var viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    viewData,
                    new TempDataDictionary(controller.HttpContext, tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                // رندر کردن ویو به رشته
                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
