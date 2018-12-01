using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArchApp.Entities
{
    [Table("SerhBolumleri")]
    public class SerhBolumu : EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(25)]
        public string MaddeNo { get; set; }
        public int? SayfaNo { get; set; }
        public int SerhId { get; set; }
        

        public virtual Serh Serh { get; set; }
        public virtual List<Yazar> Yazarlar { get; set; }


    }
}