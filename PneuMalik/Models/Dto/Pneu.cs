namespace PneuMalik.Models.Dto
{
    public class Pneu : Product
    {

        public string Pattern { get; set; }         // Dezén
        public VehicleType Vehicle { get; set; }
        public Season Season { get; set; }
        public string Design { get; set; }          // Provedení
        public int IndexLi { get; set; }
        public int SerieWidth { get; set; }
        public string Construction { get; set; }
        public string IndexSi { get; set; }
        public int HighPr { get; set; }
    }
}