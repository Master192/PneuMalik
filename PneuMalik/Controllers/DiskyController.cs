using PneuMalik.Helpers;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{

    [LayoutInjecter("_EshopLayout")]
    public class DiskyController : Controller
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

            var view = "~/Views/Eshop/SteelRims.cshtml";
            switch (title)
            {
                case "ocelove-disky":
                    view = "~/Views/Eshop/SteelRims.cshtml";
                    break;
                case "hlinikove-disky":
                    view = "~/Views/Eshop/AluRims.cshtml";
                    break;
            }

            var cathegory = db.Cathegories.FirstOrDefault(c => c.Url == title);

            return View(view, new EshopViewModel(db, cathegory.Type));
        }

        public ActionResult Detail(int? id, string suffix)
        {

            if (!id.HasValue)
            {

                return View("~/Views/Eshop/Disky.cshtml");
            }

            var product = db.Products.FirstOrDefault(p => p.Id == id.Value);

            if (product == null)
            {

                return View("~/Views/Eshop/Disky.cshtml");
            }

            if (product.AluDiscId.HasValue)
            {
                product.AluDisc = db.ProductsAluDisc.FirstOrDefault(d => d.Id == product.AluDiscId.Value);

                product.AluDisc.Rafek = db.ParamRafek.FirstOrDefault(r => r.Id == product.AluDisc.RafekId.Value);
                product.AluDisc.Sirka = db.ParamSirka.FirstOrDefault(s => s.Id == product.AluDisc.SirkaId.Value);
            }

            if (product.PbDiscId.HasValue)
            {
                product.PbDisc = db.ProductsPbDisc.FirstOrDefault(d => d.Id == product.PbDiscId.Value);

                product.PbDisc.Rafek = db.ParamRafek.FirstOrDefault(r => r.Id == product.PbDisc.RafekId.Value);
                product.PbDisc.Znacka = db.ParamZnacka.FirstOrDefault(z => z.Id == product.PbDisc.ZnackaId.Value);
                product.PbDisc.Model = db.ParamModel.FirstOrDefault(m => m.Id == product.PbDisc.ModelId.Value);
            }

            var model = new EshopViewModel(db, 1)
            {
                ProductDetail = product
            };

            return View("~/Views/Eshop/DetailDisk.cshtml", model);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}