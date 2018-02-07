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

            var diameter = 0;
            if (!Int32.TryParse(filterRim, out diameter))
            {
                diameter = 0;
            }

            var brand = filterBrand ?? "0";
            var model = filterModel ?? "0";

            var filtered = db.Products
                .Where(p => p.Active && p.VehicleType.Id == 10 && (diameter == 0 || p.Diameter == diameter)
                    && (brand == "0" || p.Model == brand) && (model == "0" || p.Construction == model))
                .Take(100).ToList();
            
            return View("~/Views/Eshop/SteelRims.cshtml", new EshopViewModel(db, SteelDiscVehicleTypeId, filtered));
        }

        private const int SteelDiscVehicleTypeId = 10;
        private ApplicationDbContext db = new ApplicationDbContext();
    }
}