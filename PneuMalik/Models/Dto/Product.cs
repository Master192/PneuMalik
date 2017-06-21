using System.Collections.Generic;

namespace PneuMalik.Models.Dto
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public IList<ProductCathegory> Cathegory { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string ShortDescription { get; set; }
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
        public ProductType Type { get; set; }

        public enum ProductType
        {
            Pneu,
            AluDisk,
            PbDisk,
            Accessories
        }
    }
}