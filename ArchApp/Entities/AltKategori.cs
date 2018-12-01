using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("AltKategoriler")]
    public class AltKategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "Kategori")]
        public string Adi { get; set; }
        public int KategoriId { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
}