﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hinothuduc.Areas.Hino.Controllers
{
    public class HomeAdminController : BaseController
    {
        // GET: Hino/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}