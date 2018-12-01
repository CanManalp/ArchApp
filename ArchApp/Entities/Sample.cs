using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Samplelar")]
    public class Sample : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Baslik { get; set; }
        [StringLength(250)]
        public string Ozet { get; set; }
        public int KategoriId { get; set; }

        public virtual Tur Tur { get; set; }
    }
}