using DataAccess.DAO;
using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace thuc_tap_nhom.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class CategoryController : BaseController
    {
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                model.CategoryURL = SlugGenerator.SlugGenerator.GenerateSlug(model.CategoryName);
                model.CreatedDate = DateTime.Now;
                int result = await new CategoryDAO().CreateCategory(model);
                return RedirectToAction("CreateCategory");
            }
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> EditCategory(Category model, int id)
        {
            if (ModelState.IsValid)
            {
                model.CategoryURL = SlugGenerator.SlugGenerator.GenerateSlug(model.CategoryName);
                int result = await new CategoryDAO().EditCategory(model, id);
                if (result == 0)
                {
                    return Json(new { Success = false, id }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = true, id }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = false, id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CategoryList()
        {
            return PartialView("CategoryList", await new CategoryDAO().LoadData());
        }

        [HttpPost]
        public async Task<ActionResult> CategoryListPartial()
        {
            return PartialView("CategoryListPartial", await new CategoryDAO().LoadData());
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCategory(int id)
        {
            if (await new CategoryDAO().LoadByID(id) == null)
            {
                return Json(new { Success = 0 }, JsonRequestBehavior.AllowGet);
            }
            if (!(await new CategoryDAO().DeleteCategory(id)))
            {
                return Json(new { Success = 0 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
        }
    }
}