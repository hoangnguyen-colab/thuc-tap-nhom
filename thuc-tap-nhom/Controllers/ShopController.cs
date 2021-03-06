﻿using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace thuc_tap_nhom.Controllers
{
    public class ShopController : Controller
    {
        [HttpGet]
        [Route("shop")]
        public ActionResult Shop()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ShopPartial()
        {
            var list = await new ProductDAO().LoadProduct();
            return PartialView("ShopPartial", list);
        }

        [HttpGet]
        [Route("thuong-hieu/{url}-{id:int}")]
        public async Task<ActionResult> ShopCategory(int id, string url, string sort)
        {
            var brand = await new CategoryDAO().LoadByID(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand = brand;
            ViewBag.sort = sort;
            return View();
        }

        [HttpGet]
        [Route("san-pham/{url}-{id:int}")]
        public async Task<ActionResult> Detail(int id, string url)
        {
            try
            {
                var prod = await new ProductDAO().LoadByID(id);
                if (prod == null)
                {
                    return HttpNotFound();
                }
                return View(prod);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetProductName(string prefix)
        {
            return Json(new { name = await new ProductDAO().LoadName(prefix) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SelectTop(string cond)
        {
            int top = 8;
            var list = await new ProductDAO().SelectCondition(cond, top);
            return PartialView("SelectTop", list);
        }
    }
}