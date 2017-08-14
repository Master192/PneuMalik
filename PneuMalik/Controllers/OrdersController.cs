using PneuMalik.Helpers;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    [Authorize]
    [LayoutInjecter("_Layout")]
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            return View("/Views/Admin/Orders.cshtml");
        }
    }
}