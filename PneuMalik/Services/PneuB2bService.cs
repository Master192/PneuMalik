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
            _serviceUrlStock = new Uri("http://www.pneub2b.eu/PartnerCommunication.ashx?cmd=stock_price_list");
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

                SetStatus("Chyba stažených dat");
                return;
            }

            SetStatus($"Načtení zdrojových dat do databáze: Ceny (disky)");

            // prices
            db.PriceInfos.RemoveRange(db.PriceInfos);
            db.SaveChanges();

            foreach (var rim in _response.SteelRims)
            {

                if (rim.StockPriceInfo != null)
                {

                    db.PriceInfos.Add(new PriceInfo(rim.StockPriceInfo)
                    {
                        Period = 24,
                        Type = PriceInfo.PriceInfoType.Rim,
                        ProductId = rim.Id
                    });
                }
            }

            db.SaveChanges();

            var counter = 0;
            SetStatus($"Načtení zdrojových dat do databáze: Ceny (pneumatiky)");

            foreach (var tyre in _response.Tyres)
            {
                if (tyre.StockPriceInfo != null)
                {
                    db.PriceInfos.Add(new PriceInfo(tyre.StockPriceInfo)
                    {
                        Period = 24,
                        Type = PriceInfo.PriceInfoType.Tyre,
                        ProductId = tyre.Id
                    });
                }

                if (tyre.StockPriceInfo_48 != null)
                {
                    db.PriceInfos.Add(new PriceInfo(tyre.StockPriceInfo_48)
                    {
                        Period = 48,
                        Type = PriceInfo.PriceInfoType.Tyre,
                        ProductId = tyre.Id
                    });
                }

                if (counter % 200 == 0)
                {

                    SetStatus($"Načtení zdrojových dat do databáze: Ceny (pneumatiky) ({counter}/{_response.Tyres.Count()})");

                    db.SaveChanges();
                    db.Dispose();
                    db = new ApplicationDbContext();
                    db.Configuration.AutoDetectChangesEnabled = false;
                }

                counter++;
            }

            db.SaveChanges();

            // call stored procedure
            using (var context = new ApplicationDbContext())
            {

                SetStatus($"Zpracovávání načtených dat o skladech a cenách");
                var result = context.Database.SqlQuery<string>("ProcessPneuB2BPrices");
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

            SetStatus($"Načtení zdrojových dat do databáze: Disky");

            // rims
            db.SteelRims.RemoveRange(db.SteelRims);
            db.SaveChanges();

            var counter = 0;
            foreach (var rim in _response.SteelRims)
            {
                db.SteelRims.Add(rim);

                if (counter % 200 == 0)
                {

                    SetStatus($"Načtení zdrojových dat do databáze: Disky ({counter}/{_response.SteelRims.Count()})");

                    db.SaveChanges();
                    db.Dispose();
                    db = new ApplicationDbContext();
                    db.Configuration.AutoDetectChangesEnabled = false;
                }

                counter++;
            }

            db.SaveChanges();

            SetStatus($"Načtení zdrojových dat do databáze: Pneumatiky");

            // tyres
            db.Tyres.RemoveRange(db.Tyres);
            db.SaveChanges();

            counter = 0;
            foreach (var tyre in _response.Tyres)
            {
                db.Tyres.Add(tyre);

                if (counter % 200 == 0)
                {

                    SetStatus($"Načtení zdrojových dat do databáze: Disky ({counter}/{_response.Tyres.Count()})");

                    db.SaveChanges();
                    db.Dispose();
                    db = new ApplicationDbContext();
                    db.Configuration.AutoDetectChangesEnabled = false;
                }

                counter++;
            }

            db.SaveChanges();

            SetStatus($"Načtení zdrojových dat do databáze: Ceny (disky)");

            // prices
            db.PriceInfos.RemoveRange(db.PriceInfos);
            db.SaveChanges();

            foreach (var rim in _response.SteelRims)
            {
                db.PriceInfos.Add(new PriceInfo(rim.StockPriceInfo) {
                        Period = 24,
                        Type = PriceInfo.PriceInfoType.Rim,
                        ProductId = rim.Id
                });
            }

            db.SaveChanges();

            SetStatus($"Načtení zdrojových dat do databáze: Ceny (pneumatiky)");

            foreach (var tyre in _response.Tyres)
            {
                if (tyre.StockPriceInfo != null)
                {
                    db.PriceInfos.Add(new PriceInfo(tyre.StockPriceInfo)
                    {
                        Period = 24,
                        Type = PriceInfo.PriceInfoType.Tyre,
                        ProductId = tyre.Id
                    });
                }

                if (tyre.StockPriceInfo_48 != null)
                {
                    db.PriceInfos.Add(new PriceInfo(tyre.StockPriceInfo_48)
                    {
                        Period = 48,
                        Type = PriceInfo.PriceInfoType.Tyre,
                        ProductId = tyre.Id
                    });
                }
            }

            db.SaveChanges();

            SetStatus($"Zpracování doplňkových informací (výrobci)");

            foreach (var dist in _response.Tyres.GroupBy(m => m.ManufacturerID))
            {
                var manufacturer = _response.Tyres.FirstOrDefault(m => m.ManufacturerID == dist.Key).Manufacturer;

                if (!db.Manufacturers.Any(m => m.Name == manufacturer))
                {

                    db.Manufacturers.Add(new Manufacturer()
                    {
                        Name = manufacturer
                    });
                }
            }

            db.SaveChanges();

            SetStatus($"Zpracování obrázků");

            var images = new ImageHelper();

            var manufacturers = db.Manufacturers.ToList();
            var vehicleTypes = db.VehicleTypes.ToList();
            var seasons = Enum.GetValues(typeof(Season)).Cast<Season>().ToList();

            foreach (var tyreToUpdate in _response.Tyres)
            {
                try
                {
                    images.Save(tyreToUpdate.ImageUrl, tyreToUpdate.Id);
                }
                catch { }
            }

            foreach (var rimToUpdate in _response.SteelRims)
            {
                try
                {
                    images.Save(rimToUpdate.ImageUrl, rimToUpdate.Id);
                }
                catch { }
            }

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