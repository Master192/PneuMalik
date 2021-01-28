using PneuMalik.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PneuMalik.Helpers
{
    public static class ProductHelper
    {
        public static string GetAvailabilityString(int localStorage, IList<PriceObject> prices)
        {

            if (localStorage > 0) return "SKLADEM";

            var external = prices.FirstOrDefault(p => p.DeliveryTime == 0);
            var ext24 = prices.FirstOrDefault(p => p.DeliveryTime == 24);
            var ext48 = prices.FirstOrDefault(p => p.DeliveryTime == 48);
            var extMore = prices.FirstOrDefault(p => p.DeliveryTime > 48);

            var externalStorage = external?.Stock;
            var external24 = ext24?.Stock;
            var external48 = ext48?.Stock;
            var deliveryTime = extMore?.DeliveryTime;

            deliveryTime = (deliveryTime ?? 0 ) + 12;

            var isAm = DateTime.Now.Hour < 12;

            if ((external24 ?? 0) > 0) return "SKLADEM DO " + (isAm ? "24" : "36") + " hod";
            if ((external48 ?? 0) > 0) return "SKLADEM DO " + (isAm ? "48" : "60") + " hod";
            if ((externalStorage ?? 0) > 0)
            {
                return $"SKLADEM DO {((deliveryTime.Value == 12) ? "108" : deliveryTime.Value.ToString())} hod";
            }

            return "<span class=\"redNaDotaz\">NA DOTAZ</span>";
        }
    }
}