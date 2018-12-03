using PneuMalik.Helpers;
using PneuMalik.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PneuMalik.Controllers.Api
{
    public class EshopController : ApiController
    {

        [HttpGet]
        [Route("cartadd")]
        public IHttpActionResult CartAdd(int id, double price, double dph, int count)
        {
            var customer = new CustomerHelper();

            if (db.CartRows.Any(c => c.CustomerId == customer.Id && c.ProductId == id))
            {
                var cartRow = db.CartRows.FirstOrDefault(c => c.CustomerId == customer.Id && c.ProductId == id);
                cartRow.Added = DateTime.Now;
                cartRow.Count = cartRow.Count + count;

                db.Entry(cartRow).State = EntityState.Modified;
            }
            else
            {

                db.CartRows.Add(new Models.Dto.CartRow()
                {
                    Added = DateTime.Now,
                    Count = count,
                    CustomerId = customer.Id,
                    ProductId = id,
                    PriceTmp = price * (1 + (dph / 100.0)),
                    Payment = 0,
                    Shipping = 0,
                    PriceType = 0
                });
            }

            db.SaveChanges();

            return Ok(db.CartRows.Where(c => c.CustomerId == customer.Id).ToList());
        }

        [HttpGet]
        [Route("removecartrow")]
        public IHttpActionResult RemoveCartRow(int id)
        {

            var row = db.CartRows.FirstOrDefault(c => c.Id == id);

            db.CartRows.Remove(row);
            db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("register")]
        public IHttpActionResult Register(int id, double price, int count)
        {
            return Content(HttpStatusCode.BadRequest, "Není implementováno");
        }

        [HttpGet]
        [Route("login")]
        public IHttpActionResult Login(string user, string password)
        {
            return Content(HttpStatusCode.BadRequest, "Není implementováno");
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}