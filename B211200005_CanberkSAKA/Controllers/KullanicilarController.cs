using System;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using B211200005_CanberkSAKA;

namespace B211200005_CanberkSAKA.Controllers
{
    public class KullanicilarController : Controller
    {
        
        private KITAPSATISEntities db = new KITAPSATISEntities();
        private bool girisDurumu;
        Kullanicilar kullanici;

        // GET: Kullanicilar/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Kullanicilar/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "KullaniciID,Ad,Soyad,Email,Sifre")] Kullanicilar kullanici)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Email kontrolü, aynı email ile başka bir kullanıcı varsa kayıt yapma
                    if (db.Kullanicilar.Any(x => x.Email == kullanici.Email))
                    {
                        ViewBag.ErrorMessage = "Bu email zaten kullanılıyor.";
                        return View();
                    }

                    db.Kullanicilar.Add(kullanici);
                    db.SaveChanges();

                    // Giriş sayfasına veya başka bir sayfaya yönlendir
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Kullanıcı kaydı başarısız oldu: " + ex.Message;
                    // Hata sayfasına yönlendirmek veya belirli bir görünümü döndürmek isteyebilirsin
                    return View("Error"); // Örneğin, "Error" adlı bir görünüm
                }
            }

            // Kayıt görünümünü döndür
            return View();
        }

        // GET: Kullanicilar/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Kullanicilar/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            // Kullanıcı giriş kontrolü
            bool loginIsSuccessful = CheckLogin(email, password);

           

            if (loginIsSuccessful)
            {
                // Giriş başarılıysa GirisYapildiMi'yi true olarak güncelle
                UpdateLoginStatus(email, true);

                // E-postayı Session'a kaydet
                Session["userEmail"] = email;

                Session["GirisDurumu"] = true;

                var girisYapmisKullanici = db.Kullanicilar.SingleOrDefault(u => u.Email == email);

                Session["kullanici"] = girisYapmisKullanici;

                girisDurumu = true;
                

                return RedirectToAction("Index", "Kitaplars");
            }
            else
            {
                // Giriş başarısızsa kullanıcıyı giriş sayfasına geri yönlendir
                ViewBag.ErrorMessage = "Kullanıcı adı ya da şifre hatalı.";
                return View("Login");
            }
        }

        // Kullanıcı girişini kontrol etmek için ayrı bir metot
        private bool CheckLogin(string email, string password)
        {
            var user = db.Kullanicilar.SingleOrDefault(u => u.Email == email && u.Sifre == password);

            return user != null;
        }

        // GirisYapildiMi'yi güncellemek için ayrı bir metot
        private void UpdateLoginStatus(string email, bool loginStatus)
        {
            var user = db.Kullanicilar.SingleOrDefault(u => u.Email == email);

            if (user != null)
            {
                user.GirisYapildiMi = loginStatus;
                db.SaveChanges();
            }
        }

        // Logout işlemi
        public ActionResult Logout()
        {
            // Session'dan e-posta adresini al
            string userEmail = Session["userEmail"] as string;

            // Giriş durumunu güncelle
            UpdateLoginStatus(userEmail, false);
            girisDurumu = false;

            // Session'ı temizle
            Session.Clear();
            Session.Remove("GirisDurumu");

            // Çıkış işlemi tamamlandıktan sonra başka bir sayfaya yönlendir
            return RedirectToAction("Index", "Kitaplars");
        }





    }

}
