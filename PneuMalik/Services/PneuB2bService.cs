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
            _logFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "/pneub2b/fullLog.txt");
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

            try
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

                SetStatus($"Příprava dočasných tabulek pro přenos.");

                // tyres
                // call stored procedure - delete already inserted tyres
                db.Database.ExecuteSqlCommand("DELETE FROM Tyres");
                db.Database.ExecuteSqlCommand("DELETE FROM PriceInfoes");
                db.Dispose();
                db = new ApplicationDbContext();
                db.Configuration.AutoDetectChangesEnabled = false;

                SetStatus($"Načtení zdrojových dat do databáze: Pneumatiky");

                counter = 0;
                foreach (var tyre in _response.Tyres)
                {
                    try
                    {
                        db.Tyres.Add(tyre);
                    }
                    catch (Exception e)
                    {
                        SetStatus($"Chyba načítání pneumatiky: ({e.Message})");
                        return;
                    }

                    if (counter % 200 == 0)
                    {

                        SetStatus($"Načtení zdrojových dat do databáze: Pneumatiky ({counter}/{_response.Tyres.Count()})");

                        db.SaveChanges();
                        db.Dispose();
                        db = new ApplicationDbContext();
                        db.Configuration.AutoDetectChangesEnabled = false;
                    }

                    counter++;
                }

                db.SaveChanges();
                db.Dispose();
                db = new ApplicationDbContext();
                db.Configuration.AutoDetectChangesEnabled = false;

                counter = 0;
                foreach (var tyre in _response.Tyres)
                {

                    if (tyre.StockPriceInfo != null)
                    {
                        db.PriceInfos.Add(new PriceInfo(tyre.StockPriceInfo)
                        {
                            Period = 0,
                            Type = PriceInfo.PriceInfoType.Tyre,
                            ProductId = tyre.Id
                        });
                    }

                    if (tyre.StockPriceInfo_24 != null)
                    {
                        db.PriceInfos.Add(new PriceInfo(tyre.StockPriceInfo_24)
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

                        SetStatus($"Načtení zdrojových dat do databáze: Pneumatiky - ceny ({counter}/{_response.Tyres.Count()})");

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
                db.Dispose();
                db = new ApplicationDbContext();
                db.Configuration.AutoDetectChangesEnabled = false;

                foreach (var rim in _response.SteelRims)
                {
                    db.PriceInfos.Add(new PriceInfo(rim.StockPriceInfo)
                    {
                        Period = 24,
                        Type = PriceInfo.PriceInfoType.Rim,
                        ProductId = rim.Id
                    });
                }

                db.SaveChanges();
                db.Dispose();
                db = new ApplicationDbContext();

                // call stored procedure - delete already inserted tyres
                SetStatus($"Příprava na vložení nových pneumatik.");
                db.Database.ExecuteSqlCommand("EXEC [dbo].[ProcessPneuB2BTyres]");

                db.Dispose();
                db = new ApplicationDbContext();
                db.Configuration.AutoDetectChangesEnabled = false;

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

                SetStatus($"Zpracování doplňkových informací (hmotnostní index)");

                // li
                foreach (var dist in _response.Tyres.GroupBy(m => m.LoadIndexFrom))
                {
                    var loadIndex = _response.Tyres.FirstOrDefault(m => m.LoadIndexFrom == dist.Key).LoadIndexFrom;

                    if (!db.ParamLi.Any(m => m.Name == loadIndex))
                    {

                        db.ParamLi.Add(new ProductParamLi()
                        {
                            Name = loadIndex
                        });
                    }
                }

                SetStatus($"Zpracování doplňkových informací (model)");

                // model
                foreach (var dist in _response.Tyres.GroupBy(m => m.ConstructionType))
                {
                    var model = _response.Tyres.FirstOrDefault(m => m.ConstructionType == dist.Key).ConstructionType;

                    if (!db.ParamModel.Any(m => m.Name == model))
                    {

                        db.ParamModel.Add(new ProductParamModel()
                        {
                            Name = model
                        });
                    }
                }

                SetStatus($"Zpracování doplňkových informací (profil)");

                // profil
                foreach (var dist in _response.Tyres.GroupBy(m => m.Profile))
                {
                    var profile = _response.Tyres.FirstOrDefault(m => m.Profile == dist.Key).Profile;

                    if (!db.ParamProfil.Any(m => m.Name == profile.ToString()))
                    {

                        db.ParamProfil.Add(new ProductParamProfil()
                        {
                            Name = profile.ToString()
                        });
                    }
                }

                SetStatus($"Zpracování doplňkových informací (ráfek)");

                // ráfek
                foreach (var dist in _response.Tyres.GroupBy(m => m.Diameter))
                {
                    var rafek = _response.Tyres.FirstOrDefault(m => m.Diameter == dist.Key).Diameter;

                    if (!db.ParamRafek.Any(m => m.Name == rafek.ToString()))
                    {

                        db.ParamRafek.Add(new ProductParamRafek()
                        {
                            Name = rafek.ToString()
                        });
                    }
                }

                SetStatus($"Zpracování doplňkových informací (rychlostní index)");

                // si
                foreach (var dist in _response.Tyres.GroupBy(m => m.SpeedIndex))
                {
                    var si = _response.Tyres.FirstOrDefault(m => m.SpeedIndex == dist.Key).SpeedIndex;

                    if (!db.ParamSi.Any(m => m.Name == si))
                    {

                        db.ParamSi.Add(new ProductParamSi()
                        {
                            Name = si
                        });
                    }
                }

                SetStatus($"Zpracování doplňkových informací (šířka)");

                // sirka
                foreach (var dist in _response.Tyres.GroupBy(m => m.Width))
                {
                    var width = _response.Tyres.FirstOrDefault(m => m.Width == dist.Key).Width;

                    if (!db.ParamSirka.Any(m => m.Name == width.ToString()))
                    {

                        db.ParamSirka.Add(new ProductParamSirka()
                        {
                            Name = width.ToString()
                        });
                    }
                }

                SetStatus($"Zpracování doplňkových informací (značka)");

                // znacka
                foreach (var dist in _response.Tyres.GroupBy(m => m.PatternID))
                {
                    var pattern = _response.Tyres.FirstOrDefault(m => m.PatternID == dist.Key).Pattern;

                    if (!db.ParamZnacka.Any(m => m.Name == pattern))
                    {

                        db.ParamZnacka.Add(new ProductParamZnacka()
                        {
                            Name = pattern
                        });
                    }
                }

                db.SaveChanges();
                db.Dispose();

                SetStatus($"Vkládání nových pneumatik.");

                db = new ApplicationDbContext();
                db.Configuration.AutoDetectChangesEnabled = false;

                foreach (var newTyre in db.Tyres.ToList())
                {

                    try
                    {
                        var manufacturer = db.Manufacturers.FirstOrDefault(m => m.Name == newTyre.Manufacturer);
                        var vehicleType = db.VehicleTypes.FirstOrDefault(v => v.Id == newTyre.VehicleTypeCode);
                        var li = db.ParamLi.FirstOrDefault(v => v.Name == newTyre.LoadIndexFrom);
                        var model = db.ParamModel.FirstOrDefault(v => v.Name == newTyre.ConstructionType);
                        var profil = db.ParamProfil.FirstOrDefault(v => v.Name == newTyre.Profile.ToString());
                        var rafek = db.ParamRafek.FirstOrDefault(v => v.Name == newTyre.Diameter.ToString());
                        var si = db.ParamSi.FirstOrDefault(v => v.Name == newTyre.SpeedIndex);
                        var sirka = db.ParamSirka.FirstOrDefault(v => v.Name == newTyre.Width.ToString());
                        var znacka = db.ParamZnacka.FirstOrDefault(v => v.Name == newTyre.Pattern);

                        db.Products.Add(new Product(newTyre, manufacturer, vehicleType, 
                            SeasonHelper.Parse(newTyre.Usage), li, model, profil, rafek, si, sirka, znacka));
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        SetStatus($"Chyba vkládání. ({e.Message} - {e.InnerException.Message})");
                    }
                }

                db.Dispose();
                db = new ApplicationDbContext();
                db.Configuration.AutoDetectChangesEnabled = false;

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
            catch (Exception e)
            {
                SetStatus($"Globální chyba importu: {e.Message}");
            }
        }

        private string GetStatus()
        {

            return File.ReadAllText(_statusFilePath);
        }

        private void SetStatus(string message)
        {

            File.WriteAllText(_statusFilePath, message);
            File.AppendAllText(_logFilePath, $"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {message}\n");
        }

        private void Download(Uri serviceUrl)
        {

            using (var client = new WebClient())
            {

                var tempFile = _dataFilePath + ".gzip";

                client.Credentials = _debug ? _debugCredentials : _credentials;
                client.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
                client.DownloadFile(serviceUrl, tempFile);

                // rozbalit gzip
                var fileToUnzip = new FileInfo(tempFile);
                ZipHelper.Decompress(fileToUnzip);
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
        private readonly string _logFilePath;
        private readonly string _dataFilePath;
        private readonly Uri _serviceUrlStock;
        private readonly Uri _serviceUrlFull;
        private readonly NetworkCredential _debugCredentials = new NetworkCredential("0", "PartnerPa$$w0Rd");
        private readonly NetworkCredential _credentials = new NetworkCredential("2976", "n1p2bc");

        private Response _response;
        private ApplicationDbContext db = new ApplicationDbContext();
    }
}