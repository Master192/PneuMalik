using PneuMalik.Helpers;
using PneuMalik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    public class KategorieController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Index(string title)
        {

            if (string.IsNullOrEmpty(title))
            {
                return View("~/Views/Eshop/Index.cshtml");
            }

            return View(db.Cathegories.FirstOrDefault(c => c.Url == title));
        }
    }
}