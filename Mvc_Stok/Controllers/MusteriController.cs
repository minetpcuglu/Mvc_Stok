using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Mvc_Stok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Musteri
        public ActionResult Index(int sayfa = 1 ,string t="")
        {

            var degerler = db.Tbl_Musteriler.ToList().ToPagedList(sayfa, 7) ;
            //if (!string.IsNullOrEmpty(t))
            //{
            //    degerler = (IPagedList<Tbl_Musteriler>)degerler.Where(m => m.MusteriAdı.Contains(t));
            //}
            return View(degerler);
        }
        [HttpPost]
        public ActionResult MusteriEkle(Tbl_Musteriler m1)
        {

            //26 asamadan dolayı ekledık//
            if (!ModelState.IsValid)  //dogrulama işlemi yapılmaıdysa 
            {
                return View("MusteriEkle");
            }
            db.Tbl_Musteriler.Add(m1);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }

        public ActionResult SIL(int id)
        {
            var degerler = db.Tbl_Musteriler.Find(id);
            db.Tbl_Musteriler.Remove(degerler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var degerler = db.Tbl_Musteriler.Find(id);

            return View("MusteriGetir", degerler);
        }

        public ActionResult Guncelle(Tbl_Musteriler m)  //neye göre güncellenck
        {
            var mst = db.Tbl_Musteriler.Find(m.MsuteriId); //m musterisini bul
            mst.MusteriAdı = m.MusteriAdı;
            mst.MusteriSoyadı = m.MusteriSoyadı;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

     
    }
}