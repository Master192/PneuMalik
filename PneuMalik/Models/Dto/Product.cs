using PneuMalik.Models.PneuB2b;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PneuMalik.Models.Dto
{
    public class Product
    {

        public Product()
        {
            // empty constructor
        }

        public Product(Response.Tyre tyre)
        {
            Code = tyre.Id.ToString();
            Name = tyre.DisplayName;
            Active = true;
            Width = Convert.ToInt32(tyre.Width);
            Diameter = Convert.ToInt32(tyre.Diameter);
            Ean = tyre.Ean;
            Price = tyre.StockPriceInfo.TotalPriceCZK;
            Type = ProductType.Pneu;
            Pattern = tyre.Pattern;
            Design = tyre.ConstructionType;
            IndexLi = Convert.ToInt32(tyre.LoadIndexFrom);
            IndexSi = tyre.SpeedIndex;
            HighPr = Convert.ToInt32(tyre.Profile);
            FuelConsumption = tyre.TagConsumption;
            Adhesion = tyre.TagAdhesion;
            NoiseLevelDb = Convert.ToInt32(tyre.TagNoiseLevel_dB);
            NoiseLevel = Convert.ToInt32(tyre.TagNoiseLevel);
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public IList<Cathegory> Cathegories { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Tip { get; set; }
        public bool Action { get; set; }
        public bool InStock { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int Width { get; set; }
        public int Diameter { get; set; }           // Průměr
        public string Ean { get; set; }
        public double PriceCommon { get; set; }
        public double Price { get; set; }
        public double Sale { get; set; }
        public double Dph { get; set; }
        public ProductType Type { get; set; }

        // Pneu
        public string Pattern { get; set; }         // Dezén
        public VehicleType Vehicle { get; set; }
        public Season Season { get; set; }
        public string Design { get; set; }          // Provedení
        public int IndexLi { get; set; }
        public int SerieWidth { get; set; }
        public string Construction { get; set; }
        public string IndexSi { get; set; }
        public int HighPr { get; set; }
        public string FuelConsumption { get; set; }
        public string Adhesion { get; set; }
        public int NoiseLevelDb { get; set; }
        public int NoiseLevel { get; set; }
        public string EfficiencyCathegory { get; set; }
        public string Standard { get; set; }

        // Disky
        public DiscType DiscType { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public int Et { get; set; }
        public int Holes { get; set; }
        public int Year { get; set; }
        public int Pitch { get; set; }          // Rozteč
        public int MiddleHole { get; set; }     // Středový otvor

        public enum ProductType
        {
            Pneu,
            AluDisk,
            PbDisk,
            Accessories
        }
    }
}