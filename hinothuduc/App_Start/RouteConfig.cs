﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace hinothuduc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
name: "Contact",
url: "lien-he/{action}/{id}",
defaults: new { controller = "Blogs", action = "Contact", id = UrlParameter.Optional }
);
            routes.MapRoute(
     name: "Blogs",
     url: "tin-tuc/{meta}/{id}",
     defaults: new { controller = "Blogs", action = "Detail", id = UrlParameter.Optional }
 );
            routes.MapRoute(
        name: "Blog",
        url: "tin-tuc/{action}/{id}",
        defaults: new { controller = "Blogs", action = "Index", id = UrlParameter.Optional }
    );
            routes.MapRoute(
          name: "Services",
          url: "dich-vu/{meta}/{id}",
          defaults: new { controller = "Services", action = "Detail", id = UrlParameter.Optional }
      );
            routes.MapRoute(
    name: "dicvucuuho",
    url: "dich-vu-cuu-ho/{meta}/{id}",
    defaults: new { controller = "RescueServices", action = "Detail", id = UrlParameter.Optional }
);
            routes.MapRoute(
         name: "dicvucuuho1",
         url: "dich-vu-cuu-ho/{action}/{id}",
         defaults: new { controller = "RescueServices", action = "Index", id = UrlParameter.Optional }
     );
            routes.MapRoute(
           name: "TraGop",
           url: "ho-tro-tra-gop/{action}/{id}",
           defaults: new { controller = "Services", action = "TraGop", id = UrlParameter.Optional }
       );
            routes.MapRoute(
            name: "Service",
            url: "dich-vu/{action}/{id}",
            defaults: new { controller = "Services", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "products",
             url: "tim-kiem/{meta}/{id}",
             defaults: new { controller = "Products", action = "Seach", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "product",
             url: "san-pham/{meta}/{id}",
             defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "cateproduct",
               url: "danh-muc/{meta}/{id}",
               defaults: new { controller = "CateProducts", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
          name: "CSMX",
          url: "chinh-sach-mua-xe/{action}/{id}",
          defaults: new { controller = "Home", action = "CSMX", id = UrlParameter.Optional }
      );
            routes.MapRoute(
            name: "CSBH",
            url: "chinh-sach-bao-hanh/{action}/{id}",
            defaults: new { controller = "Home", action = "CSBH", id = UrlParameter.Optional }
        );
            routes.MapRoute(
           name: "CSBT",
           url: "chinh-sach-bao-tri/{action}/{id}",
           defaults: new { controller = "Home", action = "CSBT", id = UrlParameter.Optional }
       );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
