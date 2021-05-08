using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity; //asama1
using PagedList;
using PagedList.Mvc;

namespace Mvc_Stok.Controllers
{
    
    public class KategoriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();  //asama2 nesne türetmek modeli tutan tablolara ulasmak için 
        // GET: Kategori
        public ActionResult Index(int sayfa=1)  //sayfalar arası geçiş icin
        {
            /* var degerler = db.Tbl_Kategoriler.ToList(); */ //select işlemi degerleri listeleme işlemi

            var degerler = db.Tbl_Kategoriler.ToList().ToPagedList(sayfa,7);  //1 ci sayfan basla 7 oge ol simdi ındexe git orayıda düzelt 
            return View(degerler);
        }

        [HttpPost]  //sayfada işlem yapılcak 
        public ActionResult YeniKategori(Tbl_Kategoriler p1)
        {
            //26 asamadan dolayı ekledık//
            if (!ModelState.IsValid)  //dogrulama işlemi yapılmaıdysa 
            {
                return View("YeniKategori");
            }
            db.Tbl_Kategoriler.Add(p1);
            db.SaveChanges();
            return View();
        }

        [HttpGet]  //işlem yapılmayacak
        public ActionResult YeniKategori()
        {
            return View();
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var degerler = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir", degerler);
        }
        public ActionResult Guncelle(Tbl_Kategoriler p)
        {
            var ktg = db.Tbl_Kategoriler.Find(p.KategoriID);
            ktg.KategoriAdı = p.KategoriAdı;
            db.SaveChanges();
           return RedirectToAction("Index");
        }
    }
}