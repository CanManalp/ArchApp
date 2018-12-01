using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Serhler")]
    public class Serh : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Baslik { get; set; }
        [StringLength(100)]
        public string AltBaslik { get; set; }
        [StringLength(50)]
        public string Hazirlayan { get; set; }
        public int? CiltSayisi { get; set; }
        [Required]
        public int Basi { get; set; }
        [Required]
        public int YayinYili { get; set; }
        public int KategoriId { get; set; }

        public virtual List<SerhBolumu> SerhBolumleri { get; set; }
        public virtual List<Yazar> Yazarlar { get; set; }
    }
}