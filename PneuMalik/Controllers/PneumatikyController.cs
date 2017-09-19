using PneuMalik.Helpers;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{

    [LayoutInjecter("_EshopLayout")]
    public class PneumatikyController : Controller
    {

        public ActionResult Index(string title)
        {

            if (string.IsNullOrEmpty(title))
            {
                return View("~/Views/Eshop/Pneumatiky.cshtml");
            }

            var cathegory = db.Cathegories.FirstOrDefault(c => c.Url == title);

            var model = new EshopViewModel()
            {
                Products = db.Products.Where(p => p.Type == cathegory.Type && p.Active).ToList(),
                Tips = db.Products.Where(p => p.Tip && p.Active).ToList()
            };

            return View(model);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}