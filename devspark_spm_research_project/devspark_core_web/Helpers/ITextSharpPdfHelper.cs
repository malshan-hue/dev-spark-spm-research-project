using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.tool.xml;

namespace devspark_core_web.Helpers
{
    public static class ITextSharpPdfHelper
    {
        public static byte[] GeneratePdfFromHtml(string htmlContent)
        {
            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                using (var stringReader = new StringReader(htmlContent))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
                }

                document.Close();
                return memoryStream.ToArray();
            }
        }

        public static async Task<string> RenderViewToStringAsync<TModel>(Controller controller, string templateName, TModel model)
        {
            var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var tempData = controller.TempData;

            var viewPath = $"/Views/PdfTemplates/{templateName}.cshtml";
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
