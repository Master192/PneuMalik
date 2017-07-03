using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PneuMalik.Models.Dto;

namespace PneuMalik.Models
{
    public class PneusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pneus
        public ActionResult Index()
        {
            return View(db.Pneus.ToList());
        }

        // GET: Pneus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pneu pneu = db.Pneus.Find(id);
            if (pneu == null)
            {
                return HttpNotFound();
            }
            return View(pneu);
        }

        // GET: Pneus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pneus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pattern,Design,IndexLi,SerieWidth,Construction,IndexSi,HighPr,Code,Name,Active,ShortDescription,Description,Image,Tip,Action,InStock,Width,Diameter,Ean,PriceCommon,Price,Type")] Pneu pneu)
        {
            if (ModelState.IsValid)
            {
                db.Pneus.Add(pneu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pneu);
        }

        // GET: Pneus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pneu pneu = db.Pneus.Find(id);
            if (pneu == null)
            {
                return HttpNotFound();
            }
            return View(pneu);
        }

        // POST: Pneus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pattern,Design,IndexLi,SerieWidth,Construction,IndexSi,HighPr,Code,Name,Active,ShortDescription,Description,Image,Tip,Action,InStock,Width,Diameter,Ean,PriceCommon,Price,Type")] Pneu pneu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pneu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pneu);
        }

        // GET: Pneus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pneu pneu = db.Pneus.Find(id);
            if (pneu == null)
            {
                return HttpNotFound();
            }
            return View(pneu);
        }

        // POST: Pneus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pneu pneu = db.Pneus.Find(id);
            db.Pneus.Remove(pneu);
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
