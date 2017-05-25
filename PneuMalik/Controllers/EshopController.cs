using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{
    public class EshopController : Controller
    {
        // GET: Eshop
        public ActionResult Index()
        {
            ViewBag.Title = "E-shop Pneumalik";

            return View();
        }
    }
}