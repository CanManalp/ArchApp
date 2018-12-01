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
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100),Display(Name ="Başlık")]
        public string Baslik { get; set; }
        [StringLength(100), Display(Name = "Alt Başlık")]
        public string AltBaslik { get; set; }
        [StringLength(25), Display(Name = "Çeviren")]
        public string Ceviren { get; set; }
        [StringLength(75), Display(Name = "Yayın Evi")]
        public string YayinEvi { get; set; }        //Basılı Yayın ise
        [Required,StringLength(25), Display(Name = "Yayın Yeri")]
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
    }
}