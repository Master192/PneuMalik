using PneuMalik.Helpers;
using PneuMalik.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace PneuMalik.Controllers.Api
{
    public class EshopController : ApiController
    {

        [HttpGet]
        [Route("cartadd")]
        public IHttpActionResult CartAdd(int id, double price, int count)
        {
            var customer = new Customer();

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
                    PriceTmp = price,
                    Payment = 0,
                    Shipping = 0,
                    PriceType = 0
                });
            }

            db.SaveChanges();

            return Ok(db.CartRows.Where(c => c.CustomerId == customer.Id).ToList());
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}