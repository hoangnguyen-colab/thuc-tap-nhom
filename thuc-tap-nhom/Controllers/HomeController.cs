using DataAccess.EF; //test
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace thuc_tap_nhom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var list = new EShopDbContext().Products.ToList(); //test
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}