using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Makaleler")]
    public class Makale : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Baslik { get; set; }
        [StringLength(100)]
        public string AltBaslik { get; set; }
        [StringLength(25)]
        public string Ceviren { get; set; }
        public int? CiltNo { get; set; }
        public int? SayiNo { get; set; }
        public int Yili { get; set; }    
        public int BasimSayisi { get; set; }
        public int SayfaNo { get; set; }
        [Required,StringLength(75)]
        public string DergiAdi { get; set; }
        public int KategoriId { get; set; }

        public virtual List<Yazar> Yazarlar { get; set; }
    }
}