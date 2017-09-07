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
            var text = db.Texts.First(t => t.Id == 1);
            var banner = db.Texts.First(t => t.Id == 11);
            var provozniDoba = db.Texts.First(t => t.Id == 4);
            var kontakty = db.Texts.First(t => t.Id == 7);
            var firstStop = db.Texts.First(t => t.Id == 8);
            var footer = db.Texts.First(t => t.Id == 13);

            ViewBag.Title = text.Title;
            ViewBag.Uvod = text.Content;
            ViewBag.UvodH1 = text.Title;
            ViewBag.Banner = banner.Content;
            ViewBag.ProvozniDoba = provozniDoba.Content;
            ViewBag.Kontakty = kontakty.Content;
            ViewBag.FirstStop = firstStop.Content;
            ViewBag.Footer = footer.Content;

            return View();
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}