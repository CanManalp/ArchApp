using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("TopluKitaplar")]
    public class TopluKitap:EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Baslik { get; set; }
        [StringLength(100)]
        public string AltBaslik { get; set; }
        public string Hazirlayan { get; set; }
        [Required,StringLength(50)]
        public string YayinYeri { get; set; }
        [StringLength(75)]
        public string YayinEvi { get; set; }
        public int? CiltNo { get; set; }
        [Required]
        public int YayinYili { get; set; }
        public int KategoriId { get; set; }

        public virtual List<TopluKitapBolumu> TopluKitapBolumleri { get; set; }
        public virtual List<Yazar> Yazarlar { get; set; }
    }
}