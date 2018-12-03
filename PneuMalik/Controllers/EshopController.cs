using PneuMalik.Helpers;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    public class EshopController : Controller
    {

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Index()
        {

            var text = db.Texts.FirstOrDefault(t => t.Id == 1);
            var banner = db.Texts.FirstOrDefault(t => t.Id == 11);
            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.Title = text != null ? text.Title : string.Empty;
            ViewBag.Uvod = text != null ? text.Content : string.Empty;
            ViewBag.UvodH1 = text != null ? text.Title : string.Empty;
            ViewBag.Banner = banner != null ? banner.Content : string.Empty;
            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            // default product type
            var cathegory = 1;

            var model = new EshopViewModel(db, cathegory);
            foreach (var tip in model.Tips)
            {

                tip.Prices = db.Prices.Where(p => p.ProductId == tip.Id).ToList();
            }

            return View("~/Views/Eshop/Index.cshtml", model);
        }

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Kosik()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            var customer = new CustomerHelper();

            var model = new EshopViewModel(db)
            {
                Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList()
            };

            var vehicleTypes = db.VehicleTypes.ToList();

            model.CartProducts = db.CartRows.Join(db.Products, 
                c => c.ProductId, 
                p => p.Id, 
                (cart, product) => new { Cart = cart, Product = product })
                .Where(cp => cp.Cart.CustomerId == customer.Id)
                .Select(cp => cp.Product).ToList();

            model.Shipping = GetShipping(model.Cart, model.CartProducts);

            return View("~/Views/Eshop/Cart.cshtml", model);
        }

        [LayoutInjecter("_EshopLayout")]
        [ActionName("osobni-dodaci-udaje")]
        public ActionResult ObjednavkaKrok1()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            var customer = new CustomerHelper();

            var model = new EshopViewModel(db)
            {
                Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList()
            };

            var vehicleTypes = db.VehicleTypes.ToList();

            model.CartProducts = db.CartRows.Join(db.Products,
                c => c.ProductId,
                p => p.Id,
                (cart, product) => new { Cart = cart, Product = product })
                .Where(cp => cp.Cart.CustomerId == customer.Id)
                .Select(cp => cp.Product).ToList();

            model.Shipping = GetShipping(model.Cart, model.CartProducts);

            return View("~/Views/Eshop/OrderStep1.cshtml", model);
        }

        [LayoutInjecter("_EshopLayout")]
        [ActionName("doprava-a-platba")]
        public ActionResult ObjednavkaKrok2()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            var customer = new CustomerHelper();

            var model = new EshopViewModel(db)
            {
                Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList()
            };

            return View("~/Views/Eshop/OrderStep2.cshtml", model);
        }

        [LayoutInjecter("_EshopLayout")]
        [ActionName("souhrn-objednavky")]
        public ActionResult ObjednavkaKrok3()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            var customer = new CustomerHelper();

            var model = new EshopViewModel(db)
            {
                Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList()
            };

            var vehicleTypes = db.VehicleTypes.ToList();

            model.CartProducts = db.CartRows.Join(db.Products,
                c => c.ProductId,
                p => p.Id,
                (cart, product) => new { Cart = cart, Product = product })
                .Where(cp => cp.Cart.CustomerId == customer.Id)
                .Select(cp => cp.Product).ToList();

            model.Shipping = GetShipping(model.Cart, model.CartProducts);

            var customerId = Int32.Parse(customer.Id);
            model.Customer = db.Customers.FirstOrDefault(c => c.Id == customerId);

            return View("~/Views/Eshop/OrderStep3.cshtml", model);
        }

        [LayoutInjecter("_EshopLayout")]
        [ActionName("potvrzeni-objednavky")]
        public ActionResult ObjednavkaKrok4()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            var customer = new CustomerHelper();
            var model = new EshopViewModel(db)
            {
                Cart = db.CartRows.Where(c => c.CustomerId == customer.Id).ToList()
            };

            return View("~/Views/Eshop/OrderStep4.cshtml", model);
        }

        [LayoutInjecter("_EshopLayout")]
        public ActionResult Konfigurator()
        {

            var provozniDoba = db.Texts.FirstOrDefault(t => t.Id == 4);
            var kontakty = db.Texts.FirstOrDefault(t => t.Id == 7);
            var firstStop = db.Texts.FirstOrDefault(t => t.Id == 8);
            var footer = db.Texts.FirstOrDefault(t => t.Id == 13);

            ViewBag.ProvozniDoba = provozniDoba != null ? provozniDoba.Content : string.Empty;
            ViewBag.Kontakty = kontakty != null ? kontakty.Content : string.Empty;
            ViewBag.FirstStop = firstStop != null ? firstStop.Content : string.Empty;
            ViewBag.Footer = footer != null ? footer.Content : string.Empty;

            return View("~/Views/Eshop/Konfigurator.cshtml", new EshopViewModel(db, 1));
        }

        [HttpPost]
        public ActionResult Register(CustomerRegistration customer)
        {

            var user = new Customer(customer)
            {
                Ip = Request.UserHostAddress,
                Date = DateTime.Now
            };

            var customerHelper = new CustomerHelper();

            if (string.IsNullOrEmpty(customer.Email) && string.IsNullOrEmpty(customer.EmailNoreg))
            {

                var model = new EshopViewModel(db)
                {
                    Cart = db.CartRows.Where(c => c.CustomerId == customerHelper.Id).ToList()
                };

                return View("~/Views/Eshop/OrderStep1.cshtml", model);
            }

            var newCustomer = db.Customers.Add(user);
            db.SaveChanges();

            var oldCustomerId = customerHelper.Id;
            customerHelper.Id = newCustomer.Id.ToString();

            // change card id
            var cart = db.CartRows.Where(c => c.CustomerId == oldCustomerId).ToList();
            foreach(var row in cart)
            {
                row.CustomerId = newCustomer.Id.ToString();
                db.Entry(row).State = EntityState.Modified;
            }
            db.SaveChanges();

            db.CartRows.RemoveRange(cart);

            return RedirectToAction("doprava-a-platba");
        }

        [HttpPost]
        public ActionResult Shipping(string doprava)
        {

            var customer = new CustomerHelper();
            var customerId = customer.Id;

            foreach (var cart in db.CartRows.Where(c => c.CustomerId == customerId).ToList())
            {

                cart.Shipping = Int32.Parse(doprava);
            }
            db.SaveChanges();

            return RedirectToAction("souhrn-objednavky");
        }

        [LayoutInjecter("_EshopLayout")]
        [HttpPost]
        public ActionResult CartAmount(FormCollection collection)
        {

            foreach (var key in collection.AllKeys)
            {

                var cartRowId = Int32.Parse(key.Replace("mn", ""));
                var value = collection[key];

                var row = db.CartRows.FirstOrDefault(c => c.Id == cartRowId);
                row.Count = Int32.Parse(collection[key]);
                db.Entry(row).State = EntityState.Modified;
            }

            db.SaveChanges();

            return Kosik();
        }

        [HttpPost]
        public ActionResult Confirm(string Souhlas, string Pripominka)
        {

            var customHelper = new CustomerHelper();
            var customerId = Int32.Parse(customHelper.Id);
            var customer = db.Customers.FirstOrDefault(c => c.Id == customerId);
            var cart = db.CartRows.Where(c => c.CustomerId == customHelper.Id).ToList();
            var shipping = cart.First().Shipping;

            var order = new Order(customer)
            {
                Note = Pripominka,
                Shipping = shipping,
                Status = OrderStatus.New,
                Total = (decimal)cart.Sum(c => c.PriceTmp * c.Count),
                ShippingPrice = (decimal)200.0,
                Date = DateTime.Now,
                Sale = ((shipping == 2 || shipping == 3 || shipping == 5) ? 2 : 0)
            };

            var result = db.Orders.Add(order);
            db.SaveChanges();

            foreach(var row in cart)
            {

                var product = db.Products.FirstOrDefault(p => p.Id == row.ProductId);

                var orderRow = new OrderItem()
                {
                    OrderId = result.Id,
                    ProductId = row.ProductId,
                    Name = product.Name,
                    Dph = product.Dph,
                    Price = row.PriceTmp,
                    Quantity = row.Count
                };

                db.OrderItems.Add(orderRow);
            }

            db.SaveChanges();

            return RedirectToAction("potvrzeni-objednavky");
        }

        public ActionResult Error()
        {

            return View();
        }

        private double GetShipping(IList<CartRow> cart, IList<Product> products)
        {
            var personalCount = 0;
            var motoCount = 0;
            foreach (var vehicle in products)
            {

                var vehicleType = vehicle.VehicleType.Id;

                if (vehicleType == 1 || vehicleType == 2)
                {
                    personalCount += cart.FirstOrDefault(c => c.ProductId == vehicle.Id).Count;
                }
                else if (vehicleType == 5)
                {
                    motoCount += cart.FirstOrDefault(c => c.ProductId == vehicle.Id).Count;
                }
            }

            var shipping = 0.0;

            if (personalCount == 1)
            {
                shipping += 200.0;
            }
            else
            {
                shipping += personalCount * 100.0;
            }

            if (motoCount > 0)
            {
                shipping += (motoCount % 2) * 200.0; 
            }

            return shipping;
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}