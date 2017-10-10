using PneuMalik.Helpers;
using PneuMalik.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace PneuMalik.Models
{
    public class EshopViewModel
    {

        public EshopViewModel(ApplicationDbContext db, int cathegory, FilterParams parameters) 
            : this(db, cathegory, parameters, db.Products.Where(p => p.Action && p.Active).ToList()) {
        }

        public EshopViewModel(ApplicationDbContext db, int cathegory, FilterParams parameters,
            List<Product> products)
        {
            Products = products;
            Tips = db.Products.Where(p => p.Tip && p.Active).ToList();
            Manufacturers = db.Manufacturers.ToList();
            VehicleTypes = db.VehicleTypes.ToList();
            Seasons = db.Seasons.ToList();
            CathegoryType = cathegory;
            Widths = parameters.Widths;
            Rims = parameters.Rims;
            Profiles = parameters.Profiles;
        }

        public int CathegoryType { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Product> Tips { get; set; }
        public IList<Manufacturer> Manufacturers { get; set; }
        public IList<VehicleType> VehicleTypes { get; set; }
        public IList<Season> Seasons { get; set; }
        public IList<int> Widths { get; set; }
        public IList<int> Rims { get; set; }
        public IList<int> Profiles { get; set; }
    }
}