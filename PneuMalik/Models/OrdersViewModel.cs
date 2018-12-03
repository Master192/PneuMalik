using PneuMalik.Models.Dto;
using System.Collections.Generic;

namespace PneuMalik.Models
{
    public class OrdersViewModel
    {

        public List<Order> Orders { get; set; }
        public List<Customer> Customers { get; set; }
        public Dictionary<OrderStatus, string> Statuses { get; set; }
    }
}