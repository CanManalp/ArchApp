using ArchApp.Context;
using ArchApp.Entities;
using ArchApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.ViewModels
{
    public class KitapViewModel : BaseModel
    {
       
        public string BaslikSortPram { get; set; }
        public string YazarSortPram { get; set; }
        public string TurSortPram { get; set; }       
        public Kitap Kitap { get; set; }
        public IPagedList<Kitap> Kitaplar { get; set; }
        public SelectList Tur { get; set; }
 
        public SelectList Kategori { get; set; }
        public SelectList AltKategori { get; set; }
        //public Yazar Yazar { get; set; }
        //public List<Yazar> Yazarlar { get; set; }

        public KitapViewModel()
        {
            //Yazar = new Yazar();
            Kitap = new Kitap();
            //Kitaplar = new List<Kitap>();
            //Yazarlar = new List<Yazar>();
        }


        public KitapViewModel KitapMainPagePrep()   //Kitap Eklerkenki dropdownları hazırlar, Tablodan kitap listesini çeker, boş yazarları ve tagleri oluşturur
        {
            
            DbContextApp db = new DbContextApp();
            db.Database.CreateIfNotExists();

            KitapViewModel vm = new KitapViewModel
            {

                Kitaplar = db.Kitaplar.OrderBy(c => c.Baslik).ToPagedList(PageNumber??1, PageSize),
                Tur = new SelectList(db.Turler.Where(c => c.NeTuru == 1), "Id", "Adi"),
                Ulke = new SelectList(db.Ulkeler.ToList(), "Id", "Dil"),
                Kategori = new SelectList(db.Kategoriler.Where(c => c.NeKategorisi == 1), "Id", "Adi"),
                EntityName = "Kitap",
                PViewName = "~/Views/Shared/_KitapPartial.cshtml",
                BaslikSortPram = "Baslik_Descending"
            };
            
            Yazar yazar = new Yazar();
            Tag tag = new Tag();

            for (int i = 0; i < 10; i++)
            {

                vm.Kitap.Yazarlar.Add(yazar);
            }

            for (int i = 0; i < 10; i++)
            {

                vm.Kitap.Tags.Add(tag);
            }

            return vm;
        }

        public KitapViewModel KitapDuzenlePagePrep(int id)
        {
           
            Kitap kitap = new Kitap();
            Yazar yazar = new Yazar();
            Tag etiket = new Tag();
            DbContextApp db = new DbContextApp();
            kitap = db.Kitaplar.Find(id);
            KitapViewModel vm = new KitapViewModel
            {
                Kitaplar = db.Kitaplar.OrderBy(c => c.Baslik).ToPagedList(PageNumber?? 1, PageSize),
                Kitap = kitap,
                Tur = new SelectList(db.Turler.Where(c => c.NeTuru == 1), "Id", "Adi", kitap.TurId),     //Tur Dropdown
                Ulke = new SelectList(db.Ulkeler.ToList(), "Id", "Dil"),     //Ülke Dropdown
                Kategori = new SelectList(db.Kategoriler.Where(c => c.NeKategorisi == 1), "Id", "Adi", kitap.AltKategori.KategoriId),   //Kategori DropDown
                AltKategori = new SelectList(db.AltKategoriler.Where(c => c.KategoriId == kitap.AltKategori.KategoriId), "Id", "Adi", kitap.AltKategoriId),   //AltKategori DropDown
                
            };


            int yazarCount = kitap.Yazarlar.Count;
            int etiketCount = kitap.Tags.Count;
            int eklenecekYazar = 10 - yazarCount;
            int eklenecekEtiket = 10 - etiketCount;

            for (int i = 0; i < eklenecekYazar; i++)
            {

                vm.Kitap.Yazarlar.Add(yazar);
            }
            for (int i = 0; i < eklenecekEtiket; i++)
            {

                vm.Kitap.Tags.Add(etiket);
            }
            if (vm.Kitap.AttachedFilePath != null && vm.Kitap.AttachedFileName != null)
            {
                vm.IsAttached = true;
            }

            vm.EntityName = "Kitap";
            vm.PViewName = "~/Views/Shared/_KitapPartial.cshtml";
            vm.IsEdit = true;
            return vm;

        }

        public KitapViewModel KitapSearchPagePrep()   //Kitap Eklerkenki dropdownları hazırlar, Tablodan kitap listesini çeker, boş yazarları ve tagleri oluşturur
        {
            DbContextApp db = new DbContextApp();
           

            KitapViewModel vm = new KitapViewModel
            {
                Tur = new SelectList(db.Turler.Where(c => c.NeTuru == 1), "Id", "Adi"),
                Ulke = new SelectList(db.Ulkeler.ToList(), "Id", "Dil"),
                Kategori = new SelectList(db.Kategoriler.Where(c => c.NeKategorisi == 1), "Id", "Adi"),
                EntityName = "Kitap",
                PViewName = "~/Views/Shared/_KitapPartial.cshtml",
                
            };

            Yazar yazar = new Yazar();
            Tag tag = new Tag();

            for (int i = 0; i < 10; i++)
            {

                vm.Kitap.Yazarlar.Add(yazar);
            }

            for (int i = 0; i < 10; i++)
            {

                vm.Kitap.Tags.Add(tag);
            }

            return vm;
        }

    }
}