using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Areas.Hino.Controllers
{
    public class DefaultController : Controller
    {
        private hinothuducEntities db = new hinothuducEntities();
        // GET: Hino/Default
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "HomeAdmin");
            }
            return View();
        }
        [HttpPost]
        public JsonResult Login(string user, string pass)
        {
            try
            {
                var acc = db.UserAdmins.SingleOrDefault(x => x.User == user && x.Pass == pass );
                if (acc != null)
                {
                    Session["user"] = acc;
                    return Json(new { code = 100, Url = "/Hino/HomeAdmin/Index/" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { code = 300, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}