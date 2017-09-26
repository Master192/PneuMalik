using PneuMalik.Helpers;
using PneuMalik.Models;
using System.Linq;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    public class EshopController : Controller
    {

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Index()
        {

            var text = db.Texts.FirstOrDefault(t => t.Id == 1);
            var banner = db.Texts.FirstOrDefault(t => t.Id == 11);
            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.Title = text != null ? text.Title : string.Empty;
            ViewBag.Uvod = text != null ? text.Content : string.Empty;
            ViewBag.UvodH1 = text != null ? text.Title : string.Empty;
            ViewBag.Banner = banner != null ? banner.Content : string.Empty;
            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            var model = new EshopViewModel()
            {
                Products = db.Products.Where(p => p.Action && p.Active).ToList(),
                Tips = db.Products.Where(p => p.Tip && p.Active).ToList(),
                Manufacturers = db.Manufacturers.ToList(),
                VehicleTypes = db.VehicleTypes.ToList(),
                Seasons = db.Seasons.ToList()
            };

            return View(model);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}