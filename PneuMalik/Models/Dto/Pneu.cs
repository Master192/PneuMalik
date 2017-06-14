using System.Collections.Generic;

namespace PneuMalik.Models.Dto
{
    public class Pneu
    {

        public int Id { get; set; }
        public string Code { get; set; }
        public IList<PneuCathegory> Cathegory { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Tip { get; set; }
        public bool Action { get; set; }
        public bool InStock { get; set; }
        public string Pattern { get; set; }         // Dezén
        public VehicleType Vehicle { get; set; }
        public Season Season { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string Design { get; set; }          // Provedení
        public int IndexLi { get; set; }
        public int SerieWidth { get; set; }
        public int Width { get; set; }
        public string Construction { get; set; }
        public int Diameter { get; set; }           // Průměr
        public string IndexSi { get; set; }
        public int HighPr { get; set; }
        public string Ean { get; set; }
        public double PriceCommon { get; set; }
        public double Price { get; set; }
    }
}