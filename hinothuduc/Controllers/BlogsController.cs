using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Controllers
{
    public class BlogsController : Controller
    {
        private hinothuducEntities db = new hinothuducEntities();
        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blogs.Where(x => x.Id > 0).ToList());
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Detail(string meta, int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        [HttpGet]
        public JsonResult Blog10()
        {
            try
            {
                var a = (from b in db.Blogs.Where(x => x.Id > 0)
                         orderby b.ModifyDate descending
                         select new
                         {
                             id = b.Id,
                             meta = b.Meta,
                             name = b.Name,
                             image = b.Image,
                             content = b.Content
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