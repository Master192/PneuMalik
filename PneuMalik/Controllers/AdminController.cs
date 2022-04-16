using PneuMalik.Helpers;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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
                                //product.Design = values[2];
                                //product.Cathegories = db.Cathegories.Where(c => c.Name == values[3]).ToList();
                                //product.Width = Int32.Parse(values[4]);
                                //product.SerieWidth = Int32.Parse(values[5]);
                                //product.Construction = values[6];
                                //product.Diameter = Int32.Parse(values[7]);
                                //product.Pattern = values[8];
                                //product.IndexLi = Int32.Parse(values[9]);
                                //product.IndexSi = values[10];
                                //product.Sale = Double.Parse(values[11]);
                                //product.Dph = Double.Parse(values[12]);
                                //product.Season = db.Seasons.First(s => s.Name == values[13]);
                                //product.FuelConsumption = values[14];
                                //product.Adhesion = values[15];
                                //product.NoiseLevelDb = Int32.Parse(values[16]);
                                //product.NoiseLevel = Int32.Parse(values[17]);
                                //product.EfficiencyCathegory = values[18];
                                //product.Standard = values[19];
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

            if (file != null)
            {

                string strSaveURL = ConfigurationManager.AppSettings["FileAppUrl"] + "xml\\" + file.FileName;
                file.SaveAs(strSaveURL);

                var errors = new StringBuilder();

                using (var lReader = new XmlTextReader(strSaveURL))
                {
                    string strCode = "";
                    bool blnGetItemCode = false;

                    string strName = "";
                    bool blnGetItemName = false;

                    string strKusu = "";
                    bool blnGetItemKusu = false;

                    string strPrice = "";
                    bool blnGetItemPrice = false;

                    string strSklad = "";
                    bool blnGetItemSklad = false;

                    while (lReader.Read())
                    {
                        //rozhodneme se podle typu uzlu jak zareagujeme
                        if (lReader.NodeType == XmlNodeType.Element && lReader.Name == "item_code") blnGetItemCode = true;
                        if (lReader.NodeType == XmlNodeType.EndElement && lReader.Name == "item_code") blnGetItemCode = false;

                        if (lReader.NodeType == XmlNodeType.Element && lReader.Name == "item_name") blnGetItemName = true;
                        if (lReader.NodeType == XmlNodeType.EndElement && lReader.Name == "item_name") blnGetItemName = false;

                        if (lReader.NodeType == XmlNodeType.Element && lReader.Name == "item_kusu") blnGetItemKusu = true;
                        if (lReader.NodeType == XmlNodeType.EndElement && lReader.Name == "item_kusu") blnGetItemKusu = false;

                        if (lReader.NodeType == XmlNodeType.Element && lReader.Name == "item_price") blnGetItemPrice = true;
                        if (lReader.NodeType == XmlNodeType.EndElement && lReader.Name == "item_price") blnGetItemPrice = false;

                        if (lReader.NodeType == XmlNodeType.Element && lReader.Name == "item_sklad") blnGetItemSklad = true;
                        if (lReader.NodeType == XmlNodeType.EndElement && lReader.Name == "item_sklad") blnGetItemSklad = false;

                        if (blnGetItemCode && lReader.NodeType == XmlNodeType.Text && lReader.HasValue) strCode = lReader.Value;
                        if (blnGetItemName && lReader.NodeType == XmlNodeType.Text && lReader.HasValue) strName = lReader.Value;
                        if (blnGetItemKusu && lReader.NodeType == XmlNodeType.Text && lReader.HasValue) strKusu = lReader.Value;
                        if (blnGetItemPrice && lReader.NodeType == XmlNodeType.Text && lReader.HasValue) strPrice = lReader.Value;
                        if (blnGetItemSklad && lReader.NodeType == XmlNodeType.Text && lReader.HasValue) strSklad = lReader.Value;

                        if (lReader.NodeType == XmlNodeType.EndElement && lReader.Name == "item")
                        {
                            if (strCode != "")
                            {
                                try
                                {

                                    var code = strCode.Substring(1);
                                    var tyre = db.Tyres.FirstOrDefault(p => p.PartNo == code);

                                    if (tyre != null)
                                    {

                                        var price = db.Prices.FirstOrDefault(p => p.ProductId == tyre.Id);
                                        price.Stock = int.Parse(strKusu);
                                        price.Price = double.Parse(strPrice, CultureInfo.InvariantCulture) / 100.0;
                                        price.DeliveryTime = 0;
                                        db.SaveChanges();
                                    }
                                }
                                catch
                                {
                                    errors.Append("<br />Chyba u položky s kódem: " + strCode.Substring(1));
                                }

                                strCode = "";
                                strName = "";
                                strKusu = "";
                                strPrice = "";
                                strSklad = "";
                            }
                        }

                    }

                }

                ViewBag.Info = "Soubor byl úspěšně načten a údaje aktualizovány." + errors;
            }

            return View("ImportXml");
        }

        public ActionResult AlcarDisk()
        {
            ViewBag.Title = "Marže disků Alcar";

            return View(db.DiskSales.ToList());
        }
        [HttpPost]
        public ActionResult NewDiskSale(string newdiskname, double newdisksale)
        {
            if (!db.DiskSales.Any(d => d.Manufacturer == newdiskname))
            {
                db.DiskSales.Add(new DiskItemSale()
                {
                    Manufacturer = newdiskname,
                    Sale = newdisksale
                });
                db.SaveChanges();
            }

            ViewBag.Title = "Marže disků Alcar";

            return View("AlcarDisk", db.DiskSales.ToList());
        }

        [HttpPost]
        public ActionResult UpdateDiskSales()
        {

            ViewBag.Title = "Marže disků Alcar";

            return View("AlcarDisk", db.DiskSales.ToList());
        }

        public ActionResult AlcarUpdate()
        {
            ViewBag.Title = "Sklady Alcar";

            return View(string.Empty);
        }

        [HttpPost]
        public ActionResult AlcarDoUpdate()
        {
            ViewBag.Title = "Sklady Alcar";

            var result = new StringBuilder();

            // download from FTP
            var files = new string[] { "CZ_A_00", "CZ_D3_00", "CZ_R_00", "CZ_S_00", "CZ_T1_00" };
            var targetDir = $"{ConfigurationManager.AppSettings["FileAppUrl"]}alcar\\";
            var client = new WebClient
            {
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AlcarFtpName"], ConfigurationManager.AppSettings["AlcarFtpPass"])
            };
            foreach (var file in files)
            {
                try
                {
                    client.DownloadFile($"ftp://ftp.alcar-wheels.com/{file}.csv", $"{targetDir}{file}.csv");

                    result.AppendLine($"Source file {file}.csv successfuly downloaded.<br>");
                }
                catch (Exception ed)
                {
                    result.AppendLine($"Error downloading file {file}.csv: {ed.Message}<br>");
                }
            }

            // load to sql
            foreach (var file in files)
            {
                var content = System.IO.File.ReadAllLines($"{targetDir}{file}.csv");
                var counter = 0;

                foreach (var line in content)
                {
                    try
                    {
                        var lineItems = line.Split(';');

                        // header rows
                        if (lineItems.Length < 5)
                        {
                            continue;
                        }

                        var price = double.Parse(lineItems[4]);
                        var article = lineItems[0];
                        var qtyLocal = int.Parse(string.IsNullOrEmpty(lineItems[1]) ? "0" : lineItems[1]);
                        var qtyExternal = int.Parse(string.IsNullOrEmpty(lineItems[2]) ? "0" : lineItems[2]);
                        var ean = lineItems[3];

                        counter++;

                        if (db.DiskStocks.Any(s => s.Article == article))
                        {
                            var dbItem = db.DiskStocks.FirstOrDefault(s => s.Article == article);

                            if (dbItem.QtyLocal == qtyLocal && dbItem.QtyExternal == qtyExternal)
                            {
                                continue;
                            }

                            dbItem.Price = price;
                            dbItem.QtyLocal = qtyLocal;
                            dbItem.QtyExternal = qtyExternal;
                            db.Entry(dbItem).State = EntityState.Modified;
                        }
                        else
                        {
                            db.DiskStocks.Add(new DiskStock()
                            {
                                Article = article,
                                Ean = ean,
                                Price = price,
                                QtyExternal = qtyExternal,
                                QtyLocal = qtyLocal
                            });
                        }

                        db.SaveChanges();
                    }
                    catch
                    {
                        continue;
                    }
                }

                result.AppendLine($"Source file {file}.csv successfully processed. Updtated articles: {counter}<br>");
            }

            return View(result.ToString());
        }

        public ActionResult PriceChange()
        {

            ViewBag.Title = "Změna cen";

            var model = new MultipleChangeViewModel()
            {
                Manufacturers = db.Manufacturers.OrderBy(m => m.Name).Select(m => m.Name).ToList(),
                Types = db.VehicleTypes.OrderBy(t => t.Name).Select(t => t.Name).ToList(),
                IndexesSi = db.Products.OrderBy(p => p.Tyre.RychlostniIndex).Select(p => p.Tyre.RychlostniIndex).Distinct().ToList()
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
                IndexesSi = db.Products.OrderBy(p => p.Tyre.RychlostniIndex).Select(p => p.Tyre.RychlostniIndex).Distinct().ToList()
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
                IndexesSi = db.Products.OrderBy(p => p.Tyre.RychlostniIndex).Select(p => p.Tyre.RychlostniIndex).Distinct().ToList()
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
                IndexesSi = db.Products.OrderBy(p => p.Tyre.RychlostniIndex).Select(p => p.Tyre.RychlostniIndex).Distinct().ToList()
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
                IndexesSi = db.Products.OrderBy(p => p.Tyre.RychlostniIndex).Select(p => p.Tyre.RychlostniIndex).Distinct().ToList()
            };

            return View(model);
        }

        public ActionResult Images()
        {

            ViewBag.Title = "Vložení obrázků";

            return View(LoadImages());
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

            return View(LoadImages());
        }

        private List<string> LoadImages()
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images/image/");

            if (Directory.Exists(directory))
            {
                return Directory.GetFiles(directory).Select(f => $"/images/image/{new FileInfo(f).Name}").ToList();
            }

            return new List<string>();
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