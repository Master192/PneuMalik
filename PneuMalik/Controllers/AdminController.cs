using PneuMalik.Helpers;
using PneuMalik.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    [Authorize]
    [LayoutInjecter("_Layout")]
    public class AdminController : Controller
    {

        public ActionResult Actualization()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoActualization(HttpPostedFileBase file)
        {
            return View("Actualization");
        }

        public ActionResult ImportXml()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoImport(HttpPostedFileBase file)
        {
            return View("ImportXml");
        }

        public ActionResult PriceChange()
        {

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = new List<string>() { "Aeolus", "Achilles" },
                Types = new List<string>() { "4x4", "Moto", "Nákladní", "Osobní", "VAN" },
                IndexesSi = new List<string>() { "A3", "A8", "B" }
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult DoPriceChange(HttpPostedFileBase file)
        {
            return View("PriceChange");
        }

        public ActionResult ImageChange()
        {
            var model = new MultipleChangeViewModel()
            {
                Manufacturers = new List<string>() { "Aeolus", "Achilles" },
                Types = new List<string>() { "4x4", "Moto", "Nákladní", "Osobní", "VAN" },
                IndexesSi = new List<string>() { "A3", "A8", "B" }
            };

            return View(model);
        }

        public ActionResult MultiDelete()
        {
            var model = new MultipleChangeViewModel()
            {
                Manufacturers = new List<string>() { "Aeolus", "Achilles" },
                Types = new List<string>() { "4x4", "Moto", "Nákladní", "Osobní", "VAN" },
                IndexesSi = new List<string>() { "A3", "A8", "B" }
            };

            return View(model);
        }

        public ActionResult SaleChange()
        {

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = new List<string>() { "Aeolus", "Achilles" },
                Types = new List<string>() { "4x4", "Moto", "Nákladní", "Osobní", "VAN" },
                IndexesSi = new List<string>() { "A3", "A8", "B" }
            };

            return View(model);
        }

        public ActionResult TextChange()
        {
            var model = new MultipleChangeViewModel()
            {
                Manufacturers = new List<string>() { "Aeolus", "Achilles" },
                Types = new List<string>() { "4x4", "Moto", "Nákladní", "Osobní", "VAN" },
                IndexesSi = new List<string>() { "A3", "A8", "B" }
            };

            return View(model);
        }
    }
}