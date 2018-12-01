using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Kategoriler")]
    public class Kategori
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int? NeKategorisi { get; set; }  //kitap Makale Dergi vs.
        [Required, StringLength(50),Display(Name ="Kategori")]
        public string Adi { get; set; }
     
    }
}