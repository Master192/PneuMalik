using PneuMalik.Helpers;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    [Authorize]
    [LayoutInjecter("_Layout")]
    public class OrdersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var model = new OrdersViewModel()
            {
                Orders = db.Orders.ToList(),
                Customers = db.Customers.ToList(),
                Statuses = GetStatusNames()
            };

            return View("/Views/Admin/Orders.cshtml", model);
        }

        private Dictionary<OrderStatus, string> GetStatusNames()
        {

            var result = new Dictionary<OrderStatus, string>();

            result.Add(OrderStatus.New, "Nová");
            result.Add(OrderStatus.Accepted, "Přijatá");
            result.Add(OrderStatus.Discarded, "Stornovaná");
            result.Add(OrderStatus.Confirmed, "Potvrzená");
            result.Add(OrderStatus.Finished, "Vyřízená");

            return result;
        }
    }
}