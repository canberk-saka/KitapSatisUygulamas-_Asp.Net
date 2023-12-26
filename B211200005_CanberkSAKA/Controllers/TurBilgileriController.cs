using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using B211200005_CanberkSAKA;

namespace B211200005_CanberkSAKA.Controllers
{
    public class TurBilgileriController : Controller
    {
        private KITAPSATISEntities db = new KITAPSATISEntities();

        // GET: TurBilgileri
        public ActionResult Index()
        {
            return View(db.Tur.ToList());
        }

        // GET: TurBilgileri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tur tur = db.Tur.Find(id);
            if (tur == null)
            {
                return HttpNotFound();
            }
            return View(tur);
        }

        // GET: TurBilgileri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TurBilgileri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TurID,TurAdi")] Tur tur)
        {
            if (ModelState.IsValid)
            {
                db.Tur.Add(tur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tur);
        }

        // GET: TurBilgileri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tur tur = db.Tur.Find(id);
            if (tur == null)
            {
                return HttpNotFound();
            }
            return View(tur);
        }

        // POST: TurBilgileri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TurID,TurAdi")] Tur tur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tur).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tur);
        }

        // GET: TurBilgileri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tur tur = db.Tur.Find(id);
            if (tur == null)
            {
                return HttpNotFound();
            }
            return View(tur);
        }

        // POST: TurBilgileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tur tur = db.Tur.Find(id);
            db.Tur.Remove(tur);
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
