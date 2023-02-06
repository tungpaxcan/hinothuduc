using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Controllers
{
    public class CateProductsController : Controller
    {
        private hinothuducEntities db = new hinothuducEntities();
        public ActionResult Index(string meta,int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateProduct cateProduct = db.CateProducts.Find(id);
            if (cateProduct == null)
            {
                return HttpNotFound();
            }
            return View(cateProduct);

        }
        // GET: CateProducts
        [HttpGet]
        public JsonResult CateProduct()
        {
            try
            {
                var a = (from b in db.CateProducts.Where(x => x.Id > 0)
                         select new
                         {
                             meta= b.Meta,
                             id=b.Id,
                             name = b.Name
                         }).ToList();
                var c = (from b in db.CateCars.Where(x => x.Id.Length > 0)
                         orderby b.ModifyDate descending
                         select new
                         {
                             meta = b.Meta,
                             id = b.Id,
                             name = b.Name
                         }).ToList();
                return Json(new { code = 200, a = a ,c=c}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult IDCATECAR(string id)
        {
            try
            {
                var a = (from b in db.CateProducts.Where(x => x.Id > 0 && x.idCateCar == id)
                         select new
                         {
                             meta = b.Meta,
                             id = b.Id,
                             name = b.Name
                         }).ToList();
                return Json(new { code = 200, a = a }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Href(int id)
        {
            try
            {
                var a = (from b in db.CateProducts.Where(x => x.Id == id )
                         select new
                         {
                             meta = b.Meta,
                             id = b.Id,
                             name = b.Name
                         }).ToList();
                return Json(new { code = 200, a = a }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}