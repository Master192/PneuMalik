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
                    Manufacturers = available.GroupBy(g => g.Manufacturer.Id)
                        .Select(s => s.FirstOrDefault().Manufacturer.Id).ToList()
                };

                if (cathegoryType < 9)      // pneu
                {

                    var avail = available.ToList();

                    Filter.Rims = available.Where(r => r.Tyre.Rafek.Name != "-")
                        .GroupBy(g => g.Tyre.Rafek.Id)
                        .Select(s => s.FirstOrDefault().Tyre.Rafek.Id).ToList();
                    Filter.Widths = available.Where(r => r.Tyre.Sirka.Name != "-")
                        .GroupBy(g => g.Tyre.Sirka.Id)
                        .Select(s => s.FirstOrDefault().Tyre.Sirka.Id).ToList();
                    Filter.Profiles = available.Where(r => r.Tyre.Profil.Name != "-")
                        .GroupBy(g => g.Tyre.Profil.Id)
                        .Select(s => s.FirstOrDefault().Tyre.Profil.Id).ToList();
                    Filter.Seasons = available.Where(r => (Season)r.Tyre.Sezona != Season.Unknown)
                        .GroupBy(g => g.Tyre.Sezona)
                        .Select(s => s.FirstOrDefault().Tyre.Sezona).ToList();
                    
                }

                if (cathegoryType == 9)     // ocelové disky
                {

                    Filter.Brands = available.Where(r => r.PbDisc.Znacka.Name != "-")
                        .GroupBy(g => g.PbDisc.Znacka.Id)
                        .Select(s => s.FirstOrDefault().PbDisc.Znacka.Id).ToList();
                    Filter.Models = available.Where(r => r.PbDisc.Model.Name != "-")
                        .GroupBy(g => g.PbDisc.Model.Id)
                        .Select(s => s.FirstOrDefault().PbDisc.Model.Id).ToList();
                }

                if (cathegoryType == 10)        // hlinikové disky
                {

                    Filter.Rims = available.Where(r => r.AluDisc.Rafek.Name != "-")
                        .GroupBy(g => g.AluDisc.Rafek.Id)
                        .Select(s => s.FirstOrDefault().AluDisc.Rafek.Id).ToList();
                    Filter.Widths = available.Where(r => r.AluDisc.Sirka.Name != "-")
                        .GroupBy(g => g.AluDisc.Sirka.Id)
                        .Select(s => s.FirstOrDefault().AluDisc.Sirka.Id).ToList();
                }

                File.WriteAllText(path, JsonConvert.SerializeObject(Filter));
            }

            Filter = JsonConvert.DeserializeObject<Filter>(File.ReadAllText(path));
        }

        public Filter Filter { get; set; }
    }
}