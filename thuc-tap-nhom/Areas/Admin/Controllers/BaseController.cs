﻿using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace thuc_tap_nhom.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var session = Session["AdminLogin"] as DataAccess.EF.Admin;
            //if (session == null)
            //{
            //    filterContext.Result = new RedirectToRouteResult(
            //        new RouteValueDictionary(
            //            new { controller = "Admin", action = "Login", Area = "Admin" })
            //        );
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}