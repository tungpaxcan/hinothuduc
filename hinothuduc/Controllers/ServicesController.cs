using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Controllers
{
    public class ServicesController : Controller
    {
        private hinothuducEntities db = new hinothuducEntities();
        // GET: Services
        public ActionResult Index()
        {
            return View(db.Services.Where(x => x.Id>0).ToList());
        }
        public ActionResult Detail(string meta,int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }
        public ActionResult TraGop()
        {
            return View();
        }
        public ActionResult DICHVUCUUHO()
        {
            return View();
        }
        [HttpGet]
        public JsonResult AllService()
        {
            try
            {
                var a = (from b in db.Services.Where(x => x.Id>0)
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