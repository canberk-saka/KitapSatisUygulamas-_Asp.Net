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
    public class YoneticilerController : Controller
    {
        private KITAPSATISEntities db = new KITAPSATISEntities();

        // GET: Yoneticiler
        public ActionResult Index()
        {
            if (Session["YoneticiGirisi"] == null || !(bool)Session["YoneticiGirisi"])
            {
                return RedirectToAction("Giris");
            }

            var kitaplar = db.Kitaplar.Include(k => k.Kategoriler).Include(k => k.Tur).Include(k => k.Yayinevleri).Include(k => k.Yazarlar);
            return View(kitaplar.ToList());
        }

        // GET: Yoneticiler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitaplar kitaplar = db.Kitaplar.Find(id);
            if (kitaplar == null)
            {
                return HttpNotFound();
            }
            return View(kitaplar);
        }

        // GET: Yoneticiler/Create
        public ActionResult Create()
        {
            if (Session["YoneticiGirisi"] == null || !(bool)Session["YoneticiGirisi"])
            {
                return RedirectToAction("Giris");
            }

            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi");
            ViewBag.TurID = new SelectList(db.Tur, "TurID", "TurAdi");
            ViewBag.YayineviID = new SelectList(db.Yayinevleri, "YayineviID", "Ad");
            ViewBag.YazarID = new SelectList(db.Yazarlar, "YazarID", "AdSoyad");
            return View();
        }

        // POST: Yoneticiler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitapID,KitapAdi,YazarID,KategoriID,TurID,YayineviID,YildizID,Fiyat,StokMiktari,KitapResim,AciklamaID")] Kitaplar kitaplar)
        {

           

            if (ModelState.IsValid)
            {
                db.Kitaplar.Add(kitaplar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", kitaplar.KategoriID);
            ViewBag.TurID = new SelectList(db.Tur, "TurID", "TurAdi", kitaplar.TurID);
            ViewBag.YayineviID = new SelectList(db.Yayinevleri, "YayineviID", "Ad", kitaplar.YayineviID);
            ViewBag.YazarID = new SelectList(db.Yazarlar, "YazarID", "AdSoyad", kitaplar.YazarID);
            return View(kitaplar);
        }

        // GET: Yoneticiler/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["YoneticiGirisi"] == null || !(bool)Session["YoneticiGirisi"])
            {
                return RedirectToAction("Giris");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitaplar kitaplar = db.Kitaplar.Find(id);
            if (kitaplar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", kitaplar.KategoriID);
            ViewBag.TurID = new SelectList(db.Tur, "TurID", "TurAdi", kitaplar.TurID);
            ViewBag.YayineviID = new SelectList(db.Yayinevleri, "YayineviID", "Ad", kitaplar.YayineviID);
            ViewBag.YazarID = new SelectList(db.Yazarlar, "YazarID", "AdSoyad", kitaplar.YazarID);
            return View(kitaplar);
        }

        // POST: Yoneticiler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitapID,KitapAdi,YazarID,KategoriID,TurID,YayineviID,YildizID,Fiyat,StokMiktari,KitapResim,AciklamaID")] Kitaplar kitaplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitaplar).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", kitaplar.KategoriID);
            ViewBag.TurID = new SelectList(db.Tur, "TurID", "TurAdi", kitaplar.TurID);
            ViewBag.YayineviID = new SelectList(db.Yayinevleri, "YayineviID", "Ad", kitaplar.YayineviID);
            ViewBag.YazarID = new SelectList(db.Yazarlar, "YazarID", "AdSoyad", kitaplar.YazarID);
            return View(kitaplar);
        }

        // GET: Yoneticiler/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Session["YoneticiGirisi"] == null || !(bool)Session["YoneticiGirisi"])
            {
                return RedirectToAction("Giris");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitaplar kitaplar = db.Kitaplar.Find(id);
            if (kitaplar == null)
            {
                return HttpNotFound();
            }
            return View(kitaplar);
        }

        // POST: Yoneticiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitaplar kitaplar = db.Kitaplar.Find(id);
            db.Kitaplar.Remove(kitaplar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Giris()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Giris(string Email, string Sifre)
        {
            var yonetici = db.Yoneticiler.FirstOrDefault(y => y.Email == Email && y.Sifre == Sifre);

            if (yonetici != null)
            {
                Session["YoneticiGirisi"] = true;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Geçersiz e-posta veya şifre");
            return View();
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
