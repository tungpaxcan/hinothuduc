﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hinothuduc.Models;

namespace hinothuduc.Areas.Hino.Controllers
{
    public class BannerController : BaseController
    {
        private hinothuducEntities db = new hinothuducEntities();
        // GET: Hino/Banner
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
                    _FileName = sub11 + sub21 + "banner" + file.FileName;
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));
                    return "/Images/" + _FileName;
                }
            }
            return "";

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Adds()
        {
            return View();
        }
        public ActionResult Edits(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }
        [HttpGet]
        public JsonResult List(int pagenum, int page, string seach)
        {
            try
            {
                var pageSize = pagenum;
                var a = (from b in db.Banners.Where(x => x.Id > 0 && x.Status == true)
                         select new
                         {
                             id = b.Id,
                             name=b.Name,
                             image = b.Image,
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
        [HttpPost]
        public JsonResult Add(string name, string image)
        {
            try
            {
                var session = (UserAdmin)Session["user"];
                var nameAdmin = session.Name;
                var d = new Banner();
                d.Name = name;
                d.Image = image;
                d.CreateDate = DateTime.Now;
                d.ModifyDate = DateTime.Now;
                d.CreateBy = nameAdmin;
                d.ModifyBy = nameAdmin;
                d.Status = true;
                db.Banners.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id,string name,string image)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var session = (UserAdmin)Session["user"];
                var nameAdmin = session.Name;
                var d = db.Banners.Find(id);
                d.Name = name;
                d.Image = image;
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
                var d = db.Banners.Find(id);
                db.Banners.Remove(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "Xóa Thất Bại" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}