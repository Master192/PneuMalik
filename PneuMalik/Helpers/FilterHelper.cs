using Newtonsoft.Json;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System;
using System.IO;
using System.Linq;

namespace PneuMalik.Helpers
{
    public class FilterHelper
    {

        public FilterHelper(ApplicationDbContext db, int cathegoryType)
        {

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", $"filter-default-{cathegoryType}.json");

            if (!File.Exists(path) || File.GetLastWriteTime(path) < DateTime.Now.AddDays(-1))
            {

                var available = db.Products.Where(p => p.VehicleType.Id == cathegoryType && p.Active);

                Filter = new Filter()
                    {
                        Manufacturers = available.GroupBy(g => g.Manufacturer.Id).Select(s => s.FirstOrDefault().Manufacturer.Id).ToList(),
                        Seasons = available.GroupBy(g => g.Season.Id).Select(s => s.FirstOrDefault().Season.Id).ToList(),
                        Widths = available.GroupBy(g => g.Width).Select(s => s.FirstOrDefault().Width).ToList(),
                        Rims = available.GroupBy(g => g.Diameter).Select(s => s.FirstOrDefault().Diameter).ToList(),
                        Profiles = available.GroupBy(g => g.HighPr).Select(s => s.FirstOrDefault().HighPr).ToList()
                    };

                File.WriteAllText(path, JsonConvert.SerializeObject(Filter));
            }

            Filter = JsonConvert.DeserializeObject<Filter>(File.ReadAllText(path));
        }

        public Filter Filter { get; set; }
    }
}