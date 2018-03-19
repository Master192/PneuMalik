using PneuMalik.Helpers;
using PneuMalik.Models;
using PneuMalik.Models.Dto;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PneuMalik.Controllers
{

    [Authorize]
    [LayoutInjecter("_Layout")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(int? code, string text)
        {

            var products = new List<Product>();

            if (!code.HasValue && string.IsNullOrEmpty(text))
            {

                products = db.Products.Take(50).ToList();
            }
            else if (string.IsNullOrEmpty(text))
            {

                products = db.Products.Where(p => p.Code == code.Value).ToList();
            }
            else if (!code.HasValue)
            {

                products = db.Products.Where(p => p.Name.Contains(text)).ToList();
            }
            else
            {

                products = db.Products.Where(p => p.Code == code.Value && p.Name.Contains(text)).ToList();
            }

            foreach (var product in products)
            {

                product.Tyre = db.ProductsTyres.FirstOrDefault(t => t.Id == product.TyreId);
                product.AluDisc = db.ProductsAluDisc.FirstOrDefault(d => d.Id == product.AluDiscId);
                product.PbDisc = db.ProductsPbDisc.FirstOrDefault(d => d.Id == product.PbDiscId);
                product.Prices = db.Prices.Where(p => p.ProductId == product.Id).ToList();
            }

            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            product.Tyre = db.ProductsTyres.FirstOrDefault(t => t.Id == product.TyreId);
            product.AluDisc = db.ProductsAluDisc.FirstOrDefault(d => d.Id == product.AluDiscId);
            product.PbDisc = db.ProductsPbDisc.FirstOrDefault(d => d.Id == product.PbDiscId);
            product.Prices = db.Prices.Where(p => p.ProductId == product.Id).ToList();

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Active,ShortDescription,Description,Image,Tip,Action,InStock,Width,Diameter,Ean,PriceCommon,Price,Type,Pattern,Design,IndexLi,SerieWidth,Construction,IndexSi,HighPr,Model,Size,Et,Holes,Year,Pitch,MiddleHole")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Active,ShortDescription,Description,Image,Tip,Action,InStock,Width,Diameter,Ean,PriceCommon,Price,Type,Pattern,Design,IndexLi,SerieWidth,Construction,IndexSi,HighPr,Model,Size,Et,Holes,Year,Pitch,MiddleHole")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
