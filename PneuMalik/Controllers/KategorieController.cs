﻿using PneuMalik.Helpers;
using PneuMalik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    public class KategorieController : Controller
    {

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Index(string title)
        {

            var banner = db.Texts.FirstOrDefault(t => t.Id == 11);
            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.Banner = banner != null ? banner.Content : string.Empty;
            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            if (string.IsNullOrEmpty(title))
            {
                return View("~/Views/Eshop/Index.cshtml");
            }

            var view = new EshopViewModel(db);
            view.Cathegory = db.Cathegories.FirstOrDefault(c => c.Url == title);

            return View("~/Views/Kategorie/Index.cshtml", view);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}