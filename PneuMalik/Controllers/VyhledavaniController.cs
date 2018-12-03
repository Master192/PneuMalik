using PneuMalik.Helpers;
using PneuMalik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{

    [LayoutInjecter("_EshopLayout")]
    public class VyhledavaniController : Controller
    {
        
        [HttpGet]
        public ActionResult Index(string s)
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

            var filtered = db.Products.OrderBy(o => o.Price)
                .Where(p => p.Name.Contains(s))
                .Take(100).ToList();

            return View("~/Views/Eshop/Pneumatiky.cshtml", new EshopViewModel(db, 1, filtered));
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}