using Newtonsoft.Json;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System;
using System.Collections.Generic;
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

                    Rims = available.GroupBy(g => g.Diameter).Select(s => s.FirstOrDefault().Diameter).ToList(),
                    Manufacturers = available.GroupBy(g => g.Manufacturer.Id).Select(s => s.FirstOrDefault().Manufacturer.Id).ToList(),
                    Widths = available.GroupBy(g => g.Width).Select(s => s.FirstOrDefault().Width).ToList()
                };

                if (cathegoryType < 9)      // pneu
                {

                    Filter.Profiles = available.GroupBy(g => g.HighPr)
                        .Select(s => s.FirstOrDefault().HighPr).ToList();
                    Filter.Seasons = available
                        .GroupBy(g => g.Season.Id).Select(s => s.FirstOrDefault().Season.Id).ToList();
                    
                }

                if (cathegoryType == 10)     // ocelové disky
                {

                    var models = available.Where(w => !string.IsNullOrEmpty(w.Construction))
                        .GroupBy(g => g.Construction).Select(s => s.FirstOrDefault().Construction).ToList();

                    var modelsDelimited = new List<string>();
                    foreach (var model in models)
                    {
                        modelsDelimited.AddRange(model.Split('/'));
                    }

                    Filter.Brands = available.Where(w => !string.IsNullOrEmpty(w.Model))
                        .GroupBy(g => g.Model).Select(s => s.FirstOrDefault().Model).ToList();
                    Filter.Models = modelsDelimited.GroupBy(g => g).Select(s => s.FirstOrDefault())
                        .OrderBy(o => o).ToList();
                }

                File.WriteAllText(path, JsonConvert.SerializeObject(Filter));
            }

            Filter = JsonConvert.DeserializeObject<Filter>(File.ReadAllText(path));
        }

        public Filter Filter { get; set; }
    }
}