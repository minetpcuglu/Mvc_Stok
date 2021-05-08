using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity; //asama 1
using PagedList;
using PagedList.Mvc;

namespace Mvc_Stok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.Tbl_Urunler.ToList();
            var degerler = db.Tbl_Urunler.ToList().ToPagedList(sayfa, 6);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()   /*Madde 15 teki 3 cu alan*/
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAdı,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrunEkle(Tbl_Urunler u1)
        {
            var ktg = db.Tbl_Kategoriler.Where(p => p.KategoriID == u1.Tbl_Kategoriler.KategoriID).FirstOrDefault(); /*Madde 16 1 ci madde */
            u1.Tbl_Kategoriler = ktg;  //ktg den gelen degeri ata
            db.Tbl_Urunler.Add(u1);
            db.SaveChanges();
            return RedirectToAction("Index");  //kaydetme işlemi gerceklestirdikten sonra ındex sayfasına yönlendir
        }

        public ActionResult SIL(int id)
        {
            var degerler = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(degerler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var deger = db.Tbl_Urunler.Find(id);
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()   /*Madde 15 teki 3 cu alan*/
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAdı,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", deger);
        }

        public ActionResult Guncelle(Tbl_Urunler p)
        {
            var urun = db.Tbl_Urunler.Find(p.UrunId);
            urun.UrunAdı = p.UrunAdı;
            urun.UrunMarka = p.UrunMarka;
            urun.Stok = p.Stok;
            urun.UrunFiyat = p.UrunFiyat;
            //urun.UrunKategori = p.UrunKategori;

            
            var ktg = db.Tbl_Kategoriler.Where(m=> m.KategoriID == p.Tbl_Kategoriler.KategoriID).FirstOrDefault(); /*Madde 16 1 ci madde */
           urun.UrunKategori = ktg.KategoriID;  //ktg den gelen degeri ata
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}