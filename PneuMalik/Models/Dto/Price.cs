using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PneuMalik.Models.Dto
{
    public class PriceObject
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int DeliveryTime { get; set; }
    }
}