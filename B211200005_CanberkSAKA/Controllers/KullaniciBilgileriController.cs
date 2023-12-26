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
    public class KullaniciBilgileriController : Controller
    {
        private KITAPSATISEntities db = new KITAPSATISEntities();

        // GET: KullaniciBilgileri
        public ActionResult Index()
        {
            return View(db.Kullanicilar.ToList());
        }

        // GET: KullaniciBilgileri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // GET: KullaniciBilgileri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KullaniciBilgileri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KullaniciID,Ad,Soyad,Email,Sifre,GirisYapildiMi")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.Kullanicilar.Add(kullanicilar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kullanicilar);
        }

        // GET: KullaniciBilgileri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // POST: KullaniciBilgileri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KullaniciID,Ad,Soyad,Email,Sifre,GirisYapildiMi")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kullanicilar).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kullanicilar);
        }

        // GET: KullaniciBilgileri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // POST: KullaniciBilgileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanicilar kullanicilar = db.Kullanicilar.Find(id);
            db.Kullanicilar.Remove(kullanicilar);
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
