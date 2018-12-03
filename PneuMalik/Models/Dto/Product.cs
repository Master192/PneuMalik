using PneuMalik.Models.PneuB2b;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Mvc;

namespace PneuMalik.Models.Dto
{
    public class Product
    {

        public Product()
        {
            // empty constructor
        }

        public Product(Response.Tyre tyre, Manufacturer manufacturer, VehicleType vehicleType, Season season,
            ProductParamLi li, ProductParamModel model, ProductParamProfil profil, ProductParamRafek rafek,
            ProductParamSi si, ProductParamSirka sirka, ProductParamZnacka znacka)
        {
            Code = tyre.Id;
            Name = tyre.DisplayName;
            Active = true;
            Ean = tyre.Ean;

            Price = 0;
            if (!string.IsNullOrEmpty(tyre.RetailPrice_CZ))
            {
                Price = Convert.ToDouble(tyre.RetailPrice_CZ, CultureInfo.InvariantCulture);
            }

            Manufacturer = manufacturer;
            VehicleType = vehicleType;

            Tyre = new ProductsTyre()
            {
                Sirka = sirka,
                Rafek = rafek,
                Dezen = tyre.Pattern,
                Li = li,
                PrumerRafku = tyre.Diameter.ToString(),
                Prilnavost = tyre.TagAdhesion,
                Si = si,
                Spotreba = tyre.TagConsumption,
                UrovenHluku = tyre.TagNoiseLevel,
                UrovenHlukudB = tyre.TagNoiseLevel_dB,
                Konstrukce = tyre.ConstructionType,
                Profil = profil,
                Sezona = (int)season
            };

            Prices = new List<PriceObject>();
            if (tyre.StockPriceInfo != null)
            {
                Prices.Add(new PriceObject()
                {
                    DeliveryTime = tyre.StockPriceInfo.DeliveryTime,
                    Id = tyre.Id,
                    Price = tyre.StockPriceInfo.TotalPriceCZK,
                    ProductId = tyre.Id,
                    Stock = Convert.ToInt32(tyre.StockPriceInfo.StockAmount)
                });
            }

            if (tyre.StockPriceInfo_48 != null)
            {
                Prices.Add(new PriceObject()
                {
                    DeliveryTime = tyre.StockPriceInfo_48.DeliveryTime,
                    Id = tyre.Id,
                    Price = tyre.StockPriceInfo_48.TotalPriceCZK,
                    ProductId = tyre.Id,
                    Stock = Convert.ToInt32(tyre.StockPriceInfo_48.StockAmount)
                });
            }
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Code2 { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Tip { get; set; }
        public bool Action { get; set; }
        public string Ean { get; set; }

        public double PriceCommon { get; set; }
        public double Price { get; set; }
        public double Sale { get; set; }
        public double Dph { get; set; }

        public DataSource Source { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public VehicleType VehicleType { get; set; }

        [Column("Tyre_Id")]
        public int? TyreId { get; set; }
        public ProductsTyre Tyre { get; set; }
        [Column("AluDisc_Id")]
        public int? AluDiscId { get; set; }
        public ProductsAluDisc AluDisc { get; set; }
        [Column("PbDisc_Id")]
        public int? PbDiscId { get; set; }
        public ProductsPbDisc PbDisc { get; set; }

        public IList<PriceObject> Prices { get; set; }

        public enum DataSource
        {
            PneuB2B = 1,
            Gedip
        }
    }
}