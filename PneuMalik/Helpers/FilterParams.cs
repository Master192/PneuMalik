using System.Collections.Generic;

namespace PneuMalik.Helpers
{
    public class FilterParams
    {

        public FilterParams()
        {

            //Widths = db.Products.Where(p => p.VehicleType.Id == cathegory && p.Active).GroupBy(g => g.Width).Select(s => s.FirstOrDefault().Width).ToList();
            //Rims = db.Products.Where(p => p.VehicleType.Id == cathegory && p.Active).GroupBy(g => g.Diameter).Select(s => s.FirstOrDefault().Diameter).ToList();
            //Profiles = db.Products.Where(p => p.VehicleType.Id == cathegory && p.Active).GroupBy(g => g.HighPr).Select(s => s.FirstOrDefault().HighPr).ToList();

            Widths = new List<int> { 125, 135, 145, 155, 165, 175, 185, 195, 205, 215, 225, 235, 245,
                255, 265, 275, 285, 295, 305, 315, 325, 335 };
            Rims = new List<int> { 10, 16, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85};
            Profiles = new List<int> { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
        }

        public List<int> Widths { get; set; }
        public List<int> Rims { get; set; }
        public List<int> Profiles { get; set; }
    }
}