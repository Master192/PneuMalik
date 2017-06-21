using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PneuMalik.Models;
using PneuMalik.Models.Dto;

namespace PneuMalik.Controllers
{
    public class CathegoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cathegories
        public ActionResult Index()
        {
            return View(db.Cathegories.ToList());
        }

        // GET: Cathegories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cathegory cathegory = db.Cathegories.Find(id);
            if (cathegory == null)
            {
                return HttpNotFound();
            }
            return View(cathegory);
        }

        // GET: Cathegories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cathegories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Url,Title,Default,Active,Keywords,Description,Annotation,Content")] Cathegory cathegory)
        {
            if (ModelState.IsValid)
            {
                db.Cathegories.Add(cathegory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cathegory);
        }

        // GET: Cathegories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cathegory cathegory = db.Cathegories.Find(id);
            if (cathegory == null)
            {
                return HttpNotFound();
            }
            return View(cathegory);
        }

        // POST: Cathegories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Url,Title,Default,Active,Keywords,Description,Annotation,Content")] Cathegory cathegory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cathegory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cathegory);
        }

        // GET: Cathegories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cathegory cathegory = db.Cathegories.Find(id);
            if (cathegory == null)
            {
                return HttpNotFound();
            }
            return View(cathegory);
        }

        // POST: Cathegories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cathegory cathegory = db.Cathegories.Find(id);
            db.Cathegories.Remove(cathegory);
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
