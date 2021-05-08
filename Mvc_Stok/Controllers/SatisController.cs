using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;

namespace Mvc_Stok.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        MvcDbStokEntities db = new MvcDbStokEntities();  //asama2 nesne türetmek modeli tutan tablolara ulasmak için 
        public ActionResult Index()
        {
            return View();
        }
         
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(Tbl_Satıslar p)
        {
            db.Tbl_Satıslar.Add(p);
            db.SaveChanges();
            return View("Index");
        }
    }
}