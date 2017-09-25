using PneuMalik.Helpers;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using PneuMalik.Models.PneuB2b;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace PneuMalik.Services
{
    public class PneuB2bService
    {

        public PneuB2bService(bool debug)
        {

            _debug = debug;
            _statusFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "/pneub2b/status.txt");
            _dataFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "/pneub2b/data.xml");
            _serviceUrlFull = new Uri("http://www.pneub2b.eu/PartnerCommunication.ashx?cmd=products_list");
            _serviceUrlStock = new Uri("http://www.pneub2b.eu/PartnerCommunication.ashx?cmd=stock_list");
        }

        public string Status()
        {
            return GetStatus();
        }

        public void ImportStock()
        {

            SetStatus("Začal import skladu a cen");

            SetStatus("Začalo stahování datového souboru");

            Download(_serviceUrlStock);

            SetStatus("Začalo zpracování stažených dat");

            if (!CheckDownloadedData())
            {
                return;
            }

            SetStatus("Import skladu a cen byl ukončen");
        }

        public void ImportAll()
        {

            SetStatus("Začal import produktů");

            SetStatus("Začalo stahování datového souboru");

            Download(_serviceUrlFull);

            SetStatus("Začalo zpracování stažených dat");

            if (!CheckDownloadedData())
            {
                return;
            }

            SetStatus($"Zpracování doplňkových informací");

            foreach (var manufacturer in _response.Tyres.GroupBy(m => m.ManufacturerID))
            {

                if (!db.Manufacturers.Any(m => m.Id == manufacturer.Key))
                {

                    db.Manufacturers.Add(new Manufacturer()
                    {
                        Id = manufacturer.Key,
                        Name = _response.Tyres.FirstOrDefault(m => m.ManufacturerID == manufacturer.Key).Manufacturer
                    });
                }
            }

            db.SaveChanges();

            var counter = 0;

            var images = new ImageHelper();

            SetStatus($"Zpracování pneumatik ({_response.Tyres.Count()})");

            var manufacturers = db.Manufacturers.ToList();
            var vehicleTypes = db.VehicleTypes.ToList();
            var seasons = db.Seasons.ToList();

            foreach (var productToUpdate in _response.Tyres)
            {

                if (counter % 20 == 0)
                {

                    SetStatus($"Zpracování pneumatik ({counter}/{_response.Tyres.Count()})");
                }

                if (!db.Products.Any(p => p.Code == productToUpdate.Id))
                {

                    db.Products.Add(new Product(productToUpdate,
                        manufacturers.FirstOrDefault(m => m.Id == productToUpdate.ManufacturerID),
                        vehicleTypes.FirstOrDefault(v => v.Title == productToUpdate.VehicleType),
                        seasons.FirstOrDefault(s => s.Id == (productToUpdate.Usage == "Summer" ? 3 : productToUpdate.Usage == "Winter" ? 2 : 4))));
                }

                // obrázek
                try
                {
                    images.Save(productToUpdate.ImageUrl, productToUpdate.Id);
                }
                catch { }

                counter++;
            }

            db.SaveChanges();

            SetStatus($"Zpracování cen");

            foreach (var product in db.Products.Where(p => p.Active).ToList())
            {

                db.Prices.RemoveRange(db.Prices.Where(p => p.ProductId == product.Id).ToList());
                db.SaveChanges();

                var tyre = _response.Tyres.FirstOrDefault(t => t.Id == product.Code);
                db.Prices.Add(new PriceObject()
                {
                    ProductId = product.Id,
                    Price = tyre.StockPriceInfo.TotalPriceCZK,
                    Stock = Convert.ToInt32(tyre.StockPriceInfo.StockAmount),
                    DeliveryTime = tyre.StockPriceInfo.DeliveryTime
                });
                if (tyre.StockPriceInfo_48 != null)
                {

                    db.Prices.Add(new PriceObject()
                    {
                        ProductId = product.Id,
                        Price = tyre.StockPriceInfo_48.TotalPriceCZK,
                        Stock = Convert.ToInt32(tyre.StockPriceInfo_48.StockAmount),
                        DeliveryTime = tyre.StockPriceInfo_48.DeliveryTime
                    });
                }
                db.SaveChanges();
            }

            // Remove inactive
            SetStatus($"Deaktivace odstraněných položek");
            foreach (var product in db.Products.Where(p => p.Active).ToList())
            {

                if (_response.Tyres.Any(t => t.Id == product.Code))
                {

                    continue;
                }

                product.Active = true;
                db.Entry(product).State = EntityState.Modified;
            }
            db.SaveChanges();

            SetStatus("Import produktů byl ukončen");
        }

        private string GetStatus()
        {

            return File.ReadAllText(_statusFilePath);
        }

        private void SetStatus(string message)
        {

            File.WriteAllText(_statusFilePath, message);
        }

        private void Download(Uri serviceUrl)
        {

            using (var client = new WebClient())
            {

                client.Credentials = _debug ? _debugCredentials : _credentials;
                client.DownloadFile(serviceUrl, _dataFilePath);
            }
        }

        private bool CheckDownloadedData()
        {

            if (File.Exists(_dataFilePath))
            {

                var serializer = new XmlSerializer(typeof(Response));

                using (var reader = new StreamReader(_dataFilePath))
                {
                    _response = (Response)serializer.Deserialize(reader);

                    if (_response != null)
                    {

                        if (_response.State != "Error")
                        {

                            return true;
                        }
                        else
                        {
                            SetStatus($"Chyba: {_response.Message}");
                            return false;
                        }
                    }
                    else
                    {
                        SetStatus("Datový soubor nemá správný formát");
                        return false;
                    }
                }
            }
            else
            {
                SetStatus("Datový soubor nebyl stažen (neznámá chyba)");
                return false;
            }
        }

        private readonly bool _debug;
        private readonly string _statusFilePath;
        private readonly string _dataFilePath;
        private readonly Uri _serviceUrlStock;
        private readonly Uri _serviceUrlFull;
        private readonly NetworkCredential _debugCredentials = new NetworkCredential("0", "PartnerPa$$w0Rd");
        private readonly NetworkCredential _credentials = new NetworkCredential("2976", "n1p2bc");

        private Response _response;
        private ApplicationDbContext db = new ApplicationDbContext();
    }
}