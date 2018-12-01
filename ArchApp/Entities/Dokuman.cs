using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Dokumanlar")]
    public class Dokuman : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Baslik { get; set; }
        [StringLength(100)]
        public string AltBaslik { get; set; }
        [StringLength(25)]
        public string Ceviren { get; set; }
        public int? YayinYili { get; set; }
        public string Ozet { get; set; }
        public int KategoriId { get; set; }

        public virtual Tur Tur { get; set; }
        public virtual List<Yazar> Yazarlar { get; set; }
    }
}