using PneuMalik.Models.Dto;
using System.Collections.Generic;

namespace PneuMalik.Models
{
    public class ProductDetailViewModel
    {

        public Product Product { get; set; }
        public IList<Product> Tips { get; set; }
        public IList<Manufacturer> Manufacturers { get; set; }
        public IList<Season> Seasons { get; set; }
        public IList<VehicleType> VehicleTypes { get; set; }
    }
}