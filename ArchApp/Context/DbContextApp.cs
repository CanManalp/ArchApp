using ArchApp.Entities;
using ArchApp.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ArchApp.Context
{
    public class DbContextApp : DbContext
    {
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<TopluKitap> TopluKitap { get; set; }
        public DbSet<TopluKitapBolumu> TopluKitapBolumleri { get; set; }
        public DbSet<Makale> Makaleler { get; set; }
        public DbSet<Karar> Kararlar { get; set; }
        public DbSet<Serh> Serhler { get; set; }
        public DbSet<SerhBolumu> SerhBolumleri { get; set; }
        public DbSet<Dokuman> Dokumanlar { get; set; }
        public DbSet<Sample> Samplelar { get; set; }
        public DbSet<Kaynak> Kaynaklar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<AltKategori> AltKategoriler { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }
        public DbSet<Tag> Etiketler { get; set; }
        public DbSet<Tur> Turler { get; set; }

        public DbContextApp()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContextApp, Configuration>());
        }
    }
}