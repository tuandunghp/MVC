using MVC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        // các controller khác kế thừa BaseController này để kiểm tra Session UserName
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var session = Session["UserName"];
            var session = (UserLogin)Session[CommonSession.USER_SESSION];
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}