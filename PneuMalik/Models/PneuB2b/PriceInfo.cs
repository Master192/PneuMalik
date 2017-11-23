using System.ComponentModel.DataAnnotations;
using static PneuMalik.Models.PneuB2b.Response;

namespace PneuMalik.Models.PneuB2b
{
    public class PriceInfo : StockPriceInfo
    {

        public PriceInfo()
        {
        }

        public PriceInfo(StockPriceInfo price)
        {

            Currency = price.Currency;
            DeliveryTime = price.DeliveryTime;
            DeliveryTimeTerm = price.DeliveryTimeTerm;
            StockAmount = price.StockAmount;
            SuppliersCountry = price.SuppliersCountry;
            TotalPrice = price.TotalPrice;
            TotalPriceCZK = price.TotalPriceCZK;
            TotalPriceIncDelivery = price.TotalPriceIncDelivery;
            TotalPriceIncDeliveryComputed = price.TotalPriceIncDeliveryComputed;
            TotalPriceIncDeliveryCZK = price.TotalPriceIncDeliveryCZK;
        }

        [Key]
        public int Kis { get; set; }
        public int ProductId { get; set; }
        public PriceInfoType Type { get; set; }
        public int Period { get; set; }

        public enum PriceInfoType
        {
            Tyre,
            Rim
        }
    }
}