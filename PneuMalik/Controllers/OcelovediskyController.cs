using PneuMalik.Helpers;
using PneuMalik.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{

    [LayoutInjecter("_EshopLayout")]
    public class OcelovediskyController : Controller
    {
        // GET: Ocelovedisky
        public ActionResult Index()
        {

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
        public ActionResult Filtrace(string filterRim, string filterBrand, string filterModel)
        {

            if (string.IsNullOrEmpty(filterRim) && string.IsNullOrEmpty(filterBrand)
                && string.IsNullOrEmpty(filterModel))
            {
                return View("~/Views/Eshop/SteelRims.cshtml");
            }

            if (!Int32.TryParse(filterRim, out int diameter))
            {
                diameter = 0;
            }

            if (!Int32.TryParse(filterBrand, out int brand))
            {
                brand = 0;
            }

            if (!Int32.TryParse(filterModel, out int model))
            {
                model = 0;
            }

            var filtered = db.Products
                .Where(p => p.Active && p.VehicleType.Id == SteelDiscVehicleTypeId
                    && (diameter == 0 || p.PbDisc.Rafek.Id == diameter)
                    && (brand == 0 || p.PbDisc.Znacka.Id == brand) 
                    && (model == 0 || p.PbDisc.Model.Id == model))
                .Take(100).ToList();
            
            return View("~/Views/Eshop/SteelRims.cshtml", new EshopViewModel(db, SteelDiscVehicleTypeId, filtered));
        }

        private const int SteelDiscVehicleTypeId = 9;
        private ApplicationDbContext db = new ApplicationDbContext();
    }
}