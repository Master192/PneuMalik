using PneuMalik.Helpers;
using PneuMalik.Models.Dto;
using PneuMalik.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
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

            ViewData["Message"] = "";
            ViewBag.Title = "Aktualizace";

            return View();
        }

        [HttpPost]
        public ActionResult DoActualization(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp", $"{Guid.NewGuid()}.csv");

                file.SaveAs(path);

                var lines = System.IO.File.ReadAllLines(path).ToList();
                var success = 0;

                foreach (var line in lines)
                {

                    var values = line.Split('\t');
                    if (values.Length >= 21)
                    {
                        Product product = db.Products.First(p => p.Code == Convert.ToInt32(values[0]));

                        if (product != null)
                        {

                            try
                            {
                                product.Manufacturer = db.Manufacturers.First(m => m.Name == values[1]);
                                product.Design = values[2];
                                product.Cathegories = db.Cathegories.Where(c => c.Name == values[3]).ToList();
                                product.Width = Int32.Parse(values[4]);
                                product.SerieWidth = Int32.Parse(values[5]);
                                product.Construction = values[6];
                                product.Diameter = Int32.Parse(values[7]);
                                product.Pattern = values[8];
                                product.IndexLi = Int32.Parse(values[9]);
                                product.IndexSi = values[10];
                                product.Sale = Double.Parse(values[11]);
                                product.Dph = Double.Parse(values[12]);
                                product.Season = db.Seasons.First(s => s.Name == values[13]);
                                product.FuelConsumption = values[14];
                                product.Adhesion = values[15];
                                product.NoiseLevelDb = Int32.Parse(values[16]);
                                product.NoiseLevel = Int32.Parse(values[17]);
                                product.EfficiencyCathegory = values[18];
                                product.Standard = values[19];
                                product.Price = Double.Parse(values[20]);
                            }
                            catch
                            {
                                continue;
                            }

                            db.Entry(product).State = EntityState.Modified;
                            db.SaveChanges();
                            success++;
                        }
                    }
                }

                ViewData["Message"] = $"Aktualizováno {success}/{lines.Count} záznamů.";

                return View("Actualization");
            }

            ViewData["Message"] = "Nevybrán žádný soubor, nebo je soubor prázdný.";
            return View("Actualization");
        }

        public ActionResult ImportXml()
        {

            ViewBag.Title = "Import Xml";

            return View();
        }

        [HttpPost]
        public ActionResult DoImportFromFile(HttpPostedFileBase file)
        {
            return View("ImportXml");
        }

        public ActionResult PriceChange()
        {

            ViewBag.Title = "Změna cen";

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = db.Manufacturers.OrderBy(m => m.Name).Select(m => m.Name).ToList(),
                Types = db.VehicleTypes.OrderBy(t => t.Name).Select(t => t.Name).ToList(),
                IndexesSi = db.Products.OrderBy(p => p.IndexSi).Select(p => p.IndexSi).Distinct().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult DoPriceChange()
        {
            return View("PriceChange");
        }

        public ActionResult ImageChange()
        {

            ViewBag.Title = "Změna obrázků";

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = db.Manufacturers.OrderBy(m => m.Name).Select(m => m.Name).ToList(),
                Types = db.VehicleTypes.OrderBy(t => t.Name).Select(t => t.Name).ToList(),
                IndexesSi = db.Products.OrderBy(p => p.IndexSi).Select(p => p.IndexSi).Distinct().ToList()
            };

            return View(model);
        }

        public ActionResult MultiDelete()
        {

            ViewBag.Title = "Hromadné mazání";

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = db.Manufacturers.OrderBy(m => m.Name).Select(m => m.Name).ToList(),
                Types = db.VehicleTypes.OrderBy(t => t.Name).Select(t => t.Name).ToList(),
                IndexesSi = db.Products.OrderBy(p => p.IndexSi).Select(p => p.IndexSi).Distinct().ToList()
            };

            return View(model);
        }

        public ActionResult SaleChange()
        {

            ViewBag.Title = "Změna slev";

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = db.Manufacturers.OrderBy(m => m.Name).Select(m => m.Name).ToList(),
                Types = db.VehicleTypes.OrderBy(t => t.Name).Select(t => t.Name).ToList(),
                IndexesSi = db.Products.OrderBy(p => p.IndexSi).Select(p => p.IndexSi).Distinct().ToList()
            };

            return View(model);
        }

        public ActionResult TextChange()
        {

            ViewBag.Title = "Změna textů";

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = db.Manufacturers.OrderBy(m => m.Name).Select(m => m.Name).ToList(),
                Types = db.VehicleTypes.OrderBy(t => t.Name).Select(t => t.Name).ToList(),
                IndexesSi = db.Products.OrderBy(p => p.IndexSi).Select(p => p.IndexSi).Distinct().ToList()
            };

            return View(model);
        }

        public ActionResult Images()
        {

            ViewBag.Title = "Vložení obrázků";

            return View();
        }

        [HttpPost]
        public ActionResult Images(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images/image/", $"{file.FileName}");

                file.SaveAs(path);
            }

            ViewBag.Title = "Vložení obrázků";
            ViewBag.Uploaded = $"/images/image/{file.FileName}";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}