namespace PneuMalik.Models.Dto
{
    public class Disc : Product
    {

        public DiscType Type { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public int Et { get; set; }
        public int Holes { get; set; }
        public int Year { get; set; }
        public int Pitch { get; set; }          // Rozteč
        public int MiddleHole { get; set; }     // Středový otvor
    }
}