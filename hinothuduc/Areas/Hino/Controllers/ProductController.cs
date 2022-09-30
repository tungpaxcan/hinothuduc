using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Areas.Hino.Controllers
{
    public class ProductController : BaseController
    {
        private hinothuducEntities db = new hinothuducEntities();
        // GET: Hino/Product
        public string UploadImage(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var now = DateTime.Now.ToString().Trim();
                    var index1 = now.IndexOf(" ");
                    var sub1 = now.Substring(0, index1);
                    var sub11 = sub1.Replace("/", "");
                    var index2 = now.IndexOf(" ", index1 + 1);
                    var sub2 = now.Substring(index1 + 1);
                    var sub21 = sub2.Replace(":", "");
                    string _FileName = "";
                    int index = file.FileName.IndexOf('.');
                    _FileName = sub11 + sub21 + "Product" + file.FileName;
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));
                    return "/Images/" + _FileName;
                }
            }
            return "";

        }
        public ActionResult Index(int id)
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
        public ActionResult Image(int id)
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
        public ActionResult Adds(int id)
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
        public ActionResult Edits(int id)
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
        [HttpGet]
        public JsonResult List(int pagenum, int page, string seach,int idcate)
        {
            try
            {
                var pageSize = pagenum;
                var a = (from b in db.Products.Where(x => x.IdCateProcduct == idcate && x.Status == true)
                         select new
                         {
                             id = b.Id,
                             namecate = b.CateProduct.Name,
                             name = b.Name,
                             image=  b.Image
                         }).ToList().Where(x => x.name.ToLower().Contains(seach));
                var pages = a.Count() % pageSize == 0 ? a.Count() / pageSize : a.Count() / pageSize + 1;
                var c = a.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var count = a.Count();
                return Json(new { code = 200, c = c, pages = pages, count = count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ImageAdd(int pagenum, int page, string seach, int id)
        {
            try
            {
                var pageSize = pagenum;
                var a = (from b in db.ImageProducts.Where(x => x.IdProduct == id && x.Status == true)
                         select new
                         {
                             id = b.Id,
                             image = b.Image
                         }).ToList().Where(x => x.image.ToLower().Contains(seach));
                var pages = a.Count() % pageSize == 0 ? a.Count() / pageSize : a.Count() / pageSize + 1;
                var c = a.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var count = a.Count();
                return Json(new { code = 200, c = c, pages = pages, count = count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Add(int idcate,string name, string title, string meta, string image, string content)
        {
            try
            {
                var session = (UserAdmin)Session["user"];
                var nameAdmin = session.Name;
                var d = new Product();
                d.IdCateProcduct = idcate;
                d.Name = name;
                d.Title = title;
                d.Meta = meta;
                d.Image = image;
                d.Content = content;
                d.CreateDate = DateTime.Now;
                d.ModifyDate = DateTime.Now;
                d.CreateBy = nameAdmin;
                d.ModifyBy = nameAdmin;
                d.Status = true;
                db.Products.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddImage(int id,string image)
        {
            try
            {
                var session = (UserAdmin)Session["user"];
                var nameAdmin = session.Name;
                var d = new ImageProduct();
                d.IdProduct = id;
                d.Image = image;
                d.CreateDate = DateTime.Now;
                d.ModifyDate = DateTime.Now;
                d.CreateBy = nameAdmin;
                d.ModifyBy = nameAdmin;
                d.Status = true;
                db.ImageProducts.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Edit(int id, string name, string title, string meta, string image, string content)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var session = (UserAdmin)Session["user"];
                var nameAdmin = session.Name;
                var d = db.Products.Find(id);
                d.Name = name;
                d.Title = title;
                d.Meta = meta;
                d.Image = image;
                d.Content = content;
                d.ModifyDate = DateTime.Now;
                d.ModifyBy = nameAdmin;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var d = db.Products.Find(id);
                db.Products.Remove(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteImage(int id)
        {
            try
            {
                var d = db.ImageProducts.Find(id);
                db.ImageProducts.Remove(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Detail(int id)
        {
            try
            {
                var d = db.Products.Find(id);
                var content = d.Content;
                db.SaveChanges();
                return Json(new { code = 200, content= content }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}