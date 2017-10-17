using System;

namespace PneuMalik.Models.Dto
{
    public class CartRow
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime Added { get; set; }
        public double PriceTmp { get; set; }
        public int PriceType { get; set; }
        public int Shipping { get; set; }
        public int Payment { get; set; }
    }
}