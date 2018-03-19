using System;

namespace PneuMalik.Models.Dto
{
    public class Customer
    {

        public Customer()
        { }

        public Customer(CustomerRegistration registrated)
        {

            // registration with password
            if (registrated.hidSmer == 2)
            {

                this.Password = registrated.Heslo;
                this.Name = registrated.Jmeno;
                this.Surname = registrated.Prijmeni;
                this.Email = registrated.Email;
                this.Phone = registrated.Telefon;

                this.Street = registrated.Ulice;
                this.City = registrated.Mesto;
                this.Zip = registrated.PSC;
                this.Country = registrated.Stat;

                this.Company = registrated.chbFirma == "on";
                this.CompanyName = registrated.Firma;
                this.CompanyId = registrated.IC;
                this.TaxId = registrated.DIC;

                this.DeliveryAddress = registrated.Dodaci != "on";
                this.DeliveryName = registrated.Jmeno2;
                this.DeliveryStreet = registrated.Ulice2;
                this.DeliveryCity = registrated.Mesto2;
                this.DeliveryZip = registrated.PSC2;
                this.DeliveryCountry = registrated.Stat2;
            }
            else
            {

                this.Password = "no-pass-user-unable-to-login";
                this.Name = registrated.JmenoNoreg;
                this.Surname = registrated.PrijmeniNoreg;
                this.Email = registrated.EmailNoreg;
                this.Phone = registrated.TelefonNoreg;

                this.Street = registrated.UliceNoreg;
                this.City = registrated.MestoNoreg;
                this.Zip = registrated.PSCNoreg;
                this.Country = registrated.StatNoreg;

                this.Company = registrated.chbFirmaNoreg == "on";
                this.CompanyName = registrated.FirmaNoreg;
                this.CompanyId = registrated.ICNoreg;
                this.TaxId = registrated.DICNoreg;

                this.DeliveryAddress = registrated.DodaciNoreg != "on";
                this.DeliveryName = registrated.Jmeno2Noreg;
                this.DeliveryStreet = registrated.Ulice2Noreg;
                this.DeliveryCity = registrated.Mesto2Noreg;
                this.DeliveryZip = registrated.PSC2Noreg;
                this.DeliveryCountry = registrated.Stat2Noreg;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

        public bool Company { get; set; }
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string TaxId { get; set; }

        public bool DeliveryAddress { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryZip { get; set; }
        public string DeliveryCountry { get; set; }

        public string Ip { get; set; }
        public DateTime Date { get; set; }
    }
}