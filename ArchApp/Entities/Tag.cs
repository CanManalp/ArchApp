using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("Tagler")]
    public class Tag
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Etiket { get; set; }
        public int KitapId { get; set; }

        public virtual Kitap Kitap { get; set; }
        public virtual Makale Makale { get; set; }
        public virtual Karar Karar { get; set; }
        //public virtual Serh Serh { get; set; }
        //public virtual SerhBolumu SerhBolumu { get; set; }
        //public virtual Dokuman Dokuman { get; set; }
        //public virtual Sample Sample { get; set; }

    }
}