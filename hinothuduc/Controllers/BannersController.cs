using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Controllers
{
    public class BannersController : Controller
    {
        private hinothuducEntities db = new hinothuducEntities();
        // GET: Banners
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult banner()
        {
            try
            {
                var a = (from b in db.Banners.Where(x => x.Id > 0)
                         select new
                         {
                             image = b.Image
                         }).ToList();
                return Json(new { code = 200, a=a }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}