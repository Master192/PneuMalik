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

            // default product type
            var cathegory = 1;

            return View("~/Views/Eshop/Index.cshtml", new EshopViewModel(db, cathegory));
        }

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Kosik()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            var customer = new Customer();

            var model = new EshopViewModel(db)
            {
                Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList()
            };

            model.CartProducts = db.CartRows.Join(db.Products, 
                c => c.ProductId, 
                p => p.Id, 
                (cart, product) => new { Cart = cart, Product = product })
                .Where(cp => cp.Cart.CustomerId == customer.Id)
                .Select(cp => cp.Product).ToList();

            return View("~/Views/Eshop/Cart.cshtml", model);
        }

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Konfigurator()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            return View("~/Views/Eshop/Konfigurator.cshtml", new EshopViewModel(db, 1));
        }

        public ActionResult Error()
        {

            return View();
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}