using PneuMalik.Models.PneuB2b;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PneuMalik.Models.Dto
{
    public class Product
    {

        public Product()
        {
            // empty constructor
        }

        public Product(Response.Tyre tyre, Manufacturer manufacturer, VehicleType vehicleType, Season season)
        {
            Code = tyre.Id;
            Name = tyre.DisplayName;
            Active = true;
            Ean = tyre.Ean;
            Price = tyre.StockPriceInfo.TotalPriceCZK;

            Manufacturer = manufacturer;
            VehicleType = vehicleType;

            //Width = Convert.ToInt32(tyre.Width);
            //Diameter = Convert.ToInt32(tyre.Diameter);
            //Type = ProductType.Pneu;
            //Pattern = tyre.Pattern;
            //Design = tyre.ConstructionType;
            //IndexLi = Convert.ToInt32(tyre.LoadIndexFrom);
            //IndexSi = tyre.SpeedIndex;
            //HighPr = Convert.ToInt32(tyre.Profile);
            //FuelConsumption = tyre.TagConsumption;
            //Adhesion = tyre.TagAdhesion;
            //NoiseLevelDb = Convert.ToInt32(tyre.TagNoiseLevel_dB);
            //NoiseLevel = Convert.ToInt32(tyre.TagNoiseLevel);
            //Season = season;

            //Prices = new List<PriceObject>();
            //if (tyre.StockPriceInfo != null)
            //{
            //    Prices.Add(new PriceObject()
            //    {
            //        DeliveryTime = tyre.StockPriceInfo.DeliveryTime,
            //        Id = tyre.Id,
            //        Price = tyre.StockPriceInfo.TotalPriceCZK,
            //        ProductId = tyre.Id,
            //        Stock = Convert.ToInt32(tyre.StockPriceInfo.StockAmount)
            //    });
            //}

            //if (tyre.StockPriceInfo_48 != null)
            //{
            //    Prices.Add(new PriceObject()
            //    {
            //        DeliveryTime = tyre.StockPriceInfo_48.DeliveryTime,
            //        Id = tyre.Id,
            //        Price = tyre.StockPriceInfo_48.TotalPriceCZK,
            //        ProductId = tyre.Id,
            //        Stock = Convert.ToInt32(tyre.StockPriceInfo_48.StockAmount)
            //    });
            //}
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