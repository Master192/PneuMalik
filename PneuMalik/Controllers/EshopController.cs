using PneuMalik.Models;
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
            var text = db.Texts.First(t => t.Id == 1);

            ViewBag.Title = text.Title;
            ViewBag.Uvod = text.Content;
            ViewBag.UvodH1 = text.Title;

            return View();
        }

        private ApplicationDbContext db = new ApplicationDbContext();
    }
}