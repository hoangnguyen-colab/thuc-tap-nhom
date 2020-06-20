using DataAccess.DAO;
using DataAccess.EF;
using cong_nghe_web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace thuc_tap_nhom.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class ProductController : BaseController
    {
        [HttpPost]
        public async Task<JsonResult> DeleteProduct(int id)
        {
            if (await new ProductDAO().LoadByID(id) == null)
            {
                return Json(new { Success = 0}, JsonRequestBehavior.AllowGet);
            }
            if (!(await new ProductDAO().DeleteProduct(id)))
            {
                return Json(new { Success = 0 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ProductDetail(int id)
        {
            ViewBag.Brand = await new CategoryDAO().LoadData();

            var item = await new ProductDAO().LoadByID(id);
            return View(item);
        }

        public async Task<ActionResult> ProductList()
        {
            return View(await new ProductDAO().LoadProduct());
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductPartial(int categoryid)
        {
            return PartialView("CreateProductPartial", await new CategoryDAO().LoadByID(categoryid));
        }

        [HttpPost]
        public async Task<JsonResult> CreateProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var prod = new Product
                {
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    ProductDescription = model.ProductDescription,
                    ProductStock = model.ProductStock,
                    ProductURL = SlugGenerator.SlugGenerator.GenerateSlug(model.ProductName),
                    ProductImage = model.ProductImage,
                    ProductStatus = model.ProductStatus,
                    CategoryID = model.CategoryID,
                    CreatedDate = DateTime.Now
                };
                int result = await new ProductDAO().CreateProduct(prod);
                if (result == 0)
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

                return Json(new { Success = true, id = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}