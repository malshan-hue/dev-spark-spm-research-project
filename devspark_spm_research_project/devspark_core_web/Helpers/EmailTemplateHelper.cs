using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace devspark_core_web.Helpers
{
    public static class EmailTemplateHelper
    {
        public static async Task<string> GenerateEmailBody<TModel>(Controller controller, string templateName, TModel model)
        {
            var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var tempData = controller.TempData;

            var viewPath = $"/Views/EmailTemplates/{templateName}.cshtml";
            var actionContext = new ActionContext(controller.HttpContext, controller.RouteData, controller.ControllerContext.ActionDescriptor);

            var viewData = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), actionContext.ModelState)
            {
                Model = model
            };

            using (var sw = new StringWriter())
            {
                var viewResult = viewEngine.GetView(viewPath, viewPath, false);

                if (!viewResult.Success)
                {
                    throw new FileNotFoundException($"The view '{viewPath}' was not found.");
                }

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewData,
                    tempData,
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
