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
    public class PrislusenstviController : Controller
    {

        [HttpGet]
        public ActionResult Index(string title)
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            if (string.IsNullOrEmpty(title))
            {
                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var cathegory = db.Cathegories.FirstOrDefault(c => c.Url == title);
            var products = db.Products.Where(p => p.Active && p.VehicleType.Id == cathegory.Type).ToList();

            return View("~/Views/Eshop/Prislusenstvi.cshtml", new EshopViewModel(db, cathegory.Type, products));
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}