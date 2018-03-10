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
    public class HlinikovediskyController : Controller
    {

        public ActionResult Index()
        {

            var banner = db.Texts.FirstOrDefault(t => t.Id == 11);
            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            return View();
        }

        [HttpPost]
        [Route("filtrace")]
        public ActionResult Filtrace(string filterManufacturer, string filterWidth, string filterRim)
        {

            if (string.IsNullOrEmpty(filterManufacturer) && string.IsNullOrEmpty(filterWidth)
                && string.IsNullOrEmpty(filterRim))
            {
                return View("~/Views/Eshop/AluRims.cshtml");
            }

            var diameter = 0;
            if (!Int32.TryParse(filterRim, out diameter))
            {
                diameter = 0;
            }

            var manufacturer = 0;
            if (!Int32.TryParse(filterManufacturer, out manufacturer))
            {
                manufacturer = 0;
            }

            var width = 0;
            if (!Int32.TryParse(filterWidth, out width))
            {
                width = 0;
            }

            var filtered = db.Products
                .Where(p => p.Active && p.VehicleType.Id == AluDiscVehicleTypeId
                    && (diameter == 0 || p.AluDisc.Rafek.Id == diameter)
                    && (manufacturer == 0 || p.Manufacturer.Id == manufacturer) 
                    && (width == 0 || p.AluDisc.Sirka.Id == width))
                .Take(100).ToList();

            return View("~/Views/Eshop/AluRims.cshtml", new EshopViewModel(db, AluDiscVehicleTypeId, filtered));
        }

        private const int AluDiscVehicleTypeId = 10;
        private ApplicationDbContext db = new ApplicationDbContext();
    }
}