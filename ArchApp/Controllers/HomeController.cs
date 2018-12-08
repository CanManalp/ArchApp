﻿using ArchApp.Context;
using ArchApp.Entities;
using ArchApp.Models;
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
        [HttpPost]
        public ActionResult Search(string prefix, string entity)
        {
            const string kitap = "kitap";
            const string makale = "makale";
            const string karar = "karar";

            ViewModel vm = new ViewModel();

            switch (entity)
            {
                case kitap:
                    vm = vm.KitapSearchPagePrep();
                    Search<Kitap> srcK = new Search<Kitap>();
                    //vm.Kitaplar = srcK.MSearch(c => c.Baslik.Contains(prefix));

                    DbContextApp db = new DbContextApp();
                    //var list = db.Yazarlar.Join(db.Etiketler, yzrlr => yzrlr.KitapId, etktlr => etktlr.KitapId, (yzrlr, etktlr) => new { Kitap = yzrlr.Kitap,
                    //                                                                                                                     YazarAdi = yzrlr.Adi,
                    //                                                                                                                     Etiket = etktlr.Etiket,
                    //                                                                                                                     KitapId = yzrlr.KitapId}).ToList();

                    //var yzrlrEtiketlerTbl = from e in db.Etiketler
                    //                        join y in db.Yazarlar

                    //                        select new
                    //                        {
                    //                            y,
                    //                            e

                    //                        };

                    //foreach (var item in yzrlrEtiketlerTbl)
                    //{
                    //    var etiket = item.e.Etiket;

                    //    var yazarAdi = item.y.Adi;
                    //}



                    //vm.Kitaplar = db.Yazarlar.Where(yzrlr => yzrlr.Kitap.Baslik.Contains(prefix) ||
                    //                                         yzrlr.Kitap.AltBaslik.Contains(prefix) ||
                    //                                         yzrlr.Kitap.YayinEvi.Contains(prefix) ||
                    //                                         yzrlr.Kitap.YayinYeri.Contains(prefix) ||
                    //                                         yzrlr.Kitap.Ceviren.Contains(prefix) ||
                    //                                         yzrlr.Kitap.Tur.Adi.Contains(prefix) ||
                    //                                         yzrlr.Adi.Contains(prefix) ||
                    //                                         yzrlr.Kitap.AltKategori.Kategori.Adi.Contains(prefix) ||
                    //                                         yzrlr.Kitap.AltKategori.Adi.Contains(prefix) ||
                    //                                         yzrlr.Kitap.Tags.Select(c=> c.Etiket).Contains(prefix))
                    //                                        .Select(c => c.Kitap)                                         //GROUP BY kullanımı sonrası Igrouping'i List'e çevirmek için
                    //                                        .GroupBy(ktp => ktp.Id)
                    //                                        .Select(g => g.FirstOrDefault()).ToList();

                    vm.Kitaplar = db.Kitaplar.Where(ktp => ktp.Baslik.Contains(prefix) ||
                                         ktp.AltBaslik.Contains(prefix) ||
                                         ktp.YayinEvi.Contains(prefix) ||
                                         ktp.YayinYeri.Contains(prefix) ||
                                         ktp.Ceviren.Contains(prefix) ||
                                         ktp.Tur.Adi.Contains(prefix) ||
                                         ktp.Yazarlar.Any(c => c.Adi.Contains(prefix)) ||   //ÇOK ÖNEMLİ One To Many ilişki de kitaptan yazarlara gidip yazarlar tablosunda Filter
                                         ktp.AltKategori.Kategori.Adi.Contains(prefix) ||
                                         ktp.AltKategori.Adi.Contains(prefix) ||
                                         ktp.Tags.Any(c => c.Etiket.Contains(prefix))).ToList();      //ÇOK ÖNEMLİ One To Many ilişki de kitaptan yazarlara gidip yazarlar tablosunda Filter




                    return View("~/Views/Home/index.cshtml", vm);

                case makale:
                    Search<Makale> srcM = new Search<Makale>();
                    srcM.MSearch(c => c.Baslik.Contains(prefix));
                    break;
            }

            return View();
        }
    }
}