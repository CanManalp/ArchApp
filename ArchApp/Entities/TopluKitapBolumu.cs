using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("TopluKitapBolumleri")]
    public class TopluKitapBolumu : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Baslik { get; set; }
        [StringLength(100)]
        public string AltBaslik { get; set; }
        public int? SayfaNo { get; set; }
      

        public virtual TopluKitap TopluKitap { get; set; }
        public virtual List<Yazar> Yazarlar { get; set; }
    }
}