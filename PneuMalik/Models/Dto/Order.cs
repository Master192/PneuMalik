using System;

namespace PneuMalik.Models.Dto
{
    public class Order
    {

        public Order()
        { }

        public Order(Customer customer)
        {

            this.CustomerId = customer.Id;
            if (customer.DeliveryAddress)
            {
                this.Name = customer.DeliveryName;
                this.Street = customer.DeliveryStreet;
                this.City = customer.DeliveryCity;
                this.Zip = customer.DeliveryZip;
                this.Country = customer.DeliveryCountry;
            }
            else
            {
                this.Name = $"{customer.Name} {customer.Surname}";
                this.Street = customer.Street;
                this.City = customer.City;
                this.Zip = customer.Zip;
                this.Country = customer.Country;
            }
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Note { get; set; }
        public int Shipping { get; set; }
        public OrderStatus Status { get; set; }
        public double Total { get; set; }
        public double ShippingPrice { get; set; }
        public DateTime Date { get; set; }
        public double Sale { get; set; }
    }

    public enum OrderStatus
    {

        New = 1,
        Accepted,
        Discarded,
        Confirmed,
        Finished
    }
}