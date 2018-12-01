using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Turler")]
    public class Tur
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(25)]
        public string Adi { get; set; }
        public int NeTuru { get; set; }  // 1-kitap, 2-internet dokuman, 3-Sample

        
    }
}