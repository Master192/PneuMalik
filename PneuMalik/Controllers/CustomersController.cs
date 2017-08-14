using PneuMalik.Helpers;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    [Authorize]
    [LayoutInjecter("_Layout")]
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            return View("/Views/Admin/Customers.cshtml");
        }
    }
}