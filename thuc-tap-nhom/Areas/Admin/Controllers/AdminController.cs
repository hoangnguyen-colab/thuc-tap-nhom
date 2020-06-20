using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace thuc_tap_nhom.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ValidateAdmin(DataAccess.EF.Admin model)
        {
            if (ModelState.IsValid)
            {
                var result = await new AdminDAO().LoginAsync(model.AdminUsername, model.AdminUsername);
                if (result == true)
                {
                    Session["AdminLogin"] = model;
                    return Json(new { Success = true, Username = model.AdminUsername }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session["AdminLogin"] = null;
            return RedirectToAction("Login", "Admin");
        }
    }
}