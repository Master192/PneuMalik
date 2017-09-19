using PneuMalik.Models.Dto;
using System.Collections.Generic;

namespace PneuMalik.Models
{
    public class EshopViewModel
    {

        public IList<Product> Products { get; set; }
        public IList<Product> Tips { get; set; }
    }
}