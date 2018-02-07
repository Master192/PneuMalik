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

            var model = new EshopViewModel(db, 1)
            {
                ProductDetail = product
            };

            return View("~/Views/Eshop/DetailDisk.cshtml", model);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}