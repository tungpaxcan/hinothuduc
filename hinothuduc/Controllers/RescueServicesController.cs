using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Controllers
{
    public class RescueServicesController : Controller
    {
        private hinothuducEntities db = new hinothuducEntities();
        // GET: RescueServices
        public ActionResult Index()
        {
            return View(db.RescueServices.Where(x=>x.Id>0&& x.Status==true));
        }
        public ActionResult Detail(string meta, int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RescueService rescueService = db.RescueServices.Find(id);
            if (rescueService == null)
            {
                return HttpNotFound();
            }
            return View(rescueService);
        }
    }
}