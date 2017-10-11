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

            if (string.IsNullOrEmpty(title))
            {
                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var cathegory = db.Cathegories.FirstOrDefault(c => c.Url == title);

            return View("~/Views/Eshop/Pneumatiky.cshtml", new EshopViewModel(db, cathegory.Type));
        }

        public ActionResult Detail(int? id, string suffix)
        {

            if (!id.HasValue)
            {

                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var product = db.Products.FirstOrDefault(p => p.Id == id.Value);

            if (product == null)
            {

                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var model = new ProductDetailViewModel()
            {
                Product = product,
                Tips = db.Products.Where(p => p.Active && p.Tip).ToList(),
                Manufacturers = db.Manufacturers.ToList(),
                VehicleTypes = db.VehicleTypes.ToList(),
                Seasons = db.Seasons.ToList()
            };

            return View("~/Views/Eshop/Detail.cshtml", model);
        }

        [HttpPost]
        public ActionResult Index(string filterCathegory, string filterSeason, string filterRim,
            string filterManufacturer, string filterWidth, string filterProfile)
        {

            if (string.IsNullOrEmpty(filterCathegory) && string.IsNullOrEmpty(filterSeason) 
                && string.IsNullOrEmpty(filterRim) && string.IsNullOrEmpty(filterManufacturer)
                && string.IsNullOrEmpty(filterWidth) && string.IsNullOrEmpty(filterProfile))
            {
                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var cathegory = 1;
            if (!Int32.TryParse(filterCathegory, out cathegory))
            {
                cathegory = 1;
            }
            var season = 0;
            if (!Int32.TryParse(filterSeason, out season))
            {
                season = 0;
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

            var highPr = 0;
            if (!Int32.TryParse(filterProfile, out highPr))
            {
                highPr = 0;
            }

            var diameter = 0;
            if (!Int32.TryParse(filterRim, out diameter))
            {
                diameter = 0;
            }

            var filtered = db.Products
                .Where(p => p.Active && p.VehicleType.Id == cathegory && (season == 0 || p.Season.Id == season)
                        && (manufacturer == 0 || p.Manufacturer.Id == manufacturer)
                        && (width == 0 || p.Width == width)
                        && (highPr == 0 || p.HighPr == highPr)
                        && (diameter == 0 || p.Diameter == diameter))
                .Take(100).ToList();

            return View("~/Views/Eshop/Pneumatiky.cshtml", new EshopViewModel(db, cathegory, filtered));
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}