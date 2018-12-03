using PneuMalik.Helpers;
using PneuMalik.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{

    [LayoutInjecter("_EshopLayout")]
    public class PneumatikyController : Controller
    {

        [HttpGet]
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
                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var cathegory = db.Cathegories.FirstOrDefault(c => c.Url == title);

            return View("~/Views/Eshop/Pneumatiky.cshtml", new EshopViewModel(db, cathegory.Type));
        }

        public ActionResult Detail(int? id, string suffix)
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

            if (!id.HasValue)
            {

                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var product = db.Products.FirstOrDefault(p => p.Id == id.Value);

            if (product == null)
            {

                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            if (product.TyreId.HasValue)
            {
                product.Tyre = db.ProductsTyres.FirstOrDefault(t => t.Id == product.TyreId.Value);
                if (product.Tyre.SirkaId.HasValue)
                {
                    product.Tyre.Sirka = db.ParamSirka.FirstOrDefault(s => s.Id == product.Tyre.SirkaId);
                }
                if (product.Tyre.ProfilId.HasValue)
                {
                    product.Tyre.Profil = db.ParamProfil.FirstOrDefault(s => s.Id == product.Tyre.ProfilId);
                }
                if (product.Tyre.RafekId.HasValue)
                {
                    product.Tyre.Rafek = db.ParamRafek.FirstOrDefault(s => s.Id == product.Tyre.RafekId);
                }
            }

            product.Prices = db.Prices.Where(p => p.ProductId == product.Id).ToList();

            var model = new EshopViewModel(db, 1)
            {
                ProductDetail = product
            };

            return View("~/Views/Eshop/Detail.cshtml", model);
        }

        [HttpPost]
        public ActionResult Index(string filterCathegory, string filterSeason, string filterRim,
            string filterManufacturer, string filterWidth, string filterProfile, string filterSi,
            string filterLi)
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

            if (string.IsNullOrEmpty(filterCathegory) && string.IsNullOrEmpty(filterSeason) 
                && string.IsNullOrEmpty(filterRim) && string.IsNullOrEmpty(filterManufacturer)
                && string.IsNullOrEmpty(filterWidth) && string.IsNullOrEmpty(filterProfile)
                && string.IsNullOrEmpty(filterSi) && string.IsNullOrEmpty(filterLi))
            {
                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            if (!Int32.TryParse(filterCathegory, out int cathegory))
            {
                cathegory = 1;
            }
            if (!Int32.TryParse(filterSeason, out int season))
            {
                season = 0;
            }
            if (!Int32.TryParse(filterManufacturer, out int manufacturer))
            {
                manufacturer = 0;
            }
            if (!Int32.TryParse(filterWidth, out int width))
            {
                width = 0;
            }
            if (!Int32.TryParse(filterProfile, out int highPr))
            {
                highPr = 0;
            }
            if (!Int32.TryParse(filterRim, out int diameter))
            {
                diameter = 0;
            }
            if (!Int32.TryParse(filterSi, out int si))
            {
                si = 0;
            }
            if (!Int32.TryParse(filterLi, out int li))
            {
                li = 0;
            }

            var filtered = db.Products.OrderBy(c => c.Price)
                .Where(p => p.Active && p.VehicleType.Id == cathegory 
                        && (season == 0 || p.Tyre.Sezona == season)
                        && (manufacturer == 0 || p.Manufacturer.Id == manufacturer)
                        && (width == 0 || p.Tyre.Sirka.Id == width)
                        && (highPr == 0 || p.Tyre.Profil.Id == highPr)
                        && (diameter == 0 || p.Tyre.Rafek.Id == diameter)
                        && (si == 0 || p.Tyre.Si.Id == si)
                        && (li == 0 || p.Tyre.Li.Id == li))
                .Take(100).ToList();

            return View("~/Views/Eshop/Pneumatiky.cshtml", new EshopViewModel(db, cathegory, filtered));
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}