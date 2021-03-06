﻿using DataAccess.EF; //test
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace thuc_tap_nhom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public ActionResult About()
        {

            return View();
        }

        [Route("contact")]
        public ActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> NavbarCategory()
        {
            return PartialView("NavbarCategory", await new CategoryDAO().LoadData());
        }
    }
}