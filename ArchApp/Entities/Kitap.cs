using ArchApp.Context;
using ArchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Kitaplar")]
    public class Kitap : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50), Display(Name = "Kitap Ref No")]
        public string KitapRefNo { get; set; }
        [Required, StringLength(100), Display(Name = "Başlık")]
        public string Baslik { get; set; }
        [StringLength(100), Display(Name = "Alt Başlık")]
        public string AltBaslik { get; set; }
        [StringLength(50), Display(Name = "Çeviren")]
        public string Ceviren { get; set; }
        [StringLength(75), Display(Name = "Yayın Evi")]
        public string YayinEvi { get; set; }        //Basılı Yayın ise
        [Required, StringLength(25), Display(Name = "Yayın Yeri")]
        public string YayinYeri { get; set; }       //Basılı Yayın ise      
        [Required, Display(Name = "Bası")]
        public int Basi { get; set; }
        [Required, Display(Name = "Yayın Yılı")]
        public int YayinYili { get; set; }
        public int? Isbn { get; set; }
        public int TurId { get; set; }
        public int UlkeId { get; set; }
        public int AltKategoriId { get; set; }

        public virtual Tur Tur { get; set; }
        public virtual List<Yazar> Yazarlar { get; set; }

        public Kitap()
        {
            Yazarlar = new List<Yazar>();
            Tags = new List<Tag>();

        }
        private static string RefNoUret()
        {
            using (DbContextApp db = new DbContextApp())
            {
                
                int id = db.Kitaplar.OrderByDescending(c => c.Id).FirstOrDefault().Id;
                id = id + 1;
                string kitapRefNo = "ktp" + id.ToString();
                return kitapRefNo;
            }
           
          

        }
        public int KitapKayıt(Kitap kitap)
        {
            DbContextApp db = new DbContextApp();
            KitapViewModel vm = new KitapViewModel();
            Kategori kategori = new Kategori();
            Yazar yazar = new Yazar();

            kitap.Yazarlar.RemoveAll(c => string.IsNullOrEmpty(c.Adi));
            kitap.Tags.RemoveAll(c => string.IsNullOrEmpty(c.Etiket));

            AltKategori altKategori = db.AltKategoriler.FirstOrDefault(c => c.Id == kitap.AltKategoriId);
            kitap.AltKategori = altKategori;

            kitap.KitapRefNo = RefNoUret();
            //Yapılacak - Bu Kitap başlığından var emin misiniz? 
            db.Kitaplar.Add(kitap);
            int saveChangesResult = db.SaveChanges();
            return saveChangesResult;
        }
      
    }
}