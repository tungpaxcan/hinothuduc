using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Controllers
{
    public class ProductsController : Controller
    {
        private hinothuducEntities db = new hinothuducEntities();
        public ActionResult Index(string meta, int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);

        }
        public ActionResult Seach(string meta)
        {
            var seach = (String)Session["seach"];
            return View(db.Products.Where(x => x.Name.ToLower().Contains(seach)).ToList());
        }
        // GET: Products
        [HttpGet]
        public JsonResult Product(int id)
        {
            try
            {
                
                var a = (from b in db.Products.Where(x => x.IdCateProcduct == id)
                         orderby b.ModifyDate descending
                         select new
                         {
                             id=b.Id,
                             image=b.Image,
                             name = b.Name,
                             meta = b.Meta
                         }).ToList().Take(4);
                
                return Json(new { code = 200, a = a }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Seachs(string seach)
        {
            try
            {
                Session["seach"] = seach;
                var a = (from b in db.Products.Where(x => x.Id> 0)
                         orderby b.ModifyDate descending
                         select new
                         {
                             id = b.Id,
                             image = b.Image,
                             name = b.Name,
                             meta = b.Meta
                         }).ToList().Where(x=>x.name.ToLower().Contains(seach));

                return Json(new { code = 200, a = a }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Show(int idcate,int page)
        {
            try
            {
                var pageSize = 10;
                var a = (from b in db.Products.Where(x => x.IdCateProcduct == idcate)
                         orderby b.ModifyDate descending
                         select new
                         {
                             id = b.Id,
                             image = b.Image,
                             name = b.Name,
                             meta = b.Meta
                         }).ToList();
                var pages = a.Count() % pageSize == 0 ? a.Count() / pageSize : a.Count() / pageSize + 1;
                var c = a.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200, a = c, count = a.Count(), pages = pages }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ProductNext(int id)
        {
            try
                {
                var a = (from b in db.Products.Where(x => x.Id == (id+1))
                         orderby b.ModifyDate descending
                         select new
                         {
                             id = b.Id,
                             image = b.Image,
                             name = b.Name,
                             meta = b.Meta
                         }).ToList();
                return Json(new { code = 200, a = a}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ImgAdd(int id)
        {
            try
            {
                var a = (from b in db.ImageProducts.Where(x => x.IdProduct == id)
                         orderby b.ModifyDate descending
                         select new
                         {
                             id = b.Id,
                             image = b.Image,
                         }).ToList().Take(4);
                return Json(new { code = 200, a = a }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SanPhamTuongTu(int idcate,int id)
        {
            try
            {
                var a = (from b in db.Products.Where(x => x.IdCateProcduct == idcate && x.Id!=id)
                         orderby b.ModifyDate descending
                         select new
                         {
                             id = b.Id,
                             meta=b.Meta,
                             name=b.Name,
                             image = b.Image,
                         }).ToList().Take(4);
                return Json(new { code = 200, a = a }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}