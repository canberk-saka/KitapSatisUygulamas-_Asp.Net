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
    public class YazarBilgileriController : Controller
    {
        private KITAPSATISEntities db = new KITAPSATISEntities();

        // GET: YazarBilgileri
        public ActionResult Index()
        {
            return View(db.Yazarlar.ToList());
        }

        // GET: YazarBilgileri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazarlar yazarlar = db.Yazarlar.Find(id);
            if (yazarlar == null)
            {
                return HttpNotFound();
            }
            return View(yazarlar);
        }

        // GET: YazarBilgileri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YazarBilgileri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YazarID,AdSoyad")] Yazarlar yazarlar)
        {
            if (ModelState.IsValid)
            {
                db.Yazarlar.Add(yazarlar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yazarlar);
        }

        // GET: YazarBilgileri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazarlar yazarlar = db.Yazarlar.Find(id);
            if (yazarlar == null)
            {
                return HttpNotFound();
            }
            return View(yazarlar);
        }

        // POST: YazarBilgileri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YazarID,AdSoyad")] Yazarlar yazarlar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yazarlar).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yazarlar);
        }

        // GET: YazarBilgileri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yazarlar yazarlar = db.Yazarlar.Find(id);
            if (yazarlar == null)
            {
                return HttpNotFound();
            }
            return View(yazarlar);
        }

        // POST: YazarBilgileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yazarlar yazarlar = db.Yazarlar.Find(id);
            db.Yazarlar.Remove(yazarlar);
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
