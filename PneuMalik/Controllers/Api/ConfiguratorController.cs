using PneuMalik.Services;
using PneuMalik.Models.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System.IO;

namespace PneuMalik.Controllers.Api
{
    public class ConfiguratorController : ApiController
    {

        [HttpGet]
        [Route("brands")]
        public IHttpActionResult Brands()
        {

            var configurator = new ConfiguratorService();
            var brands = configurator.GetItemsFromXml<ACMBrandData_A2>("ACMBrand_A2").DataLines;

            brands.Insert(0, new DataLine() { BrandCode = "0", BrandText = "Vyberte ze seznamu" });

            return Ok(brands);
        }

        [HttpGet]
        [Route("types")]
        public IHttpActionResult Types(string brand)
        {

            var configurator = new ConfiguratorService();
            configurator.XmlTagAdd("BrandCode", brand);
            var types = configurator.GetItemsFromXml<ACMTypeData_A2>("ACMType_A2").DataLines;

            types.Insert(0, new DataLine2() { TypeCode = "0", TypeText = "Vyberte ze seznamu" });

            return Ok(types);
        }

        [HttpGet]
        [Route("models")]
        public IHttpActionResult Models(string brand, string type)
        {

            var configurator = new ConfiguratorService();
            configurator.XmlTagAdd("BrandCode", brand);
            configurator.XmlTagAdd("TypeCode", type);
            var models = configurator.GetItemsFromXml<ACMModelData_A2>("ACMModel_A2").DataLines;

            models.Insert(0, new DataLine3() { ModelCode = "0", ModelText = "Vyberte ze seznamu" });

            return Ok(models);
        }

        [HttpGet]
        [Route("sizes")]
        public IHttpActionResult Sizes(string model)
        {

            var configurator = new ConfiguratorService();
            configurator.XmlTagAdd("ModelCode", model);
            var sizes = configurator.GetItemsFromXml<ACMWheelData_A2>("ACMWheel_A2").DataLines;

            int[,] availableDimensions = { { 10, 0 }, { 11, 0 }, { 12, 0 }, { 13, 0 }, { 14, 0 }, { 15, 0 }, 
                { 16, 0 }, { 17, 0 }, { 18, 0 }, { 19, 0 }, { 20, 0 }, { 21, 0 }, { 22, 0 }, { 23, 0 }, 
                { 24, 0 }, { 25, 0 }, { 26, 0 }, { 27, 0 }, { 28, 0 }, { 29, 0 }, { 30, 0 },};

            // vyhledání možných rozměrů
            foreach (var size in sizes)
            {
                foreach (var tyre in size.TyreLines)
                {
                    var dimension = tyre.Tyre.IndexOf('R') > -1 ? tyre.Tyre.Substring(tyre.Tyre.IndexOf('R') + 1, 2) : "";

                    try
                    {
                        int ad = Int32.Parse(dimension);
                        for (int j = 0; j < availableDimensions.Length / 2; j++)
                        {
                            if (availableDimensions[j, 0] == ad) availableDimensions[j, 1] = 1;
                        }
                    }
                    catch { }
                }
            }

            var result = new List<string>();
            result.Add("Vyberte ze seznamu");

            for (int i = 0; i < availableDimensions.Length / 2; i++)
            {
                if (availableDimensions[i, 1] == 1)
                    result.Add(availableDimensions[i, 0].ToString());
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("disks")]
        public IHttpActionResult Disks(string model, string size)
        {

            var configurator = new ConfiguratorService();
            configurator.XmlTagAdd("ModelCode", model);
            var disks = configurator.GetItemsFromXml<ACMWheelData_A2>("ACMWheel_A2").DataLines;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var result = new List<DiskCar>();

            // download images if necessary
            foreach (var disk in disks)
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "alcar", model, $"{disk.Article}.jpg");

                var dbDisk = db.Disks.FirstOrDefault(d => d.Article == disk.Article);

                if (dbDisk == null)
                {
                    continue;
                }

                result.Add(new DiskCar() { DataLine = disk, Disk = dbDisk });

                if (!System.IO.File.Exists(path))
                {
                    var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "alcar", model);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    using (var client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFile(new Uri(disk.ImageOnCar.Replace("viewerhtml.php", "viewerimagev2.php")), path);
                        }
                        catch (Exception e)
                        {
                            throw new Exception($"Error download image {disk.ImageOnCarBinary} to location {path} because: {e.Message}.");
                        }
                    }
                }
            }

            return Ok(result);

            /*var result = new List<Product>();

            foreach (var disk in disks)
            {
                if (!disk.TyreLines.Any(t => t.Tyre.IndexOf($"R{size}") > -1))
                {
                    continue;
                }

                if (result.Any(p => p.Ean == disk.Article))
                {
                    continue;
                }

                if (!db.Products.Any(p => p.Code2 == disk.Article))
                {
                    int code = 1;
                    if (db.Products.Any())
                    {
                        code += db.Products.Max(p => p.Code);
                    }

                    db.Products.Add(new Product()
                    {
                        Dph = 21,
                        Code = code,
                        Code2 = disk.Article,
                        Active = true,
                        Name = disk.Article,
                        Price = 1,
                        Tip = false,
                        Action = false,
                        PriceCommon = 1,
                        Sale = 0,
                        Source = Product.DataSource.Alcar
                    });
                }

                var product = db.Products.FirstOrDefault(p => p.Code2 == disk.Article);
                product.Description = disk.ImageOnCar;
                result.Add(product);
            }

            return Ok(result);*/
        }

        [HttpGet]
        [Route("updatedata")]
        public IHttpActionResult UpdateData(string category)
        {
            var configurator = new ConfiguratorService();
            configurator.XmlTagAdd("Category", category);
            var details = configurator.GetItemsFromXml<ArticleData_A2>("Article_A2").DataLines;

            var counter = 0;

            foreach (var detail in details)
            {
                if (!db.Disks.Any(d => d.Article == detail.Article))
                {
                    db.Disks.Add(new Disk(category, detail));
                    counter++;
                }
            }

            db.SaveChanges();

            return Ok(counter);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}
