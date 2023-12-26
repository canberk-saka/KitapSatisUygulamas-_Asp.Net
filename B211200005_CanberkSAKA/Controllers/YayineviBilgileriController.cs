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
    public class YayineviBilgileriController : Controller
    {
        private KITAPSATISEntities db = new KITAPSATISEntities();

        // GET: YayineviBilgileri
        public ActionResult Index()
        {
            return View(db.Yayinevleri.ToList());
        }

        // GET: YayineviBilgileri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yayinevleri yayinevleri = db.Yayinevleri.Find(id);
            if (yayinevleri == null)
            {
                return HttpNotFound();
            }
            return View(yayinevleri);
        }

        // GET: YayineviBilgileri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YayineviBilgileri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YayineviID,Ad,TelefonNumarasi")] Yayinevleri yayinevleri)
        {
            if (ModelState.IsValid)
            {
                db.Yayinevleri.Add(yayinevleri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yayinevleri);
        }

        // GET: YayineviBilgileri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yayinevleri yayinevleri = db.Yayinevleri.Find(id);
            if (yayinevleri == null)
            {
                return HttpNotFound();
            }
            return View(yayinevleri);
        }

        // POST: YayineviBilgileri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YayineviID,Ad,TelefonNumarasi")] Yayinevleri yayinevleri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yayinevleri).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yayinevleri);
        }

        // GET: YayineviBilgileri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yayinevleri yayinevleri = db.Yayinevleri.Find(id);
            if (yayinevleri == null)
            {
                return HttpNotFound();
            }
            return View(yayinevleri);
        }

        // POST: YayineviBilgileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yayinevleri yayinevleri = db.Yayinevleri.Find(id);
            db.Yayinevleri.Remove(yayinevleri);
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
