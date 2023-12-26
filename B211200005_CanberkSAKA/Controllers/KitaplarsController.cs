using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using B211200005_CanberkSAKA;
using System.Data.Entity;
using System.Diagnostics;

namespace B211200005_CanberkSAKA.Controllers
{
    
    public class KitaplarsController : Controller
    {
        private KITAPSATISEntities db = new KITAPSATISEntities();

        // GET: Kitaplars
        public ActionResult Index()
        {
            var kitaplar = db.Kitaplar.Include(k => k.Kategoriler).Include(k => k.Yayinevleri).ToList();
            return View(kitaplar);
        }

        // GET: Kitaplars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kitaplar kitaplar = db.Kitaplar.Find(id);
            var kullanicilar = db.Kullanicilar.FirstOrDefault().GirisYapildiMi;
            bool girisDurumu = GetLoginStatus();

            if (kitaplar == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.GirisDurumu = girisDurumu;


            ViewBag.YazarAdSoyad = kitaplar.KitapYazarIliskisi.FirstOrDefault()?.Yazarlar.AdSoyad;
            ViewBag.KategoriAdi = kitaplar.Kategoriler.KategoriAdi;
            ViewBag.TurAdi = kitaplar.Tur.TurAdi;
            ViewBag.YayineviAd = kitaplar.Yayinevleri.Ad;

            return View(kitaplar);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Yazarlar = new SelectList(db.Yazarlar, "YazarID", "AdSoyad");
            ViewBag.Kategoriler = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi");
            ViewBag.Turler = new SelectList(db.Tur, "TurID", "TurAdi");
            ViewBag.Yayinevleri = new SelectList(db.Yayinevleri, "YayineviID", "Ad");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitapID,KitapAdi,YazarID,KategoriID,TurID,KitapResim,Fiyat,Stok,Yildiz")] Kitaplar kitaplar, string KitapResimURL)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(KitapResimURL))
                    {
                        kitaplar.KitapResim = KitapResimURL;
                    }

                    // YayineviID'yi eklemek için aşağıdaki satırı kullanabilirsin
                    kitaplar.YayineviID = Convert.ToInt32(Request.Form["YayineviID"]);

                    db.Kitaplar.Add(kitaplar);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Kitap eklenirken bir hata oluştu: " + ex.Message;
                }
            }

            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", kitaplar.KategoriID);
            ViewBag.TurID = new SelectList(db.Tur, "TurID", "TurAdi", kitaplar.TurID);
            ViewBag.YayineviID = new SelectList(db.Yayinevleri, "YayineviID", "Ad", kitaplar.YayineviID);
            ViewBag.Yazarlar = db.Yazarlar.ToList();
            ViewBag.Kategoriler = db.Kategoriler.ToList();
            ViewBag.Turler = db.Tur.ToList();

            return View(kitaplar);
        }


        // GET: Kitaplars/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", kitaplar.KategoriID);
            ViewBag.TurID = new SelectList(db.Tur, "TurID", "TurAdi", kitaplar.TurID);
            ViewBag.YayineviID = new SelectList(db.Yayinevleri, "YayineviID", "Ad", kitaplar.YayineviID);
            return View("Create", kitaplar);

        }

        // POST: Kitaplars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitapID,KitapAdi,YazarID,KategoriID,TurID,YayineviID,YildizID,Fiyat,StokMiktari")] Kitaplar kitaplar)
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
            return View(kitaplar);
        }

        // GET: Kitaplars/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Kitaplars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitaplar kitaplar = db.Kitaplar.Find(id);
            db.Kitaplar.Remove(kitaplar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult BuySayfasi(int? id)
        {
            int? kitapId = TempData["KitapId"] as int?;
            if (id == null)
            {
                Debug.WriteLine("ID is null");
                return RedirectToAction("Index");
            }

           
            Kitaplar kitap = db.Kitaplar.Find(id);

            if (kitap == null)
            {
                return HttpNotFound(); 
            }

            ViewBag.KitapAdi = kitap.KitapAdi;
            ViewBag.Fiyat = kitap.Fiyat;
            ViewBag.KitapId = kitap.KitapID;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(int kitapId)
        {
            TempData["KitapId"] = kitapId;
           

            Kullanicilar kullanici = (Kullanicilar)Session["kullanici"];
            Kitaplar kitap = db.Kitaplar.Find(kitapId);

            Siparisler yeniSiparis = new Siparisler
            {
                KullaniciID = kullanici.KullaniciID,
                SiparisTarihi = DateTime.Now,
                ToplamTutar = kitap.Fiyat
                
            };

            
            db.Siparisler.Add(yeniSiparis);
            db.SaveChanges();

           
            int siparisID = yeniSiparis.SiparisID;

            SiparisDetaylari yeniSiparisDetayi = new SiparisDetaylari
            {
                SiparisID = siparisID,
                KitapID = kitapId,
                Miktar = 1 
            };

            
            db.SiparisDetaylari.Add(yeniSiparisDetayi);
            db.SaveChanges();

            
            return RedirectToAction("BuySayfasi", "Kitaplars", new { id = kitapId });
        }


        
        private bool GetLoginStatus()
        {
            
            string userEmail = Session["userEmail"] as string;

            if (!string.IsNullOrEmpty(userEmail))
            {
                
                return true;
            }

            return false;
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
