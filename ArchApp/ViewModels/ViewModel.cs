﻿using ArchApp.Context;
using ArchApp.Entities;
using ArchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.ViewModels
{
    public class ViewModel : BaseModel
    {
        public string EntityName { get; set; }
        public string PViewName { get; set; }
        public bool IsEdit { get; set; }
        public Kitap Kitap { get; set; }
        public List<Kitap> Kitaplar { get; set; }
        public SelectList Tur { get; set; }
        public SelectList Ulke { get; set; }
        public SelectList Kategori { get; set; }
        public SelectList AltKategori { get; set; }
        //public Yazar Yazar { get; set; }
        //public List<Yazar> Yazarlar { get; set; }

        public ViewModel()
        {
            //Yazar = new Yazar();
            Kitap = new Kitap();
            Kitaplar = new List<Kitap>();
            //Yazarlar = new List<Yazar>();
        }
        public ViewModel KitapMainPagePrep()   //Kitap Eklerkenki dropdownları hazırlar, Tablodan kitap listesini çeker, boş yazarları ve tagleri oluşturur
        {
            DbContextApp db = new DbContextApp();
            db.Database.CreateIfNotExists();

            ViewModel vm = new ViewModel
            {
                Kitaplar = db.Kitaplar.ToList(),
                Tur = new SelectList(db.Turler.Where(c => c.NeTuru == 1), "Id", "Adi"),
                Ulke = new SelectList(db.Ulkeler.ToList(), "Id", "Dil"),
                Kategori = new SelectList(db.Kategoriler.ToList(/*c => c.NeKategorisi == 1*/), "Id", "Adi"),
                EntityName = "Kitap",
                PViewName = "~/Views/Shared/_KitapPartial.cshtml"
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

        public ViewModel KitapDuzenlePagePrep(int id)
        {
           
            Kitap kitap = new Kitap();
            Yazar yazar = new Yazar();
            Tag etiket = new Tag();
            DbContextApp db = new DbContextApp();
            kitap = db.Kitaplar.Find(id);
            ViewModel vm = new ViewModel
            {
                Kitaplar = db.Kitaplar.ToList(),
                Kitap = kitap,
                Tur = new SelectList(db.Turler.Where(c => c.NeTuru == 1), "Id", "Adi", kitap.TurId),     //Tur Dropdown
                Ulke = new SelectList(db.Ulkeler.ToList(), "Id", "Dil"),     //Ülke Dropdown
                Kategori = new SelectList(db.Kategoriler.ToList(/*c => c.NeKategorisi == 1*/), "Id", "Adi", kitap.AltKategori.KategoriId),   //Kategori DropDown
                AltKategori = new SelectList(db.AltKategoriler.Where(c => c.Id == kitap.AltKategoriId), "Id", "Adi", kitap.AltKategoriId)   //AltKategori DropDown
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

            vm.EntityName = "Kitap";
            vm.PViewName = "~/Views/Shared/_KitapPartial.cshtml";
            vm.IsEdit = true;
            return vm;
        }
    }
}