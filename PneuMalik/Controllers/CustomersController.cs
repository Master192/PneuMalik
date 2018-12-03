using PneuMalik.Helpers;
using PneuMalik.Models;
using System.Linq;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    [Authorize]
    [LayoutInjecter("_Layout")]
    public class CustomersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {

            return View("/Views/Admin/Customers.cshtml", db.Customers.ToList());
        }
    }
}