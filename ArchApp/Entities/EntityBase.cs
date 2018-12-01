using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    public class EntityBase
    {
        [StringLength(500)]
        public string Notlar { get; set; }
        [StringLength(75)]
        public string WebAdresi { get; set; }       
        public DateTime? ErisimTarihi { get; set; }

        //public int UlkeId { get; set; }
        //public int TurId { get; set; }
        //public int YayımlandigiMecraId { get; set; }

        public virtual Ulke Ulke { get; set; }
        public virtual List<Tag> Tags { get; set; }
        //public virtual Mecra YayımlandigiMecra { get; set; }
        //public virtual Kategori Kategori { get; set; }
        public virtual AltKategori AltKategori { get; set; }

    }
}