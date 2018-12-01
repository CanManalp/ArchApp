using ArchApp.Context;
using ArchApp.Entities;
using ArchApp.Repository;
using ArchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel();

            vm = vm.KitapMainPagePrep();

            return View(vm);
        }

     

        public JsonResult AltKategoriDoldur(int kategoriId)
        {
            DbContextApp db = new DbContextApp();

            var altKategoriler = new SelectList(db.AltKategoriler.Where(c => c.KategoriId == kategoriId), "Id", "Adi");


            return Json(altKategoriler, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Kitap()
        {
            ViewModel vm = new ViewModel();
            vm = vm.KitapMainPagePrep();

          

            return View("~/Views/Home/index.cshtml", vm);
        }
        [HttpPost]
        public ActionResult Kitap(ViewModel Vm)
        {
            //Repository<Kitap> repoKitap = new Repository<Kitap>();
            //var Tblkitap = repoKitap.Get();


            return View("~/Views/Home/index.cshtml");
        }


        public ActionResult Makale()
        {
            ViewBag.bName = "Makale";
            ViewBag.partial = "~/Views/Shared/_KitapPartial.cshtml";
            return View("~/Views/Home/index.cshtml");
        }

        public ActionResult Serh()
        {
            ViewBag.bName = "Şerh";
            ViewBag.partial = "~/Views/Shared/_KitapPartial.cshtml";
            return View("~/Views/Home/index.cshtml");
        }

        public ActionResult TopluKitap()
        {
            ViewBag.bName = "Toplu Kitap";
            ViewBag.partial = "~/Views/Shared/_KitapPartial.cshtml";
            return View("~/Views/Home/index.cshtml");
        }

        public ActionResult Karar()
        {
            ViewBag.bName = "Karar";
            ViewBag.partial = "~/Views/Shared/_KitapPartial.cshtml";
            return View("~/Views/Home/index.cshtml");
        }

        public ActionResult Dokuman()
        {
            ViewBag.bName = "İnternet Dökümanı";
            ViewBag.partial = "~/Views/Shared/_KitapPartial.cshtml";
            return View("~/Views/Home/index.cshtml");
        }

        public ActionResult Sample()
        {
            ViewBag.bName = "Sample";
            ViewBag.partial = "~/Views/Shared/_KitapPartial.cshtml";
            return View("~/Views/Home/index.cshtml");
        }
    }
}