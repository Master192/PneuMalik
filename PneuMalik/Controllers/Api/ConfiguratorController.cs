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
                    var dimension = tyre.Tyre.Substring(tyre.Tyre.IndexOf('R') + 1, 2);

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

            var result = new List<Product>();

            foreach (var disk in disks)
            {
                if (!disk.TyreLines.Any(t => t.Tyre.IndexOf($"R{size}") > -1))
                    continue;

                if (result.Any(p => p.Code.ToString() == disk.Article))
                    continue;

                try
                {
                    if (!db.Products.Any(p => p.Code == Int32.Parse(disk.Article)))
                        continue;
                }
                catch { continue; }

                var product = db.Products.FirstOrDefault(p => p.Code == Int32.Parse(disk.Article));
                product.Description = disk.ImageOnCar;
                result.Add(product);
            }

            return Ok(result);
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}
