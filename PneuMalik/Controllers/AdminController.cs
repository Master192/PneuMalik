using PneuMalik.Helpers;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    [Authorize]
    [LayoutInjecter("_Layout")]
    public class AdminController : Controller
    {

        public ActionResult Actualization()
        {
            return View();
        }

        public ActionResult ImageChange()
        {
            return View();
        }

        public ActionResult ImportXml()
        {
            return View();
        }

        public ActionResult MultiDelete()
        {
            return View();
        }

        public ActionResult PriceChange()
        {
            return View();
        }

        public ActionResult SaleChange()
        {
            return View();
        }

        public ActionResult TextChange()
        {
            return View();
        }
    }
}