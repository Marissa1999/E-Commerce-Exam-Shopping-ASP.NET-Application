using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarissaGoncalvesQuestion4.ActionFilters
{
    public class LoginFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Session["user_id"] == null)
            {
                var routeValues = new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                };

                filterContext.Result = new RedirectToRouteResult("Default", routeValues);
            }
        }


    }
}