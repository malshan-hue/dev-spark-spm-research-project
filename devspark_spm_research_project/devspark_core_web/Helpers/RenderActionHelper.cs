using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace devspark_core_web.Helpers
{
    public static class RenderActionHelper
    {
        public static void RenderAction(this HtmlHelper htmlHelper, string actionName)
        {
            htmlHelper.RenderAction(actionName);
        }

        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, object routeValues)
        {
            htmlHelper.RenderAction(actionName, routeValues);
        }

        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            htmlHelper.RenderAction(actionName, controllerName);
        }

        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
        {
            htmlHelper.RenderAction(actionName, routeValues);
        }

        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            htmlHelper.RenderAction(actionName, controllerName, routeValues);
        }

        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            htmlHelper.RenderAction(actionName, controllerName, routeValues);
        }
    }
}
